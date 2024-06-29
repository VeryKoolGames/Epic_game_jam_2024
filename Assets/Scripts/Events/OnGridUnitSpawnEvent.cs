// namespace DefaultNamespace
// {
//     using UnityEngine;
//     using UnityEngine.Events;
//     using System.Collections.Generic;
//     using DefaultNamespace;
//
//     [CreateAssetMenu(fileName = "OnGridUnitSpawnEvent", menuName = "Events/OnGridUnitSpawnEvent")]
//     public class OnGridUnitSpawnEvent : ScriptableObject
//     {
//         // A list of responses to the event
//         private List<OnGridUnitSpawnListener> listeners = new List<OnGridUnitSpawnListener>();
//
//         public void Raise(GridNode gridNode) {
//             for(int i = listeners.Count - 1; i >= 0; i--) {
//                 listeners[i].OnEventRaised(gridNode);
//             }
//         }
//
//         public void RegisterListener(OnGridUnitSpawnListener listener) {
//             if (!listeners.Contains(listener))
//                 listeners.Add(listener);
//         }
//
//         public void UnregisterListener(OnGridUnitSpawnListener listener) {
//             if (listeners.Contains(listener))
//                 listeners.Remove(listener);
//         }
//     }
// }