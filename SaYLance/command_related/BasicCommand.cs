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
            if (ArgumentsCount == -1 || args.Count == ArgumentsCount)
                return Command(args);
            throw new ArgumentException("incorrect arguments count in BasicCommand");
          
        }
    }
}
