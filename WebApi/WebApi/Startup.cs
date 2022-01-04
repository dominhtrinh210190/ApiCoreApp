using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Services;

namespace WebApi
{
    // tao bao no la main
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            // add dbcontext để kết nối database
            services.AddDbContext<VpsDbContext>(option => {
                option.UseSqlServer(Configuration.GetConnectionString("MyDb"));
            });

            // add service
            services.AddScoped<IServiceWrapper, ServiceWrapper>();   

            // add service này để nó tự động khởi tạo Repository cho ứng dụng mỗi khi inject
            //services.AddScoped<IHangHoaRepository, HangHoaRepository>();

            // xác thực người dùng với [Authorize] trước mỗi action
            services.AddAuthentication();

            services.AddSwaggerGen(c =>
            { 
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" }); 
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseRouting();

            // ủy quyền
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
