using SaYLance.interfaces;

namespace SaYLance.variable_types
{
    internal class sl_Int : Isl_TypeValue
    {
        public int Value { get; set; }
        public object GetValue() => Value;
        public sl_Int(int value) { Value = value; }

        public static bool IsValidFormat(string input) => int.TryParse(input, out _);
        bool Isl_TypeValue.IsValidFormat(string input) => sl_String.IsValidFormat(input);
        public static bool TryCreateFromString(string value, out sl_Int result)
        {
            if (IsValidFormat(value))
            {
                result = new sl_Int(int.Parse(value));
                return true;
            }
            result = null;
            return false;
        }

        public static bool TryCreateFromFloat(float value, out sl_Int result)
        {
            if (value > int.MaxValue || value < int.MinValue || float.IsNaN(value) || float.IsInfinity(value))
            {
                result = null;
                return false;
            }
            result = new sl_Int((int)value);
            return true;
        }
        public override string ToString() { return Value.ToString(); }
    }
}
