using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrushSizeUiHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] brushSizeRenderers;
    [SerializeField] private Sprite[] brushSizeSelectedRenderers;
    [SerializeField] private Sprite[] brushSizeUnSelectedRenderers;
    [SerializeField] private UnityEvent<int> OnBrushSizeSelected;
    // Start is called before the first frame update

    public void OnUIClicked(int id, int brushSize)
    {
        Debug.Log("Brush size clicked " + id);
        for (int i = 0; i < brushSizeRenderers.Length; i++)
        {
            if (i == id)
            {
                brushSizeRenderers[i].sprite = brushSizeSelectedRenderers[i];
                OnBrushSizeSelected.Invoke(brushSize);
            }
            else
            {
                brushSizeRenderers[i].sprite = brushSizeUnSelectedRenderers[i];
            }
        }
    }
}
