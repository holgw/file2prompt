namespace file2prompt.Core.External.OutputWriter;

public interface IOutputWriter
{
    void SеtProgress(int stepValue);

    void WriteLine(string message);

    void Clear();
}