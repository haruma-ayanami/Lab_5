namespace Lab_5;

using System.Collections.Generic;

public interface IFileSystem
{
    List<string> ListItems(string path);
    byte[] ReadFile(string path);
    void WriteFile(string path, byte[] data);
    void DeleteItem(string path);
}