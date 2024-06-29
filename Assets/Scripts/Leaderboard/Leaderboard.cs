using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
    private string leaderboardFilePath;

    void Awake()
    {
        leaderboardFilePath = Path.Combine(Application.persistentDataPath, "leaderboard.json");
        LoadLeaderboard();
    }

    public void AddEntry(string playerName, float completionPercentage, Sprite sprite)
    {
        entries.Add(new LeaderboardEntry(playerName, completionPercentage, sprite));
        SaveLeaderboard();
    }

    public void SaveLeaderboard()
    {
        string json = JsonUtility.ToJson(new LeaderboardData(entries));
        File.WriteAllText(leaderboardFilePath, json);
    }

    public void LoadLeaderboard()
    {
        if (File.Exists(leaderboardFilePath))
        {
            string json = File.ReadAllText(leaderboardFilePath);
            LeaderboardData data = JsonUtility.FromJson<LeaderboardData>(json);
            entries = data.entries;
        }
    }

    [System.Serializable]
    private class LeaderboardData
    {
        public List<LeaderboardEntry> entries;

        public LeaderboardData(List<LeaderboardEntry> entries)
        {
            this.entries = entries;
        }
    }
}