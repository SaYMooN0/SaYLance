using SaYLance.errors_related;
using SaYLance.interfaces;
using SaYLance.results;
using SaYLance.std_lib;

namespace SaYLance.executable
{
    public class Instruction : IExecutable
    {
        private Instruction(List<BasicCommandWithArgs> commands){Commands = commands;}

        public List<BasicCommandWithArgs> Commands { get; set; }

        public ExecutionResult Execute()
        {
            if (Commands is null || Commands.Count < 1)
                throw new ArgumentException("Undefined commands");
            else if (Commands.Count > 1)
                throw new ArgumentException("To many commands for one instruction");
            Isl_TypeValue? result = Commands[0].Run();
            return ExecutionResult.Success(result);
        }
        static public Instruction FromAbstract(AbstractExecutable abstractEx) => new Instruction(abstractEx.Commands);

    }
}
