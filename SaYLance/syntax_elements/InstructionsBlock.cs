namespace SaYLance.syntax_elements
{
    internal class InstructionsBlock
    {
        private List<Variable> OutsideVariables { get; set; } = new();
        private List<Variable> InnerVariables { get; set; } = new();
        private LinkedList<Instruction> Instructions { get; set; } = new();
        public InstructionsBlock(List<Variable> outsideVariables, LinkedList<Instruction> instructions)
        {
            OutsideVariables = outsideVariables;
            Instructions = instructions;
        }
        public void Execute() { }

    }
}
