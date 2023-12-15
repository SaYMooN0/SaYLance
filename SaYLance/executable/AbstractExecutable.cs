using SaYLance.components;
using SaYLance.interfaces;
using SaYLance.parsing_components;
using SaYLance.results;
using SaYLance.std_lib;
using SaYLance.variable_types;
using System.Collections.Generic;

namespace SaYLance.executable
{
    public class AbstractExecutable : IExecutable
    {
        public List<BasicCommandWithArgs> Commands { get; set; }
        public readonly ExecutableType Type;
        public AbstractExecutable(List<BasicCommandWithArgs> commands, ExecutableType type)
        {
            Commands = commands;
            Type = type;
        }
        public AbstractExecutable(BasicCommandWithArgs commands, ExecutableType type)
        {
            Commands = new() { commands };
            Type = type;
        }
        public ExecutionResult Execute() { return ExecutionResult.Success(new sl_Void()); }
        static public AbstractExecutable MeaningLess() => new AbstractExecutable(commands: (BasicCommandWithArgs?)null, ExecutableType.Abstract);

    }
}