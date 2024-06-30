using System;
using System.Collections;
using UnityEngine;

public class FadeEffectController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer paintingRenderer;
    [SerializeField] private SpriteRenderer overlayRenderer;
    [SerializeField] private PaintingParser paintingParser;
    public float fadeDuration = 2.0f;
    private bool startFade = false;
    private bool fadeOut = true; // Determines the direction of the fade
    private int fadeSteps = 20; // Number of steps in the fade effect

    private Vector3 initialOverlayScale;

    private void Start()
    {
        overlayRenderer.color = Color.black;
        initialOverlayScale = overlayRenderer.transform.localScale;
    }

    public void FadePainting(Sprite painting)
    {
        StartFadeOut();
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.tableauArrival, transform.position);
        StartCoroutine(WaitFade(painting));
    }

    public void FirstFade(Sprite painting)
    {
        paintingRenderer.sprite = painting;
        StartCoroutine(DelayStart());
    }

    private IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(1);
        paintingParser.parsePainting();
        Debug.Log("Parsing painting in first fade");
        StartFadeIn();
    }

    private IEnumerator WaitFade(Sprite painting)
    {
        yield return new WaitForSeconds(1);
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.tableauDeparture, transform.position);
        paintingRenderer.sprite = painting;
        paintingParser.parsePainting();
        StartFadeIn();
    }

    private void Update()
    {
        if (startFade)
        {
            float stepTime = fadeDuration / fadeSteps;
            StartCoroutine(ApplyFadeEffect(stepTime));
        }
    }

    private IEnumerator ApplyFadeEffect(float stepTime)
    {
        startFade = false;
        float stepScale = initialOverlayScale.y / fadeSteps;

        if (fadeOut)
        {
            for (int i = 0; i <= fadeSteps; i++)
            {
                overlayRenderer.transform.localScale = new Vector3(initialOverlayScale.x, stepScale * i, initialOverlayScale.z);
                yield return new WaitForSeconds(stepTime);
            }
        }
        else
        {
            for (int i = fadeSteps; i >= 0; i--)
            {
                overlayRenderer.transform.localScale = new Vector3(initialOverlayScale.x, stepScale * i, initialOverlayScale.z);
                yield return new WaitForSeconds(stepTime);
            }
        }
        overlayRenderer.transform.localScale = new Vector3(initialOverlayScale.x, fadeOut ? initialOverlayScale.y : 0, initialOverlayScale.z);
    }

    public void StartFadeOut()
    {
        overlayRenderer.transform.localScale = new Vector3(initialOverlayScale.x, 0, initialOverlayScale.z);
        fadeOut = true;
        startFade = true;
    }

    public void StartFadeIn()
    {
        overlayRenderer.transform.localScale = initialOverlayScale;
        fadeOut = false;
        startFade = true;
    }

    private void OnDisable()
    {
        overlayRenderer.transform.localScale = initialOverlayScale;
    }
}
