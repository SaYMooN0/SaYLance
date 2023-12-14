using SaYLance.interfaces;

namespace SaYLance.std_lib
{
    public static class BasicCommandStorage
    {
        static private Dictionary<string, BasicCommand> _cmnds = new(){
            { "VariableDefinition", VariableDefinition() }
        };

        static public Isl_TypeValue InvokeCommand(string name, List<Isl_TypeValue> args)
        {
            if (_cmnds.TryGetValue(name, out BasicCommand command))
                return command.Run(args);
            return null;
        }
        static private BasicCommand VariableDefinition()
        {
            Func<List<Isl_TypeValue>, Isl_TypeValue> command = null;
            return new BasicCommand(2, command);
        }
    }
}
