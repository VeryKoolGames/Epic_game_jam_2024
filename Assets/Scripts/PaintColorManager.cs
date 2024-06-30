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
    private int numberOfColors = 0;
    private List<Color> unlockableColors = new List<Color>();
    private const float colorThreshold = 0.1f; // Threshold for color comparison

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
        numberOfColors = 0;
        unlockableColors.Clear();
    }

    public void TryToUnlockColor(Color newColor)
    {
        foreach (Color unlockableColor in unlockableColors)
        {
            if (ColorsAreSimilar(newColor, unlockableColor, colorThreshold))
            {
                UpdateUnlockableUI(unlockableColor);
                unlockableColors.Remove(unlockableColor);
                break;
            }
        }
    }

    private void UpdateUnlockableUI(Color unlockableColor)
    {
        foreach (var button in colorPickingButtons)
        {
            StoreColorPick storeColorPick = button.GetComponent<StoreColorPick>();
            if (storeColorPick.color == unlockableColor)
            {
                button.transform.localScale = new Vector3(0, 0, 0);
                button.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBounce);
                break;
            }
        }
    }

    private bool ColorsAreSimilar(Color a, Color b, float threshold)
    {
        float rDiff = Mathf.Abs(a.r - b.r);
        float gDiff = Mathf.Abs(a.g - b.g);
        float bDiff = Mathf.Abs(a.b - b.b);
        return rDiff < threshold && gDiff < threshold && bDiff < threshold;
    }

    public void SetColor(Color color)
    {
        foreach (var button in colorPickingButtons)
        {
            StoreColorPick storeColorPick = button.GetComponent<StoreColorPick>();
            if (!storeColorPick.IsOccupied)
            {
                numberOfColors += 1;
                button.transform.localScale = new Vector3(0, 0, 0);
                button.GetComponent<SpriteRenderer>().color = color;
                storeColorPick.color = color;
                storeColorPick.IsOccupied = true;
                if (numberOfColors < 4)
                {
                    button.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBounce);
                }
                else
                {
                    unlockableColors.Add(color);
                }
                break;
            }
        }
    }
}
