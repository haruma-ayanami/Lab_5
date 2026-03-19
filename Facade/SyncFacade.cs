using FileManagerPatterns.Adapters;

namespace FileManagerPatterns.Facade;

public class SyncFacade
{
    private readonly IFileSystem _sourceFS;
    private readonly IFileSystem _targetFS;

    public SyncFacade(IFileSystem source, IFileSystem target)
    {
        _sourceFS = source;
        _targetFS = target;
    }

    public void SyncFolder(string sourcePath, string targetPath)
    {
        SyncRecursive(sourcePath, targetPath);
        Console.WriteLine("Синхронизация завершена.");
    }

    public void Backup(string sourcePath, string backupPath)
    {
        try
        {
            SyncRecursive(sourcePath, backupPath);
            Console.WriteLine("Резервное копирование завершено.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка резервного копирования: {ex.Message}");
        }
    }

    private void SyncRecursive(string sourcePath, string targetPath)
    {
        var items = _sourceFS.ListItems(sourcePath);

        foreach (var item in items)
        {
            string sourceItemPath = Combine(sourcePath, item);
            string targetItemPath = Combine(targetPath, item);

            try
            {
                byte[] data = _sourceFS.ReadFile(sourceItemPath);
                _targetFS.WriteFile(targetItemPath, data);
                Console.WriteLine($"Скопирован файл: {sourceItemPath} -> {targetItemPath}");
            }
            catch
            {
                SyncRecursive(sourceItemPath, targetItemPath);
            }
        }
    }

    private string Combine(string basePath, string name)
    {
        if (string.IsNullOrWhiteSpace(basePath) || basePath == "/")
            return "/" + name;

        return basePath.TrimEnd('/') + "/" + name;
    }
}