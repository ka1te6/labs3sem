using System.Collections.ObjectModel;
using Lab13App.Models;
using Lab13App.Services;

namespace Lab13App.Pages;

public partial class ResearchersPage : ContentPage
{
    private readonly LabDatabaseService _database;
    private Researcher? _selected;

    public ObservableCollection<Researcher> Researchers { get; } = new();

    public ResearchersPage(LabDatabaseService database)
    {
        InitializeComponent();
        _database = database;
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await RefreshAsync();
    }

    private async Task RefreshAsync()
    {
        var items = await _database.GetResearchersAsync();
        Researchers.Clear();
        foreach (var researcher in items)
        {
            Researchers.Add(researcher);
        }

        StatusLabel.Text = $"Всего: {Researchers.Count}";
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var fullName = FullNameEntry.Text?.Trim();
        if (string.IsNullOrWhiteSpace(fullName))
        {
            await DisplayAlert("Ошибка", "ФИО обязательно для заполнения.", "OK");
            return;
        }

        var payload = _selected ?? new Researcher();
        payload.FullName = fullName;
        payload.Position = string.IsNullOrWhiteSpace(PositionEntry.Text) ? null : PositionEntry.Text.Trim();
        payload.Email = string.IsNullOrWhiteSpace(EmailEntry.Text) ? null : EmailEntry.Text.Trim();

        await _database.SaveResearcherAsync(payload);
        await RefreshAsync();
        ResetForm();
        StatusLabel.Text = $"Сохранено: {payload.FullName}";
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (_selected is null)
        {
            await DisplayAlert("Удаление", "Выберите исследователя в списке.", "OK");
            return;
        }

        var confirm = await DisplayAlert("Подтверждение", $"Удалить {_selected.FullName}?", "Да", "Нет");
        if (!confirm)
        {
            return;
        }

        await _database.DeleteResearcherAsync(_selected.Id);
        await RefreshAsync();
        ResetForm();
        StatusLabel.Text = "Удалено";
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        ResetForm();
        StatusLabel.Text = "Форма очищена";
    }

    private void ResetForm()
    {
        _selected = null;
        ResearchersCollection.SelectedItem = null;
        FullNameEntry.Text = string.Empty;
        PositionEntry.Text = string.Empty;
        EmailEntry.Text = string.Empty;
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Researcher researcher)
        {
            return;
        }

        _selected = researcher;
        FullNameEntry.Text = researcher.FullName;
        PositionEntry.Text = researcher.Position;
        EmailEntry.Text = researcher.Email;
    }
}

