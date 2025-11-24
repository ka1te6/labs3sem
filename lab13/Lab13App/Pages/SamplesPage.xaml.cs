using System.Collections.ObjectModel;
using Lab13App.Models;
using Lab13App.Services;

namespace Lab13App.Pages;

public partial class SamplesPage : ContentPage
{
    private readonly LabDatabaseService _database;
    private Sample? _selected;

    public ObservableCollection<Sample> Samples { get; } = new();
    public ObservableCollection<Experiment> ExperimentOptions { get; } = new();

    public SamplesPage(LabDatabaseService database)
    {
        InitializeComponent();
        _database = database;
        BindingContext = this;
        CollectedDatePicker.Date = DateTime.Today;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await RefreshAsync();
    }

    private async Task RefreshAsync()
    {
        var experiments = await _database.GetExperimentsAsync();
        ExperimentOptions.Clear();
        foreach (var experiment in experiments)
        {
            ExperimentOptions.Add(experiment);
        }

        var samples = await _database.GetSamplesAsync();
        Samples.Clear();
        foreach (var sample in samples)
        {
            Samples.Add(sample);
        }

        StatusLabel.Text = $"Всего: {Samples.Count}";
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(CodeEntry.Text))
        {
            await DisplayAlert("Ошибка", "Код образца обязателен.", "OK");
            return;
        }

        if (ExperimentPicker.SelectedItem is not Experiment experiment)
        {
            await DisplayAlert("Ошибка", "Выберите эксперимент.", "OK");
            return;
        }

        var payload = _selected ?? new Sample();
        payload.SampleCode = CodeEntry.Text.Trim();
        payload.Type = string.IsNullOrWhiteSpace(TypeEntry.Text) ? null : TypeEntry.Text.Trim();
        payload.Status = string.IsNullOrWhiteSpace(StatusEntry.Text) ? null : StatusEntry.Text.Trim();
        payload.CollectedDate = CollectedDatePicker.Date;
        payload.ExperimentId = experiment.Id;

        await _database.SaveSampleAsync(payload);
        await RefreshAsync();
        ResetForm();
        StatusLabel.Text = $"Сохранено: {payload.SampleCode}";
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (_selected is null)
        {
            await DisplayAlert("Удаление", "Сначала выберите образец.", "OK");
            return;
        }

        var confirm = await DisplayAlert("Подтверждение", $"Удалить {_selected.SampleCode}?", "Да", "Нет");
        if (!confirm)
        {
            return;
        }

        await _database.DeleteSampleAsync(_selected.Id);
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
        SamplesCollection.SelectedItem = null;
        CodeEntry.Text = string.Empty;
        TypeEntry.Text = string.Empty;
        StatusEntry.Text = string.Empty;
        CollectedDatePicker.Date = DateTime.Today;
        ExperimentPicker.SelectedItem = null;
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Sample sample)
        {
            return;
        }

        _selected = sample;
        CodeEntry.Text = sample.SampleCode;
        TypeEntry.Text = sample.Type;
        StatusEntry.Text = sample.Status;
        CollectedDatePicker.Date = sample.CollectedDate;
        ExperimentPicker.SelectedItem = ExperimentOptions.FirstOrDefault(x => x.Id == sample.ExperimentId);
    }
}

