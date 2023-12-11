namespace SaYLance.syntax_elements
{
    internal class Loop
    {
     
        private InstructionsBlock _instructions;
        private Condition _condition;
        public Loop(InstructionsBlock instructions, Condition condition)
        {
            _instructions = instructions;
            _condition = condition;
        }
        public void Execute() { }
    }
}
