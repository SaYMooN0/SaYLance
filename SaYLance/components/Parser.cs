using SaYLance.interfaces;
using SaYLance.parsing_components;
using SaYLance.variable_types;
using System.Text.RegularExpressions;

namespace SaYLance.components
{
    public class Parser
    {
        private ILanguageModel _lm;
        private const char equalSign = '=';

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
                else if (!IsStringValidForName(stringTokens[1]))
                    return ParsingResult.ParsingError(ErrorMaker.InvalidVariableName(stringTokens[1], lineNumber, line.IndexOf(stringTokens[1])));
                else if (stringTokens.Length < 3 || stringTokens[2] != _lm.VariableNameAndTypeDivider.ToString())
                    return ParsingResult.ParsingError(ErrorMaker.ExpectedVariableNameAndTypeDivider(lineNumber, line.Length - 1));
                else if (stringTokens.Length < 4)
                    return ParsingResult.ParsingError(ErrorMaker.ExpectedTypeName(lineNumber, line.Length - 1));
                else if (!_lm.IsTypeKeyword(stringTokens[3]))
                    return ParsingResult.ParsingError(ErrorMaker.UnknownType(stringTokens[3], lineNumber, line.IndexOf(stringTokens[3])));
                else if (stringTokens.Length < 5)
                    return ParsingResult.ParsingError(ErrorMaker.NoAssignmentWhenDefining(lineNumber, line.Length - 1));
                else if (stringTokens[4].Trim().Length != 1 || stringTokens[4].Trim()[0] != equalSign)
                    return ParsingResult.ParsingError(ErrorMaker.NoAssignmentWhenDefining(lineNumber, line.IndexOf(stringTokens[4])));
                else if(stringTokens.Length<6)
                    return ParsingResult.ParsingError(ErrorMaker.NoValueReceived(lineNumber, line.Length - 1));
            }
            else
            {
                return ParsingResult.Success(TokenSequence.Meaningless());
            }
        }
        private static bool IsStringValidForName(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            var regex = new Regex(@"^[a-zA-Zа-яА-Я_][a-zA-Zа-яА-Я0-9_]*$");

            return regex.IsMatch(str);
        }
        private bool IsValueTypeOf(string stringValue, string stringType)
        {
            var type = _lm.GetVariableTypeFromString(stringType);
            switch (type)
            {
                case variable_types.VariableType.Null: return false;
                case variable_types.VariableType.Int: return sl_Int.IsValidFormat(stringValue);
                case variable_types.VariableType.String: return sl_String.IsValidFormat(stringValue);
                case variable_types.VariableType.Bool: return sl_Bool.IsValidFormat(stringValue);
                case variable_types.VariableType.Float: return sl_Float.IsValidFormat(stringValue);
                default: return false;
            }
        }
        static private Isl_TypeValue? GetValueFromStringTokenSequence(string[] tokeSequence)
        {
            //for  function create instructs block with helping vars
            return null;
        }
    }
}