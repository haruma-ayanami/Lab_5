namespace FileManagerPatterns.Core;

public class MyFile : FileSystemItem
{
    private byte[] _content;

    public MyFile(string name, long size) : base(name)
    {
        _content = new byte[size];
    }

    public MyFile(string name, byte[] content) : base(name)
    {
        _content = content;
    }

    public override long GetSize() => _content.LongLength;

    public byte[] ReadContent() => _content;

    public void WriteContent(byte[] data)
    {
        _content = data;
    }

    public override void Print(string indent = "")
    {
        Console.WriteLine($"{indent}Файл: {Name}, размер: {GetSize()} байт");
    }
}