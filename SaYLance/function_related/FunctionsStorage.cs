using SaYLance.errors_related;
using SaYLance.executable;
using SaYLance.interfaces;
using SaYLance.language_models;
using SaYLance.std_lib;
using SaYLance.variable_types;

namespace SaYLance.function_related
{
    public class FunctionsStorage
    {
        private static IDefaultTextIO textIO;
        private Dictionary<string, Function> _funcs;

        public FunctionsStorage(IDefaultTextIO textInputOutput, LanguageModel languageModel)
        {
            textIO = textInputOutput;
            _funcs = new Dictionary<string, Function>
            {
                { languageModel.GetKeyWordFor("printFunc"), new Function(new BasicCommand(-1, WriteIntoDefaultTextIO)) },
                { languageModel.GetKeyWordFor("readLineFunc"), new Function(new BasicCommand(0, ReadStringFromDefaultTextIO)) },
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
            if (values.Count < 1)
                throw new Exception("not enough args for WriteIntoDefaultTextIO");
            textIO.Log(string.Join(", ", values));
            return new sl_Void();
        };
        private Func<List<Isl_TypeValue>, Isl_TypeValue> ReadStringFromDefaultTextIO = (List<Isl_TypeValue> values) =>
        {
            return new sl_String(Task.Run(async () =>
                await textIO.StringInputAsync()).Result
            );
        };
    }
}
