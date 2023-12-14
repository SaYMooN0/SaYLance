using SaYLance.errors_related;

namespace SaYLance.interfaces
{
    public interface IResult
    {
        public Error? Error { get;  set; }
        public bool IsSuccess { get; set; }
    }
}
