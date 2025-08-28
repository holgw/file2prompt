using file2prompt.Core.UseCases.Common;
using file2prompt.Core.UseCases.Common.Tools.PresetsManagement;
using file2prompt.Core.UseCases.Common.Tools.PromptsManagement;

namespace file2prompt.Core.UseCases.GetAllSettings;

public class GetAllSettings_Result : Result
{
    public string? PresetsDirectory { get; set; }

    public PresetsDictionary? Presets { get; set; }

    public string? PromptsDirectory { get; set; }

    public PromptsDictionary? Prompts { get; set; }

    public string? OutputDirectory { get; set; }
}
