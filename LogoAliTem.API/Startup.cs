using LogoAliTem.Application;
using LogoAliTem.Application.Interfaces;
using LogoAliTem.Domain.Identity;
using LogoAliTem.Persistence;
using LogoAliTem.Persistence.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LogoAliTem.API;
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
        string connectionString = Configuration.GetConnectionString("Default"); 
        
        services.AddDbContext<LogoAliTemContext>(
            context => context.UseNpgsql(connectionString)
        );
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddIdentityCore<User>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 4;
        })
              .AddRoles<Role>()
              .AddRoleManager<RoleManager<Role>>()
              .AddSignInManager<SignInManager<User>>()
              .AddRoleValidator<RoleValidator<Role>>()
              .AddEntityFrameworkStores<LogoAliTemContext>()
              .AddDefaultTokenProviders();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

        services.AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
                )
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<IMotoristaService, MotoristaService>();
        services.AddScoped<IBaseRepository, BaseRepository>();
        services.AddScoped<IMotoristaRepository, MotoristaRespository>();
        services.AddScoped<IVeiculoRepository, VeiculoRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IVeiculoService, VeiculoService>();

        services.AddCors();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "LogoAliTem.API", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header usando Bearer.
                                Entre com 'Bearer ' [espa�o] ent�o coloque seu token.
                                Exemplo: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LogoAliTem.API v1"));

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseHttpsRedirection();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
