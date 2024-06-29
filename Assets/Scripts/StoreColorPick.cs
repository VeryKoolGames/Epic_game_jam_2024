using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreColorPick : MonoBehaviour
{
    private bool isOccupied;
    public Color color;
    
    public bool IsOccupied
    {
        get => isOccupied;
        set => isOccupied = value;
    }
}