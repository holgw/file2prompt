using System.Diagnostics;
using System.Text.RegularExpressions;

namespace file2prompt.Core.Tools.FileSystemManagement;

internal class FileSystemManager : IFileSystemManager
{
    public string GetCurrentDirectory() => Directory.GetCurrentDirectory();

    public string[] Find(string rootDir, string pattern = "*", string? regex = null)
    {
        var files = Directory.GetFiles(rootDir, pattern, SearchOption.AllDirectories);

        if (!string.IsNullOrEmpty(regex))
        {
            var reg = new Regex(regex);
            files = [.. files.Where(path => reg.IsMatch(Path.GetFileName(path)))];
        }

        return files;
    }

    public void CreateDirectory(string filePath)
    {
        Directory.CreateDirectory(filePath);
    }

    public void WriteTextFile(string filePath, string content)
    {
        var dir = new FileInfo(filePath).Directory!.FullName;
        Directory.CreateDirectory(dir);

        using var outputFile = new StreamWriter(filePath);
        outputFile.Write(content);
    }

    public string ReadAllText(string filePath)
    {
        return File.ReadAllText(filePath);
    }

    public void OpenFileOrFolder(string path)
        => Process.Start("explorer.exe", $"\"{path}\"");

    public void DeleteFile(string filePath)
        => File.Delete(filePath);
}