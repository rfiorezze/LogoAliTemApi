using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace LogoAliTem.Persistence
{
    public class LogoAliTemContextFactory : IDesignTimeDbContextFactory<LogoAliTemContext>
    {
        public LogoAliTemContext CreateDbContext(string[] args)
        {
            // Define o ambiente atual (padrão é "Development" se não estiver definido)
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            // Define o caminho absoluto para a pasta onde está o appsettings.json
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "LogoAliTem.API");

            // Configura o carregamento do arquivo de configuração correto
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Carrega o appsettings.json padrão
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true) // Carrega o arquivo específico do ambiente
                .Build();

            // Cria o DbContextOptions com a string de conexão apropriada
            var optionsBuilder = new DbContextOptionsBuilder<LogoAliTemContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("Default"));

            return new LogoAliTemContext(optionsBuilder.Options);
        }
    }
}