using FileManagerPatterns.Core;
using FileManagerPatterns.Adapters;
using FileManagerPatterns.Facade;

namespace FileManagerPatterns;


internal class Program
{
    static void Main(string[] args)
    {
        // Локальная файловая система
        var localRoot = new Folder("root");
        var docs = new Folder("docs");
        var images = new Folder("images");

        docs.Add(new MyFile("report.docx", 1200));
        docs.Add(new MyFile("notes.txt", 300));

        images.Add(new MyFile("photo1.jpg", 1500));
        images.Add(new MyFile("photo2.jpg", 2000));

        localRoot.Add(docs);
        localRoot.Add(images);
        localRoot.Add(new MyFile("readme.md", 100));

        Console.WriteLine("Локальная файловая система:");
        localRoot.Print();
        Console.WriteLine($"Размер корневой директории: {localRoot.GetSize()} байт");
        Console.WriteLine();

        // FTP
        var ftpRoot = new Folder("root");
        ftpRoot.Add(new Folder("backup"));

        // Cloud
        var cloudRoot = new Folder("root");
        cloudRoot.Add(new Folder("cloud_backup"));

        IFileSystem localFs = new LocalFileSystemAdapter(localRoot);
        IFileSystem ftpFs = new FtpFileSystemAdapter(ftpRoot);
        IFileSystem cloudFs = new CloudFileSystemAdapter(cloudRoot);

        // Фасад для синхронизации
        var syncToCloud = new SyncFacade(localFs, cloudFs);
        syncToCloud.SyncFolder("/", "/cloud_backup");

        Console.WriteLine();

        var backupToFtp = new SyncFacade(localFs, ftpFs);
        backupToFtp.Backup("/", "/backup");

        Console.WriteLine();
        Console.WriteLine("Проверка содержимого cloud_backup:");
        var cloudItems = cloudFs.ListItems("/cloud_backup");
        foreach (var item in cloudItems)
        {
            Console.WriteLine(item);
        }
    }
}