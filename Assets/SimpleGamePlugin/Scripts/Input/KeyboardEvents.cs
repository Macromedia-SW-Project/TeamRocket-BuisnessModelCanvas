using UnityEngine;
using UnityEngine.Events;

namespace Course.PrototypeScripting
{
    public class KeyboardEvents : MonoBehaviour
    {
        [System.Serializable]
        public class KeyboardEvent
        {
            public KeyCode keyOnKeyboard;
            public bool triggerWhileHolding;
            public UnityEvent eventToTrigger;

            public UnityEvent eventOnKeyUp;

            public void Check()
            {
                if (triggerWhileHolding && UnityEngine.Input.GetKey(keyOnKeyboard))
                    eventToTrigger.Invoke();
                else if(UnityEngine.Input.GetKeyDown(keyOnKeyboard))
                    eventToTrigger.Invoke();
                else if (UnityEngine.Input.GetKeyUp(keyOnKeyboard))
                    eventOnKeyUp.Invoke();
            }
        }

        public KeyboardEvent[] keysAndTheirEvents;

        // Update is called once per frame
        void Update()
        {
            foreach(KeyboardEvent keyEvent in keysAndTheirEvents)
            {
                keyEvent.Check();
            }
        }
    }
}
