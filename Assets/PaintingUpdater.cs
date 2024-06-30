using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingUpdater : MonoBehaviour
{
    [SerializeField] private FadeEffectController fadeEffectController;
    [SerializeField] private List<Sprite> paintings = new List<Sprite>();
    [SerializeField] private List<Sprite> paintingCopy = new List<Sprite>();
    private int currentPaintingIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        paintingCopy = paintings;
        fadeEffectController.FirstFade(GetRandomPainting());
    }
    
    private Sprite GetRandomPainting()
    {
        if (paintingCopy.Count == 0)
        {
            Debug.Log("No more paintings to show!");
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
}
