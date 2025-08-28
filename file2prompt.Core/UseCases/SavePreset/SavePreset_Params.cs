using file2prompt.Core.UseCases.Common.Tools.PresetsManagement;

namespace file2prompt.Core.UseCases.SavePreset;

public record SavePreset_Params(string PresetsDirectory, Preset Preset);
