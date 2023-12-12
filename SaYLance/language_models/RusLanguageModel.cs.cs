using SaYLance.errors_related;
using SaYLance.interfaces;

namespace SaYLance.language_models
{
    internal class RusLanguageModel : ILanguageModel
    {
        public string VariableDefinitionWord { get; } = "пусть";
        public char VariableNameAndTypeDivider { get; } = ':';

        public string[] InstructionsDividers { get; } = { "\n", ";" };

        public string InstructionBlockOpener { get; } = "{";

        public string InstructionBlockCloser { get; } = "}";


        public string ErrToStr(Error error)
        {
            return $"{error.ErrorCode} at {error.Line}:{error.Character}\n{error.Message}";
        }
    }
}
