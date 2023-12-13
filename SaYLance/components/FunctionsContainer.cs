using SaYLance.executable;
using System.Collections.Generic;

namespace SaYLance.components
{
    public class FunctionsContainer
    {
        private Dictionary<string, Function> _funcs = new();

        public void AddFunction(string name, Function function) { _funcs.Add(name, function); }

        public bool FunctionExists(string name)
        {
            return _funcs.ContainsKey(name);
        }

        public Function? GetFunction(string name)
        {
            if (_funcs.TryGetValue(name, out Function function))
                return function;
            return null;
        }
    }
}
