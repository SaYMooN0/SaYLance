using SaYLance.errors_related;
using SaYLance.variable_types;

public static class ErrorMaker
{
    public static Error NoHeaderFound() =>
        new Error(ErrorCode.NoHeaderFound, "The header of the form SaYLance<Language Model> was not found in the file");

    public static Error UnknownLanguageModel(string languageModel, int character) =>
        new Error(ErrorCode.UnknownLanguageModel, $"Unknown language model '{languageModel}'", 1, character);

    public static Error NoTokens() =>
        new Error(ErrorCode.NoTokens, "No tokens found");

    public static Error IncorrectNumberOfTokens(int expected, int received) =>
        new Error(ErrorCode.NoTokens, $"Incorrect number of tokens. Expected: {expected}, received: {received}");

    public static Error ExpectedVariableName(int line, int character) =>
        new Error(ErrorCode.ExpectedVariableName, "Expected variable name", line, character);

    public static Error ExpectedVariableNameAndTypeDivider(int line, int character) =>
        new Error(ErrorCode.ExpectedVariableNameAndTypeDivider, "Expected variable name and type divider", line, character);

    public static Error ExpectedTypeName(int line, int character) =>
        new Error(ErrorCode.ExpectedTypeName, "Expected type name", line, character); 
    public static Error NoAssignmentWhenDefining(int line, int character) =>
        new Error(ErrorCode.AssignmentExpected, "It is impossible to create a variable without assigning it a value", line, character); 
    public static Error AssignmentExpected(int line, int character) =>
        new Error(ErrorCode.AssignmentExpected, "Assignment expected", line, character);
    public static Error InvalidVariableName(string variableName, int line, int character) =>
       new Error(ErrorCode.InvalidVariableName, $"Invalid variable name: '{variableName}'", line, character);
    public static Error UnknownType(string type, int line, int character) =>
       new Error(ErrorCode.UnknownType, $"Received unknown type name: '{type}'", line, character);
    public static Error NoValueReceived(int line, int character) =>
       new Error(ErrorCode.NoValueReceived, $"Expected value. No value received", line, character);
    public static Error UnableToParse(string value, VariableType expectedType,  int line, int character) =>
       new Error(ErrorCode.UnableToParse, $"Failed to parse {value} into {expectedType} type", line, character); 
    public static Error DefinedVariableDefining(string variableName, int line) =>
       new Error(ErrorCode.DefinedVariableDefining, $"variable '{variableName}' is already defined", line, 1);
    public static Error UndefinedVariableDeleting(string variableName, int line) =>
       new Error(ErrorCode.UndefinedVariableDeleting, $"Can not delete undefined variable '{variableName}'", line, 1);
    public static Error UndefinedVariableAccessing(string variableName, int line) =>
       new Error(ErrorCode.UndefinedVariableAccessing, $"Variable '{variableName}' is not defined", line, 1);
    public static Error UndefinedFunctionAccessing(string functionName, int line) =>
       new Error(ErrorCode.UndefinedVariableAccessing, $"Function '{functionName}' is not defined", line, 1);
    public static Error FunctionNameExpected(int line, int character) =>
       new Error(ErrorCode.FunctionNameExpected, $"Function name was expected", line, character);
    public static Error ParameterFunctionCall(int line) =>
       new Error(ErrorCode.ParameterFunctionCall, $"Function can't be called as a parameter. Define a variable, assign it a function value, and pass it as a parameter", line, 1);
    public static Error InvalidFunctionCallFormat(int line) =>
       new Error(ErrorCode.ParameterFunctionCall, $"Invalid function call format", line, 1);
}