using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ImageZoom : MonoBehaviour
{
    private Camera mainCamera;
    private BoxCollider2D boxCollider;
    private bool isOnImage;
    private bool isZoomOn;
    private bool hasEntered;
    [SerializeField] private GameObject hoverEffect;
    [SerializeField] private OnPaintingClickEvent OnPaintingClickEvent;

    private void Awake()
    {
        mainCamera = Camera.main;
        boxCollider = GetComponent<BoxCollider2D>();
    }
    
    public void SetIsZoomOn(bool isZoom)
    {
        this.isZoomOn = isZoom;
    }

    private void Update()
    {
        if (isZoomOn) return;
        isOnImage = boxCollider.OverlapPoint(mainCamera.ScreenToWorldPoint(Input.mousePosition));
        if (isOnImage)
        {
            hoverEffect.SetActive(true);
            if (hasEntered)
            {
                AudioManager.Instance.PlayOneShot(FmodEvents.Instance.splooshSound, transform.position);
                hasEntered = false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                OnImageClick();
            }
        }
        else
        {
            hasEntered = true;
            hoverEffect.SetActive(false);
        }
    }

    private void OnImageClick()
    {
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.yaySound, transform.position);
        hoverEffect.SetActive(false);
        OnPaintingClickEvent.Raise(GetComponent<Image>().sprite, true);
    }
}