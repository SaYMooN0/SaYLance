using SaYLance.parsing_components;
namespace SaYLance.components
{
    internal class FileReader : IDisposable
    {
        private readonly string _path;
        private readonly StreamReader _reader;
        private ulong lastReadLine = 0;
        public bool IsEnded { get; private set; } = false;

        public FileReader(string filePath)
        {
            _path = filePath;
            _reader = new StreamReader(_path);
        }

        public CodeLine? GetFirstLine()
        {
            lastReadLine = 1;
            return ReadNextLine();
        }

        public CodeLine? GetNextNonEmptyLine()
        {
            CodeLine? line;
            while ((line = ReadNextLine()) is not null)
            {
                if (!string.IsNullOrWhiteSpace(line.Content))
                    return line;
            }
            return null;
        }

        public CodeLine? GetLineByNumber(ulong lineNumber)
        {
            if (lineNumber < lastReadLine)
            {
                _reader.DiscardBufferedData();
                _reader.BaseStream.Seek(0, SeekOrigin.Begin);
                lastReadLine = 0;
                IsEnded = false;
            }

            CodeLine? line;
            while ((line = ReadNextLine()) is not null)
            {
                if (lastReadLine == lineNumber)
                    return line;
            }
            return null;
        }

        private CodeLine? ReadNextLine()
        {
            if (IsEnded) return null;

            string? content = _reader.ReadLine();
            if (content is null)
            {
                IsEnded = true;
                return null;
            }

            lastReadLine++;
            return new CodeLine(content, (int)lastReadLine);
        }

        public void Dispose()
        {
            _reader?.Dispose();
        }
    }
}
