using UnityEngine;

public class DetectPlayerCursor : MonoBehaviour
{
    [SerializeField] private FollowCursor _followCursor;
    [SerializeField] private Color testColor;
    private bool isMouseInZone;

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure the z-coordinate is zero for a 2D game

        Collider2D zoneCollider = GetComponent<Collider2D>();

        if (zoneCollider.OverlapPoint(mousePosition))
        {
            if (!isMouseInZone)
            {
                isMouseInZone = true;
                _followCursor.SetIsMouseInZone(true);
            }
        }
        else
        {
            if (isMouseInZone)
            {
                isMouseInZone = false;
                _followCursor.SetIsMouseInZone(false);
            }
        }
    }
    
}