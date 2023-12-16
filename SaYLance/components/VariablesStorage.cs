using SaYLance.errors_related;
using SaYLance.interfaces;
using SaYLance.std_lib;
using SaYLance.variable_types;

namespace SaYLance.components
{
    public static class VariablesStorage
    {
        private static Dictionary<string, Isl_TypeValue> _vars = new();
        public static void Dispose() { _vars.Clear(); }

        //(string name, Isl_TypeValue value, int lineNumber) -> void
        public static BasicCommand DefineNew = new BasicCommand(3, args =>
        {
            if (args.Count != 3 || args[0] is null || args[1] is null || args[2] is null)
                throw new ArgumentException("Invalid arguments for DefineNew");
            sl_Int line;
            if (!sl_Int.TryCreateFromString(args[0].ToString(), out line))
                throw new ArgumentException();
            _DefineNew(args[1].ToString(), args[2], line.Value);
            return new sl_Void();
        });

        //(string name, int lineNumber) -> void
        public static BasicCommand DeleteVariable = new BasicCommand(2, args =>
        {
            if (args.Count != 2 || args[0] is null || args[1] is null)
                throw new ArgumentException("Invalid arguments for DeleteVariable");
            sl_Int line;
            if (!sl_Int.TryCreateFromString(args[1].ToString(), out line))
                throw new ArgumentException();
            _DeleteVariable(args[0].ToString(), line.Value);
            return new sl_Void();
        });
        public static bool ContainsVariable(string name) => _vars.Keys.Contains(name);

        private static void _DefineNew(string name, Isl_TypeValue value, int lineNumber)
        {
            if (!_vars.ContainsKey(name))
                _vars.Add(name, value);
            else
                throw new ExecutionException(ErrorMaker.DefinedVariableDefining(name, lineNumber));
        }

        private static void _DeleteVariable(string name, int lineNumber)
        {
            if (!_vars.Remove(name))
                throw new ExecutionException(ErrorMaker.UndefinedVariableDeleting(name, lineNumber));
        }

        public static Isl_TypeValue GetVariable(string name, int lineNumber)
        {
            if (_vars.TryGetValue(name, out Isl_TypeValue value)) return value;
            else throw new ExecutionException(ErrorMaker.UndefinedVariableAccessing(name, lineNumber));
        }
        ////(string name, int lineNumber) -> Isl_TypeValue
        //public static BasicCommand GetVariable = new BasicCommand(2, args =>
        //{
        //    if (args.Count != 2 || args[0] is null || args[1] is null)
        //        throw new ArgumentException("Invalid arguments for GetVariable");
        //    sl_Int line;
        //    if (!sl_Int.TryCreateFromString(args[1].ToString(), out line))
        //        throw new ArgumentException();
        //    return _GetVariable(args[0].ToString(), line.Value);
        //});
        ////(string name) -> bool
        //public static BasicCommand ContainsVariable = new BasicCommand(1, args =>
        //{
        //    if (args.Count != 1 || args[0] is null)
        //        throw new ArgumentException("Invalid arguments for ContainsVariable");
        //    return new sl_Bool(_ContainsVariable(args[0].ToString()));
        //});
    }
}
