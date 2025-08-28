namespace file2prompt.Core.UseCases.Common;

public class Result
{
    public bool IsSuccess { get; set; } = true;
    public Exception? Exception { get; set; }
}