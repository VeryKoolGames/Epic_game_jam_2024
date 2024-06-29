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
    // [SerializeField] private CanvasSaver canvasSaver;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTimerUI();
        baseTimeToFinish = timeToFinish;
    }

    void Update()
    {
        timeToFinish -= Time.deltaTime * currentSpeed;
        if (timeToFinish <= 0)
        {
            timeToFinish = baseTimeToFinish;
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
}
