using System.Collections.ObjectModel;
using Lab13App.Models;
using Lab13App.Services;

namespace Lab13App.Pages;

public partial class ExperimentsPage : ContentPage
{
    private readonly LabDatabaseService _database;
    private Experiment? _selected;

    public ObservableCollection<Experiment> Experiments { get; } = new();
    public ObservableCollection<Researcher> ResearcherOptions { get; } = new();

    public ExperimentsPage(LabDatabaseService database)
    {
        InitializeComponent();
        _database = database;
        BindingContext = this;
        StartDatePicker.Date = DateTime.Today;
        EndDatePicker.Date = DateTime.Today;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await RefreshAsync();
    }

    private async Task RefreshAsync()
    {
        var researchers = await _database.GetResearchersAsync();
        ResearcherOptions.Clear();
        foreach (var researcher in researchers)
        {
            ResearcherOptions.Add(researcher);
        }

        var experiments = await _database.GetExperimentsAsync();
        Experiments.Clear();
        foreach (var experiment in experiments)
        {
            Experiments.Add(experiment);
        }

        StatusLabel.Text = $"Всего: {Experiments.Count}";
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitleEntry.Text))
        {
            await DisplayAlert("Ошибка", "Название эксперимента обязательно.", "OK");
            return;
        }

        if (ResearcherPicker.SelectedItem is not Researcher investigator)
        {
            await DisplayAlert("Ошибка", "Выберите руководителя.", "OK");
            return;
        }

        var payload = _selected ?? new Experiment();
        payload.Title = TitleEntry.Text.Trim();
        payload.Description = string.IsNullOrWhiteSpace(DescriptionEditor.Text) ? null : DescriptionEditor.Text.Trim();
        payload.StartDate = StartDatePicker.Date;
        payload.EndDate = EndDatePicker.Date;
        payload.PrincipalInvestigatorId = investigator.Id;

        await _database.SaveExperimentAsync(payload);
        await RefreshAsync();
        ResetForm();
        StatusLabel.Text = $"Сохранено: {payload.Title}";
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (_selected is null)
        {
            await DisplayAlert("Удаление", "Сначала выберите эксперимент.", "OK");
            return;
        }

        var confirm = await DisplayAlert("Подтверждение", $"Удалить {_selected.Title}?", "Да", "Нет");
        if (!confirm)
        {
            return;
        }

        await _database.DeleteExperimentAsync(_selected.Id);
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
        ExperimentsCollection.SelectedItem = null;
        TitleEntry.Text = string.Empty;
        DescriptionEditor.Text = string.Empty;
        StartDatePicker.Date = DateTime.Today;
        EndDatePicker.Date = DateTime.Today;
        ResearcherPicker.SelectedItem = null;
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Experiment experiment)
        {
            return;
        }

        _selected = experiment;
        TitleEntry.Text = experiment.Title;
        DescriptionEditor.Text = experiment.Description;
        StartDatePicker.Date = experiment.StartDate;
        EndDatePicker.Date = experiment.EndDate ?? experiment.StartDate;
        var pi = ResearcherOptions.FirstOrDefault(r => r.Id == experiment.PrincipalInvestigatorId);
        ResearcherPicker.SelectedItem = pi;
    }
}

