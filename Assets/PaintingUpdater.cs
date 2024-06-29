using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingUpdater : MonoBehaviour
{
    [SerializeField] private FadeEffectController fadeEffectController;
    [SerializeField] private Sprite[] paintings;
    private int currentPaintingIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnPaintingChange()
    {
        fadeEffectController.FadePainting(paintings[currentPaintingIndex]);
        currentPaintingIndex++;
    }
}
