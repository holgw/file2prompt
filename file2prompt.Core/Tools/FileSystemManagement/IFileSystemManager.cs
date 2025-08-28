namespace file2prompt.Core.Tools.FileSystemManagement;

internal interface IFileSystemManager
{
    string GetCurrentDirectory();

    string[] Find(string rootDir, string pattern = "*", string? regex = null);

    string ReadAllText(string filePath);

    void CreateDirectory(string filePath);

    void WriteTextFile(string filePath, string content);

    void OpenFileOrFolder(string path);

    void DeleteFile(string filePath);
}