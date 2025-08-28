namespace file2prompt.Core.UseCases.Common.Tools.PresetsManagement;

public class PresetsDictionary(IEnumerable<Preset> presets)
{
    private readonly IReadOnlyDictionary<string, Preset> _prompts
        = presets.ToDictionary(x => x.Name);

    public Preset[] GetAll()
        => [.. _prompts.Values];

    public Preset? TryGet(string name)
        => _prompts.TryGetValue(name, out var result) ? result : null;
}
