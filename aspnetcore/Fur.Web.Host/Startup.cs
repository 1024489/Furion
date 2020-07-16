using Autofac;
using Fur.DependencyInjection;
using Fur.EntityFramework.Core.Extensions;
using Fur.MirrorController.Extensions;
using Fur.Mvc.Extensions;
using Fur.Mvc.Filters;
using Fur.ObjectMapper.Extensions.ServiceCollection;
using Fur.SwaggerGen.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Fur.Web.Host
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            Environment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes("monksoul@outlook.com")),
                    ValidateIssuer = true,
                    ValidIssuer = "Fur",
                    ValidateAudience = true,
                    ValidAudience = "power by Fur",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(20)
                };
            });

            services.AddHttpContextAccessor();
            services.AddControllers().AddFurMirrorControllers(Configuration);
            services.AddFurSwaggerGen(Configuration);
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add<ExceptionAsyncFilter>();
                options.Filters.Add<ValidateModelAsyncActionFilter>();

                //options.Filters.Add<UnifyResultAsyncResultFilter>();
            });
            services.AddFurObjectMapper();
            services.AddFurDbContextPool(Environment, Configuration);
        }

        public void ConfigureContainer(ContainerBuilder builder) => Injection.Initialize(builder);

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiniProfiler();

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // �淶�����
            app.UseFurUnifyResult();

            app.UseRouting();

            app.AddFurSwaggerUI();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}