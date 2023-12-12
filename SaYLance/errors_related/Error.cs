namespace SaYLance.errors_related
{
    public class Error
    {
        public readonly ErrorCode ErrorCode;
        public readonly int Line;
        public readonly int Character;
        public readonly string Message;
        public Error(ErrorCode errorCode, string message, int line, int character)
        {
            ErrorCode = errorCode;
            Line = line;
            Character = character;
            Message = message;
        }
        public Error(ErrorCode errorCode, string message) : this(errorCode, message,1,1) { }
        public override string ToString()
        {
            return $"{ErrorCode} at {Line}:{Character}\n{Message}";
        }
    }
}
