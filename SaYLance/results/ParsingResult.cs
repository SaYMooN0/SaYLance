using SaYLance.errors_related;
using SaYLance.executable;
using SaYLance.interfaces;

namespace SaYLance.results
{
    public class ParsingResult:IResult
    {
        public readonly AbstractExecutable? Executable;
        public Error? Error { get; set; }
        public bool IsSuccess { get; set; }
        private ParsingResult(AbstractExecutable? executable, Error? error, bool isSuccess)
        {
            this.Executable = executable;
            this.Error = error;
            IsSuccess = isSuccess;
        }

        public static ParsingResult ParsingError(Error error) => new ParsingResult(null, error, false);
        public static ParsingResult Success(AbstractExecutable? executable) => new ParsingResult(executable, null, true);

    }
}
