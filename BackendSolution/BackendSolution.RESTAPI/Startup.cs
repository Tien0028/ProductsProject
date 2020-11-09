using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BackendSolution.Core.ApplicationService;
using BackendSolution.Core.ApplicationService.Impl;
using BackendSolution.Core.DomainService;
using BackendSolution.Infrastructure.Data;
using BackendSolution.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PetShopApplicationSolution.Infrastructure.Data.Helpers;

namespace BackendSolution.RESTAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Create a byte array with random values. This byte array is used
            // to generate a key for signing JWT tokens.
            Byte[] secretBytes = new byte[40];
            Random rand = new Random();
            rand.NextBytes(secretBytes);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });

            services.AddControllers().AddNewtonsoftJson();

            services.AddDbContext<BackendContext>(
                opt => opt.UseSqlite("Data Source = backend.db")
                );
            // Register repositories for dependency injection
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IProductService, ProductService>();


            // Register database initializer
            services.AddTransient<IDBInitializer, DBInitializer>();


            services.AddSingleton<IAuthenticationHelper>(new AuthenticationHelper(secretBytes));

            services.AddCors(options =>
                    options.AddDefaultPolicy(builder => {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    })
                    );

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("V1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "PetShop Api",
                        Description = "PetShop Swagger",
                        Version = "V1"
                    });
                var fileName = $"{ Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
                options.IncludeXmlComments(filePath);
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using(var scope = app.ApplicationServices.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var btx = scope.ServiceProvider.GetService<BackendContext>();
                    var dbInitializer = services.GetService<IDBInitializer>();
                    dbInitializer.SeedDB(btx);
                }
            }
            else if(env.IsProduction())
            {

            }
            using(var scope = app.ApplicationServices.CreateScope())
            {
                var userRepo = scope.ServiceProvider.GetService<IUserRepository>();
                var proRepo = scope.ServiceProvider.GetService<IProductRepository>();
            }

            app.UseHttpsRedirection();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend Api V1");
                options.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
