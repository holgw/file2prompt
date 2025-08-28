namespace file2prompt.Core.UseCases.Common.Tools.PromptsManagement;

public class PromptsDictionary(IEnumerable<PromptInfo> prompts)
{
    private readonly IReadOnlyDictionary<string, PromptInfo> _prompts
        = prompts.ToDictionary(x => x.Name);

    public IEnumerable<PromptInfo> GetAll()
        => _prompts.Values;

    public PromptInfo? TryGet(string name)
        => _prompts.TryGetValue(name, out var result) ? result : null;
}
