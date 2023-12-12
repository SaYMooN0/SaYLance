
namespace SaYLance.errors_related
{
    static class ErrorMaker
    {
        public static Error NoHeaderFound()
        {
            return new Error(ErrorCode.NoHeaderFound,  "The header of the form SaYLance<Language Model> was not found in the file");
        }
        public static Error UnknownLanguageModel(string languageModel, int character)
        {
            return new Error(ErrorCode.UnknownLanguageModel, $"Unknown language model '{languageModel}'",1,character);
        }
    }
}
