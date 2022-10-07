using Microsoft.Data.SqlClient;

namespace test;

public class Worker : BackgroundService
{
    private ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            if (true)
            {
                var random = new Random();

                await GetData();
            }
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
        catch (NullReferenceException)
        {

        }
    }

    private async Task GetData() 
    {
        using var connection = new SqlConnection("connection string");
        using var command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM table WHERE field " + Environment.GetEnvironmentVariable("VAR");

        await command.ExecuteNonQueryAsync();
    }
}
