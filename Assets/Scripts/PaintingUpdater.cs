using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using Random = UnityEngine.Random;

public class PaintingUpdater : MonoBehaviour
{
    [SerializeField] private FadeEffectController fadeEffectController;
    private List<Sprite> paintings = new List<Sprite>();
    private List<Sprite> paintingCopy = new List<Sprite>();
    private List<EventInstance> mainMusics = new List<EventInstance>();
    private EventInstance currentMusic;
    private int currentPaintingIndex = 0;

    private void Awake()
    {
        LoadPaintingsFromResources();
    }

    private void OnEnable()
    {
        currentPaintingIndex = 0;
        mainMusics.Add(AudioManager.Instance.CreateInstance(FmodEvents.Instance.musicOne));
        mainMusics.Add(AudioManager.Instance.CreateInstance(FmodEvents.Instance.musicTwo));
        mainMusics.Add(AudioManager.Instance.CreateInstance(FmodEvents.Instance.musicThree));
        paintingCopy = new List<Sprite>(paintings);
        mainMusics[0].start();
        currentMusic = mainMusics[0];
        fadeEffectController.FirstFade(GetRandomPainting());
    }

    private void LoadPaintingsFromResources()
    {
        paintings.Clear();
        paintingCopy.Clear();
        
        paintings.Add(Resources.Load<Sprite>("Oeuvre1"));
        paintings.Add(Resources.Load<Sprite>("Oeuvre3"));
        paintings.Add(Resources.Load<Sprite>("Oeuvre4"));

        paintingCopy.AddRange(paintings);
    }

    private Sprite GetRandomPainting()
    {
        if (paintingCopy.Count == 0)
        {
            CopyPaintings();
        }

        if (currentPaintingIndex < mainMusics.Count - 1)
            currentPaintingIndex++;
        currentMusic.stop(STOP_MODE.ALLOWFADEOUT);
        mainMusics[currentPaintingIndex].start();
        currentMusic = mainMusics[currentPaintingIndex];

        Sprite randomSprite = paintingCopy[Random.Range(0, paintingCopy.Count)];
        paintingCopy.Remove(randomSprite);
        return randomSprite;
    }

    private void CopyPaintings()
    {
        paintingCopy = new List<Sprite>(paintings);
    }

    public void OnPaintingChange()
    {
        fadeEffectController.FadePainting(GetRandomPainting());
    }

    private void OnDisable()
    {
        currentMusic.stop(STOP_MODE.ALLOWFADEOUT);
        mainMusics.Clear();
        paintingCopy = new List<Sprite>(paintings);
    }
}
