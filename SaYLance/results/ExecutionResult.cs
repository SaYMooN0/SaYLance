using SaYLance.errors_related;
using SaYLance.interfaces;

namespace SaYLance.results
{
    public class ExecutionResult:IResult
    {
        public Isl_TypeValue? ReturnValue { get; private set; }
        public Error? Error { get; set; }
        public bool IsSuccess { get; set; }
        private ExecutionResult(Isl_TypeValue returnValue, Error? error, bool isSuccess)
        {
            ReturnValue = returnValue;
            Error = error;
            IsSuccess = isSuccess;
        }
        public static ExecutionResult ExecutionError(Error error) => new ExecutionResult(null, error, false);
        public static ExecutionResult Success(Isl_TypeValue returnValue) => new ExecutionResult(returnValue, null, true);
    }
}
