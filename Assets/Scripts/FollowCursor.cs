using System;
using DefaultNamespace;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GridStateManager gridStateManager;
    private bool isMouseInZone;
    private bool isMouseButtonDown;
    private ColorsEnum currentColor = ColorsEnum.WHITE;
    private Color currentPaintColor = Color.white;
    [SerializeField] private OnColorChoiceListener onColorChoiceListener;
    private bool hasBlended;

    private void Start()
    {
        onColorChoiceListener.Response.AddListener(SetCurrentColor);
    }
    
    public void SetCurrentColor(Color color)
    {
        hasBlended = false;
        currentPaintColor = color;
    }

    void Update()
    {
        if (isMouseInZone)
        {
            LerpToMouse();
            if (Input.GetMouseButtonDown(0))
            {
                isMouseButtonDown = true;
            }
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMouseButtonDown = false;
        }
        if (isMouseButtonDown)
        {
            PaintSquares(transform.position);
        }
    }

    private void PaintSquares(Vector3 position)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, 0.4f);

        int n = 0;
        foreach (Collider2D hitCollider in hitColliders)
        {
            SpriteRenderer squareRenderer = hitCollider.GetComponent<SpriteRenderer>();
            if (squareRenderer != null && hitCollider != this.GetComponent<Collider2D>())
            {
                // squareRenderer.color = currentPaintColor;
                if (!hasBlended)
                {
                    squareRenderer.color = BlendColors(currentPaintColor, squareRenderer.color);
                    currentPaintColor = squareRenderer.color;
                }
                else
                {
                    squareRenderer.color = currentPaintColor;
                }
                gridStateManager.UpdateNodeColorById(hitCollider.gameObject.GetComponent<StoreGridNodeId>().id, currentColor, currentPaintColor);
                n++;
            }
        }
        Debug.Log("paintsquares: "+n); // un clic == 245
    }
    
    private Color BlendColors(Color color1, Color color2)
    {
        if (color2 == Color.white || color1 == color2)
        {
            return color1;
        }
        float blendFactor = 0.5f; // You can adjust this factor to control the blending weight
        float r = Mathf.Lerp(color1.r, color2.r, blendFactor);
        float g = Mathf.Lerp(color1.g, color2.g, blendFactor);
        float b = Mathf.Lerp(color1.b, color2.b, blendFactor);
        Color ret = new Color(r, g, b, 1.0f);
        Debug.Log(ret.ToHexString());
        hasBlended = true;
        return ret;
    }

    private void LerpToMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = Vector3.Lerp(transform.position, mousePos, speed * Time.deltaTime);
    }
    
    public void SetIsMouseInZone(bool value)
    {
        if (!value)
        {
            MoveCursorTowardsCenter();
            isMouseButtonDown = false;
        }
        isMouseInZone = value;
    }

    private void MoveCursorTowardsCenter()
    {
        transform.DOMove(Vector3.zero, 0.5f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.45f);
    }
}
