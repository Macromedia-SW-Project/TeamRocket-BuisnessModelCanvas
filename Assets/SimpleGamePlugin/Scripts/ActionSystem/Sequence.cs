using Course.PrototypeScripting;
using UnityEngine;

namespace Course.PrototypeScripting
{
    public class Sequence : MonoBehaviour
    {
        public int allowedGameState = -1;

        public Action[] actions;
        public delegate void EndOfSequence();
        public event EndOfSequence OnTimeEndOfSequenceEvent;

        private void Awake()
        {
            Component[] list = gameObject.transform.GetComponents(typeof(Action));
            actions = new Action[list.Length];
            for (int i = 0; i < list.Length; i++)
                actions[i] = (Action)list[i];
        }
        public virtual void ExecuteCompleteSequence()
        {
            if(allowedGameState != -1)
            {
                if ((int)RuntimeGlobal.gameState != allowedGameState)
                    return;
            }

            SequenceHandler.Instance.StartNewSequence(this);
        }

        public void InvokeEndOfSequence()
        {
            if (OnTimeEndOfSequenceEvent != null)
                OnTimeEndOfSequenceEvent.Invoke();
            OnTimeEndOfSequenceEvent = null;
        }
    }
}
