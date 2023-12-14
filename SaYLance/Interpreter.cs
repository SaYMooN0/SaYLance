using SaYLance.components;
using SaYLance.errors_related;
using SaYLance.interfaces;
using SaYLance.language_models;
using SaYLance.parsing_components;
using SaYLance.results;
using System.Text.RegularExpressions;

namespace SaYLance
{
    public class Interpreter
    {
        private readonly IDefaultTextIO _TextIO;
        private ILanguageModel _LanModel = null;
        private FileReader _FileReader = null;
        private Parser _Parser = null;

        public Interpreter(IDefaultTextIO textIO)
        {
            _TextIO = textIO;
        }
        public void Run(string filePath)
        {
            _FileReader = new FileReader(filePath);
            Error? err = TryToSetLanguageModelFromHeader(_FileReader.GetFirstLine()?.Content ?? string.Empty);
            if (err is not null)
            {
                End(err);
                return;
            }
            _TextIO.Log("Running...");
            _Parser = new(_LanModel);
            ParsingResult parsingRes = null;
            while (!_FileReader.IsEnded)
            {
                CodeLine? line = _FileReader.GetNextNonEmptyLine();
                if (line is null || string.IsNullOrEmpty(line.Content))
                    continue;
                parsingRes = _Parser.ParseCodeLine(line.Content, line.LineNumber);
                if(!parsingRes.IsSuccess)
                {
                    End(parsingRes.Error);
                    return;
                }
                Executor.Execute(parsingRes.Executable);
            }
        }
        private void End(Error? error)
        {
            _FileReader.Dispose();
            if (error is not null)
            {
                if (_LanModel is not null)
                    _TextIO.Error(_LanModel.ErrToStr(error));
                else
                    _TextIO.Error(error.ToString());
                return;
            }
            _TextIO.Log("Program was completed successfully");
        }
        private Error? TryToSetLanguageModelFromHeader(string header)
        {
            if (String.IsNullOrWhiteSpace(header))
                return ErrorMaker.NoHeaderFound();

            header = header.ToLower();
            var regex = new Regex(@"^\s*saylance<(.+)>\s*$");
            var match = regex.Match(header);

            if (!match.Success)
                return ErrorMaker.NoHeaderFound();

            string lanModelString = match.Groups[1].Value;

            switch (lanModelString)
            {
                case "default":
                    {
                        _LanModel = new DefaultLanguageModel();
                        return null;
                    }
                case "russian":
                    {
                        _LanModel = new RusLanguageModel();
                        return null;
                    }
                default:
                    int startPosition = header.IndexOf(lanModelString);
                    return ErrorMaker.UnknownLanguageModel(lanModelString, startPosition);
            }
        }
    }
}
