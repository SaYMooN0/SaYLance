using SaYLance.components;
using SaYLance.errors_related;
using SaYLance.interfaces;
using SaYLance.language_models;
using System.Text.RegularExpressions;

namespace SaYLance
{
    public class Interpreter
    {
        private readonly IDefaultTextIO textIO;
        private ILanguageModel lanModel = null;
        private FileReader fileReader = null;


        public Interpreter(IDefaultTextIO textIO)
        {
            this.textIO = textIO;
        }
        public void Run(string filePath)
        {
            fileReader = new FileReader(filePath);
            Error? err = TryToSetLanguageModelFromHeader(fileReader.GetFirstLine()?.Content ?? string.Empty);
            if (err is not null)
            {
                End(err);
                return;
            }
            textIO.Log("Running");
            while(!fileReader.IsEnded)
            {
                string line = fileReader.GetNextNonEmptyLine()?.Content ?? string.Empty;
                textIO.Log(line);
            }
        }
        private void End(Error? error)
        {
            fileReader.Dispose();
            if (error is not null)
            {
                if (lanModel is not null)
                    textIO.Error(lanModel.ErrToStr(error));
                else
                    textIO.Error(error.ToString());
                return;
            }
            textIO.Log("Program was completed successfully");
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
                        lanModel = new DefaultLanguageModel();
                        return null;
                    }
                case "russian":
                    {
                        lanModel = new RusLanguageModel();
                        return null;
                    }
                default:
                    int startPosition = header.IndexOf(lanModelString);
                    return ErrorMaker.UnknownLanguageModel(lanModelString, startPosition);
            }
        }
    }
}
