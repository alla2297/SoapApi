using Microsoft.EntityFrameworkCore;
using SoapApi.Contracts;
using SoapApi.Data;
using SoapApi.Services;
using SoapCore;

namespace SoapApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
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

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                )
            ));


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