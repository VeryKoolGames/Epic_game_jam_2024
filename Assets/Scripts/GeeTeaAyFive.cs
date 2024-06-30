using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
// copied from https://github.com/Firnox/Minesweeper
public class GeeTeeAyFive : MonoBehaviour {
  [Header("Tile Sprites")]
  [SerializeField] private Sprite unclickedTile;
  [SerializeField] private Sprite flaggedTile;
  [SerializeField] private Sprite clickedTile;
  [SerializeField] private List<Sprite> clickedTiles;
  [SerializeField] private Sprite mineTile;
  [SerializeField] private Sprite mineWrongTile;
  [SerializeField] private Sprite mineHitTile;

  [Header("GM set via code")]
  public GeeTeeAyFiveManager gameManager;
  public GameObject smiley;

  private SpriteRenderer spriteRenderer;
  public bool flagged = false;
  public bool active = true;
  public bool isMine = false;
  public int mineCount = 0;

  void Awake() {
    // This should always exist due to the RequireComponent helper.
    spriteRenderer = GetComponent<SpriteRenderer>();
  }
  
  private void OnMouseOver() {
    // If it hasn't already been pressed.
    if (active) {
      if (Input.GetMouseButtonDown(0)) {
        spriteRenderer.sprite = clickedTile;
        smiley.GetComponent<SpriteRenderer>().sprite = smiley.GetComponent<GeeTeeAyFiveSmiley>().getClickedTileSmiley();
      }

      if (Input.GetMouseButtonUp(0))
      {
        ClickedTile();
        smiley.GetComponent<SpriteRenderer>().sprite = smiley.GetComponent<GeeTeeAyFiveSmiley>().getUnclickedSmiley();
      }

      if (Input.GetMouseButtonDown(1)) {
        // If right click toggle flag on/off.
        flagged = !flagged;
        if (flagged) {
          spriteRenderer.sprite = flaggedTile;
        } else {
          spriteRenderer.sprite = unclickedTile;
        }
      }
    } else {
      // If you're pressing both mouse buttons.
      if (Input.GetMouseButton(0) && Input.GetMouseButton(1)) {
        // Check for valid expansion.
        gameManager.ExpandIfFlagged(this.gameObject);
      }
    }
  }

  public void ClickedTile() {
    // Don't allow left clicks on flags.
    if (active & !flagged) {
      // Ensure it can no longer be pressed again.
      active = false;
      if (isMine) {
        // Game over :(
        spriteRenderer.sprite = mineHitTile;
        gameManager.GameOver();
      } else {
        // It was a safe click, set the correct sprite.
        spriteRenderer.sprite = clickedTiles[mineCount];
        if (mineCount == 0) {
          // Register that the click should expand out to the neighbours.
          gameManager.ClickNeighbours(this.gameObject);
        }
        // Whenever we successfully make a change check for game over.
        gameManager.CheckGameOver();
      }
    }
  }

  // If this tile should be shown at game over, do so.
  public void ShowGameOverState() {
    if (active) {
      active = false;
      if (isMine & !flagged) {
        // If mine and not flagged show mine.
        spriteRenderer.sprite = mineTile;
      } else if (flagged & !isMine) {
        // If flagged incorrectly show crossthrough mine
        spriteRenderer.sprite = mineWrongTile;
      }
    }
  }

  // Helper function to flag remaning mines on game completion.
  public void SetFlaggedIfMine() {
    if (isMine) {
      flagged = true;
      spriteRenderer.sprite = flaggedTile;
    }
  }

  public Sprite getUnclickedTile()
  {
    return (unclickedTile);
  }
}