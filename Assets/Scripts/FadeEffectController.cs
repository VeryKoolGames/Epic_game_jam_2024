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

    private void Start()
    {
        FadePainting(paintingRenderer.sprite);
    }

    public void FadePainting(Sprite painting)
    {
        StartFadeOut();
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.tableauArrival, transform.position);
        StartCoroutine(waitFade(painting));
    }
    
    private IEnumerator waitFade(Sprite painting)
    {
        yield return new WaitForSeconds(1);
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.tableauDeparture, transform.position);
        paintingRenderer.sprite = painting;
        StartFadeIn();
    }

    void Update()
    {
        if (startFade)
        {
            if (fadeOut)
            {
                fadeAmount += Time.deltaTime / fadeDuration;
                fadeMaterial.SetFloat("_Fade", fadeAmount);

                if (fadeAmount >= 1.0f)
                {
                    startFade = false;
                    fadeAmount = 1.0f;
                }
            }
            else
            {
                fadeAmount -= Time.deltaTime / fadeDuration;
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
        fadeOut = true;
        startFade = true;
    }

    public void StartFadeIn()
    {
        fadeOut = false;
        startFade = true;
    }
}