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
        private AbstractExecutable(List<BasicCommandWithArgs> functions, ExecutableType type)
        {
            Functions = functions;
            Type = type;
        }
        public ExecutionResult Execute()
        {
            throw new NotImplementedException("Can not execute Abstract Executable");
        }
        static public AbstractExecutable MeaningLess() => new AbstractExecutable(null, ExecutableType.Abstract);
        static public AbstractExecutable VariableDefining(string variableName, Isl_TypeValue value)
        {
            List<BasicCommandWithArgs> instruction = new List<BasicCommandWithArgs> {
                new BasicCommandWithArgs(
                    VariablesStorage.DefineNew,
                    BasicCommandWithArgs.ArgsList( new sl_String(variableName), value)
                )
            };
            return new AbstractExecutable(instruction, ExecutableType.Instruction);
        }

    }
}