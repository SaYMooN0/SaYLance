
using SaYLance.variable_types;

namespace SaYLance.interfaces
{
    public interface ILanguageModel
    {
        public string ErrToStr(errors_related.Error err);
        public string VariableDefinitionWord { get; }
        public char VariableNameAndTypeDivider { get; }
        public string InstructionBlockOpener { get; }
        public string InstructionBlockCloser { get; }
        public string BoolTypeKeyWord { get; }
        public string FloatTypeKeyWord { get; }
        public string IntTypeKeyWord { get; }
        public string StringTypeKeyWord { get; }
        public bool IsTypeKeyword(string str)
        {
            return new string[] { 
                BoolTypeKeyWord, FloatTypeKeyWord,IntTypeKeyWord,StringTypeKeyWord
            }.Contains(str);
        }
        public VariableType GetVariableTypeFromString(string type)
        {
            if (String.IsNullOrWhiteSpace(type)) return VariableType.Null;
            if (type.Trim() == BoolTypeKeyWord) return VariableType.Bool;
            if (type.Trim() == FloatTypeKeyWord) return VariableType.Float;
            if (type.Trim() == IntTypeKeyWord) return VariableType.Int;
            if (type.Trim() == StringTypeKeyWord) return VariableType.String;
            return VariableType.Null;
        }

    }


}
