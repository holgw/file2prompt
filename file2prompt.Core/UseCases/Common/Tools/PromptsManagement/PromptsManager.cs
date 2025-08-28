using file2prompt.Core.Tools.FileSystemManagement;

namespace file2prompt.Core.UseCases.Common.Tools.PromptsManagement;

internal class PromptsManager(IFileSystemManager fileSystemManager) : IPromptsManager
{
    public PromptInfo? LoadPrompt(string promptsDir, string? promptName)
    {
        if (string.IsNullOrEmpty(promptName))
            return null;

        var pathList = fileSystemManager.Find(promptsDir);

        foreach (var filePath in pathList)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);

            if (!promptName.Equals(fileName, StringComparison.OrdinalIgnoreCase))
                continue;

            var body = fileSystemManager.ReadAllText(filePath);

            return new PromptInfo(filePath, promptName, body);
        }

        return null;
    }

    public PromptsDictionary LoadDictionary(string promptsDir)
    {
        var pathList = fileSystemManager.Find(promptsDir);
        var prompts = new List<PromptInfo>(pathList.Length);

        foreach (var filePath in pathList)
        {
            var promptName = Path.GetFileNameWithoutExtension(filePath);
            var body = fileSystemManager.ReadAllText(filePath);
            var prompt = new PromptInfo(filePath, promptName, body);
            prompts.Add(prompt);
        }

        return new PromptsDictionary(prompts);
    }
}
