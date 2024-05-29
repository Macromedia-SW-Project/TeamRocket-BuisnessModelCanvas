namespace Course.PrototypeScripting
{
    public class ActionStartSequence : Action
    {
        public Sequence sequenceToStart;

        override public void ExecuteAction()
        {
            SequenceHandler.Instance.StartNewSequence(sequenceToStart);
        }

        override public string GetAdditionalInfo()
        {
            if (sequenceToStart == null)
                return "- No Sequence set ! -";
            return sequenceToStart.gameObject.name;
        }
    }
}
