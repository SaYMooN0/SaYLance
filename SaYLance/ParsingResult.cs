
using SaYLance.errors_related;
using SaYLance.parsing_components;

namespace SaYLance
{
    public class ParsingResult
    {
        public readonly TokenSequence? TokenSequence;
        public readonly Error? Error;
        public readonly bool IsSuccess;
        private ParsingResult(TokenSequence? tokenSequence, Error? error, bool isSuccess)
        {
            TokenSequence = tokenSequence;
            Error = error;
            IsSuccess = isSuccess;
        }
        public static ParsingResult ParsingError(Error error) => new ParsingResult(TokenSequence.Meaningless(), error, false);
        public static ParsingResult Success(TokenSequence tokens) => new ParsingResult(tokens, null, false);

    }
}
