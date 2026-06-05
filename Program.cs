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
            

            Env.Load();
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

            
            var connectionString =
                    $"server=localhost;" +
                    $"port={System.Environment.GetEnvironmentVariable("SOAP_DB_PORT")};" +
                    $"database=soapdb;" +
                    $"user=root;" +
                    $"password={System.Environment.GetEnvironmentVariable("SOAP_DB_PASSWORD")}";
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
                    SoapSerializer.DataContractSerializer);
            });

            app.Run();
        }
    }
}