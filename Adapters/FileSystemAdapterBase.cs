using FileManagerPatterns.Core;

namespace FileManagerPatterns.Adapters;

public abstract class FileSystemAdapterBase : IFileSystem
{
    protected readonly Folder Root;

    protected FileSystemAdapterBase(Folder root)
    {
        Root = root;
    }

    public List<string> ListItems(string path)
    {
        var item = FindByPath(path);

        if (item is Folder folder)
        {
            return folder.Children.Select(x => x.Name).ToList();
        }

        return new List<string>();
    }

    public byte[] ReadFile(string path)
    {
        var item = FindByPath(path);

        if (item is MyFile file)
            return file.ReadContent();

        throw new FileNotFoundException($"Файл по пути {path} не найден.");
    }

    public void WriteFile(string path, byte[] data)
    {
        var normalized = Normalize(path);
        var parts = SplitPath(normalized);

        if (parts.Length == 0)
            throw new InvalidOperationException("Некорректный путь.");

        var fileName = parts[^1];
        var parentPath = "/" + string.Join("/", parts.Take(parts.Length - 1));

        Folder parentFolder;
        if (parts.Length == 1)
        {
            parentFolder = Root;
        }
        else
        {
            var parentItem = FindByPath(parentPath);
            if (parentItem is not Folder folder)
                throw new DirectoryNotFoundException($"Папка {parentPath} не найдена.");

            parentFolder = folder;
        }

        var existing = parentFolder.Children.FirstOrDefault(x => x.Name == fileName);

        if (existing is MyFile existingFile)
        {
            existingFile.WriteContent(data);
        }
        else if (existing == null)
        {
            parentFolder.Add(new MyFile(fileName, data));
        }
        else
        {
            throw new InvalidOperationException("Нельзя записать файл поверх папки.");
        }
    }

    public void DeleteItem(string path)
    {
        var normalized = Normalize(path);
        var parts = SplitPath(normalized);

        if (parts.Length == 0)
            throw new InvalidOperationException("Нельзя удалить корневую папку.");

        var targetName = parts[^1];
        var parentPath = "/" + string.Join("/", parts.Take(parts.Length - 1));

        Folder parentFolder;
        if (parts.Length == 1)
        {
            parentFolder = Root;
        }
        else
        {
            var parent = FindByPath(parentPath);
            if (parent is not Folder folder)
                throw new DirectoryNotFoundException($"Папка {parentPath} не найдена.");

            parentFolder = folder;
        }

        var target = parentFolder.Children.FirstOrDefault(x => x.Name == targetName);
        if (target == null)
            throw new FileNotFoundException($"Элемент {path} не найден.");

        parentFolder.Remove(target);
    }

    protected FileSystemItem? FindByPath(string path)
    {
        var normalized = Normalize(path);

        if (normalized == "/")
            return Root;

        var parts = SplitPath(normalized);
        FileSystemItem current = Root;

        foreach (var part in parts)
        {
            if (current is not Folder folder)
                return null;

            current = folder.Children.FirstOrDefault(x => x.Name == part)!;
            if (current == null)
                return null;
        }

        return current;
    }

    protected static string Normalize(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return "/";

        path = path.Replace("\\", "/");

        if (!path.StartsWith("/"))
            path = "/" + path;

        return path.TrimEnd('/') == string.Empty ? "/" : path.TrimEnd('/');
    }

    protected static string[] SplitPath(string path)
    {
        return path.Trim('/').Split('/', StringSplitOptions.RemoveEmptyEntries);
    }
}