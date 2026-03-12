namespace Lab_5;

using System;

public class SyncFacade
{
    private IFileSystem source;
    private IFileSystem target;

    public SyncFacade(IFileSystem source, IFileSystem target)
    {
        this.source = source;
        this.target = target;
    }

    public void SyncFolder(string sourcePath, string targetPath)
    {
        var items = source.ListItems(sourcePath);

        foreach (var item in items)
        {
            var data = source.ReadFile(item);
            target.WriteFile(item, data);
        }

        Console.WriteLine("Синхронизация завершена");
    }

    public void Backup(string sourcePath, string backupPath)
    {
        SyncFolder(sourcePath, backupPath);
    }
}