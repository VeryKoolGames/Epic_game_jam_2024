namespace DefaultNamespace
{
    using UnityEngine;
    using UnityEngine.Events;
    using System.Collections.Generic;
    using DefaultNamespace;

    [CreateAssetMenu(fileName = "OnDeletePaintingEvent", menuName = "Events/OnDeletePaintingEvent")]
    public class OnDeletePaintingEvent : ScriptableObject
    {
        // A list of responses to the event
        private List<OnDeletePaintingListener> listeners = new List<OnDeletePaintingListener>();

        public void Raise(int id) {
            for (int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised(id);
            }
        }

        public void RegisterListener(OnDeletePaintingListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(OnDeletePaintingListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}