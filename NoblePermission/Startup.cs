using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NoblePermission.BusinessLayer.Services.PaymentLimitServices;
using NoblePermission.BusinessLayer.Services.PermissionServices;
using NoblePermission.BusinessLayer.Services.UserServices;
using NoblePermission.Persistance;
using NoblePermission.BusinessLayer.Services.FtpAccountServices;
using NoblePermission.MiddleWare;

namespace NoblePermission
{
    public class Startup
    {
        private IServiceCollection _services;
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins(Configuration.GetSection("frontend:IpAndServerAddress").Value,
                                Configuration.GetSection("frontend:ForNobleApplication").Value,
                                Configuration.GetSection("frontend:ForLocalHost").Value,
                                Configuration.GetSection("frontend:ForLocalHost1").Value,
                                Configuration.GetSection("frontend:ForLocalHost2").Value,
                                Configuration.GetSection("frontend:ForLocalHost3").Value,
                                Configuration.GetSection("frontend:ForLocalHost4").Value,
                                Configuration.GetSection("frontend:ForLocalHost5").Value,
                                Configuration.GetSection("frontend:ForLocalHost6").Value,
                                Configuration.GetSection("frontend:ForLocalHost7").Value,
                                Configuration.GetSection("frontend:ForLocalHost8").Value,
                                Configuration.GetSection("frontend:ForLocalHost9").Value,
                                Configuration.GetSection("frontend:ForLocalHost10").Value,
                                Configuration.GetSection("frontend:ForLocalHost11").Value,
                                Configuration.GetSection("frontend:ForLocalHost12").Value,
                                Configuration.GetSection("frontend:ForLocalHost13").Value,
                                Configuration.GetSection("frontend:ForLocalHost14").Value,
                                Configuration.GetSection("frontend:ForLocalHost15").Value,
                                Configuration.GetSection("frontend:ForLocalHost16").Value,
                                Configuration.GetSection("frontend:ForLocalHost17").Value,
                                Configuration.GetSection("frontend:ForLocalHost18").Value,
                                Configuration.GetSection("frontend:ForLocalHost19").Value,

                                Configuration.GetSection("frontend:ForLocalHost20").Value,
                                Configuration.GetSection("frontend:ForLocalHost21").Value,
                                Configuration.GetSection("frontend:ForLocalHost22").Value,
                                Configuration.GetSection("frontend:ForLocalHost23").Value,
                                Configuration.GetSection("frontend:ForLocalHost24").Value,
                                Configuration.GetSection("frontend:ForLocalHost25").Value,
                                Configuration.GetSection("frontend:ForLocalHost26").Value,
                                Configuration.GetSection("frontend:ForLocalHost27").Value,
                                Configuration.GetSection("frontend:ForLocalHost28").Value,
                                Configuration.GetSection("frontend:ForLocalHost29").Value,

                                Configuration.GetSection("frontend:ForLocalHost30").Value,
                                Configuration.GetSection("frontend:ForLocalHost31").Value,
                                Configuration.GetSection("frontend:ForLocalHost32").Value,
                                Configuration.GetSection("frontend:ForLocalHost33").Value,
                                Configuration.GetSection("frontend:ForLocalHost34").Value,
                                Configuration.GetSection("frontend:ForLocalHost35").Value,
                                Configuration.GetSection("frontend:ForLocalHost36").Value,
                                Configuration.GetSection("frontend:ForLocalHost37").Value,
                                Configuration.GetSection("frontend:ForLocalHost38").Value,
                                Configuration.GetSection("frontend:ForLocalHost39").Value,
                                "app://.")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), providerOptions => providerOptions.EnableRetryOnFailure()));
            services.AddTransient<IPermission, Permission>();
            services.AddTransient<IUser, UserS>();
            services.AddTransient<IPaymentLimit, PaymentLimitS>();
            services.AddTransient<IFtpAccountDetail, FtpAccountDetails>();
            services.AddControllers();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
