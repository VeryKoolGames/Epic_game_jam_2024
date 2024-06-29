using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour
{
    public Leaderboard leaderboard;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerScoreText;
    public Image playerSprite;

    void Start()
    {
        DisplayLeaderboard();
    }

    void DisplayLeaderboard()
    {

        foreach (LeaderboardEntry entry in leaderboard.entries)
        {
            playerNameText.text = entry.playerName;
            playerScoreText.text = $"{entry.completionPercentage}%";
            playerSprite.sprite = LeaderboardEntry.Base64ToSprite(entry.spriteBase64);
        }
    }
}