using System;
using System.Collections;
using UnityEngine;

public class FadeEffectController : MonoBehaviour
{
    public Material fadeMaterial;
    [SerializeField] private SpriteRenderer paintingRenderer;
    public float fadeDuration = 2.0f;
    private float fadeAmount = 0.0f;
    private bool startFade = false;
    private bool fadeOut = true; // Determines the direction of the fade
    private int fadeSteps = 2;
    [SerializeField] private PaintingParser paintingParser;

    private void OnEnable()
    {
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.welcomeSound, transform.position);
    }

    public void FadePainting(Sprite painting)
    {
        StartFadeOut();
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.tableauArrival, transform.position);
        StartCoroutine(WaitFade(painting));
    }

    public void FirstFade(Sprite painting)
    {
        fadeAmount = 1.0f;
        fadeMaterial.SetFloat("_SetBlack", 1.0f);
        fadeMaterial.SetFloat("_Fade", fadeAmount);

        paintingRenderer.sprite = painting;

        StartCoroutine(delayStart());
    }
    
    IEnumerator delayStart()
    {
        yield return new WaitForSeconds(1);
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

    void Update()
    {
        if (startFade)
        {
            float stepIncrement = 1.0f / fadeSteps;

            if (fadeOut)
            {
                fadeAmount += stepIncrement * Time.deltaTime / fadeDuration;
                fadeMaterial.SetFloat("_Fade", fadeAmount);

                if (fadeAmount >= 1.0f)
                {
                    startFade = false;
                    fadeAmount = 1.0f;
                }
            }
            else
            {
                fadeAmount -= stepIncrement * Time.deltaTime / fadeDuration;
                fadeMaterial.SetFloat("_Fade", fadeAmount);

                if (fadeAmount <= 0.0f)
                {
                    startFade = false;
                    fadeAmount = 0.0f;
                }
            }
        }
    }

    public void StartFadeOut()
    {
        fadeAmount = 0.0f; // Ensure fade starts from fully visible
        fadeMaterial.SetFloat("_SetBlack", 0.0f);
        fadeMaterial.SetFloat("_Fade", fadeAmount);

        fadeOut = true;
        startFade = true;
    }

    public void StartFadeIn()
    {
        fadeAmount = 1.0f; // Ensure fade starts from fully black
        fadeMaterial.SetFloat("_SetBlack", 0.0f);
        fadeMaterial.SetFloat("_Fade", fadeAmount);

        fadeOut = false;
        startFade = true;
    }
}
