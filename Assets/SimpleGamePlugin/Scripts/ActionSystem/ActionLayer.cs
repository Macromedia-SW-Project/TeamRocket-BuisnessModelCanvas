using UnityEngine;

namespace Course.PrototypeScripting
{
    public class ActionLayer : Action
    {
        public LayerMask visible_layer;

        override public void ExecuteAction()
        {
            UnityEngine.Camera.main.cullingMask = visible_layer;
            SequenceHandler.Instance.ReportActionEnd();
        }

        override public string GetAdditionalInfo()
        {

            return "";
        }
    }
}
