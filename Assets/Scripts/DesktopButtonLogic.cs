using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopButtonLogic : MonoBehaviour
{
    [SerializeField] private GameObject outline;

    private void OnMouseEnter()
    {
        outline.SetActive(true);
    }
    
    private void OnMouseExit()
    {
        outline.SetActive(false);
    }
}
