using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class OnPaintingClickListener : MonoBehaviour
{
    public OnPaintingClickEvent Event;
    public UnityEvent<Sprite, bool> Response;

    private void OnEnable() {
        Event.RegisterListener(this);
    }

    private void OnDisable() {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(Sprite sprite, bool isZoomOn) {
        Response.Invoke(sprite, isZoomOn);
    }
}