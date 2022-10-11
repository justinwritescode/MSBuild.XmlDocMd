namespace JustinWritesCode.MSBuild.XmlDocMd;

/// <summary>
/// Specify permalink style, ‘none’ or ‘pretty’ (default ‘none’). ‘pretty’ permalinks do not contain file extensions, and when you select this option periods have to be removed from file names, for example, ‘System.Console’ would have to be ‘SystemConsole’. since the removal of the ‘.md’ extension would make Jekyll think ‘.Console’ is a file extension which doesn’t work.
/// </summary>
[EnumFastToStringGenerated.EnumGenerator]
public enum PermalinkStyle
{
    ///<summary>This is the default stye.</summary>
    none,
    ///<summary>‘Pretty’ permalinks do not contain file extensions, and when you select this option periods have to be removed from file names, for example, ‘System.Console’ would have to be ‘SystemConsole’. since the removal of the ‘.md’ extension would make Jekyll think ‘.Console’ is a file extension which doesn’t work.</summary>
    pretty
}