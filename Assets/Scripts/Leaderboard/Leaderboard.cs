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
    
    private void DeleteFile()
    {
        if (File.Exists(leaderboardFilePath))
        {
            Debug.Log("Deleting leaderboard file");
            File.Delete(leaderboardFilePath);
        }
    }

    public void AddEntry(LeaderboardEntry entry)
    {
        entries.Add(entry);
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