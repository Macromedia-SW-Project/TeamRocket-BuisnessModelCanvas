using UnityEngine;
using UnityEngine.Events;

namespace Course.PrototypeScripting
{
    public class TriggerEvent : MonoBehaviour
    {
        public UnityEvent eventOnTriggerEnter;
        public UnityEvent eventOnTriggerExit;

        private void OnTriggerEnter(Collider other)
        {
            if (eventOnTriggerEnter != null)
                eventOnTriggerEnter.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (eventOnTriggerExit != null)
                eventOnTriggerExit.Invoke();
        }
    }
}
