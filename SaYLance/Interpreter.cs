using SaYLance.components;
using SaYLance.errors_related;
using SaYLance.function_related;
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
        private LanguageModel _LanModel = new();
        private FileReader _FileReader = null;
        private Parser _Parser = null;
        private FunctionsStorage _funcStorage = null;

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
                End(err); return;
            }
            _TextIO.Log("Running...");
            _funcStorage = new(_TextIO, _LanModel);
            _Parser = new(_LanModel, _funcStorage);
     
            ParsingResult parsingRes = null;
            ExecutionResult executionRes = null;
            while (!_FileReader.IsEnded)
            {
                CodeLine? line = _FileReader.GetNextNonEmptyLine();
                if (line is null || string.IsNullOrEmpty(line.Content))
                    continue;
                parsingRes = _Parser.ParseCodeLine(line.Content, line.LineNumber);
                if (!parsingRes.IsSuccess)
                {
                    End(parsingRes.Error); return;
                }
                executionRes = Executor.Execute(parsingRes.Executable);
                if (!executionRes.IsSuccess)
                {
                    End(executionRes.Error);
                    return;
                }
            }
            End();
        }
        private void End()
        {
            _FileReader.Dispose();
            VariablesStorage.Dispose();
            _TextIO.Log("Program ended");
        }
        private void End(Error? error)
        {
            if (error is not null)
            {
                if (_LanModel is not null)
                    _TextIO.Error(_LanModel.ErrToStr(error));
                else
                    _TextIO.Error(error.ToString());
                return;
            }
            End();
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
                        _LanModel = new LanguageModel();
                        return null;
                    }
                case "russian":
                    {
                        _LanModel = new CustomLanguageModel();
                        return null;
                    }
                default:
                    int startPosition = header.IndexOf(lanModelString);
                    return ErrorMaker.UnknownLanguageModel(lanModelString, startPosition);
            }
        }
    }
}
