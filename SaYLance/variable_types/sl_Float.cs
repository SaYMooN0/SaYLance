namespace SaYLance.variable_types
{
    public class sl_Float
    {
        public float Value { get; set; }

        public sl_Float(float value)
        {
            Value = value;
        }

        public static bool TryCreateFromInt(int value, out sl_Float result)
        {
            result = new sl_Float(value);
            return true;
        }

        public static bool TryCreateFromString(string value, out sl_Float result)
        {
            if (float.TryParse(value, out float floatValue))
            {
                result = new sl_Float(floatValue);
                return true;
            }
            result = null;
            return false;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
