using SaYLance.errors_related;
using SaYLance.interfaces;
using SaYLance.variable_types;

namespace SaYLance.language_models
{
    internal class RusLanguageModel : ILanguageModel
    {
        public string VariableDefinitionWord { get; } = "пусть";
        public char VariableNameAndTypeDivider { get; } = ':';
        public string InstructionBlockOpener { get; } = "{";
        public string InstructionBlockCloser { get; } = "}";
        public string BoolTypeKeyWord { get; } = "логический";
        public string FloatTypeKeyWord { get; } = "вещественный";
        public string IntTypeKeyWord { get; } = "целочисленный";
        public string StringTypeKeyWord { get; } = "строчный";
        public string ErrToStr(Error error)
        {
            return $"{error.ErrorCode} в строке {error.Line} на символе {error.Character}\n{error.Message}";
        }
        public VariableType GetVariableTypeFromString(string type)
        {
            throw new NotImplementedException();
        }
    }
}
