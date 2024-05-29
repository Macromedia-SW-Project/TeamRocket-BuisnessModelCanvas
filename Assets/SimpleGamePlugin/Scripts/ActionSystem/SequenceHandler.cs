using System.Collections.Generic;
using UnityEngine;

namespace Course.PrototypeScripting
{
    public class SequenceHandler : MonoBehaviour
    {
        [System.Serializable]
        public class OpenSequence
        {
            public Sequence seq;
            public int actionPointer;

            public OpenSequence(Sequence _seq)
            {
                seq = _seq;
                actionPointer = 0;
            }

            public bool ActionLeft()
            {
                if (seq == null || seq.actions == null || seq.actions.Length == 0)
                    return false;

                return actionPointer < seq.actions.Length;
            }

            public void ExecuteCurrentAction()
            {
                if (actionPointer <= seq.actions.Length - 1 && seq.actions[actionPointer] != null)
                    seq.actions[actionPointer].ExecuteAction();
                else
                    SequenceHandler.Instance.ReportActionEnd();
            }

            public void InvokeEndOfSequence()
            {
                seq.InvokeEndOfSequence();
            }
        }
        public static SequenceHandler _instance;
        public static SequenceHandler Instance
        {
            get {
                if (_instance == null)
                    throw new System.Exception("No SequenceHandler in the scene. Import the prefab SimpleGameEngine from the project folder.");
                return _instance; 
            }
            private set { _instance = value; }
        }

        // Start is called before the first frame update
        void Awake()
        {
            openSequences = new Stack<OpenSequence>();
            if(_instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public Stack<OpenSequence> openSequences;
        public OpenSequence currentSequence;

        int ActionPointer 
        {  
            get { return currentSequence.actionPointer; }
            set { currentSequence.actionPointer = value; }
        }

        public void StartNewSequence(Sequence seq, Sequence.EndOfSequence endOfSeqFct = null)
        {
            OpenSequence newSeq = new OpenSequence(seq);
            if(endOfSeqFct != null)
                newSeq.seq.OnTimeEndOfSequenceEvent += endOfSeqFct;
            if (currentSequence != null && currentSequence.seq != null && currentSequence.seq.actions.Length > 0)
                openSequences.Push(currentSequence);
            currentSequence = newSeq;
            currentSequence.ExecuteCurrentAction();
        }

        public void ReportActionEnd()
        {
            if (currentSequence == null)
            {
                EndOfSequence();
                return;
            }
            ExecuteNextAction();

        }

        void ExecuteNextAction()
        {
            currentSequence.actionPointer++;
            if (currentSequence.ActionLeft())
                currentSequence.ExecuteCurrentAction();
            else
                EndOfSequence();
        }

        public void EndOfSequence()
        {
            if(currentSequence != null)
                currentSequence.InvokeEndOfSequence();
            currentSequence = null;
            if (openSequences != null && openSequences.Count > 0)
            {
            
                currentSequence = openSequences.Pop();
                ExecuteNextAction();
            }
            else
                openSequences = new Stack<OpenSequence>();
        }

        GameObject instantiatedSequenceHolder;
        public void InstantiateAssetSequence(GameObject asset)
        {
            instantiatedSequenceHolder = (GameObject)Instantiate(asset);
            StartNewSequence(instantiatedSequenceHolder.GetComponent<Sequence>(), DestroyAfterExecution);
        }

        void DestroyAfterExecution()
        {
            Destroy(instantiatedSequenceHolder);
        }

    }
}
