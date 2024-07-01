using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject gameWindow;
    [SerializeField] private GameObject loadingWindow;
    [SerializeField] private GameObject creditWindow;
    [SerializeField] private GameObject gtaWindow;
    [SerializeField] private CanvasGroup logoCanvasGroup;
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private GameObject startCanvas;

    [SerializeField] private UnityEvent onGameSceneClosed;
    // Start is called before the first frame update
    private void Awake()
    {
        Cursor.visible = false;
        OnGameLaunch();
    }

    public void LaunchLoadingWindow()
    {
        loadingWindow.SetActive(true);
    }
    
    public void CloseGameWindow()
    {
        Vector3 gameWindowScale = gameWindow.transform.localScale;
        gameWindow.transform.DOScale(0f, .2f).OnComplete(() =>
        {
            onGameSceneClosed.Invoke();
            gameWindow.SetActive(false);
            gameWindow.transform.localScale = gameWindowScale;
        });
    }

    public void LauchCreditScene()
    {
        creditWindow.SetActive(true);
    }
    
    public void LaunchGtaScene()
    {
        gtaWindow.SetActive(true);
    }

    
    private void OnGameLaunch()
    {
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.windowsStart, transform.position);
        StartCoroutine(SteppedFade(logoCanvasGroup, 1f, 3, 15));
        StartCoroutine(DelayFadeOut(5f, logoCanvasGroup));
        StartCoroutine(DelayFadeOut(7f, fadeCanvasGroup));
        StartCoroutine(DelayMusicStart(6f));
        StartCoroutine(DelayCanvasDeactivation(8f));
    }

    private IEnumerator SteppedFade(CanvasGroup canvasGroup, float targetAlpha, float duration, int steps)
    {
        float stepDuration = duration / steps;
        float alphaIncrement = (targetAlpha - canvasGroup.alpha) / steps;

        for (int i = 0; i < steps; i++)
        {
            canvasGroup.alpha += alphaIncrement;
            yield return new WaitForSeconds(stepDuration);
        }
        canvasGroup.alpha = targetAlpha;
    }
    
    private IEnumerator DelayFadeOut(float delay, CanvasGroup fadeTarget)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(SteppedFade(fadeTarget, 0, 2f, 15));
    }
    
    private IEnumerator DelayMusicStart(float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.oldComputerSound, transform.position);
    }
    
    private IEnumerator DelayCanvasDeactivation(float delay)
    {
        yield return new WaitForSeconds(delay);
        startCanvas.SetActive(false);
    }
    
}
