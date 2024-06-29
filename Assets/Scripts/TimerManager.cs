using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float timeToFinish;
    [SerializeField] private int currentSpeed = 1;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private CanvasSaver canvasSaver;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTimerUI();
    }

    void Update()
    {
        timeToFinish -= Time.deltaTime * currentSpeed;
        if (timeToFinish <= 0)
        {
            timeToFinish = 0;
            // canvasSaver.SaveCanvasToLeaderboard("Jambon", 1000);
            // Should raise an event to finish game here
        }
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
