namespace Course.PrototypeScripting
{
    public class ActionSwitchScene : Action
    {
        public string sceneName;
        override public void ExecuteAction()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            SequenceHandler.Instance.EndOfSequence();
        }

        override public string GetAdditionalInfo()
        {
            return sceneName;
        }
    }
}
