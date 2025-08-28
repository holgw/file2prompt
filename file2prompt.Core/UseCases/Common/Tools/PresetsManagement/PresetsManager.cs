using file2prompt.Core.Tools.FileSystemManagement;
using System.Text.Json;

namespace file2prompt.Core.UseCases.Common.Tools.PresetsManagement;

internal class PresetsManager(IFileSystemManager fileSystemManager) : IPresetsManager
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new() { WriteIndented = true };

    public PresetsDictionary LoadDictionary(string promptsDir)
    {
        var pathList = fileSystemManager.Find(promptsDir);
        var presets = new List<Preset>(pathList.Length);

        foreach (var filePath in pathList)
        {
            var presetName = Path.GetFileNameWithoutExtension(filePath);
            var body = fileSystemManager.ReadAllText(filePath);

            try
            {
                var preset = JsonSerializer.Deserialize<Preset>(body);

                if (preset is null)
                    continue;

                preset.Name = presetName;
                presets.Add(preset);
            }
            catch
            {
                var preset = new Preset()
                {
                    Name = presetName,
                };
                presets.Add(preset);
            }
        }

        return new PresetsDictionary(presets);
    }

    public void SavePreset(string presetsDir, Preset preset)
    {
        var text = JsonSerializer.Serialize(preset, JsonSerializerOptions);
        string filePath = GetPresetPath(presetsDir, preset.Name);
        fileSystemManager.WriteTextFile(filePath, text);
    }

    public void DeletePreset(string presetsDir, string presetName)
    {
        string filePath = GetPresetPath(presetsDir, presetName);
        fileSystemManager.DeleteFile(filePath);
    }

    private static string GetPresetPath(string presetsDir, string presetName)
        => Path.Combine(presetsDir, $"{presetName}.json");
}
