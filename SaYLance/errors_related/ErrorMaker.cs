using SaYLance.errors_related;

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

    public static Error InvalidReceivedType(string typeName, int line, int character) =>
        new Error(ErrorCode.InvalidReceivedType, $"Invalid received type: {typeName}", line, character);
    public static Error InvalidVariableName(string variableName, int line, int character) =>
       new Error(ErrorCode.InvalidVariableName, $"Invalid variable name: '{variableName}'", line, character);
    public static Error UnknownType(string type, int line, int character) =>
       new Error(ErrorCode.UnknownType, $"Received unknown type name: '{type}'", line, character);
    public static Error NoValueReceived(int line, int character) =>
       new Error(ErrorCode.UnknownType, $"Expected value. No value received", line, character);
}