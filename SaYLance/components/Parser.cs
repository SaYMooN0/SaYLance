using SaYLance.executable;
using SaYLance.function_related;
using SaYLance.interfaces;
using SaYLance.language_models;
using SaYLance.parsing_components;
using SaYLance.results;
using SaYLance.std_lib;
using SaYLance.variable_types;
using System.Linq;
using System.Text.RegularExpressions;

namespace SaYLance.components
{
    public class Parser
    {
        private LanguageModel _lanModel;
        private FunctionsStorage _funcStorage;
        private const char equalSign = '=';

        public Parser(LanguageModel languageMode, FunctionsStorage functionsStorage)
        {
            _lanModel = languageMode;
            _funcStorage = functionsStorage;
        }
        public ParsingResult ParseCodeLine(string line, int lineNumber)
        {
            string[] stringTokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (stringTokens is null || stringTokens.Length < 1)
                return ParsingResult.ParsingError(ErrorMaker.NoTokens());

            string firstTokenString = stringTokens[0];

            if (firstTokenString == _lanModel.GetKeyWordFor("varDefWord"))
                return ParseVariableDefinition(line, lineNumber);

            else if (IsFunctionCall(firstTokenString))
                return ParseFunctionCall(line, lineNumber);

            else
            {
                return ParsingResult.Success(AbstractExecutable.MeaningLess());
            }
        }
        private bool IsStringValidForName(string str)
        {
            if (string.IsNullOrEmpty(str) || _lanModel.IsStringKeyWord(str))
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
            return type switch
            {
                VariableType.Bool when sl_Bool.TryCreateFromString(stringValue, out sl_Bool resultBool) => resultBool,
                VariableType.Int when sl_Int.TryCreateFromString(stringValue, out sl_Int resultInt) => resultInt,
                VariableType.Float when sl_Float.TryCreateFromString(stringValue, out sl_Float resultFloat) => resultFloat,
                VariableType.String when sl_String.TryCreateFromString(stringValue, out sl_String resultString) => resultString,
                _ => new sl_Void(),
            };
        }
        private bool IsFunctionCall(string token) => token.StartsWith(_lanModel.GetKeyWordFor("funcCall"));
        private ParsingResult ParseVariableDefinition(string line, int lineNumber)
        {
            string[] stringTokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (stringTokens.Length < 2)
                return ParsingResult.ParsingError(ErrorMaker.ExpectedVariableName(lineNumber, line.Length + 1));
            else if (!IsStringValidForName(stringTokens[1]))
                return ParsingResult.ParsingError(ErrorMaker.InvalidVariableName(stringTokens[1], lineNumber, line.IndexOf(stringTokens[1])));
            else if (stringTokens.Length < 3 || stringTokens[2] != _lanModel.GetKeyWordFor("varNameTypeDivider"))
                return ParsingResult.ParsingError(ErrorMaker.ExpectedVariableNameAndTypeDivider(lineNumber, line.Length + 1));
            else if (stringTokens.Length < 4)
                return ParsingResult.ParsingError(ErrorMaker.ExpectedTypeName(lineNumber, line.Length + 1));
            else if (!_lanModel.IsTypeKeyword(stringTokens[3]))
                return ParsingResult.ParsingError(ErrorMaker.UnknownType(stringTokens[3], lineNumber, line.IndexOf(stringTokens[3])));
            else if (stringTokens.Length < 5)
                return ParsingResult.ParsingError(ErrorMaker.NoAssignmentWhenDefining(lineNumber, line.Length + 1));
            else if (stringTokens[4].Trim().Length != 1 || stringTokens[4].Trim()[0] != equalSign)
                return ParsingResult.ParsingError(ErrorMaker.NoAssignmentWhenDefining(lineNumber, line.IndexOf(stringTokens[4])));
            else if (stringTokens.Length < 6)
                return ParsingResult.ParsingError(ErrorMaker.NoValueReceived(lineNumber, line.Length + 1));
            else
            {
                VariableType type = _lanModel.GetVariableTypeFromString(stringTokens[3]);
                if (IsFunctionCall(stringTokens[5]))
                {
                    var tokens = string.Join(" ", stringTokens[5..]);
                    throw new NotImplementedException();
                }
                if (!IsValueTypeOf(stringTokens[5], type))
                    return ParsingResult.ParsingError(ErrorMaker.UnableToParse(stringTokens[5], type, lineNumber, line.IndexOf(stringTokens[5])));

                AbstractExecutable executable = new(
                        BasicCommandStorage.VariableDefinition(stringTokens[1],
                        GetSl_ValueFromString(stringTokens[5], type),
                        lineNumber),
                    ExecutableType.Instruction);
                return ParsingResult.Success(executable);
            }
        }
        //BasicCommandWithArgs

        private ParsingResult ParseFunctionCall(string funcCallStr, int lineNumber)
        {
            if (Regex.Matches(funcCallStr, _lanModel.FuncCallWord).Count > 1)
                ParsingResult.ParsingError(ErrorMaker.ParameterFunctionCall(lineNumber));


            string funcCallPattern = $"{Regex.Escape(_lanModel.FuncCallWord)}\\s*(\\w+)\\s*\\(([^)]*)\\)";

            Match match = Regex.Match(funcCallStr, funcCallPattern);
            if (!match.Success)
                return ParsingResult.ParsingError(ErrorMaker.InvalidFunctionCallFormat(lineNumber));

            string functionName = match.Groups[1].Value;
            Function? func = _funcStorage.GetFunction(functionName);
            if (func is null)
                return ParsingResult.ParsingError(ErrorMaker.UndefinedFunctionAccessing(functionName, lineNumber));

            string argsStr = match.Groups[2].Value;
            List<string> variableNames = argsStr.Split(',')
                                       .Select(arg => arg.Trim())
                                       .Where(arg => !string.IsNullOrEmpty(arg))
                                       .ToList();
            List<Isl_TypeValue> argumentValues = new();
            foreach (string variable in variableNames)
            {
                if (!VariablesStorage.ContainsVariable(variable))
                    return ParsingResult.ParsingError(ErrorMaker.UndefinedVariableAccessing(variable, lineNumber));
                argumentValues.Add(VariablesStorage.GetVariable(variable, lineNumber));
            }
            var cmnds = func.ParseToInstructions(argumentValues).Commands;
            return ParsingResult.Success(new AbstractExecutable(cmnds, ExecutableType.InstructionsBlock));
        }
    }
}