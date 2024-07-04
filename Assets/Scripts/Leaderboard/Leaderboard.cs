using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
    private string leaderboardFilePath;
    private static int entryId = 0;

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
        entry.id = entryId++;
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
    
    public void DeleteAllEntries()
    {
        entries.Clear();
        SaveLeaderboard();
    }
    
    public void DeleteEntryFromLeaderboard(int id)
    {
        Debug.Log("Deleting entry with id: " + id);
        LeaderboardEntry entry = entries.Find(e => e.id == id);
        if (entry != null)
        {
            entries.Remove(entry);
            SaveLeaderboard();
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