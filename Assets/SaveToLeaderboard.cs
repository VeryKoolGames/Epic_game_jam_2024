using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class SaveToLeaderboard : MonoBehaviour
{
    [SerializeField] private Leaderboard leaderboard;
    [SerializeField] private CompareManager compareManager;
    [SerializeField] private TextMeshProUGUI playerTextInput;
    [SerializeField] private SpriteSaver spriteSaver;
    // Start is called before the first frame update

    private void OnEnable()
    {
        transform.DOScale(1f, .2f);
    }

    public void AddLeaderBoardEntry()
    {
        string playerName = playerTextInput.text;
        int percentage = compareManager.GetAveragePercentage();
        
        leaderboard.AddEntry(new LeaderboardEntry(playerName, percentage, spriteSaver.GetSprites()));
    }

    private void OnDisable()
    {
        gameObject.SetActive(false);
    }
}
