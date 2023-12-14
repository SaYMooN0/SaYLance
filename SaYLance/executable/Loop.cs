using SaYLance.interfaces;
using SaYLance.results;
using SaYLance.std_lib;

namespace SaYLance.executable
{
    internal class Loop : IExecutable
    {
     
        private InstructionsBlock _instructions;
        private Condition _condition;

        public List<BasicCommandWithArgs> Functions => throw new NotImplementedException();

        public ExecutionResult Execute()
        {
            throw new NotImplementedException();
        }
        static public Loop FromAbstract(AbstractExecutable abstractEx)
        {
            throw new NotImplementedException();
        }
    }
}
