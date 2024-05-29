namespace Course.PrototypeScripting
{
    public class ActionShowText : Action
    {
        public string nameToShow;
        public string textToShow;
        public float time = 4f;
        bool running;
        bool wasSkipped = false;
        public bool skippable = true;

        override public void ExecuteAction()
        {
            TextPanel.Instance.SetText(nameToShow,textToShow);
            Invoke("GoOn", time);
            wasSkipped = false;
            running = true;
            enabled = true;
        }

        private void Awake()
        {
            enabled = false;
        }

        private void Update()
        {
            if (skippable && UnityEngine.Input.anyKeyDown && running)
                Skip();
        }

        void Skip()
        {
            GoOn();
        }

        void GoOn()
        {
            if (wasSkipped)
                return;
            enabled = false;
            running = false;
            wasSkipped = true;
            TextPanel.Instance.Hide();
            SequenceHandler.Instance.ReportActionEnd();
        }

        override public string GetAdditionalInfo()
        {
            return nameToShow + ": " + textToShow;
        }
    }
}
