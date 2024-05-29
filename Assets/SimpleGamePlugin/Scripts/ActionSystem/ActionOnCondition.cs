namespace Course.PrototypeScripting
{
    public class ActionOnCondition : Action
    {
        public string variableName;
        public enum Comparison { Equal, Greater, GreaterOrEqual, Less, LessOrEqual }
        public Comparison vergleich;

        public int value;

        public Sequence sequenceIfTrue;
        public Sequence sequenceIfFalse;

        override public void ExecuteAction()
        {
            switch (vergleich)
            {
                case Comparison.Equal:
                    ExecuteResultOfComparison(VariableManager.Instance.GetVariable(variableName) == value);
                    break;
                case Comparison.Greater:
                    ExecuteResultOfComparison(VariableManager.Instance.GetVariable(variableName) > value);
                    break;
                case Comparison.GreaterOrEqual:
                    ExecuteResultOfComparison(VariableManager.Instance.GetVariable(variableName) >= value);
                    break;
                case Comparison.Less:
                    ExecuteResultOfComparison(VariableManager.Instance.GetVariable(variableName) < value);
                    break;
                case Comparison.LessOrEqual:
                    ExecuteResultOfComparison(VariableManager.Instance.GetVariable(variableName) <= value);
                    break;

            }
        
        }

        void ExecuteResultOfComparison(bool value)
        {
            if (value && sequenceIfTrue != null)
                sequenceIfTrue.ExecuteCompleteSequence();
            else if(!value && sequenceIfFalse != null)
                sequenceIfFalse.ExecuteCompleteSequence();
            else
                SequenceHandler.Instance.ReportActionEnd();
        }
        override public string GetAdditionalInfo()
        {
            if (sequenceIfFalse == null && sequenceIfTrue == null)
                return "- No Sequences set!";
            return "Is " + variableName + " " + vergleich.ToString() + " " + value + " ?";
        }

    }
}
