using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject gameWindow;
    [SerializeField] private GameObject loadingWindow;

    [SerializeField] private UnityEvent onGameSceneClosed;
    // Start is called before the first frame update
    
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
    
}
