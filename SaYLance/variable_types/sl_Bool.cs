namespace SaYLance.variable_types
{
    public class sl_Bool
    {
        public bool Value { get; set; }

        public sl_Bool(bool value) { Value = value; }

        public static bool TryCreateFromInt(int value, out sl_Bool result)
        {
            result = new sl_Bool(value != 0);
            return true;
        }

        public static bool TryCreateFromString(string value, out sl_Bool result)
        {
            if (bool.TryParse(value, out bool boolValue))
            {
                result = new sl_Bool(boolValue);
                return true;
            }

            result = null;
            return false;
        }

        public override string ToString() { return Value.ToString(); }
    }
}
