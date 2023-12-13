namespace SaYLance.variable_types
{
    internal class sl_Int
    {
        public int Value { get; set; }

        public sl_Int(int value)
        {
            Value = value;
        }

        public static bool TryCreateFromString(string value, out sl_Int result)
        {
            if (int.TryParse(value, out int intValue))
            {
                result = new sl_Int(intValue);
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
