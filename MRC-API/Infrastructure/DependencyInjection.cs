using Quartz;

namespace MRC_API.Infrastructure
{
    public static class DependecyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddQuartz(options =>
            {
                options.UseMicrosoftDependencyInjectionJobFactory();

                var jobKey = JobKey.Create(nameof(OtpCleanupJob));

                options
                    .AddJob<OtpCleanupJob>(jobKey)
                    .AddTrigger(trigger => trigger
                                .ForJob(jobKey)
                                .WithCronSchedule("0 0 0 * * ?"));
            });

            services.AddQuartzHostedService();
        }
    }
}
