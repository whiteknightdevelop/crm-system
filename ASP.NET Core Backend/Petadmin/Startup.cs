using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Petadmin.Brokers;
using Petadmin.Brokers.Interfaces;
using Petadmin.Core.Models;
using Petadmin.Hubs;
using Petadmin.Identity.Jwt;
using Petadmin.Identity.Jwt.Interfaces;
using Petadmin.Models;
using Petadmin.Repository;
using Petadmin.Repository.Cryptography;
using Petadmin.Repository.DbContext;
using Petadmin.Repository.DbEntitys;
using Petadmin.Repository.Interfaces;
using Petadmin.Repository.Services;
using Petadmin.Repository.Services.Interfaces;
using Petadmin.Services;
using Petadmin.Services.Interfaces;

namespace Petadmin
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                    .WithOrigins("http://localhost:4200")
                    .WithOrigins("https://petadmin.local:5002/")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            // Add identity types
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })

                //Adding Jwt Bearer
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = Configuration["JwtSettings:ValidAudience"],
                        ValidIssuer = Configuration["JwtSettings:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JwtSettings:SigningKey"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            // Identity Services
            services.AddTransient<IUserStore<ApplicationUser>, ApplicationUserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, ApplicationRoleStore>();
            services.AddTransient<Services.Interfaces.IAuthenticationService, Services.AuthenticationService>();

            // SignalR
            services.AddSignalR();
            services.AddControllers();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist/Petadmin";
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPetadminDbContext, PetadminContext>();
            services.AddScoped<IDbCommon, DbCommon>();
            services.AddScoped<IDbMappers, DbMappers>();
            services.AddScoped<IEntitiesList, EntitiesList>();
            services.AddTransient<IOwnerBroker, OwnerBroker>();
            services.AddTransient<IOwnerService, OwnerService>();
            services.AddTransient<IAnimalBroker, AnimalBroker>();
            services.AddTransient<IAnimalService, AnimalService>();
            services.AddTransient<IVisitBroker, VisitBroker>();
            services.AddTransient<IVisitService, VisitService>();
            services.AddTransient<IPrescriptionBroker, PrescriptionBroker>();
            services.AddTransient<IPrescriptionService, PrescriptionService>();
            services.AddTransient<IDebtBroker, DebtBroker>();
            services.AddTransient<IDebtService, DebtService>();
            services.AddTransient<IFollowUpBroker, FollowUpBroker>();
            services.AddTransient<IFollowUpService, FollowUpService>();
            services.AddTransient<IRegisterBroker, RegisterBroker>();
            services.AddTransient<IRegisterService, RegisterService>();
            services.AddTransient<ISystemBroker, SystemBroker>();
            services.AddTransient<ISystemService, SystemService>();
            services.AddTransient<IReportBroker, ReportBroker>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<ISmsBroker, SmsBroker>();
            services.AddTransient<ISmsService, SmsService>();
            services.AddTransient<ITokenFactory, TokenFactory>();
            services.AddTransient<IJwtFactory, JwtFactory>();
            services.AddTransient<IJwtTokenHandler, JwtTokenHandler>();
            services.AddTransient<IJwtTokenValidator, JwtTokenValidator>();
            services.AddTransient<Services.Interfaces.IAuthenticationService, Services.AuthenticationService>();
            services.AddTransient<IEncryptService, EncryptService>();
            services.AddTransient<IBackupDatabaseService, MysqlBackupService>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHub<SystemHub>("/systemhub");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
