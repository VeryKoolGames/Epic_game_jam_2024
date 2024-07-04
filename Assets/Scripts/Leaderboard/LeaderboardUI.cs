using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour
{
    public Leaderboard leaderboard;
    [SerializeField] private GameObject leaderboardEntryPrefab;
    [SerializeField] private GameObject leaderboardContent;
    [SerializeField] private GameObject textIfEmpty;
    [SerializeField] private OnDeletePaintingListener onDeletePaintingListener;

    void OnEnable()
    {
        onDeletePaintingListener.Response.AddListener(OnDeletePainting);
        transform.DOScale(1, .2f);
        if (leaderboard.entries.Count == 0)
        {
            textIfEmpty.SetActive(true);
        }
        else 
            DisplayLeaderboard();
    }
    
    public void OnDeletePainting(int id)
    {
        leaderboard.DeleteEntryFromLeaderboard(id);
        DisplayLeaderboard();
    }
    
    public void DisplayLeaderboard()
    {
        foreach (Transform child in leaderboardContent.transform)
        {
            Destroy(child.gameObject);
        }
        if (leaderboard.entries.Count == 0)
        {
            textIfEmpty.SetActive(true);
            return;
        }
        StartCoroutine(DelayedDisplayLeaderboard());
    }
    
    private IEnumerator DelayedDisplayLeaderboard()
    {
        int i = 0;
        foreach (var entry in leaderboard.entries)
        {
            GameObject entryObj = Instantiate(leaderboardEntryPrefab, leaderboardContent.transform);
            entryObj.GetComponent<StorePaintingId>().id = entry.id;
            entryObj.transform.Find("TextPlayerName").GetComponent<TextMeshProUGUI>().text = entry.playerName;
            entryObj.transform.Find("TextEntryNumber").GetComponent<TextMeshProUGUI>().text = "#" + i++;
            entryObj.transform.Find("TextPlayerScore").GetComponent<TextMeshProUGUI>().text = entry.completionPercentage.ToString("F2") + "%";  
            entryObj.transform.Find("ImageTableauOne").GetComponent<Image>().sprite =
                LeaderboardEntry.Base64ToSprite(entry.spriteBase64One[0]);
            entryObj.transform.Find("ImageTableauTwo").GetComponent<Image>().sprite =
                LeaderboardEntry.Base64ToSprite(entry.spriteBase64One[1]);
            entryObj.transform.Find("ImageTableauThree").GetComponent<Image>().sprite =
                LeaderboardEntry.Base64ToSprite(entry.spriteBase64One[2]);
            yield return new WaitForSeconds(.2f);
        }
    }
    
    public void ClearLeaderboard()
    {
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.tearPaper, transform.position);
        leaderboard.DeleteAllEntries();
        DisplayLeaderboard();
    }
    
    public void CloseLeaderboard()
    {
        transform.DOScale(0, .2f).OnComplete(() => gameObject.SetActive(false));
    }

    private void OnDisable()
    {
        onDeletePaintingListener.Response.RemoveListener(OnDeletePainting);
        textIfEmpty.SetActive(false);
    }
}