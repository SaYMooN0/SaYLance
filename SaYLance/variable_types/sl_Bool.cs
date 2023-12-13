using SaYLance.interfaces;

namespace SaYLance.variable_types
{
    public class sl_Bool: Isl_TypeValue
    {
        public bool Value { get; set; }
        public object GetValue() => Value;

        public sl_Bool(bool value) { Value = value; }
        public static bool IsValidFormat(string input)=> bool.TryParse(input, out _);
        bool Isl_TypeValue.IsValidFormat(string input) => sl_String.IsValidFormat(input);

        public static bool TryCreateFromString(string value, out sl_Bool result)
        {
            if (IsValidFormat(value))
            {
                result = new sl_Bool(bool.Parse(value));
                return true;
            }
            result = null;
            return false;
        }
        public static bool TryCreateFromInt(int value, out sl_Bool result)
        {
            result = new sl_Bool(value != 0);
            return true;
        }
        public override string ToString() { return Value.ToString(); }
    }
}
