using SaYLance.executable;
using SaYLance.results;
using SaYLance.parsing_components;
using SaYLance.variable_types;
using SaYLance.errors_related;
using SaYLance.function_related;
using SaYLance.interfaces;
using SaYLance.std_lib;

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
                case ExecutableType.Instruction:
                    {
                        
                        return InstructionExecution(Instruction.FromAbstract(executable));
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
        static private ExecutionResult InstructionExecution(Instruction instruction)
        {
            try
            {
                return instruction.Execute();
            }
            catch (ExecutionException ex)
            {
                return ExecutionResult.ExecutionError(
                    ex.error ?? new Error(ErrorCode.NoCodeError, ex.Message)
                );
            }
        }
        static private ExecutionResult InstructionsBlockExecution(InstructionsBlock block)
        {
            try
            {
                return block.Execute();
            }
            catch (ExecutionException ex)
            {
                return ExecutionResult.ExecutionError(ex.error);
            }
            catch (Exception ex)
            {
                return ExecutionResult.ExecutionError(new Error(ErrorCode.NoCodeError, ex.Message));
            }
        }

        static private ExecutionResult LoopExecution(Loop loop)
        {
            throw new NotImplementedException();
        }
    }
}
