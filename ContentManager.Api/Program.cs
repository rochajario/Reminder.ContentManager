using ContentManager.Api.Extensions;
using ContentManager.Data.Extensions;
using ContentManager.Domain.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ContentManager.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("Database")!;

            builder.Services
                .AddJwtAuthentication(builder.Configuration)
                .LoadDatabaseContext(connectionString)
                .LoadDomainServices();

            builder.Services.AddControllers();
            builder.Services
                .AddEndpointsApiExplorer()
                .AddSwaggerDocumentation(builder.Configuration);

            var app = builder.Build();

            app.UseSwagger()
                .UseSwaggerUI();

            app.UseHttpsRedirection()
                .UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}