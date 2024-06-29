using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class OnColorSpawnedListener : MonoBehaviour
{
    public OnColorParsedEvent Event;
    public UnityEvent<Color> Response;

    private void OnEnable() {
        Event.RegisterListener(this);
    }

    private void OnDisable() {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(Color color) {
        Response.Invoke(color);
    }
}