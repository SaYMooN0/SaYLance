﻿namespace SaYLance.errors_related
{
    public enum ErrorCode
    {
        NoCodeError,

        UnableToParse, 

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
        NoValueReceived,

        DefinedVariableDefining,
        UndefinedVariableDeleting,
        UndefinedVariableAccessing,
        UndefinedFunctionAccessing,

        FunctionNameExpected,
        ParameterFunctionCall,
        InvalidFunctionCallFormat,

        UndefinedVariable
    }
}
