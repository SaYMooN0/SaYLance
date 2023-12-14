using SaYLance.executable;
using SaYLance.results;
using SaYLance.parsing_components;
using SaYLance.variable_types;

namespace SaYLance.components
{
    static class Executor
    {
        public static ExecutionResult Execute(AbstractExecutable executable)
        {
            switch (executable.Type)
            {
                case ExecutableType.Condition:
                    {
                        Condition condition = Condition.FromAbstract(executable);
                        return ConditionExecution(condition);
                    }
                case ExecutableType.Function:
                    {
                        Function function = Function.FromAbstract(executable);
                        return FunctionExecution(function);
                    }
                case ExecutableType.Instruction:
                    {
                        return Instruction.FromAbstract(executable).Execute();
                    }
                case ExecutableType.InstructionsBlock:
                    {
                        InstructionsBlock block = InstructionsBlock.FromAbstract(executable);
                        return InstructionsBlockExecution(block);
                    }
                case ExecutableType.Loop:
                    {
                        Loop loop = Loop.FromAbstract(executable);
                        return LoopExecution(loop);
                    }
                case ExecutableType.Abstract:
                    {
                        return ExecutionResult.Success(new sl_Void());
                    }
                default:
                    throw new NotImplementedException($"Execution not implemented for type {executable.Type}");
            }
        }

        static private ExecutionResult ConditionExecution(Condition condition)
        {
            throw new NotImplementedException();
        }

        static private ExecutionResult FunctionExecution(Function function)
        {
            throw new NotImplementedException();
        }

        static private ExecutionResult InstructionsBlockExecution(InstructionsBlock block)
        {
            throw new NotImplementedException();
        }

        static private ExecutionResult LoopExecution(Loop loop)
        {
            throw new NotImplementedException();
        }
    }
}
