using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GridStateManager gridStateManager;
    private bool isMouseInZone;
    private bool isMouseButtonDown;
    private ColorsEnum currentColor = ColorsEnum.BLUE;
    // Start is called before the first frame update

    // Update is called once per frame
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
        // Use a small overlap circle to detect multiple squares under the cursor
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, 0.4f);

        foreach (Collider2D hitCollider in hitColliders)
        {
            SpriteRenderer squareRenderer = hitCollider.GetComponent<SpriteRenderer>();
            if (squareRenderer != null && hitCollider != this.GetComponent<Collider2D>())
            {
                gridStateManager.UpdateNodeColorById(hitCollider.gameObject.GetComponent<StoreGridNodeId>().id, currentColor);
                squareRenderer.color = Color.blue;
            }
        }
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
