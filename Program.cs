using System;

namespace Lab_5;

class Program
{
    static void Main()
    {
        Folder root = new Folder("root");

        FileItem file1 = new FileItem("file1.txt", 100);
        FileItem file2 = new FileItem("file2.txt", 200);

        root.Add(file1);
        root.Add(file2);

        Console.WriteLine("Размер папки: " + root.GetSize());
    }
}