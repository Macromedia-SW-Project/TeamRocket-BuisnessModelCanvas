using System.Collections.Generic;
using UnityEngine;

namespace Course.PrototypeScripting
{
    public class Conversation : MonoBehaviour
    {


        [System.Serializable]
        public class ConversationOption
        {
            public string name = "-";
            public string text = "-";
            public bool enabled = true;
            public bool endConversationAfterwards = false;
            public Sequence sequence;
            public bool open = false;
        }


        public Sequence welcomeSequence;

        public ConversationOption[] options;

        public void Init()
        {
            options = new ConversationOption[1];
            options[0] = new ConversationOption();
        }
        public void SetOptionState(int index, bool value)
        {
            options[index].enabled = value;
        }

        public List<ConversationOption> GetEnabledOptions()
        {
            List<ConversationOption> list = new List<ConversationOption>();
            foreach (ConversationOption option in options)
            {
                if (option.enabled)
                    list.Add(option);
            }
            return list;
        }
        public string[] GetOptionNames()
        {
            List<string> list = new List<string>();
            foreach (ConversationOption option in options)
            {
                list.Add(option.name);
            }
            return list.ToArray();
        }
    }
}
