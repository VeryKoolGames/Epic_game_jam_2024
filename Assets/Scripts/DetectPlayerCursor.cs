using UnityEngine;
using UnityEngine.Events;

public class DetectPlayerCursor : MonoBehaviour
{
    [SerializeField] private UnityEvent<bool> onZoneEnter;
    [SerializeField] private Color testColor;
    private bool isMouseInZone;

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Collider2D zoneCollider = GetComponent<Collider2D>();

        if (zoneCollider.OverlapPoint(mousePosition))
        {
            if (!isMouseInZone)
            {
                isMouseInZone = true;
                onZoneEnter.Invoke(true);
            }
        }
        else
        {
            if (isMouseInZone)
            {
                isMouseInZone = false;
                onZoneEnter.Invoke(false);
            }
        }
    }
    
}