namespace DefaultNamespace
{
    using UnityEngine;
    using UnityEngine.Events;
    using System.Collections.Generic;
    using DefaultNamespace;

    [CreateAssetMenu(fileName = "OnColorChoiceEvent", menuName = "Events/OnColorChoiceEvent")]
    public class OnColorChoiceEvent : ScriptableObject
    {
        // A list of responses to the event
        private List<OnColorChoiceListener> listeners = new List<OnColorChoiceListener>();

        public void Raise(Color color) {
            for(int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised(color);
            }
        }

        public void RegisterListener(OnColorChoiceListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(OnColorChoiceListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}