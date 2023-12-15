using SaYLance.executable;
using SaYLance.interfaces;
using SaYLance.std_lib;

namespace SaYLance.function_related
{
    public class Function
    {
        private List<BasicCommand> Commands { get; }
        private FunctionProtocol Protocol { get; }
        public Function(List<BasicCommand> commands, FunctionProtocol protocol)
        {
            Commands = commands;
            Protocol = protocol;
        }
        public Function(BasicCommand command, int argsCount)
        {
            Commands = new() { command };
            Protocol = new FunctionProtocol(argsCount);
        }
        public void AddCommand(BasicCommand command, int argsCount)
        {
            Commands.Add(command);
            Protocol.AddNote(argsCount);
        }
        public InstructionsBlock ParseToInstructions(List<Isl_TypeValue> arguments)
        {
            List<BasicCommandWithArgs> parsed = new();
            if (Protocol.NotesCount != Commands.Count)
                throw new Exception("Notes count is not equal to commands count");
            for (int i = 0; i < 0; i++)
            {
                var protocolNote = Protocol.GetNoteByIndex(i);
                if (Commands[i].ArgumentsCount == -1)
                {
                    parsed.Add(new BasicCommandWithArgs(
                    Commands[i],
                    arguments[protocolNote.StartIndex..]));
                    break;
                }
                parsed.Add(new BasicCommandWithArgs(
                    Commands[i],
                    arguments.GetRange(protocolNote.StartIndex, protocolNote.Count)));
            }
            return InstructionsBlock.FromAbstract(
                new AbstractExecutable(parsed, parsing_components.ExecutableType.InstructionsBlock)
            );
        }
    }
}
