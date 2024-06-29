using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PaintColorManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> colorPickingButtons = new List<GameObject>();
    [SerializeField] private OnColorSpawnedListener onColorSpawnedListener;
     // Start is called before the first frame update
     private void Awake()
     {
         onColorSpawnedListener.Response.AddListener(SetColor);
     }

     private void OnDisable()
     {
         onColorSpawnedListener.Response.RemoveListener(SetColor);
     }

     public void SetColor(Color color)
    {
        foreach (var button in colorPickingButtons)
        {
            StoreColorPick storeColorPick = button.GetComponent<StoreColorPick>();
            if (!storeColorPick.IsOccupied)
            {
                button.GetComponent<SpriteRenderer>().color = color;
                storeColorPick.color = color;
                storeColorPick.IsOccupied = true;
                break;
            }
        }
    }
}
