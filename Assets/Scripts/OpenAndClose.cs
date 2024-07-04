using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndClose : MonoBehaviour
{
    [SerializeField] private GameObject popUp;
    private bool isOpen = false;
    // Start is called before the first frame update
    public void OpenClose()
    {
        if (isOpen)
        {
            popUp.SetActive(false);
            isOpen = false;
        }
        else
        {
            popUp.SetActive(true);
            isOpen = true;
        }
    }
}
