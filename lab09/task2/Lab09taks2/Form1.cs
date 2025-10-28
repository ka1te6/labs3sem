using System.Collections.Concurrent;
using System.Text.Json;

namespace Lab09taks2
{
    public partial class Form1 : Form
    {
        private List<City> cities = new List<City>();
        private string apiKey = "d047da4d32d4128da72dce7dd1a4530f"; // Замените на ваш API ключ

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCitiesFromFile();
        }

        private void LoadCitiesFromFile()
        {
            try
            {
                // Прямой путь к файлу
                string filePath = @"C:\Users\Андрей\programmer\C# Projects\lab3sem\lab09\task2\Lab09taks2\city.txt";

                if (!File.Exists(filePath))
                {
                    statusLabel.Text = "Файл city.txt не найден";
                    MessageBox.Show($"Файл city.txt не найден по пути: {filePath}", "Ошибка", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string[] lines = File.ReadAllLines(filePath);
                
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Ваш файл имеет формат: Название[TAB]широта,долгота
                    int tabPosition = line.IndexOf('\t');
                    
                    if (tabPosition > 0)
                    {
                        string name = line.Substring(0, tabPosition).Trim();
                        string coordinatesPart = line.Substring(tabPosition + 1).Trim();

                        int commaPosition = coordinatesPart.IndexOf(',');
                        
                        if (commaPosition > 0)
                        {
                            string latStr = coordinatesPart.Substring(0, commaPosition).Trim();
                            string lonStr = coordinatesPart.Substring(commaPosition + 1).Trim();

                            if (double.TryParse(latStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double lat) && 
                                double.TryParse(lonStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double lon))
                            {
                                cities.Add(new City { Name = name, Lat = lat, Lon = lon });
                                comboBoxCities.Items.Add(name);
                            }
                        }
                    }
                }

                if (comboBoxCities.Items.Count > 0)
                {
                    comboBoxCities.SelectedIndex = 0;
                    statusLabel.Text = $"Загружено {cities.Count} городов";
                }
                else
                {
                    statusLabel.Text = "Не удалось загрузить города";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки файла городов: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonGetWeather_Click(object sender, EventArgs e)
        {
            if (comboBoxCities.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите город из списка", "Внимание", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await GetWeatherForSelectedCity();
        }

        private async Task GetWeatherForSelectedCity()
        {
            buttonGetWeather.Enabled = false;
            progressBar.Visible = true;
            statusLabel.Text = "Загрузка данных о погоде...";
            listViewResults.Items.Clear();

            var selectedCity = cities[comboBoxCities.SelectedIndex];
            
            try
            {
                using var httpClient = new HttpClient();
                string url = $"https://api.openweathermap.org/data/2.5/weather?lat={selectedCity.Lat:F4}&lon={selectedCity.Lon:F4}&appid={apiKey}&units=metric&lang=ru";

                var response = await httpClient.GetStringAsync(url);
                using var doc = JsonDocument.Parse(response);

                if (doc.RootElement.TryGetProperty("sys", out var sys) &&
                    sys.TryGetProperty("country", out var countryEl) &&
                    doc.RootElement.TryGetProperty("name", out var nameEl) &&
                    !string.IsNullOrWhiteSpace(nameEl.GetString()))
                {
                    string country = countryEl.GetString() ?? "N/A";
                    string name = nameEl.GetString() ?? selectedCity.Name;
                    double temp = doc.RootElement.GetProperty("main").GetProperty("temp").GetDouble();
                    string description = doc.RootElement.GetProperty("weather")[0].GetProperty("description").GetString() ?? "N/A";

                    var item = new ListViewItem(name);
                    item.SubItems.Add(country);
                    item.SubItems.Add($"{temp:F1}°C");
                    item.SubItems.Add(description);
                    
                    listViewResults.Items.Add(item);

                    statusLabel.Text = $"Погода загружена для {name}";
                }
                else
                {
                    statusLabel.Text = "Не удалось получить данные о погоде";
                }
            }
            catch (HttpRequestException ex)
            {
                statusLabel.Text = "Ошибка сети при загрузке данных";
                MessageBox.Show($"Ошибка сети: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Ошибка при загрузке данных";
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buttonGetWeather.Enabled = true;
                progressBar.Visible = false;
            }
        }
    }

    public class City
    {
        public string Name { get; set; } = string.Empty;
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}