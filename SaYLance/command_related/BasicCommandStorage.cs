using SaYLance.components;
using SaYLance.interfaces;
using SaYLance.variable_types;

namespace SaYLance.std_lib
{
    public static class BasicCommandStorage
    {
        static public BasicCommandWithArgs VariableDefinition(string variableName, Isl_TypeValue value, int lineNumber) => new BasicCommandWithArgs(
                    VariablesStorage.DefineNew,
                    BasicCommandWithArgs.ArgsList(new sl_Int(lineNumber), new sl_String(variableName), value)
                );
        static public BasicCommandWithArgs CalculateExpression()
        {
            throw new NotImplementedException();
        }

    }
}
