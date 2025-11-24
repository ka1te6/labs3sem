using Lab13App.Data;
using Lab13App.Helpers;
using Lab13App.Pages;
using Lab13App.Services;
using Microsoft.Extensions.DependencyInjection;
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

		var databasePath = DatabasePathProvider.GetPath();
		builder.Services.AddDbContextFactory<LabDbContext>(options =>
			options.UseSqlite($"Data Source={databasePath}"));

		builder.Services.AddSingleton<LabDatabaseService>();
		builder.Services.AddSingleton<DatabaseInitializer>();

		builder.Services.AddTransient<ResearchersPage>();
		builder.Services.AddTransient<ExperimentsPage>();
		builder.Services.AddTransient<SamplesPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		var app = builder.Build();
		InitializeDatabase(app.Services);
		return app;
	}

	private static void InitializeDatabase(IServiceProvider services)
	{
		using var scope = services.CreateScope();
		var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
		initializer.InitializeAsync().GetAwaiter().GetResult();
	}
}
