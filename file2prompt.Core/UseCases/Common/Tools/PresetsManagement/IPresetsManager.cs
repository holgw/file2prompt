namespace file2prompt.Core.UseCases.Common.Tools.PresetsManagement;

internal interface IPresetsManager
{
    PresetsDictionary LoadDictionary(string presetsDir);
    void SavePreset(string presetsDir, Preset preset);
    void DeletePreset(string presetsDir, string presetName);
}