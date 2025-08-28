using file2prompt.Core.UseCases.AbortInference;
using file2prompt.Core.UseCases.Common.Tools.PresetsManagement;
using file2prompt.Core.UseCases.Common.Tools.PromptsManagement;
using file2prompt.Core.UseCases.Common.Tools.ResponsePrinting;
using file2prompt.Core.UseCases.DeletePreset;
using file2prompt.Core.UseCases.GetAllSettings;
using file2prompt.Core.UseCases.OpenFolder;
using file2prompt.Core.UseCases.SavePreset;
using file2prompt.Core.UseCases.SubmitFiles;
using file2prompt.Core.UseCases.TestConnection;
using file2prompt.Core.UseCases.TestFileSearch;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("openAiFileFlowSolution.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace file2prompt.Core.UseCases;

internal static class DependencyInjectionExtensions
{
    public static IServiceCollection AddUseCases(this ServiceCollection services)
    {
        return services
            .AddScoped<IGetAllSettings_UseCase, GetAllSettings_UseCase>()
            .AddScoped<IOpenFolder_UseCase, OpenFolder_UseCase>()
            .AddScoped<ISubmitFiles_UseCase, SubmitFiles_UseCase>()
            .AddScoped<IAbortInference_UseCase, AbortInference_UseCase>()
            .AddScoped<ISavePreset_UseCase, SavePreset_UseCase>()
            .AddScoped<IDeletePreset_UseCase, DeletePreset_UseCase>()
            .AddScoped<ITestFileSearch_UseCase, TestFileSearch_UseCase>()
            .AddScoped<ITestConnection_UseCase, TestConnection_UseCase>()
            .AddScoped<IReponsePrinter, ReponsePrinter>()
            .AddScoped<IPromptsManager, PromptsManager>()
            .AddScoped<IPresetsManager, PresetsManager>();
    }
}
