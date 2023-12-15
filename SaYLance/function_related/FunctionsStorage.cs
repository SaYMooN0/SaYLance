﻿using SaYLance.executable;
using SaYLance.interfaces;
using SaYLance.std_lib;
using SaYLance.variable_types;

namespace SaYLance.function_related
{
    public class FunctionsStorage
    {
        private static IDefaultTextIO textIO;
        private Dictionary<string, Function> _funcs;

        public FunctionsStorage(IDefaultTextIO textInputOutput, string writeIOFuncName, string readIOFuncName)
        {
            textIO = textInputOutput;
            _funcs = new Dictionary<string, Function>
            {
                { writeIOFuncName, new Function(new BasicCommand(-1, WriteIntoDefaultTextIO), -1) }
            };
        }
        public void AddFunction(string name, Function function) { _funcs.Add(name, function); }

        public bool FunctionExists(string name) => _funcs.ContainsKey(name);

        public Function? GetFunction(string name)
        {
            if (_funcs.TryGetValue(name, out Function function))
                return function;
            return null;
        }
        private Func<List<Isl_TypeValue>, Isl_TypeValue> WriteIntoDefaultTextIO = (List<Isl_TypeValue> values) =>
        {
            if (values.Count < 2)
                throw new Exception("not enough args for WriteIntoDefaultTextIO");
            int lineNumber = (int)values[0].GetValue();
            values.RemoveAt(0);
            textIO.Log(string.Join(", ", values));
            return new sl_Void();
        };
        private BasicCommandWithArgs ReadStringFromDefaultTextIO(int lineNumber) => new BasicCommandWithArgs(
            new BasicCommand(-1, args =>
            {
                return new sl_String(
                    Task.Run(async () => await textIO.StringInputAsync()).Result
                    );
            }),
            BasicCommandWithArgs.ArgsList(new sl_Int(lineNumber))
        );
    }
}
