using SaYLance.interfaces;

namespace SaYLance.variable_types
{
    public class sl_Void : Isl_TypeValue
    {
        public object GetValue() => null;

        public bool IsValidFormat(string input)
        {
            throw new NotImplementedException("Can not validate format for void");
        }
    }
}
