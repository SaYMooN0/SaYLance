using SaYLance.executable;
using SaYLance.interfaces;
using SaYLance.parsing_components;
using SaYLance.results;
using SaYLance.std_lib;
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
        public ParsingResult ParseCodeLine(string line, int lineNumber)
        {
            string[] stringTokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (stringTokens is null || stringTokens.Length < 1)
                return ParsingResult.ParsingError(ErrorMaker.NoTokens());

            string firstTokenString = stringTokens[0];

            if (firstTokenString == _lm.VariableDefinitionWord)
            {
                if (stringTokens.Length < 2)
                    return ParsingResult.ParsingError(ErrorMaker.ExpectedVariableName(lineNumber, line.Length + 1));
                else if (!IsStringValidForName(stringTokens[1]))
                    return ParsingResult.ParsingError(ErrorMaker.InvalidVariableName(stringTokens[1], lineNumber, line.IndexOf(stringTokens[1])));
                else if (stringTokens.Length < 3 || stringTokens[2] != _lm.VariableNameAndTypeDivider.ToString())
                    return ParsingResult.ParsingError(ErrorMaker.ExpectedVariableNameAndTypeDivider(lineNumber, line.Length + 1));
                else if (stringTokens.Length < 4)
                    return ParsingResult.ParsingError(ErrorMaker.ExpectedTypeName(lineNumber, line.Length + 1));
                else if (!_lm.IsTypeKeyword(stringTokens[3]))
                    return ParsingResult.ParsingError(ErrorMaker.UnknownType(stringTokens[3], lineNumber, line.IndexOf(stringTokens[3])));
                else if (stringTokens.Length < 5)
                    return ParsingResult.ParsingError(ErrorMaker.NoAssignmentWhenDefining(lineNumber, line.Length + 1 ));
                else if (stringTokens[4].Trim().Length != 1 || stringTokens[4].Trim()[0] != equalSign)
                    return ParsingResult.ParsingError(ErrorMaker.NoAssignmentWhenDefining(lineNumber, line.IndexOf(stringTokens[4])));
                else if (stringTokens.Length < 6)
                    return ParsingResult.ParsingError(ErrorMaker.NoValueReceived(lineNumber, line.Length + 1));
                else
                {

                    VariableType type = _lm.GetVariableTypeFromString(stringTokens[3]);
                    if (IsFunctionCallingConstruction(stringTokens[5..]))
                    {
                        Isl_TypeValue? varValue = GetSl_ValueFromStringTokenSequence(stringTokens[5..], type);
                        throw new NotImplementedException();
                    }
                    if (!IsValueTypeOf(stringTokens[5], type))
                        return ParsingResult.ParsingError(ErrorMaker.UnableToParse(stringTokens[5], type, lineNumber, line.IndexOf(stringTokens[5])));

                    AbstractExecutable executable = new AbstractExecutable(BasicCommandStorage.VariableDefinition(stringTokens[1], GetSl_ValueFromString(stringTokens[5], type), lineNumber), ExecutableType.Instruction);
                    return ParsingResult.Success(executable);
                }
            }
            else
            {
                return ParsingResult.Success(AbstractExecutable.MeaningLess());
            }
        }
        private static bool IsStringValidForName(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            var regex = new Regex(@"^[a-zA-Zа-яА-Я_][a-zA-Zа-яА-Я0-9_]*$");

            return regex.IsMatch(str);
        }
        private bool IsValueTypeOf(string stringValue, VariableType type)
        {
            switch (type)
            {
                case VariableType.Void: return false;
                case VariableType.Int: return sl_Int.IsValidFormat(stringValue);
                case VariableType.String: return sl_String.IsValidFormat(stringValue);
                case VariableType.Bool: return sl_Bool.IsValidFormat(stringValue);
                case VariableType.Float: return sl_Float.IsValidFormat(stringValue);
                default: return false;
            }
        }
        static private Isl_TypeValue? GetSl_ValueFromString(string stringValue, VariableType type)
        {
            switch (type)
            {
                case VariableType.Bool:
                    {
                        sl_Bool.TryCreateFromString(stringValue, out sl_Bool result);
                        return result;
                    }
                case VariableType.Int:
                    {
                        sl_Int.TryCreateFromString(stringValue, out sl_Int result);
                        return result;
                    }
                case VariableType.Float:
                    {
                        sl_Float.TryCreateFromString(stringValue, out sl_Float result);
                        return result;
                    }
                case VariableType.String:
                    {
                        sl_String.TryCreateFromString(stringValue, out sl_String result);
                        return result;
                    }
                default: return new sl_Void();
            }
        }
        static private Isl_TypeValue? GetSl_ValueFromStringTokenSequence(string[] tokenSequence, VariableType type)
        {
            if (IsFunctionCallingConstruction(tokenSequence))
            {
                //for  function create instructs block with helping vars
                throw new NotImplementedException("no function support now");
            }
            return GetSl_ValueFromString(tokenSequence[0], type);


        }
        static private bool IsFunctionCallingConstruction(string[] tokenSequence)
        {
            return false;
        }
    }
}