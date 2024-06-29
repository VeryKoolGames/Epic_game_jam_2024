using DefaultNamespace;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class GridNode
{
    public int id;
    public Color colorHex;
    public SpriteRenderer spriteRenderer;

    public GridNode( int id, Color colorHex, SpriteRenderer spriteRenderer)
    {
        this.id = id;
        this.colorHex = colorHex;
        this.spriteRenderer = spriteRenderer;
    }
}

public class GridManager : MonoBehaviour
{
    public GameObject squarePrefab;
    public int gridWidth;
    public int gridHeight;
    // KR: w and h should be the same than sqrt(PaintingParser.GetPaintColors.Length)
    public float squareSize = 0.05f;
    [SerializeField] private OnGridUnitSpawnEvent onGridUnitSpawnEvent;
    [SerializeField] public float positionX;
    [SerializeField] public float positionY;
    [SerializeField] public float positionZ;

    void Start()
    {
        // Calculate the offset to center the grid
        int id = 0;
        float offsetX = (gridWidth - 1) * squareSize / 2.0f;
        float offsetY = (gridHeight - 1) * squareSize / 2.0f;
        
        if (positionX != 0)
        {
            offsetX = positionX;
        }
        if (positionY != 0)
        {
            offsetY = positionY;
        }
        
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = new Vector3(x * squareSize + offsetX, y * squareSize + offsetY, positionZ);
                GameObject newObj = Instantiate(squarePrefab, position, Quaternion.identity);
                newObj.GetComponent<StoreGridNodeId>().id = id;
                GridNode gridNode = new GridNode(id, Color.white,  newObj.GetComponent<SpriteRenderer>());
                newObj.GetComponent<SpriteRenderer>().sortingOrder = 2;
                onGridUnitSpawnEvent.Raise(gridNode);
                id++;
            }
        }
        // KR: id is w*h
    }
}