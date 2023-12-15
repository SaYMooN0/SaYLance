using SaYLance.interfaces;
using SaYLance.results;
using SaYLance.std_lib;

namespace SaYLance.executable
{
    public class Condition : IExecutable
    {
        public List<BasicCommandWithArgs> Commands => throw new NotImplementedException();

        public ExecutionResult Execute()
        {
            throw new NotImplementedException();
        }
        static public Condition FromAbstract(AbstractExecutable abstractEx)
        {
            throw new NotImplementedException();
        }
    }
}
