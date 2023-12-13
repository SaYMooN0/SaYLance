using SaYLance.interfaces;

namespace SaYLance.executable
{
    internal class Loop : IExecutable
    {
     
        private InstructionsBlock _instructions;
        private Condition _condition;
        public Loop(InstructionsBlock instructions, Condition condition)
        {
            _instructions = instructions;
            _condition = condition;
        }

        public Isl_TypeValue Execute(Dictionary<string, Isl_TypeValue> arguments)
        {
            throw new NotImplementedException();
        }
    }
}
