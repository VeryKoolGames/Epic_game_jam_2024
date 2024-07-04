using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class OnDeletePaintingListener : MonoBehaviour
{
    public OnDeletePaintingEvent Event;
    public UnityEvent<int> Response;

    private void OnEnable() {
        Event.RegisterListener(this);
    }

    private void OnDisable() {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(int id) {
        Response.Invoke(id);
    }
}