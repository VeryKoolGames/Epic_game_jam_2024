using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

public class ImageZoomedHolder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private OnPaintingClickListener OnPaintingClickListener;
    [SerializeField] private OnPaintingClickEvent onPaintingClick;
    private Vector3 baseScale;
    private BoxCollider2D boxCollider;
    private bool isZooming;
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        baseScale = new Vector3(2f, 2f, 2f);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        OnPaintingClickListener.Response.AddListener(OnPaintClick);
        baseScale = new Vector3(2f, 2f, 2f);
    }

    private void Update()
    {
        if (isZooming && Input.GetMouseButtonDown(0))
        {
            if (boxCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition))) ;
            {
                transform.DOScale(0f, .2f);
                onPaintingClick.Raise(null, false);
                isZooming = false;
            }
        }
    }

    private void OnPaintClick(Sprite sprite, bool isZoomOn)
    {
        if (!isZoomOn) return;
        StartCoroutine(delay());
        spriteRenderer.sprite = sprite;
        transform.DOScale(baseScale, .2f);
    }
    
    private IEnumerator delay()
    {
        yield return new WaitForSeconds(.5f);
        isZooming = true;
    }
    
    private void OnDisable()
    {
        OnPaintingClickListener.Response.RemoveListener(OnPaintClick);
    }
    
}
