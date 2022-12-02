using System.Reflection;
using Task4.Application.Common.Mapping;
using Task4.Application.Interfaces;
using Task4.Application;
using Task4.Persistence;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Task4.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Diagnostics;

namespace Task4.WebApi
{
    public static class Startup
    {
        public static IConfiguration? Configuration { get; set; }
        public static IWebHostEnvironment? Environment { get; set; }

        public static WebApplicationBuilder Init(WebApplicationBuilder builder)
        {
            Configuration = builder.Configuration;
            Environment = builder.Environment;
            return builder;
        }

        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(IRegisteredUserDbContext).Assembly));
            });

            services.AddApplication();
            services.AddPersistence(Configuration!);
            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.ApplyJwtOptions(Configuration!));
            services.AddControllers();
            services.AddCors(options => options.ApplyAllowAllCorsPolicy());
        }

        public static void ConfigureApplicationPipeline(WebApplication app)
        {

            app.UseExceptionHandling();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAdditionalAuthorization();
            app.MapControllers();
        }

        private static void ApplyAllowAllCorsPolicy(this CorsOptions options)
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyOrigin();
                policy.AllowAnyMethod();
            });
        }

        private static void ApplyJwtOptions(this JwtBearerOptions options, IConfiguration configuration)
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["Audience"],
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"]!)),
                ValidateIssuerSigningKey = true,
            };
        }

        public static void RunReact()
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = false;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            
            cmd.StandardInput.WriteLine("cd wwwroot");
            cmd.StandardInput.WriteLine("npm start");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
        }
    }
}
