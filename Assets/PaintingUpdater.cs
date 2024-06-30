using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PaintingUpdater : MonoBehaviour
{
    [SerializeField] private FadeEffectController fadeEffectController;
    [SerializeField] private List<Sprite> paintings = new List<Sprite>();
    [SerializeField] private List<Sprite> paintingCopy = new List<Sprite>();
    private int currentPaintingIndex = 0;
    // Start is called before the first frame update

    private void OnEnable()
    {
        paintingCopy = new List<Sprite>(paintings);
        fadeEffectController.FirstFade(GetRandomPainting());
    }
    
    private void CopyPaintings()
    {
        paintingCopy = new List<Sprite>(paintings);
    }

    private Sprite GetRandomPainting()
    {
        if (paintingCopy.Count == 0)
        {
            return null;
        }
        Sprite RandomSprite = paintingCopy[Random.Range(0, paintingCopy.Count)];
        paintingCopy.Remove(RandomSprite);
        return RandomSprite;
    }

    public void OnPaintingChange()
    {
        fadeEffectController.FadePainting(GetRandomPainting());
    }

    private void OnDisable()
    {
        paintingCopy = paintings;
    }
}
