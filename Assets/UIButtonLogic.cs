using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIButtonLogic : MonoBehaviour
{
    [SerializeField] private Sprite pressedSprite;
    [SerializeField] private Sprite unpressedSprite;
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private UnityEvent onClick;
    private bool isOnImage = false;
    private bool isSelected = false;
    private BoxCollider2D boxCollider;
    [SerializeField] private OnDeletePaintingEvent onDeletePaintingEvent;
    [SerializeField] private StorePaintingId storeId;

    private void OnEnable()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        GetComponent<Image>().sprite = unpressedSprite;
    }

    private void Update()
    {
        isOnImage = boxCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (isOnImage)
        {
            GetComponent<Image>().sprite = hoverSprite;
            if (Input.GetMouseButtonDown(0))
            {
                AudioManager.Instance.PlayOneShot(FmodEvents.Instance.tearPaper, transform.position);
                GetComponent<Image>().sprite = pressedSprite;
                onDeletePaintingEvent.Raise(storeId.id);
            }
        }
        else
        {
            GetComponent<Image>().sprite = unpressedSprite;
        }
    }
}
