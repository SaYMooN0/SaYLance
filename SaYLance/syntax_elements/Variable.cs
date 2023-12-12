using System.Text.RegularExpressions;

namespace SaYLance.syntax_elements
{
    internal class Variable : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        static bool IsStringValidForName(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            var regex = new Regex(@"^[a-zA-Zа-яА-Я_][a-zA-Zа-яА-Я0-9_]*$");

            return regex.IsMatch(str);
        }
    }
}