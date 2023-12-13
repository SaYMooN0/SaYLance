using SaYLance.interfaces;
using SaYLance.parsing_components;

namespace SaYLance.executable
{
    public class Instruction : IExecutable
    {
        public readonly Token[]? Tokens;
        public readonly TokenSequenceType Type;
        public readonly TokenSequenceType ExpectedSequenceType;

        public Isl_TypeValue Execute(Dictionary<string, Isl_TypeValue> arguments)
        {
            throw new NotImplementedException();
        }
    }
}
