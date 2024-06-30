using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonLogic : MonoBehaviour
{
    [SerializeField] private Sprite pressedSprite;
    [SerializeField] private Sprite unpressedSprite;
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private UnityEvent onClick;
    private bool isLocked = false;
    
    public void LockButton()
    {
        isLocked = true;
    }
    
    private void OnMouseEnter()
    {
        if (isLocked)
        {
            return;
        }
        GetComponent<SpriteRenderer>().sprite = hoverSprite;
    }
    
    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().sprite = unpressedSprite;
    }
    
    private void OnMouseDown()
    {
        if (isLocked)
        {
            return;
        }
        GetComponent<SpriteRenderer>().sprite = pressedSprite;
        onClick.Invoke();
    }
    
    private void OnMouseUp()
    {
        if (isLocked)
        {
            return;
        }
        GetComponent<SpriteRenderer>().sprite = hoverSprite;
    }

    public void PlayPaintSound()
    {
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.splooshSound, transform.position);
    }

    public void PlaySingleClickSound()
    {
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.singleClickSound, transform.position);
    }

    private void OnDisable()
    {
        GetComponent<SpriteRenderer>().sprite = unpressedSprite;
    }

    public void UnlockButton()
    {
        isLocked = false;
    }
}
