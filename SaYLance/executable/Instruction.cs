using SaYLance.errors_related;
using SaYLance.interfaces;
using SaYLance.results;
using SaYLance.std_lib;

namespace SaYLance.executable
{
    public class Instruction : IExecutable
    {
        public List<BasicCommandWithArgs> Functions { get; set; }

        public ExecutionResult Execute()
        {
            if (Functions is null || Functions.Count < 1)
                throw new ArgumentException("Undefined functions");
            else if (Functions.Count > 1)
                throw new ArgumentException("To many functions for one instruction");
            Isl_TypeValue? result = Functions[0].Run();
            return ExecutionResult.Success(result);
        }
        static public Instruction FromAbstract(AbstractExecutable abstractEx)
        {
            return new Instruction() { Functions = abstractEx.Functions };
        }
    }
}
