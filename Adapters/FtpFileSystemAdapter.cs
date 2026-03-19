using FileManagerPatterns.Core;

namespace FileManagerPatterns.Adapters;

public class FtpFileSystemAdapter : FileSystemAdapterBase
{
    public FtpFileSystemAdapter(Folder root) : base(root) { }
}