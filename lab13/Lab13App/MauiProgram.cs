using Lab13App.Data;
using Lab13App.Pages;
using Lab13App.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lab13App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		var databasePath = Path.Combine(FileSystem.AppDataDirectory, "lab.db");
		builder.Services.AddDbContextFactory<LabDbContext>(options =>
			options.UseSqlite($"Data Source={databasePath}"));

		builder.Services.AddSingleton<LabDatabaseService>();

		builder.Services.AddTransient<ResearchersPage>();
		builder.Services.AddTransient<ExperimentsPage>();
		builder.Services.AddTransient<SamplesPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
