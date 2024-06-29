using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class OnGridUnitSpawnListener : MonoBehaviour
{
    public OnGridUnitSpawnEvent Event;
    public UnityEvent<GridNode> Response;

    private void OnEnable() {
        Event.RegisterListener(this);
    }

    private void OnDisable() {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(GridNode gridNode) {
        Response.Invoke(gridNode);
    }
}