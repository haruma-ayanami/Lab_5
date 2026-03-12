namespace Lab_5;

public class FileItem : FileSystemItem
{
    private long size;

    public FileItem(string name, long size) : base(name)
    {
        this.size = size;
    }

    public override long GetSize()
    {
        return size;
    }
}