using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EndPopUpManager : MonoBehaviour
{
    [SerializeField] private GameObject window;
    // Start is called before the first frame update
    public void CloseWindow()
    {
        window.transform.DOScale(0f, .2f).OnComplete((() =>
        {
            window.SetActive(false);
            gameObject.SetActive(false);
        }));
    }
}
