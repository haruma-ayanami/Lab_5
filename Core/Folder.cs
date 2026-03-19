namespace FileManagerPatterns.Core;

public class Folder : FileSystemItem
{
    private readonly List<FileSystemItem> _children = new();

    public Folder(string name) : base(name) { }

    public override long GetSize()
    {
        long total = 0;
        foreach (var child in _children)
        {
            total += child.GetSize();
        }
        return total;
    }

    public override void Add(FileSystemItem item)
    {
        _children.Add(item);
    }

    public override void Remove(FileSystemItem item)
    {
        _children.Remove(item);
    }

    public override FileSystemItem? GetChild(int index)
    {
        if (index < 0 || index >= _children.Count)
            return null;

        return _children[index];
    }

    public IReadOnlyList<FileSystemItem> Children => _children;

    public override void Print(string indent = "")
    {
        Console.WriteLine($"{indent}Папка: {Name}, суммарный размер: {GetSize()} байт");
        foreach (var child in _children)
        {
            child.Print(indent + "  ");
        }
    }
}