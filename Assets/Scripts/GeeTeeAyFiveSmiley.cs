using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeeTeeAyFiveSmiley : MonoBehaviour
{
  [Header("Smiley Sprites")] 
  [SerializeField] private GeeTeeAyFiveManager gameManager;
  [SerializeField] private Sprite unclickedSmiley;
  [SerializeField] private Sprite clickedSmiley;
  [SerializeField] private Sprite clickedTileSmiley;
  [SerializeField] private Sprite deadSmiley;
  
  private SpriteRenderer spriteRenderer;
  public bool active = true;
  
  void Awake()
  {
    // This should always exist due to the RequireComponent helper.
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void OnMouseOver()
  {
    // If it hasn't already been pressed.
    if (Input.GetMouseButtonDown(0))
    {
      GetComponent<SpriteRenderer>().sprite = clickedSmiley;
    }
    if (Input.GetMouseButtonUp(0))
    {
      GetComponent<SpriteRenderer>().sprite = unclickedSmiley;
      gameManager.ResetGameState();
    }
  }

  public Sprite getClickedTileSmiley()
  {
    return (clickedTileSmiley);
  }
  
  public Sprite getUnclickedSmiley()
  {
    return (unclickedSmiley);
  }
  
}