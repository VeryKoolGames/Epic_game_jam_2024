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
    
    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().sprite = hoverSprite;
    }
    
    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().sprite = unpressedSprite;
    }
    
    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().sprite = pressedSprite;
        onClick.Invoke();
    }
    
    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().sprite = hoverSprite;
    }

    public void PlayPaintSound()
    {
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.splooshSound, transform.position);
    }
}
