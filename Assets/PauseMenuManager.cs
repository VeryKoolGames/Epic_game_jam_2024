using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public void Restart()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
