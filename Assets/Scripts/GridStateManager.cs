using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GridStateManager : MonoBehaviour
{
    public List<GridNode> gridNodes = new List<GridNode>();
    [SerializeField] private OnGridUnitSpawnListener onGridUnitSpawnListener;
    void Start()
    {
        onGridUnitSpawnListener.Response.AddListener(StoreGridNodes);
    }

    private void StoreGridNodes(GridNode gridNode)
    {
        gridNodes.Add(gridNode);
    }
    
    public void UpdateNodeColorById(int id, ColorsEnum color, Color colorHex)
    {
        GridNode node = gridNodes.Find(x => x.id == id);
        if (node != null)
        {
            node.color = color;
            node.colorHex = colorHex;
        }
    }
    
    private void OnDisable()
    {
        onGridUnitSpawnListener.Response.RemoveListener(StoreGridNodes);
    }
    
    public void ResetGrid()
    {
        Debug.Log("Resetting grid");
        Debug.Log(gridNodes.Count);
        foreach (GridNode gridNode in gridNodes)
        {
            gridNode.spriteRenderer.color = Color.white;
            gridNode.color = ColorsEnum.WHITE;
        }
    }
}
