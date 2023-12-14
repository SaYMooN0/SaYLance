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
        public List<BasicCommandWithArgs> Functions { get; set; }
        public readonly ExecutableType Type;
        public AbstractExecutable(List<BasicCommandWithArgs> functions, ExecutableType type)
        {
            Functions = functions;
            Type = type;
        }
        public AbstractExecutable(BasicCommandWithArgs function, ExecutableType type)
        {
            Functions = new() { function };
            Type = type;
        }
        public ExecutionResult Execute() { return ExecutionResult.Success(new sl_Void()); }
        static public AbstractExecutable MeaningLess() => new AbstractExecutable(function: null, ExecutableType.Abstract);

    }
}