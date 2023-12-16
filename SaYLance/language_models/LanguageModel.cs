using SaYLance.errors_related;
using SaYLance.variable_types;

namespace SaYLance.language_models
{
    public class LanguageModel
    {
        protected Dictionary<string, string> KeyWords = new Dictionary<string, string>
        {
            { "varDefWord", "let" },
            { "varNameTypeDivider", ":" },
            { "funcCall", "#" },
            { "blockOpen", "{" },
            { "blockClose", "}" },
            { "bool", "bool" },
            { "float", "float" },
            { "int", "int" },
            { "string", "string" },
            { "printFunc", "print" },
            { "readLineFunc", "readLine" },
        };
        public string FuncCallWord => GetKeyWordFor("funcCall");
        public string GetKeyWordFor(string keyWord) => KeyWords[keyWord];
        public bool IsStringKeyWord(string str)=>KeyWords.Keys.Contains(str) || KeyWords.Values.Contains(str);
        public string ErrToStr(Error error) { return $"{error.ErrorCode} at {error.Line}:{error.Character}\n{error.Message}"; }
        public bool IsTypeKeyword(string str)
        {
            return new string[] {
                KeyWords["int"],
                KeyWords["float"],
                KeyWords["bool"],
                KeyWords["string"]
            }.Contains(str);
        }
        public VariableType GetVariableTypeFromString(string type)
        {
            if (String.IsNullOrWhiteSpace(type)) return VariableType.Void;
            if (type.Trim() == KeyWords["bool"]) return VariableType.Bool;
            if (type.Trim() == KeyWords["float"]) return VariableType.Float;
            if (type.Trim() == KeyWords["int"]) return VariableType.Int;
            if (type.Trim() == KeyWords["string"]) return VariableType.String;
            return VariableType.Void;
        }
    }
}
