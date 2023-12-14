
namespace SaYLance.errors_related
{
    public class ExecutionException : Exception
    {
        public readonly Error? error;
        public ExecutionException() : base("An error occurred during execution.") { }
        public ExecutionException(Error error) { this.error = error; }
    }
}
