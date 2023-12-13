namespace SaYLance.parsing_components
{
    public class TokenSequence
    {
        public readonly Token[]? Tokens;
        public readonly TokenSequenceType Type;
        public readonly TokenSequenceType ExpectedSequenceType;
        public TokenSequence(Token[] tokens, TokenSequenceType type, TokenSequenceType expectedSequenceType)
        {
            Tokens = tokens;
            Type = type;
            ExpectedSequenceType = expectedSequenceType;
        }
        public static TokenSequence Meaningless() => new TokenSequence(null, TokenSequenceType.None, TokenSequenceType.None );
    }
}
