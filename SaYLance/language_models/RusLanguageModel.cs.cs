using SaYLance.errors_related;

namespace SaYLance.language_models
{
    public class CustomLanguageModel : LanguageModel
    {
        private Func<Error, string> _errToStrFunc;
        public CustomLanguageModel(Func<Error, string> errToStrFunc = null, params KeyValuePair<string, string>[] keyWordPairs)
        {
            _errToStrFunc = errToStrFunc ?? base.ErrToStr;

            foreach (var pair in keyWordPairs)
            {
                if (!KeyWords.ContainsKey(pair.Key))
                    throw new ArgumentException($"Key '{pair.Key}' does not exist in the base KeyWords dictionary.");

                KeyWords[pair.Key] = pair.Value;
            }
        }

        public new string ErrToStr(Error error)
        {
            return _errToStrFunc(error);
        }
        public static CustomLanguageModel DefaultRusLanModel()
        {
            var rusKeyWordPairs = new KeyValuePair<string, string>[]
            {
        new KeyValuePair<string, string>("varDefWord", "пусть"),
        new KeyValuePair<string, string>("bool", "логический"),
        new KeyValuePair<string, string>("float", "вещественный"),
        new KeyValuePair<string, string>("int", "целочисленный"),
        new KeyValuePair<string, string>("string", "строчный"),
            };

            return new CustomLanguageModel(
                error => $"{error.ErrorCode} в строке {error.Line} на символе {error.Character}\n{error.Message}",
                rusKeyWordPairs
            );
        }

    }
}
