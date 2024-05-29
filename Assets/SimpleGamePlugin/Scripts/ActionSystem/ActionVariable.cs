namespace Course.PrototypeScripting
{
    public class ActionVariable : Action
    {
        public string variableName;
        public enum Actions { Change, SetExplicit}
        public Actions action;
        public int value;

        override public void ExecuteAction()
        {
            switch(action)
            {
                case Actions.Change:
                    VariableManager.Instance.SetVariable(variableName, VariableManager.Instance.GetVariable(variableName) + value);
                    break;
                case Actions.SetExplicit:
                    VariableManager.Instance.SetVariable(variableName, value);
                    break;
            }
            SequenceHandler.Instance.ReportActionEnd();
        }

        override public string GetAdditionalInfo()
        {
            if (action == Actions.Change)
                return variableName + " + " + value;
            else
                return variableName + " => " + value;
        }
    }
}
