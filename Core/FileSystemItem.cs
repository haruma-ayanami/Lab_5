namespace FileManagerPatterns.Core;

public abstract class FileSystemItem
{
    public string Name { get; set; }

    protected FileSystemItem(string name)
    {
        Name = name;
    }

    public abstract long GetSize();

    public virtual void Add(FileSystemItem item)
    {
        throw new InvalidOperationException("Нельзя добавить элемент в лист.");
    }

    public virtual void Remove(FileSystemItem item)
    {
        throw new InvalidOperationException("Нельзя удалить элемент из листа.");
    }

    public virtual FileSystemItem? GetChild(int index)
    {
        throw new InvalidOperationException("У листа нет потомков.");
    }

    public abstract void Print(string indent = "");
}