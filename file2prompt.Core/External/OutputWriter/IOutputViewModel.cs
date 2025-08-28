namespace file2prompt.Core.External.OutputWriter;

/// <summary>
/// Interface representing a view model with an Output string property.
/// </summary>
public interface IOutputViewModel
{
    int Progress { get; set; }

    string Output { get; set; }
}