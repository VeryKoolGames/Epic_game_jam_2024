using System;
using System.Linq;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

// copied from https://github.com/Firnox/Minesweeper
public class GeeTeeAyFiveManager : MonoBehaviour {
  [SerializeField] private Transform tilePrefab;
  [SerializeField] private Transform geeTeeAyFiveHolder;
  [SerializeField] private GameObject geeTeeAyFiveSmiley;
  
  private List<GameObject> tiles = new();
  private Vector3 baseScale;
  
  private int width;
  private int height;
  private int numMines;
  
  private readonly float tileSize = 0.5f;
  
  // Start is called before the first frame update
  // void Start() {
  //   src = GetComponent<AudioSource>();
  //   CreateGameBoard(9, 9, 10); // Easy
  //   // CreateGameBoard(16, 16, 40); // Intermediate
  //   // CreateGameBoard(30, 16, 99); // Expert
  //   ResetGameState();
  // }

  private void Start()
  {
    baseScale = transform.localScale;
    Debug.Log("sclale: " + baseScale);
  }

  private void OnEnable()
  {
    transform.localScale = Vector3.zero;
    transform.DOScale(1f, .2f).OnComplete((() =>
    {
      CreateGameBoard(9, 9, 10); // Easy
      ResetGameState();
    }));
  }
  
  public void CloseWindow()
  {
    transform.DOScale(0f, .2f).OnComplete(() =>
    {
      gameObject.SetActive(false);
      transform.localScale = baseScale;
    });
  }

  private void OnMouseOver()
  {
    // If it hasn't already been pressed.
    if (Input.GetMouseButtonDown(0))
    {
      geeTeeAyFiveSmiley.GetComponent<SpriteRenderer>().sprite =
        geeTeeAyFiveSmiley.GetComponent<GeeTeeAyFiveSmiley>().getClickedTileSmiley();
    }
  }
  
  public void CreateGameBoard(int width, int height, int numMines) {
    // Save the game parameters we're using.
    this.width = width;
    this.height = height;
    this.numMines = numMines;
  
    // Create the array of tiles.
    for (int row = 0; row < height; row++) {
      for (int col = 0; col < width; col++) {
        // Position the tile in the correct place (centred).
        Debug.Log("Creating tile");
        Transform tileTransform = Instantiate(tilePrefab, geeTeeAyFiveHolder, true);
        float xIndex = col - ((width - 1) / 2.0f);
        float yIndex = row - ((height - 1) / 2.0f);
        tileTransform.localPosition = new Vector2(xIndex * tileSize, yIndex * tileSize);
        // Keep a reference to the tile for setting up the game.
        GameObject tile = tileTransform.gameObject;
        tiles.Add(tile);
        tile.GetComponent<GeeTeeAyFive>().gameManager = this;
        tile.GetComponent<GeeTeeAyFive>().smiley = geeTeeAyFiveSmiley;
      }
    }
  }
  public void ResetGameState() {
    
    // Reset sprite to unclicked and not mine
    for (int i = 0; i < tiles.Count; i++)
    {
      tiles[i].GetComponent<SpriteRenderer>().sprite = tiles[i].GetComponent<GeeTeeAyFive>().getUnclickedTile();
      tiles[i].GetComponent<GeeTeeAyFive>().active = true;
      tiles[i].GetComponent<GeeTeeAyFive>().isMine = false;
    }
    // Randomly shuffle the tile positions to get indices for mine positions.
    int[] minePositions = Enumerable.Range(0, tiles.Count).OrderBy(x => Random.Range(0.0f, 1.0f)).ToArray();
  
    // Set mines at the first numMines positions.
    for (int i = 0; i < numMines; i++) {
      int pos = minePositions[i];
      tiles[pos].GetComponent<GeeTeeAyFive>().isMine = true;
    }
  
    // Update all the tiles to hold the correct number of mines.
    for (int i = 0; i < tiles.Count; i++) {
      tiles[i].GetComponent<GeeTeeAyFive>().mineCount = HowManyMines(i);
    }
  }
  
  // Given a location work out how many mines are surrounding it.
  private int HowManyMines(int location) {
    int count = 0;
    foreach (int pos in GetNeighbours(location)) {
      if (tiles[pos].GetComponent<GeeTeeAyFive>().isMine) {
        count++;
      }
    }
    return count;
  }
  
  // Given a position, return the positions of all neighbours.
  private List<int> GetNeighbours(int pos) {
    List<int> neighbours = new();
    int row = pos / width;
    int col = pos % width;
    // (0,0) is bottom left.
    if (row < (height - 1)) {
      neighbours.Add(pos + width); // North
      if (col > 0) {
        neighbours.Add(pos + width - 1); // North-West
      }
      if (col < (width - 1)) {
        neighbours.Add(pos + width + 1); // North-East
      }
    }
    if (col > 0) {
      neighbours.Add(pos - 1); // West
    }
    if (col < (width - 1)) {
      neighbours.Add(pos + 1); // East
    }
    if (row > 0) {
      neighbours.Add(pos - width); // South
      if (col > 0) {
        neighbours.Add(pos - width - 1); // South-West
      }
      if (col < (width - 1)) {
        neighbours.Add(pos - width + 1); // South-East
      }
    }
    return neighbours;
  }
  
  public void ClickNeighbours(GameObject tile) {
    int location = tiles.IndexOf(tile);
    foreach (int pos in GetNeighbours(location)) {
      tiles[pos].GetComponent<GeeTeeAyFive>().ClickedTile();
    }
  }
  
  public void GameOver() {
    AudioManager.Instance.PlayOneShot(FmodEvents.Instance.kenBoom, transform.position);
    // Disable clicks on all mines.
    foreach (GameObject tile in tiles) {
      tile.GetComponent<GeeTeeAyFive>().ShowGameOverState();
    }
    // Play Kaboom Sound
  }
  
  public void CheckGameOver() {
    AudioManager.Instance.PlayOneShot(FmodEvents.Instance.kenSploosh, transform.position);
    int count = 0;
    foreach (GameObject tile in tiles) {
      if (tile.GetComponent<GeeTeeAyFive>().active) {
        count++;
      }
    }
    if (count == numMines) {
      // Flag and disable everything, we're done.
      foreach (GameObject tile in tiles) {
        tile.GetComponent<GeeTeeAyFive>().active = false;
        tile.GetComponent<GeeTeeAyFive>().SetFlaggedIfMine();
      }
    }
  }
  
  // Click on all surrounding tiles if mines are all flagged.
  public void ExpandIfFlagged(GameObject tile) {
    int location = tiles.IndexOf(tile);
    // Get the number of flags.
    int flag_count = 0;
    foreach (int pos in GetNeighbours(location)) {
      if (tiles[pos].GetComponent<GeeTeeAyFive>().flagged) {
        flag_count++;
      }
    }
    // If we have the right number click surrounding tiles.
    if (flag_count == tile.GetComponent<GeeTeeAyFive>().mineCount) {
      // Clicking a flag does nothing so this is safe.
      ClickNeighbours(tile);
    }
  }
}