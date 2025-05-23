using BasicPaymentGateway.Contracts.Interfaces;
using BasicPaymentGateway.Contracts.Services;
using BasicPaymentGateway.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BasicPaymentGateway.Extentions
{
    public static class ApplicationSettings
    {
        public static void ApplicationSettingExtensions(this IHostApplicationBuilder app)
        { 
            app.Services.AddScoped<IGenericService,GenericService>();
            app.Services.AddScoped<IPaymentService,PaymentService>();
            app.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("BasicPaymentGatewayInMemoryDb");
            });
            app.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

          
        }
    }
}
