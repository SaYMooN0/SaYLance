using SaYLance.interfaces;

namespace SaYLance.components
{
    internal class Parser
    {
        private ILanguageModel _lm;

        public Parser(ILanguageModel languageMode)
        {
            _lm = languageMode;
        }
    }
}
