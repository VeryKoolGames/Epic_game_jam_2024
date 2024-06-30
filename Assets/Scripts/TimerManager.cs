using System;
using System.Collections;
using System.Collections.Generic;
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
    private bool isDelay = true;
    private bool isTimerRunning = true;
    // [SerializeField] private CanvasSaver canvasSaver;
    // Start is called before the first frame update
    // void Start()
    // {
    //     UpdateTimerUI();
    //     baseTimeToFinish = timeToFinish;
    //     StartCoroutine(delayStart());
    // }

    private void OnEnable()
    {
        baseTimeToFinish = timeToFinish;
        currentSpeed = 1;
        UpdateTimerUI();
        StartCoroutine(delayStart());
    }
    
    public void StartTimer()
    {
        isTimerRunning = true;
    }
    
    public void StopTimer()
    {
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
        if (isDelay || !isTimerRunning)
        {
            return;
        }
        timeToFinish -= Time.deltaTime * currentSpeed;
        if (timeToFinish <= 0)
        {
            timeToFinish = baseTimeToFinish;
            currentSpeed = 1;
            onTimerEnd.Invoke();
        }
        UpdateTimerUI();
    }
    
    public void ResetTimer()
    {
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
        currentSpeed = 60;
    }

    private void OnDisable()
    {
        timeToFinish = baseTimeToFinish;
        currentSpeed = 1;
        UpdateTimerUI();
    }
}
