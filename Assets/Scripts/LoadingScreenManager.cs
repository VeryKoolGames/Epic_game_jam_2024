using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using FMOD.Studio;
using UnityEngine;

public class LoadingScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject[] loadingSegments;
    [SerializeField] private GameObject gameWindow;
    private EventInstance loadingSound;

    // Start is called before the first frame update
    void OnEnable()
    {
        loadingSound = AudioManager.Instance.CreateInstance(FmodEvents.Instance.loadingSound);
        loadingSound.start();
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, .2f).OnComplete((() => StartCoroutine(StartLoading())));
    }

    private IEnumerator StartLoading()
    {
        foreach (var segment in loadingSegments)
        {
            segment.SetActive(true);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        }
        LaunchGameScene();
    }

    private void LaunchGameScene()
    {
        Vector3 gameWindowScale = new Vector3(1.3359f, 1.3359f, 1.3359f);
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(0f, .2f).OnComplete(() =>
        {
            loadingSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            gameWindow.SetActive(true);
            gameWindow.transform.localScale = Vector3.zero;
        }));
        sequence.Append(gameWindow.transform.DOScale(gameWindowScale, .2f).OnComplete(() =>
        {
            ResetGameObjects();
            gameObject.SetActive(false);
        }));
    }
    
    private void ResetGameObjects()
    {
        foreach (var segment in loadingSegments)
        {
            segment.SetActive(false);
        }
    }
    
}
