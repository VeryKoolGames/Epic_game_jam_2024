using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class EndPopUpManager : MonoBehaviour
{
    [SerializeField] private GameObject window;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private CompareManager compareManager;
    // Start is called before the first frame update
    
    private void OnEnable()
    {
        int rank = compareManager.GetAveragePercentage();
        resultText.text = "***" + rank + "%" + "***";
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.tadaSound, transform.position);
    }
    public void CloseWindow()
    {
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(1.5f);
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.goodByeSound, transform.position);
        sequence.Append(window.transform.DOScale(0f, .2f).OnComplete((() =>
        {
            window.SetActive(false);
            gameObject.SetActive(false);
        })));
    }

    private void OnDisable()
    {
        gameObject.SetActive(false);
    }
}
