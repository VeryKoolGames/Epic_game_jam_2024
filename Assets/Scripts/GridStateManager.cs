using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GridStateManager : MonoBehaviour
{
    // public List<GridNode> gridNodes = new List<GridNode>();
    // [SerializeField] private OnGridUnitSpawnListener onGridUnitSpawnListener;
    // void Awake()
    // {
    //     onGridUnitSpawnListener.Response.AddListener(StoreGridNodes);
    // }
    //
    // private void StoreGridNodes(GridNode gridNode)
    // {
    //     gridNodes.Add(gridNode);
    // }
    //
    // public Color[] GetGridColors()
    // {
    //     Color[] colors = new Color[gridNodes.Count];
    //     for (int i = 0; i < gridNodes.Count; i++)
    //     {
    //         colors[i] = gridNodes[i].colorHex;
    //     }
    //     return colors;
    // }
    //
    // public void UpdateNodeColorById(int id, Color colorHex)
    // {
    //     GridNode node = gridNodes.Find(x => x.id == id);
    //     if (node != null)
    //     {
    //         node.colorHex = colorHex;
    //     }
    // }
    //
    // private void OnDisable()
    // {
    //     onGridUnitSpawnListener.Response.RemoveListener(StoreGridNodes);
    // }
    
}
