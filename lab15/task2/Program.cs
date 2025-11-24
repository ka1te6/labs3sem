using Task2;

var logsDir = Path.Combine(AppContext.BaseDirectory, "logs");
var textPath = Path.Combine(logsDir, "app.log");
var jsonPath = Path.Combine(logsDir, "app.json");

var logger = new MyLogger(
    new TextFileLogRepository(textPath),
    new JsonFileLogRepository(jsonPath));

logger.Info("Приложение запущено.");
logger.Debug("Выполняем тестовое действие.");

try
{
    SimulateWork();
    logger.Info("Работа завершена успешно.");
}
catch (Exception ex)
{
    logger.Error("Ошибка при выполнении работы", ex);
}

Console.WriteLine($"Логи записаны в {textPath} и {jsonPath}");

static void SimulateWork()
{
    var rnd = Random.Shared.Next(0, 3);
    if (rnd == 0)
    {
        throw new InvalidOperationException("Случайная ошибка для демонстрации логирования.");
    }
}
