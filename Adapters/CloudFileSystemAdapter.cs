using FileManagerPatterns.Core;

namespace FileManagerPatterns.Adapters;

public class CloudFileSystemAdapter : FileSystemAdapterBase
{
    public CloudFileSystemAdapter(Folder root) : base(root) { }
}