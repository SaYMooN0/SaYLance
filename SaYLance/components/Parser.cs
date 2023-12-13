using SaYLance.errors_related;
using SaYLance.interfaces;
using SaYLance.parsing_components;
using SaYLance.syntax_elements;
using System.Reflection.PortableExecutable;

namespace SaYLance.components
{
    public class Parser
    {
        private ILanguageModel _lm;

        public Parser(ILanguageModel languageMode)
        {
            _lm = languageMode;
        }
        public ParsingResult ParseLineToTokenSequence(string line, int lineNumber)
        {
            string[]? stringTokens = line.Split(' ');
            if (stringTokens is null || stringTokens.Length < 1)
                return ParsingResult.ParsingError(ErrorMaker.NoTokens());

            string firstTokenString = stringTokens[0];

            if (firstTokenString == _lm.VariableDefinitionWord)
            {
                if (stringTokens.Length < 2)
                    return ParsingResult.ParsingError(ErrorMaker.ExpectedVariableName(lineNumber, line.Length - 1));
                else if (!Variable.IsStringValidForName(stringTokens[1]))
                    return ParsingResult.ParsingError(ErrorMaker.InvalidVariableName(stringTokens[1], lineNumber, line.IndexOf(stringTokens[1])));
            }
        }
    }
}