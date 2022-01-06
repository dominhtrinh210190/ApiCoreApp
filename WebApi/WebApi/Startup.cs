using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.AppSetting;
using WebApi.Authentication;

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

            // add service repository
            services.AddScoped<IServiceWrapper, ServiceWrapper>();

            // cấu hình để mã hóa Token
            // xác thực người dùng
            var key = Configuration["AppSettings:SecretKey"]; 
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key))
                };
            });

            services.AddSingleton<IJwtAuth>(new Auth(key));
            // end

            // đọc file config appsettings.json
            var config = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(config); // đăng ký để inject
            // end
             
            // Config Sesion    
            services.AddDistributedMemoryCache();           // Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)
            services.AddSession(options => {                    // Đăng ký dịch vụ Session
                options.Cookie.Name = "SessionName";             // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
                options.IdleTimeout = new TimeSpan(0, 60, 0);    // Thời gian tồn tại của Session 
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // đối tượng này dùng để inject nhận session
            // end 

            services.AddSwaggerGen(c =>
            { 
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" }); 
            }); 
        }
         
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }
             
            app.UseRouting();

            // Đăng ký sử dụng sesion
            app.UseSession();

            // lần lượt authen trước sau đó mới đến author, xác thực trước sau đó mới check quyền
            app.UseAuthentication();
            app.UseAuthorization();
            // end xác thực + check quyền user
             
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
