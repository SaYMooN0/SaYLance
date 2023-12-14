using System.Linq;
using SaYLance.interfaces;

namespace SaYLance.variable_types
{
    internal class sl_String : Isl_TypeValue
    {
        public string Value { get; set; }

        public sl_String(string value) { Value = value; }

        public object GetValue() => Value;
        public static bool IsValidFormat(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length < 3)
                return false;

            return (input.StartsWith("\"") && input.EndsWith("\"") && input.Count(c => c == '"') == 2) ||
                   (input.StartsWith("'") && input.EndsWith("'") && input.Count(c => c == '\'') == 2);
        }
        bool Isl_TypeValue.IsValidFormat(string input) => sl_String.IsValidFormat(input);

        public static bool TryCreateFromString(string input, out sl_String result)
        {
            result = null;
            if (IsValidFormat(input))
            {
                string processedString = input.Substring(1, input.Length - 2);
                result = new sl_String(processedString);
                return true;
            }

            return false;
        }
        public override string ToString()
        {
            return Value;
        }
        public string ToStringWithQuotes() => $"\"{Value}\"";


    }
}
