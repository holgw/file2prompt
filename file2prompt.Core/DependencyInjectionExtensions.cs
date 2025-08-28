using file2prompt.Core.Services;
using file2prompt.Core.Tools;
using file2prompt.Core.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Targets;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("file2prompt.UnitTests")]

namespace file2prompt.Core;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddCore(this ServiceCollection services)
    {
        var config = GetConfiguration();

        services.AddSingleton<IConfiguration>(config);
        services.AddTools();
        services.AddUseCases();
        services.AddServices();

        SetupLogger();
        services.AddLogging(x => x.AddNLog(LogManager.Configuration));

        return services;
    }

    private static IConfigurationRoot GetConfiguration()
    {
        string appsettingsPath = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "appsettings.json"));

        return new ConfigurationBuilder()
            .AddJsonFile(appsettingsPath, true, true)
            .Build();
    }

    private static void SetupLogger()
    {
        var configuration = new LoggingConfiguration();

        configuration.AddRule(
            LogLevel.Info,
            LogLevel.Fatal,
            GetFileTarget("log"));

        LogManager.Configuration = configuration;
    }

    private static FileTarget GetFileTarget(string fileName)
    {
        return new FileTarget()
        {
            FileName = fileName,
            MaxArchiveFiles = 5,
            ArchiveAboveSize = 100000000,
            Encoding = Encoding.UTF8
        };
    }
}
