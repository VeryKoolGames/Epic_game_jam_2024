using System;
using UnityEngine;

public class SafeMode : MonoBehaviour
{
    private bool isSafeMode;

    [SerializeField] private GameObject safeModeButton;

    private void OnEnable()
    {
        PlayerPrefs.SetInt("SafeMode", 0);
        safeModeButton.SetActive(false);
    }

    private void OnMouseDown()
    {
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.singleClickSound, transform.position);
        isSafeMode = !isSafeMode;
        if (isSafeMode)
        {
            PlayerPrefs.SetInt("SafeMode", 1);
            safeModeButton.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("SafeMode", 0);
            safeModeButton.SetActive(false);
        }
    }
}
