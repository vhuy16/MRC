using MRC_API.Service;
using Quartz;

namespace MRC_API.Infrastructure
{
    public class OtpCleanupJob : IJob
    {
        private readonly AzureDatabaseService _azureDatabaseService;

        public OtpCleanupJob(AzureDatabaseService azureDatabaseService)
        {
            _azureDatabaseService = azureDatabaseService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var deletionResult = await _azureDatabaseService.DeleteExpiredOtpsAsync();

                if (deletionResult)
                {
                    Console.WriteLine("Expired OTPs successfully deleted.");
                }
                else
                {
                    Console.WriteLine("Failed to delete expired OTPs or none found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during OTP deletion: {ex.Message}");
            }
        }
    }

}
