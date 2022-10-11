namespace JustinWritesCode.MSBuild.XmlDocMd;
using System;
using XmlDocMarkdown.Core;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using static System.String;

/// <summary>
/// An MSBuild task that generates Markdown documentation from XML documentation comments.
/// </summary>
public class GenerateMdDoc : Microsoft.Build.Utilities.Task
{
    /// <summary>
    /// The XML doc markdown settings.
    /// </summary>
    public XmlDocMarkdownSettings Settings { get; set; } = new XmlDocMarkdownSettings();

    /// <summary>
    /// The absolute or relative path to the input assembly
    /// </summary>
    /// <value><inheritdoc path="./sumary" /></value>
    /// <remarks>Optional; uses Assembly if omitted.</remarks>
    public string InputAssemblyPath { get => Input.AssemblyPath; set => Input.AssemblyPath = value; }

    /// <summary>
    /// The absolute or relative path to the XML doc file.
    /// </summary>
    /// <remarks>
    /// Optional; changes the extension of AssemblyPath or Assembly.Location if omitted.
    /// </remarks>
    public string XmlDocPath { get => Input.XmlDocPath; set => Input.XmlDocPath = value; }

    /// <summary>
    /// The already-loaded assembly.
    /// </summary>
    /// <remarks>Optional; uses AssemblyPath if omitted.</remarks>
    public Assembly Assembly { get => Input.Assembly; set => Input.Assembly = value; }

    /// <summary>
    /// The input for the task.
    /// </summary>
    [Microsoft.Build.Framework.Required]
    public XmlDocInput Input { get; set; } = new XmlDocInput();


    /// <summary>
    /// The output directory.
    /// </summary>
    [Microsoft.Build.Framework.Required]
    public string TargetPath { get; set; }

    /// <summary>
    /// A list of namespaces to include in the output.
    /// </summary>
    public IEnumerable<string> ExternalDocNamespaces { get => Settings.ExternalDocs.Select(x => x.Namespace); set => Settings.ExternalDocs = value.Select(x => new ExternalDocumentation { Namespace = x }).ToList(); }

    /// <summary>
    /// If non-null, contains the path to a file that contains the Jekyll front matter
    /// template.
    /// </summary>
    public string FrontMatter { get => Settings.FrontMatter; set => Settings.FrontMatter = value; }

    /// <summary>
    /// If true, generates a .yml file that can be used in a Jekyll based site.
    /// </summary>
    public bool GenerateToc { get => Settings.GenerateToc; set => Settings.GenerateToc = value; }
    /// <summary>
    /// If true, generates documentation for obsolete types and members. (Default false.)
    /// </summary>
    public bool IncludeObsolete { get => Settings.IncludeObsolete; set => Settings.IncludeObsolete = value; }

    /// <summary>
    /// If true, executes without making changes to the file system.
    /// </summary>
    public bool IsDryRun { get => Settings.IsDryRun; set => Settings.IsDryRun = value; }

    /// <summary>
    /// If true, suppresses normal console output.
    /// </summary>
    public bool IsQuiet { get => Settings.IsQuiet; set => Settings.IsQuiet = value; }

    /// <summary>
    /// If true, deletes previously generated files that are no longer used.
    /// </summary>
    public bool ShouldClean { get => Settings.ShouldClean; set => Settings.ShouldClean = value; }
    /// <summary>
    /// If true, skips documentation for types and members with [EditorBrowsable(EditorBrowsableState.Never)].
    /// </summary>
    public bool SkipUnbrowsable { get => Settings.SkipUnbrowsable; set => Settings.SkipUnbrowsable = value; }

    ///<summary>The URL of the folder containing the source code of the assembly, e.g. at GitHub.</summary>
    /// <remarks>The URL may be absolute or relative. Required to generate source code links in the See Also sections for types.</remarks>
    public Uri SourceCodePath { get => !IsNullOrEmpty(Settings.SourceCodePath) ? new Uri(Settings.SourceCodePath) : null; set => Settings.SourceCodePath = value?.ToString(); }

    /// <summary>
    /// Generate separate pages for each namespace containing list of types in each.
    /// </summary>
    public bool GenerateNamespacePages { get => Settings.NamespacePages; set => Settings.NamespacePages = value; }
    /// <summary>
    ///  Indicates the newline used in the output.
    /// </summary>
    public NewLineStyle NewLine { get => Settings.NewLine == "\r\n" ? NewLineStyle.crlf : NewLineStyle.lf; set => Settings.NewLine = value == NewLineStyle.crlf ? "\r\n" : "\n"; }

    /// <summary>
    /// Specify permalink style, 'none' or 'pretty' (default 'none'). 'pretty' permalinks
    ///     do not contain file extensions, and when you select this option periods have
    ///     to be removed from file names, for example, 'System.Console' would have to be
    ///     'SystemConsole'. since the removal of the '.md' extension would make Jekyll think
    ///     '.Console' is a file extension which doesn't work.
    /// </summary>
    public PermalinkStyle PermalinkStyle { get => (PermalinkStyle)Enum.Parse(typeof(PermalinkStyle), Settings.PermalinkStyle); set => Settings.PermalinkStyle = value.ToString(); }

    /// <summary>The root namespace of the input assembly.</summary>
    /// <remarks>
    /// Used to generate source code links in the See Also sections for types. If omitted,
    /// the tool guesses the root namespace from the exported types.
    /// </remarks>
    public string RootNamespace { get => Settings.RootNamespace; set => Settings.RootNamespace = value; }

    /// <summary>
    ///  A path prefix to add to all links in the table of contents .yml file.
    /// </summary>
    public string TocPrefix { get => Settings.TocPrefix; set => Settings.TocPrefix = value; }

    /// <summary>
    /// The minimum visibility for documented types and members.
    /// </summary>
    /// <remarks>Defaults to Protected.</remarks>
    public XmlDocVisibilityLevel MinimumVisibilityLevel { get => Settings.VisibilityLevel ?? XmlDocVisibilityLevel.Protected; set => Settings.VisibilityLevel = value; }


    /// <summary>
    /// The output directory.
    /// </summary>
    [Microsoft.Build.Framework.Required]
    public string OutputPath { get; set; }

    public override bool Execute()
    {
        var result = XmlDocMarkdownGenerator.Generate(Input, TargetPath, Settings);
        return true;
    }
}
