using System.Text;

namespace DotObjTools.Extensions;

internal static class StreamReaderExtensions
{
    public static string? readTrimmedLogicalLine(this StreamReader reader, string continuationLexem, ref uint physicalLineNumber)
    {
        string? line;
        StringBuilder? sbLine = null;

        physicalLineNumber++;
        line = reader.ReadLine()?.Trim();
        while (line is not null && line.EndsWith(continuationLexem))
        {
            sbLine ??= new StringBuilder();
            sbLine.Append(line.Replace(continuationLexem, " "));
            physicalLineNumber++;
            line = reader.ReadLine()?.Trim();
        }

        return sbLine is null ? line : sbLine.ToString();
    }
}
