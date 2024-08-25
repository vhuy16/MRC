using AutoMapper;
using Bean_Mind.API.Utils;
using Business.Interface;
using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using System.Data;

namespace MRC_API.Service
{
    public class AzureDatabaseService : BaseService<AzureDatabaseService>
    {
        public AzureDatabaseService(IUnitOfWork<MrcContext> unitOfWork, ILogger<AzureDatabaseService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<bool> DeleteExpiredOtpsAsync()
        {
            try
            {
                var currentTime = TimeUtils.GetCurrentSEATime();

                var query = "DELETE FROM OTP WHERE ExpiresAt < @CurrentTime";

                await ExecuteDeleteCommand(query, currentTime);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong quá trình xóa OTP: {ex.Message}");
                return false;
            }
        }

        private async Task ExecuteDeleteCommand(string query, DateTime currentTime)
        {
            using (var command = _unitOfWork.Context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@CurrentTime";
                parameter.Value = currentTime;
                command.Parameters.Add(parameter);

                await _unitOfWork.Context.Database.OpenConnectionAsync();

                await command.ExecuteNonQueryAsync();
            }
        }

    }

}
