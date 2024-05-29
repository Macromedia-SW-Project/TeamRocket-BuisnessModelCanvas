using Course.PrototypeScripting;
using UnityEngine;

namespace Course.PrototypeScripting
{
    public class SequenceTriggerEvent : MonoBehaviour
    {

        public Sequence sequenceOnTriggerEnter;
        public Sequence sequenceOnTriggerExit;
        // Start is called before the first frame update
        private void OnTriggerEnter(Collider other)
        {
            if (sequenceOnTriggerEnter != null)
                sequenceOnTriggerEnter.ExecuteCompleteSequence();
        }

        private void OnTriggerExit(Collider other)
        {
            if (sequenceOnTriggerExit != null)
                sequenceOnTriggerExit.ExecuteCompleteSequence();
        }
    }
}
