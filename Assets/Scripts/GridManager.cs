using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

public class GridNode
{
    public ColorsEnum color;
    public int id;
    public Color colorHex;
    public SpriteRenderer spriteRenderer;

    public GridNode(ColorsEnum color, int id, Color colorHex, SpriteRenderer spriteRenderer)
    {
        this.color = color;
        this.id = id;
        this.colorHex = colorHex;
        this.spriteRenderer = spriteRenderer;
    }
}

public class GridManager : MonoBehaviour
{
    public GameObject squarePrefab;
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float squareSize = 1.0f;
    [SerializeField] private OnGridUnitSpawnEvent onGridUnitSpawnEvent;

    void Start()
    {
        // Calculate the offset to center the grid
        int id = 0;
        float offsetX = (gridWidth - 1) * squareSize / 2.0f;
        float offsetY = (gridHeight - 1) * squareSize / 2.0f;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = new Vector3(x * squareSize - offsetX, y * squareSize - offsetY, 0);
                GameObject newObj = Instantiate(squarePrefab, position, Quaternion.identity);
                newObj.GetComponent<StoreGridNodeId>().id = id;
                GridNode gridNode = new GridNode(ColorsEnum.WHITE, id, Color.white,  newObj.GetComponent<SpriteRenderer>());
                onGridUnitSpawnEvent.Raise(gridNode);
                id++;
            }
        }
    }
}