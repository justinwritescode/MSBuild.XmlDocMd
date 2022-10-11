namespace JustinWritesCode.MSBuild.XmlDocMd;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// The style of newlines to use in the output.
/// </summary>
[EnumFastToStringGenerated.EnumGenerator]
public enum NewLineStyle
{
    ///<summary>Newline = "\r\n"</summary>
    [Display(Name = "\r\n")]
    crlf,

    ///<summary>Newline = "\n"</summary>
    [Display(Name = "\n")]
    lf
}