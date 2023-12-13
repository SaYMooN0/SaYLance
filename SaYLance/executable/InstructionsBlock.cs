using SaYLance.interfaces;

namespace SaYLance.executable
{
    internal class InstructionsBlock : IExecutable
    {
        private List<Isl_TypeValue> InnerVariables { get; set; } = new List<Isl_TypeValue>();
        private LinkedList<Instruction> Instructions { get; set; } = new();
        public InstructionsBlock(LinkedList<Instruction> instructions)
        {
            Instructions = instructions;
        }
        public Isl_TypeValue Execute(Dictionary<string, Isl_TypeValue> arguments)
        {
            throw new NotImplementedException();
        }
    }
}
