using UnityEngine;

namespace Course.PrototypeScripting
{
    public class ActionCamSwitch : Action
    {
        public GameObject camTarget;
        public float time;

        public enum Type { StaticCam, BackToMovingCam, StaticFollowCam}
        public Type type;

        override public void ExecuteAction()
        {


            if(type == Type.StaticCam)
            {
                if(camTarget == null)
                    Debug.LogError("No GameObject set as target for Cam Switch!");
                else
                    UnityEngine.Camera.main.GetComponent<CamBase>().SwitchToStatic(camTarget, time);
            }
            else if (type == Type.StaticFollowCam)
            {
                if (camTarget == null)
                    Debug.LogError("No GameObject set as target for Cam Switch!");
                else if(UnityEngine.Camera.main.GetComponent<CamStatic>())
                    UnityEngine.Camera.main.GetComponent<CamStatic>().SwitchToFollow(camTarget, time);
                else
                    UnityEngine.Camera.main.GetComponent<CamBase>().SwitchToStatic(camTarget, time);
            }
            else
            {
                if (UnityEngine.Camera.main.GetComponent<CamFollow>())
                    UnityEngine.Camera.main.GetComponent<CamFollow>().SwitchToFollow(time);
                else
                if (UnityEngine.Camera.main.GetComponent<CamRotate>())
                    UnityEngine.Camera.main.GetComponent<CamRotate>().SwitchToFollow(time);
                else
                    Debug.LogError("MainCamera is not a Following Cam!");
            }
            SequenceHandler.Instance.ReportActionEnd();
        }

        override public string GetAdditionalInfo()
        {
            if (type == Type.BackToMovingCam)
                return "New Cam: FollowCam";
            if(camTarget == null)
                return "- No Cam ! -";
            return "New Cam: " + camTarget.name;
        }
    }
}
