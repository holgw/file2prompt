namespace file2prompt.Core.External.OutputWriter;

/// <summary>
/// Output writer that appends timestamped lines to the Output property of an IOutputViewModel.
/// </summary>
/// <remarks>
/// Initializes a new instance of the OutputWriter class with a specified view model.
/// </remarks>
/// <param name="outputViewModel">The view model that exposes the Output property.</param>
public class OutputWriter(IOutputViewModel outputViewModel) : IOutputWriter
{
    private readonly IOutputViewModel _outputViewModel = outputViewModel ?? throw new ArgumentNullException(nameof(outputViewModel));

    public void SеtProgress(int stepValue)
    {
        _outputViewModel.Progress += stepValue;
    }

    /// <summary>
    /// Appends a new line with a timestamp to the Output property of the view model.
    /// </summary>
    /// <param name="message">The message to append.</param>
    public void WriteLine(string message)
    {
        if (string.IsNullOrEmpty(message))
            message = string.Empty;

        _outputViewModel.Output ??= string.Empty;

        // Create timestamp in ISO 8601 format
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        // Combine timestamp and message
        string line = $"[{timestamp}] {message}";

        // Append to the Output property via the view model
        _outputViewModel.Output += line + Environment.NewLine;
    }

    public void Clear()
    {
        _outputViewModel.Progress = 0;
        _outputViewModel.Output = string.Empty;
    }
}
