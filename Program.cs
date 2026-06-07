using Microsoft.EntityFrameworkCore;
using SoapApi.Contracts;
using SoapApi.Data;
using SoapApi.Services;
using SoapCore;
using DotNetEnv;

namespace SoapApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Env.Load();
            }
            catch
            {
                // Running inside Docker
            }
            var builder = WebApplication.CreateBuilder(args);

            // REST
            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // SOAP
            builder.Services.AddSoapCore();

            builder.Services.AddScoped<
                ISupplierPurchaseOrderService,
                SupplierPurchaseOrderService>();

            var host =
                Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true"
                    ? "mysql"
                    : "localhost";

            builder.Services.AddScoped<AuditService>();
            var connectionString =
                    $"server={host};" +
                    $"port={System.Environment.GetEnvironmentVariable("SOAP_DB_PORT")};" +
                    $"database={System.Environment.GetEnvironmentVariable("SOAP_DB_NAME")};" +
                    $"user={System.Environment.GetEnvironmentVariable("SOAP_DB_USER")};" +
                    $"password={System.Environment.GetEnvironmentVariable("SOAP_DB_USER_PASSWORD")}";
            Console.WriteLine(connectionString);
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString)

                )
            );


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.UseEndpoints(endpoints =>
            {
                endpoints.UseSoapEndpoint<ISupplierPurchaseOrderService>(
                    "/SupplierPurchaseOrderService.asmx",
                    new SoapEncoderOptions(),
                    SoapSerializer.DataContractSerializer
                );
            });

            app.Run();
        }
    }
}