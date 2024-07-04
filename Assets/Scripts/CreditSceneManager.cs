using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using FMOD.Studio;
using UnityEngine;

public class CreditSceneManager : MonoBehaviour
{
    private Vector3 baseScale;
    private void OnEnable()
    {

        baseScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(baseScale, .2f);
    }

    public void CloseCredits()
    {
        transform.DOScale(0f, .2f).OnComplete((() =>
        {
            gameObject.SetActive(false);
            transform.localScale = baseScale;
        }));
    }
}
