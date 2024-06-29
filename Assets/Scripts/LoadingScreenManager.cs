using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LoadingScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject[] loadingSegments;
    [SerializeField] private GameObject gameWindow;
    // Start is called before the first frame update
    void OnEnable()
    {
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
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(0f, .2f).OnComplete(() =>
        {
            gameWindow.SetActive(true);
            gameWindow.transform.localScale = Vector3.zero;
        }));
        sequence.Append(gameWindow.transform.DOScale(1f, .2f).OnComplete(() =>
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
