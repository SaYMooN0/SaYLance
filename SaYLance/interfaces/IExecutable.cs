using SaYLance.results;
using SaYLance.std_lib;

namespace SaYLance.interfaces
{
    public interface IExecutable
    {
        public ExecutionResult Execute();
        public List<BasicCommandWithArgs> Commands { get; }
    }
}
