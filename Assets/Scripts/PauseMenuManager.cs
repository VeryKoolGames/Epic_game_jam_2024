using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private UnityEvent onGamePaused;
    [SerializeField] private UnityEvent onGameRestart;
    public void Restart()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        onGameRestart.Invoke();
    }

    private void OnEnable()
    {
        onGamePaused.Invoke();
    }
}
