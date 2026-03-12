namespace Lab_5;

using System.Collections.Generic;

public class Folder : FileSystemItem
{
    private List<FileSystemItem> items = new List<FileSystemItem>();

    public Folder(string name) : base(name) { }

    public override void Add(FileSystemItem item)
    {
        items.Add(item);
    }

    public override long GetSize()
    {
        long total = 0;

        foreach (var item in items)
        {
            total += item.GetSize();
        }

        return total;
    }
}