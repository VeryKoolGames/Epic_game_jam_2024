namespace DefaultNamespace
{
    using UnityEngine;
    using UnityEngine.Events;
    using System.Collections.Generic;
    using DefaultNamespace;

    [CreateAssetMenu(fileName = "OnPaintingClickEvent", menuName = "Events/OnPaintingClickEvent")]
    public class OnPaintingClickEvent : ScriptableObject
    {
        // A list of responses to the event
        private List<OnPaintingClickListener> listeners = new List<OnPaintingClickListener>();

        public void Raise(Sprite sprite, bool isZoomOn) {
            for (int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised(sprite, isZoomOn);
            }
        }

        public void RegisterListener(OnPaintingClickListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(OnPaintingClickListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}