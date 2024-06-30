using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GeeTeeAyFiveOnClick : MonoBehaviour
{
    [SerializeField] private GameObject window;
    [SerializeField] private GeeTeeAyFiveManager gameManager;
    
    public void LaunchLoadingWindow()
    {
        //if (peintureXP.isComplete)
            window.SetActive(true);
            gameManager.ResetGameState();
    }

    public void CloseWindow()
    {
        window.SetActive(false);
    }
    
    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            window.SetActive(false);
        }
    }

}
