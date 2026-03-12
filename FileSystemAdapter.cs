namespace Lab_5;

using System.Collections.Generic;

public class FileSystemAdapter : IFileSystem
{
    public List<string> ListItems(string path)
    {
        return new List<string>();
    }

    public byte[] ReadFile(string path)
    {
        return new byte[0];
    }

    public void WriteFile(string path, byte[] data)
    {
    }

    public void DeleteItem(string path)
    {
    }
}