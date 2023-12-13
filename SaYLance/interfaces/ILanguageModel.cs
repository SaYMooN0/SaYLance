
namespace SaYLance.interfaces
{
    public interface ILanguageModel
    {
        public string ErrToStr(errors_related.Error err);
        public string VariableDefinitionWord { get;}
        public char VariableNameAndTypeDivider { get; }
        public string InstructionBlockOpener { get; }
        public string InstructionBlockCloser { get; }

    }


}
