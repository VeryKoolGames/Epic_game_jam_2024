using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour
{
    public Leaderboard leaderboard;
    [SerializeField] private GameObject leaderboardEntryPrefab;
    [SerializeField] private GameObject leaderboardContent;
    [SerializeField] private GameObject textIfEmpty;

    void OnEnable()
    {
        Debug.Log("LeaderboardUI enabled: " + leaderboard.entries.Count);
        if (leaderboard.entries.Count == 0)
        {
            textIfEmpty.SetActive(true);
        }
        else 
            DisplayLeaderboard();
    }
    
    void DisplayLeaderboard()
    {
        foreach (Transform child in leaderboardContent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var entry in leaderboard.entries)
        {
            GameObject entryObj = Instantiate(leaderboardEntryPrefab, leaderboardContent.transform);
            entryObj.transform.Find("TextPlayerName").GetComponent<TextMeshProUGUI>().text = entry.playerName;
            entryObj.transform.Find("TextPlayerScore").GetComponent<TextMeshProUGUI>().text = entry.completionPercentage.ToString("F2") + "%";  
            entryObj.transform.Find("ImageTableauOne").GetComponent<Image>().sprite =
                LeaderboardEntry.Base64ToSprite(entry.spriteBase64One[0]);
            entryObj.transform.Find("ImageTableauTwo").GetComponent<Image>().sprite =
                LeaderboardEntry.Base64ToSprite(entry.spriteBase64One[1]);
            entryObj.transform.Find("ImageTableauThree").GetComponent<Image>().sprite =
                LeaderboardEntry.Base64ToSprite(entry.spriteBase64One[2]);
        }
    }

    private void OnDisable()
    {
        textIfEmpty.SetActive(false);
    }
}