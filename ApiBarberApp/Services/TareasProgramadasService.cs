using Microsoft.Data.SqlClient;

namespace ApiBarberApp.Services
{
    public class TareasProgramadasService : BackgroundService
    {
        private readonly IConfiguration _configuration;

        public TareasProgramadasService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var interval = TimeSpan.FromMinutes(10);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ExecuteStoredProcedureAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error ejecutando el procedimiento almacenado: {ex.Message}");
                }
                await Task.Delay(interval, stoppingToken);
            }
        }

        private async Task ExecuteStoredProcedureAsync()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            string procedureName = "[BarberApp].[PR_ASIGNAR_AGENDA_BARBERS]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    await command.ExecuteNonQueryAsync();
                    Console.WriteLine("Procedimiento almacenado ejecutado con éxito.");
                }
            }
        }
    }
}
