using UnityEngine;
using UnityEngine.Events;

public class DetectDoubleClick : MonoBehaviour
{
    [SerializeField] private float doubleClickTime = 0.25f; // Maximum time between clicks to be considered a double click
    [SerializeField] private Collider2D collider; // Collider to detect clicks on
    private float lastClickTime;
    private int clickCount;

    // UnityEvent that can be configured in the Inspector to respond to double-clicks
    public UnityEvent onDoubleClick;

    void Update()
    {
        // Detect mouse click
        if (Input.GetMouseButtonDown(0))
        {
            DetectDoubleMouseClick();
        }
    }

    void DetectDoubleMouseClick()
    {
        // Check if the mouse is over the collider
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

        if (hit.collider != null && hit.collider == collider)
        {
            clickCount++;
            if (clickCount == 1)
            {
                lastClickTime = Time.time;
            }

            if (clickCount > 1 && Time.time - lastClickTime < doubleClickTime)
            {
                if (onDoubleClick != null)
                {
                    onDoubleClick.Invoke();
                }
                clickCount = 0;
            }
            else if (Time.time - lastClickTime > doubleClickTime)
            {
                clickCount = 1;
                lastClickTime = Time.time;
            }
        }
    }
}