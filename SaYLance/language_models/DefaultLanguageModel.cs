using SaYLance.errors_related;
using SaYLance.interfaces;

namespace SaYLance.language_models
{
    internal class DefaultLanguageModel : ILanguageModel
    {
        public string ErrToStr(Error error)
        {
            return $"{error.ErrorCode} at {error.Line}:{error.Character}\n{error.Message}";
        }
    }
}
