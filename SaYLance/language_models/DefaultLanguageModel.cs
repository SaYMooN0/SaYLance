using SaYLance.errors_related;
using SaYLance.interfaces;

namespace SaYLance.language_models
{
    internal class DefaultLanguageModel : ILanguageModel
    {
        public string VariableDefinitionWord { get; } = "let";
        public char VariableNameAndTypeDivider { get; } = ':';
        public string InstructionBlockOpener { get; } = "{";
        public string InstructionBlockCloser { get; } = "}";

        public string ErrToStr(Error error)
        {
            return $"{error.ErrorCode} at {error.Line}:{error.Character}\n{error.Message}";
        }
    }
}
