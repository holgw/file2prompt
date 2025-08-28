using file2prompt.Core.Tools.FileSystemManagement;
using file2prompt.Core.Tools.OpenAi;
using Microsoft.Extensions.DependencyInjection;

namespace file2prompt.Core.Tools;

internal static class DependencyInjectionExtensions
{
    public static IServiceCollection AddTools(this ServiceCollection services)
    {
        return services.AddScoped<IFileSystemManager, FileSystemManager>()
            .AddScoped<IOpenAiProvider, OpenAiProvider>();
    }
}