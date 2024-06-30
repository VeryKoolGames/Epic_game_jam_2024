using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using Random = UnityEngine.Random;

public class PaintingUpdater : MonoBehaviour
{
    [SerializeField] private FadeEffectController fadeEffectController;
    [SerializeField] private List<Sprite> paintings = new List<Sprite>();
    [SerializeField] private List<Sprite> paintingCopy = new List<Sprite>();
    private List<EventInstance> mainMusics = new List<EventInstance>();
    private EventInstance currentMusic;
    private int currentPaintingIndex = 0;
    // Start is called before the first frame update

    private void OnEnable()
    {
        mainMusics.Add(AudioManager.Instance.CreateInstance(FmodEvents.Instance.musicOne));
        mainMusics.Add(AudioManager.Instance.CreateInstance(FmodEvents.Instance.musicTwo));
        mainMusics.Add(AudioManager.Instance.CreateInstance(FmodEvents.Instance.musicThree));
        paintingCopy = new List<Sprite>(paintings);
        mainMusics[0].start();
        currentMusic = mainMusics[0];
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
        if (currentPaintingIndex < mainMusics.Count - 1)
            currentPaintingIndex++;
        currentMusic.stop(STOP_MODE.ALLOWFADEOUT);
        mainMusics[currentPaintingIndex].start();
        currentMusic = mainMusics[currentPaintingIndex];
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
        currentMusic.stop(STOP_MODE.ALLOWFADEOUT);
        paintingCopy = paintings;
    }
}
