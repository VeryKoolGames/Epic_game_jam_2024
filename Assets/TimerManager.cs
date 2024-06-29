using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float timeToFinish;
    [SerializeField] private TextMeshProUGUI timerText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTimerUI(); // Initial update to set the timer text at the start
    }

    void Update()
    {
        timeToFinish -= Time.deltaTime;
        if (timeToFinish <= 0)
        {
            timeToFinish = 0;
            Debug.Log("Timer finished");
        }
        UpdateTimerUI(); // Update the timer UI every frame
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeToFinish / 60);
        int seconds = Mathf.FloorToInt(timeToFinish % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
