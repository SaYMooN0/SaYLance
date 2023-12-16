using SaYLance.errors_related;
using SaYLance.interfaces;
using SaYLance.results;
using SaYLance.std_lib;

namespace SaYLance.executable
{
    public class InstructionsBlock : IExecutable
    {
        private InstructionsBlock(List<BasicCommandWithArgs> commands) { Commands = commands; }

        public List<BasicCommandWithArgs> Commands { get; private set; }
        public ExecutionResult Execute()
        {
                Isl_TypeValue? result = null;
                foreach (BasicCommandWithArgs cmnd in Commands)
                {
                    result = cmnd.Run();
                }
                return ExecutionResult.Success(result);
        }
        static public InstructionsBlock FromAbstract(AbstractExecutable abstractEx)=> new InstructionsBlock(abstractEx.Commands);
    }
}
