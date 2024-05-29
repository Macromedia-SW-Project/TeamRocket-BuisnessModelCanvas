using UnityEngine;

namespace Course.PrototypeScripting
{
    public class ActionSetActivity : Action
    {
        public GameObject obj;
        public bool state;
        override public void ExecuteAction()
        {
            obj.SetActive(state);
            SequenceHandler.Instance.ReportActionEnd();
        }

        override public string GetAdditionalInfo()
        {
            if (obj == null)
                return "- No Object set!";
            return obj.name + " => Set " + state.ToString();
        }

    }
}
