using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
     
     public void ClearButtons()
     {
         foreach (var button in colorPickingButtons)
         {
             StoreColorPick storeColorPick = button.GetComponent<StoreColorPick>();
             button.GetComponent<SpriteRenderer>().color = Color.white;
             storeColorPick.IsOccupied = false;
             storeColorPick.color = Color.white;
             button.transform.localScale = new Vector3(0, 0, 0);
         }
     }

     public void SetColor(Color color)
    {
        foreach (var button in colorPickingButtons)
        {
            StoreColorPick storeColorPick = button.GetComponent<StoreColorPick>();
            if (!storeColorPick.IsOccupied)
            {
                var sequence = DOTween.Sequence();
                button.transform.localScale = new Vector3(0, 0, 0);
                button.GetComponent<SpriteRenderer>().color = color;
                storeColorPick.color = color;
                storeColorPick.IsOccupied = true;
                sequence.Append(button.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBounce));
                break;
            }
        }
    }
}
