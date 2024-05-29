namespace Course.PrototypeScripting
{
    public class ActionGameState : Action
    {
        public RuntimeGlobal.GameState gameState;

        override public void ExecuteAction()
        {
            RuntimeGlobal.SwitchGameState(gameState);
            RuntimeGlobal.ClearSelection();
            SequenceHandler.Instance.ReportActionEnd();
        }

        override public string GetAdditionalInfo()
        {

            return gameState.ToString();
        }

    }
}
