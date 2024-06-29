namespace DefaultNamespace
{
    using UnityEngine;
    using UnityEngine.Events;
    using System.Collections.Generic;
    using DefaultNamespace;

    [CreateAssetMenu(fileName = "OnColorParsedEvent", menuName = "Events/OnColorParsedEvent")]
    public class OnColorParsedEvent : ScriptableObject
    {
        // A list of responses to the event
        private List<OnColorSpawnedListener> listeners = new List<OnColorSpawnedListener>();

        public void Raise(Color color) {
            for(int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised(color);
            }
        }

        public void RegisterListener(OnColorSpawnedListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(OnColorSpawnedListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}