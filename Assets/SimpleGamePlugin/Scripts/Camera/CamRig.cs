using UnityEngine;

namespace Course.PrototypeScripting
{
    public class CamRig : MonoBehaviour
    {

        Transform followObject;

        public void Initialize(GameObject target)
        {
            followObject = target.transform;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = followObject.position;
        }
    }
}
