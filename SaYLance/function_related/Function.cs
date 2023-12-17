using SaYLance.executable;
using SaYLance.interfaces;
using SaYLance.parsing_components;
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
        public Function(BasicCommand command)
        {
            Commands = new() { command };
            Protocol = new FunctionProtocol(command.ArgumentsCount);
        }
        public void AddCommand(BasicCommand command)
        {
            Commands.Add(command);
            Protocol.AddNote(command.ArgumentsCount);
        }
        public InstructionsBlock ParseToInstructions(List<Isl_TypeValue> arguments)
        {
     
            if (Commands.Count == 1 && (arguments is null || arguments.Count == 0))
            {
                return InstructionsBlock.FromAbstract(
                  new AbstractExecutable(new BasicCommandWithArgs(Commands[0]),
                  ExecutableType.InstructionsBlock));
            }
            List<BasicCommandWithArgs> parsed = new();
            if (Protocol.NotesCount != Commands.Count)
                throw new Exception("Notes count is not equal to commands count");
            for (int i = 0; i < arguments.Count; i++)
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
