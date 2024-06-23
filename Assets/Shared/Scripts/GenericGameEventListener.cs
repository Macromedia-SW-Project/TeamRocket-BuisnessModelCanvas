using System;
using UnityEngine;  // Ensure you have the UnityEngine namespace included for Debug.Log

namespace HyperCasual.Core
{
    /// <summary>
    /// A generic event observer class
    /// </summary>
    [Serializable]
    public class GenericGameEventListener : IGameEventListener
    {
        /// <summary>
        /// The event this class is observing
        /// </summary>
        public AbstractGameEvent m_Event;
        
        /// <summary>
        /// The event handler invoked once the event is triggered
        /// </summary>
        public Action EventHandler;

        /// <summary>
        /// Start listening to the event
        /// </summary>
        public void Subscribe()
        {
            if (m_Event != null)
            {
                Debug.Log("Subscribing to event.");
                m_Event.AddListener(this);
            }
            else
            {
                Debug.LogWarning("m_Event is null in Subscribe.");
            }
        }

        /// <summary>
        /// Stop listening to the event
        /// </summary>
        public void Unsubscribe()
        {
            if (m_Event != null)
            {
                Debug.Log("Unsubscribing from event.");
                m_Event.RemoveListener(this);
            }
            else
            {
                Debug.LogWarning("m_Event is null in Unsubscribe.");
            }
        }
        
        /// <summary>
        /// The event handler that is called when the subscribed event is triggered
        /// </summary>
        public void OnEventRaised()
        {
            EventHandler?.Invoke();
        }
    }
}
