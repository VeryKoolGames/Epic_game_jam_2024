using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishPopUpManager : MonoBehaviour
{
    [SerializeField] private GameObject[] finishRankings;
    [SerializeField] private TextMeshProUGUI rankingText;
    [SerializeField] private String[] rankingTexts;
    [SerializeField] private CompareManager compareManager;
    [SerializeField] private TextMeshProUGUI percentageText;

    private int rank;
    private int percentage;
    // Start is called before the first frame update

    private void OnEnable()
    {
       rank = compareManager.GetPercentageCorrect();
       percentage = compareManager.GetIntPercentage();
       ShowFinishPopUp(rank);
    }

    public void ShowFinishPopUp(int ranking)
    {
        for (int i = 0; i < ranking + 1; i++)
        {
            finishRankings[i].SetActive(true);
        }

        if (ranking <= 1)
        {
            AudioManager.Instance.PlayOneShot(FmodEvents.Instance.resultBad, transform.position);
        }
        else if (ranking <= 3)
        {
            AudioManager.Instance.PlayOneShot(FmodEvents.Instance.resultMedium, transform.position);
        }
        else
        {
            AudioManager.Instance.PlayOneShot(FmodEvents.Instance.resultGood, transform.position);
        }
        rankingText.text = rankingTexts[ranking];
        percentageText.text = "***" + percentage + "%" + "***";
    }

    private void OnDisable()
    {
        for (int i = 0; i < finishRankings.Length; i++)
        {
            finishRankings[i].SetActive(false);
        }
    }
    public void CloseFinishPopUp()
    {
        gameObject.SetActive(false);
    }
}
