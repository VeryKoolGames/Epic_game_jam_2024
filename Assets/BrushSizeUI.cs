using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrushSizeUI : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private int brushSize;
    [SerializeField] private UnityEvent<int, int> OnBrushSizeSelected;


    private void OnMouseDown()
    {
        OnBrushSizeSelected.Invoke(id, brushSize);
    }
}
