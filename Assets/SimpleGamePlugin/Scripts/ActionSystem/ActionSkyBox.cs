using UnityEngine;

namespace Course.PrototypeScripting
{
    public class ActionSkyBox : Action
    {
        public Material skyBox;

        override public void ExecuteAction()
        {
            RenderSettings.skybox = skyBox;
            SequenceHandler.Instance.ReportActionEnd();
        }
    }
}
