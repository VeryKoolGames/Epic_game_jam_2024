using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float timeToFinish;
    [SerializeField] private FollowCursor follow;
    private float baseTimeToFinish;
    [SerializeField] private int currentSpeed = 1;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private UnityEvent onTimerEnd;
    [SerializeField] private UnityEvent onTimerRestart;
    [SerializeField] private GameObject finishPopUp;
    [SerializeField] private GameObject finalPopUp;
    private bool isDelay = true;
    private bool isTimerRunning = true;
    private int countPainting;
    private EventInstance accelerateSound;
    private bool isSafeMode;

    private void OnEnable()
    {
        isSafeMode = PlayerPrefs.GetInt("SafeMode") == 1;
        countPainting = 0;
        baseTimeToFinish = timeToFinish;
        currentSpeed = 1;
        if (isSafeMode)
        {
            timerText.text = "Safe Mode";
        }
        else
        {
            isTimerRunning = true;
            UpdateTimerUI();
            StartCoroutine(delayStart());
            accelerateSound = AudioManager.Instance.CreateInstance(FmodEvents.Instance.accelerateSound);
        }
    }
    
    public void StartTimer()
    {
        if (isSafeMode)
        {
            return;
        }
        countPainting++;
        isTimerRunning = true;
    }
    
    public void StopTimer()
    {
        if (isSafeMode)
        {
            return;
        }
        baseTimeToFinish = timeToFinish;
        isTimerRunning = false;
    }

    IEnumerator delayStart()
    {
        yield return new WaitForSeconds(2);
        isDelay = false;
    }

    void Update()
    {
        if (isDelay || !isTimerRunning || isSafeMode)
        {
            return;
        }
        timeToFinish -= Time.deltaTime * currentSpeed;
        if (timeToFinish <= 0)
        {
            if (currentSpeed == 60)
            {
                accelerateSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
            timeToFinish = baseTimeToFinish;
            currentSpeed = 1;
            StopTimer();
            if (countPainting < 2)
                finishPopUp.SetActive(true);
            else
            {
                finalPopUp.SetActive(true);
            }
            onTimerEnd.Invoke();
        }
        UpdateTimerUI();
    }
    
    public void ResetTimer()
    {
        if (isSafeMode)
        {
            return;
        }
        timeToFinish = baseTimeToFinish;
        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeToFinish / 60);
        int seconds = Mathf.FloorToInt(timeToFinish % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
    
    public void AccelerateTimer()
    {
        if (isSafeMode)
        {
            countPainting++;
            onTimerEnd.Invoke();
            if (countPainting < 3)
                finishPopUp.SetActive(true);
            else
            {
                finalPopUp.SetActive(true);
            }
        }
        else
        {
            accelerateSound.start();
            currentSpeed = 60;
        }
    }

    private void OnDisable()
    {
        timeToFinish = baseTimeToFinish;
        currentSpeed = 1;
        UpdateTimerUI();
    }
}
