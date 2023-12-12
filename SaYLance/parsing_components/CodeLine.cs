namespace SaYLance.parsing_components
{
    public class CodeLine
    {
        public readonly string Content;
        public readonly int LineNumber;
        public CodeLine(string content, int lineNumber)
        {
            Content = content;
            LineNumber = lineNumber;
        }
    }
}
