namespace file2prompt.Core.UseCases.Common.Tools.PresetsManagement;

public class Preset
{
    public string Name { get; set; } = String.Empty;

    // CONNECTION
    public string? Url { get; set; }
    public string? ApiKey { get; set; }
    public string? Model { get; set; }

    // PROCESSING
    public string? WorkingDirectory { get; set; }
    public string? Regex { get; set; }
    public string? PromptName { get; set; }

    // POST-PROCESSING
    public string? StartTag { get; set; }
    public string? EndTag { get; set; }
    public string? OutputFilePostfix { get; set; }
}