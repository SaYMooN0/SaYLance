namespace SaYLance.variable_types
{
    internal class sl_String
    {
        public string Value { get; set; }

        private sl_String(string value) { Value = value; }

        public static bool FromString(string input, out sl_String result)
        {
            result = null;
            if (string.IsNullOrEmpty(input) || input.Length < 3)
                return false;
            bool isValidString = (input.StartsWith("\"") && input.EndsWith("\"") && input.Count(c => c == '"') == 2) ||
                                 (input[0] == '\'' && input.Last() == '\'' && input.Count(c => c == '\'') == 2);

            if (isValidString)
            {
                string processedString = input.Substring(1, input.Length - 2);
                result = new sl_String(processedString);
                return true;
            }

            return false;
        }

        public override string ToString() { return $"\"{Value}\""; }
    }
}
