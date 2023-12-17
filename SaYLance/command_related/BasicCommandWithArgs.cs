using SaYLance.interfaces;

namespace SaYLance.std_lib
{
    public class BasicCommandWithArgs
    {
        public BasicCommandWithArgs(BasicCommand command, List<Isl_TypeValue> args)
        {
            CommandsWithArgs = new Tuple<BasicCommand, List<Isl_TypeValue>>(command, args);
        }
        public BasicCommandWithArgs(BasicCommand command, Isl_TypeValue argument)
        {
            CommandsWithArgs = new Tuple<BasicCommand, List<Isl_TypeValue>>(command, new List<Isl_TypeValue>() { argument });
        }
        public BasicCommandWithArgs(BasicCommand command)
        {
            CommandsWithArgs = new Tuple<BasicCommand, List<Isl_TypeValue>>(command, new List<Isl_TypeValue>());
        }
        private Tuple<BasicCommand, List<Isl_TypeValue>> CommandsWithArgs { get; set; }
        private List<Isl_TypeValue> Arguments => CommandsWithArgs.Item2;
        public BasicCommand Command => CommandsWithArgs.Item1;
        public Isl_TypeValue Run()
        {
            if (Arguments.Count != Command.ArgumentsCount && Command.ArgumentsCount!=-1)
                throw new ArgumentException("incorrect arguments count in BasicCommandWithArgs");
            return Command.Run(Arguments);
        }
        public static List<Isl_TypeValue> ArgsList(params Isl_TypeValue[] arguments) => new List<Isl_TypeValue>(arguments);


    }
}
