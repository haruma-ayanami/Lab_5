using FileManagerPatterns.Core;

namespace FileManagerPatterns.Adapters;

public class LocalFileSystemAdapter : FileSystemAdapterBase
{
    public LocalFileSystemAdapter(Folder root) : base(root) { }
}