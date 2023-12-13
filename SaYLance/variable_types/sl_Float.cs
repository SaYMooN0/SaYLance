using SaYLance.interfaces;

namespace SaYLance.variable_types
{
    public class sl_Float : Isl_TypeValue
    {
        private float Value { get; set; }
        public object GetValue() => Value;
        public sl_Float(float value) { Value = value; }
        public static bool IsValidFormat(string input) => float.TryParse(input, out _);
        bool Isl_TypeValue.IsValidFormat(string input) => sl_String.IsValidFormat(input);
        public static bool TryCreateFromString(string value, out sl_Float result)
        {
            if (IsValidFormat(value))
            {
                result = new sl_Float(float.Parse(value));
                return true;
            }
            result = null;
            return false;
        }

        public static bool TryCreateFromInt(int value, out sl_Float result)
        {
            result = new sl_Float(value);
            return true;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
