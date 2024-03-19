using ContentManager.Data.Extensions;
using ContentManager.Domain.Extensions;

namespace ContentManager.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host
                .CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var databaseConnectionString = hostContext.Configuration.GetConnectionString("Database")!;
                    services
                        .LoadDatabaseContext(databaseConnectionString)
                        .LoadDomainServices();

                    services.AddHostedService<PublisherWorker>();
                })
                .Build();

            host.Run();
        }
    }
}