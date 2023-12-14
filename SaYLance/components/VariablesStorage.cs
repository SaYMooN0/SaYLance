using SaYLance.interfaces;
using SaYLance.std_lib;
using SaYLance.variable_types;
using System;
using System.Collections.Generic;

namespace SaYLance.components
{
    public static class VariablesStorage
    {
        private static Dictionary<string, Isl_TypeValue> _vars = new();
        //(string name) -> bool
        public static BasicCommand ContainsVariable = new BasicCommand(1, args =>
        {
            if (args.Count != 1 || args[0] is null)
                throw new ArgumentException("Invalid arguments for ContainsVariable");
            return new sl_Bool(_ContainsVariable(args[0].ToString()));
        });

        //(string name, Isl_TypeValue value) -> void
        public static BasicCommand DefineNew = new BasicCommand(2, args =>
        {
            if (args.Count != 2 || args[0] is null || args[1] is null)
                throw new ArgumentException("Invalid arguments for DefineNew");
            _DefineNew(args[0].ToString(), args[1]);
            return new sl_Void();
        });

        //(string name) -> void
        public static BasicCommand DeleteVariable = new BasicCommand(1, args =>
        {
            if (args.Count != 1 || args[0] is null)
                throw new ArgumentException("Invalid arguments for DeleteVariable");
            _DeleteVariable(args[0].ToString());
            return new sl_Void();
        });

        //(string name) -> Isl_TypeValue
        public static BasicCommand GetVariable = new BasicCommand(1, args =>
        {
            if (args.Count != 1 || args[0] is null)
                throw new ArgumentException("Invalid arguments for GetVariable");
            return _GetVariable(args[0].ToString());
        });
        private static bool _ContainsVariable(string name) =>_vars.ContainsKey(name); 

        private static void _DefineNew(string name, Isl_TypeValue value)
        {
            if (!_vars.ContainsKey(name))
                _vars.Add(name, value);
            else
                throw new ArgumentException($"Variable with name {name} already exists.");
        }

        private static void _DeleteVariable(string name) { _vars.Remove(name); }

        private static Isl_TypeValue _GetVariable(string name)
        {
            if (_vars.TryGetValue(name, out Isl_TypeValue value))
                return value;
            else
                throw new KeyNotFoundException($"Variable with name {name} does not exist.");
        }
    }
}
