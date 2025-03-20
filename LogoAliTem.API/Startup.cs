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
using Microsoft.Extensions.Hosting;
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
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"])),
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Jwt:Issuer"], // Define um emissor válido
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Audience"], // Define uma audiência válida
                    ValidateLifetime = true, // Expiração do token
                    ClockSkew = TimeSpan.Zero // Sem tolerância para tokens expirados
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
        services.AddScoped<IMotoristaRepository, MotoristaRepository>();
        services.AddScoped<IVeiculoRepository, VeiculoRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IVeiculoService, VeiculoService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ILocalizacaoService, LocalizacaoService>();
        services.AddScoped<IReboqueService, ReboqueService>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins",
                builder => builder
                    .WithOrigins("https://app.logoalitem.com.br", "http://app.logoalitem.com.br", "http://localhost", "http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            );
        });
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
        if (!env.IsDevelopment())
        {
            app.UseHsts(); // 🔹 Força HTTPS e melhora a segurança do transporte (somente produção)
            app.UseHttpsRedirection();
        }

        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LogoAliTem.API v1"));

        app.UseRouting();

        app.Use(async (context, next) =>
        {
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff"); // Protege contra MIME sniffing
            context.Response.Headers.Add("Referrer-Policy", "no-referrer"); // Evita exposição de referrers
            context.Response.Headers.Add("X-Frame-Options", "DENY"); // Bloqueia ataques de Clickjacking
            context.Response.Headers.Add("X-XSS-Protection", "1; mode=block"); // Protege contra XSS
            await next();
        });

        app.UseCors("AllowSpecificOrigins");

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
