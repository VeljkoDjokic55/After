using AFTER.BLL.Mappings;
using AFTER.BLL.Services.Implementations;
using AFTER.BLL.Services.Interfaces;
using AFTER.DAL.Context;
using AFTER.DAL.UOWs.Implementations;
using AFTER.DAL.UOWs.Interfaces;
using AFTER.Shared.Common;
using AFTER.WebApp.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;

namespace AFTER.WebApp
{
    public class Startup
    {
        private readonly string _directoryDist = "";
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            _directoryDist = webHostEnvironment.WebRootPath + "/dist";
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddMvc();

            services.AddDbContext<AFTERContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:AFTER"], sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    sqlServerOptions.CommandTimeout(46000);
                })
                .EnableSensitiveDataLogging(),
                ServiceLifetime.Scoped
            );

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = JwtManager.GetTokenValidationParameters();
                });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = _directoryDist;
            });

            BindConfiguration(services);
            AddMappings(services);
            BindHttpClients(services);
            BindServices(services);

            services.AddControllers().AddJsonOptions(x =>
            {
                // serialize enums as strings in api responses (e.g. Role)
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                // ignore omitted parameters on models to enable optional params (e.g. User update)
                x.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.ToString());
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApp1", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description =
                            "Please enter into field the word 'Bearer' following by space and JWT",
                        Name = "authorization",
                        Type = SecuritySchemeType.ApiKey
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme {
                            Reference =
                                new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme, Id = "Bearer"
                                },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, AFTERContext AFTERContext)
        {
            AFTERContext.Database.Migrate();

            loggerFactory.AddFile("Logs/billing_management.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
                                 "WebApp1 v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");

            app.UseSpaStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.MapWhen(x => !x.Request.Path.Value.StartsWith("/api"), builder =>
            {
                builder.UseSpa(spa =>
                {
                    spa.Options.SourcePath = _directoryDist;

                });
            });



            if (env != null)
            {
                string reportDirectoryPath = env.WebRootPath != null ? Path.Combine(env.WebRootPath, "Data") : Path.Combine(env.ContentRootPath, "Data");

                Directory.CreateDirectory(reportDirectoryPath);
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(reportDirectoryPath),
                    RequestPath = "/Data"
                });

                if (env.WebRootPath != null)
                {
                    AppDomain.CurrentDomain.SetData("Directory", Path.Combine(env.WebRootPath, "Data"));
                }
                else
                {
                    AppDomain.CurrentDomain.SetData("Directory", Path.Combine(env.ContentRootPath, "Data"));
                }
            }
        }

        private void BindConfiguration(IServiceCollection services)
        {
            services.Configure<EmailConfiguration>(Configuration.GetSection("EmailSettings"));
        }

        private void AddMappings(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(TicketProfile));
        }

        private void BindHttpClients(IServiceCollection services)
        {
            //services.AddHttpClient<ISmsHttpClient, SmsHttpClient>();
        }

        private void BindServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddTransient<IAuditLogService, AuditLogService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IMD5Service, MD5Service>();
            services.AddTransient<IHttpContextService, HttpContextService>();
            services.AddTransient<ITicketService, TicketService>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
