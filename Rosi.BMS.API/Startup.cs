using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Rosi.BMS.API.Business.Abstract;
using Rosi.BMS.API.Business.Concrete.Managers;
using Rosi.BMS.API.Core.Utilities.Security.Jwt;
using Rosi.BMS.API.DataAccess.Abstract;
using Rosi.BMS.API.DataAccess.Concrete.EntityFramework;
using Rosi.BMS.API.DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Rosi.BMS.API.DataAccess.Concrete.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Rosi.BMS.API.Core.Utilities.Mail;
using Rosi.BMS.API.Core.Extensions;
using Rosi.BMS.API.Core.Logging.Serilog;
using System.Configuration;
using FluentValidation;
using Rosi.BMS.API.Business.ValidationRules;
using FluentValidation.AspNetCore;
using Rosi.BMS.API.Business.Helpers;

namespace Rosi.BMS.API
{
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
            // AddDependencyResolvers islemi
            // EFZone
            services.AddTransient<IUserService, UserManager>();
            services.AddTransient<IUserDal, EfUserDal>();
            services.AddSingleton<IZoneDal, EfZoneDal>();
            // EFZone
            // Services
            services.AddTransient<IAuthService, AuthManager>();
            services.AddTransient<ITokenHelper, JwtHelper>();
            services.AddTransient<IUserTokenService, UserTokenManager>();
            services.AddTransient<IUserTokenDal, EfUserTokenDal>();
            services.AddSingleton<IMailService, MailManager>();
            services.AddSingleton<IZoneService, ZoneManager>();
            services.AddTransient<IFileLogService, FileLogManager>();
            // Services

            services.AddDbContext<RosiBMSApiDbContext>(options => options.UseMySQL(Configuration["ConnectionStrings:RosiBMSAPIConStr"]));
            services.AddAutoMapper(typeof(AutoMapperHelper));
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<LoginUserValidator>();
            services.AddSwaggerGen((options) =>
            {
                options.SwaggerGeneratorOptions.IgnoreObsoleteActions = true;
                //options.SwaggerDoc(Configuration.GetSection("Swagger:SwaggerName").Value, new OpenApiInfo()
                //{
                //    Version = Configuration.GetSection("Swagger:SwaggerDoc:Version").Value,
                //    Title = Configuration.GetSection("Swagger:SwaggerDoc:Title").Value,
                //    Description = Configuration.GetSection("Swagger:SwaggerDoc:Description").Value,
                //    TermsOfService = new Uri(Configuration.GetSection("Swagger:SwaggerDoc:TermsOfService").Value),
                //    Contact = new OpenApiContact
                //    {
                //        Name = Configuration.GetSection("Swagger:SwaggerDoc:Contact:Name").Value,
                //        Email = string.Empty,
                //        Url = new Uri(Configuration.GetSection("Swagger:SwaggerDoc:Contact:Url").Value),
                //    },
                //    License = new OpenApiLicense
                //    {
                //        Name = Configuration.GetSection("Swagger:SwaggerDoc:License:Name").Value,
                //        Url = new Uri(Configuration.GetSection("Swagger:SwaggerDoc:License:Url").Value),
                //    }
                //});

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}

                    }
                });

                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //options.IncludeXmlComments(xmlPath);
            });

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                DataSeeding.Seed(app);
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rosi.BMS.API v1"));
                
            }

            app.ConfigureCustomExceptionMiddleware();         
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
       
        }
    }
}
