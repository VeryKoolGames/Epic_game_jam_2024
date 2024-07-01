using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class StoreColorPick : MonoBehaviour
{
    private bool isOccupied;
    public Color color;
    [SerializeField] private OnColorChoiceEvent onColorChoiceEvent;


    private void Start()
    {
        color = GetComponent<SpriteRenderer>().color;
    }

    public bool IsOccupied
    {
        get => isOccupied;
        set => isOccupied = value;
    }
    public void OnColorPick()
    {
        onColorChoiceEvent.Raise(color);
    }
}