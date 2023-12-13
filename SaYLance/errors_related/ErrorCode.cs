namespace SaYLance.errors_related
{
    public enum ErrorCode
    {
        NoHeaderFound,
        UnknownLanguageModel,
        NoTokens,
        ExpectedVariableName,
        InvalidVariableName,
        ExpectedVariableNameAndTypeDivider,
        ExpectedTypeName,
        NoAssignmentWhenDefining,
        AssignmentExpected,
        InvalidReceivedType,
        UnknownType,
        NoValueReceived
    }
}
