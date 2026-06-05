using SoapApi.Contracts;
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

            builder.Services.AddSingleton<
                ISupplierPurchaseOrderService,
                SupplierPurchaseOrderService>();

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