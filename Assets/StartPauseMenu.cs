using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    // Start is called before the first frame update
    public void StartPause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
}
