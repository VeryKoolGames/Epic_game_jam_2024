using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PaintColorManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> colorPickingButtons = new List<GameObject>();
    [SerializeField] private List<GameObject> colorPickingButtonsLock = new List<GameObject>();
    [SerializeField] private OnColorSpawnedListener onColorSpawnedListener;
    [SerializeField] private OnColorChoiceListener onColorChoiceListener;
    private int numberOfColors = 0;
    private List<Color> unlockableColors = new List<Color>();
    private const float colorThreshold = 0.1f; // Threshold for color comparison

    private void OnEnable()
    {
        onColorSpawnedListener.Response.AddListener(SetColor);
        onColorChoiceListener.Response.AddListener(SetSelectedButton);
    }

    private void OnDisable()
    {
        ClearButtons();
        onColorSpawnedListener.Response.RemoveListener(SetColor);
        onColorChoiceListener.Response.RemoveListener(SetSelectedButton);
    }
    
    public void ClearButtons()
    {
        for (int i = 0; i < colorPickingButtons.Count; i++)
        {
            StoreColorPick storeColorPick = colorPickingButtons[i].GetComponent<StoreColorPick>();
            colorPickingButtons[i].GetComponent<SpriteRenderer>().color = Color.white;
            storeColorPick.IsOccupied = false;
            storeColorPick.color = Color.white;
            colorPickingButtons[i].transform.localScale = new Vector3(0, 0, 0);
            colorPickingButtonsLock[i].GetComponent<ButtonLogic>().UnlockButton();
            colorPickingButtonsLock[i].GetComponent<ButtonLogic>().RemoveAsSelected();
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

    public void SetSelectedButton(Color color)
    {
        for (int i = 0; i < colorPickingButtons.Count; i++)
        {
            StoreColorPick storeColorPick = colorPickingButtons[i].GetComponent<StoreColorPick>();
            if (storeColorPick.color == color)
            {
                colorPickingButtonsLock[i].GetComponent<ButtonLogic>().SetAsSelected();
            }
            else
            {
                colorPickingButtonsLock[i].GetComponent<ButtonLogic>().RemoveAsSelected();
            }
        }
    }

    private void UpdateUnlockableUI(Color unlockableColor)
    {
        for (int i = 0; i < colorPickingButtons.Count; i++)
        {
            StoreColorPick storeColorPick = colorPickingButtons[i].GetComponent<StoreColorPick>();
            if (storeColorPick.color == unlockableColor)
            {
                colorPickingButtons[i].transform.localScale = new Vector3(0, 0, 0);
                colorPickingButtons[i].transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBounce);
                colorPickingButtonsLock[i].GetComponent<ButtonLogic>().UnlockButton();
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
        for (int i = 0; i < colorPickingButtons.Count; i++)
        {
            StoreColorPick storeColorPick = colorPickingButtons[i].GetComponent<StoreColorPick>();
            if (!storeColorPick.IsOccupied)
            {
                numberOfColors += 1;
                colorPickingButtons[i].transform.localScale = new Vector3(0, 0, 0);
                colorPickingButtons[i].GetComponent<SpriteRenderer>().color = color;
                storeColorPick.color = color;
                storeColorPick.IsOccupied = true;
                if (numberOfColors < 4)
                {
                    colorPickingButtons[i].transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBounce);
                }
                else
                {
                    unlockableColors.Add(color);
                    colorPickingButtonsLock[i].GetComponent<ButtonLogic>().LockButton();
                }
                break;
            }
        }
    }
}
