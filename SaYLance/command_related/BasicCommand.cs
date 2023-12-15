using SaYLance.interfaces;

namespace SaYLance.std_lib
{
    public class BasicCommand
    {
        public readonly int ArgumentsCount;
        private Func<List<Isl_TypeValue>, Isl_TypeValue> Command;

        public BasicCommand(int argumentsCount, Func<List<Isl_TypeValue>, Isl_TypeValue> command)
        {
            ArgumentsCount = argumentsCount;
            Command = command;
        }
        public Isl_TypeValue Run(List<Isl_TypeValue> args)
        {
            if (args.Count != -1 && args.Count != ArgumentsCount)
                throw new ArgumentException("incorrect arguments count");
            return Command(args);
        }
    }
}
