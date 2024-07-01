using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class FmodEvents : MonoBehaviour
{
    public static FmodEvents Instance;
    public EventReference eraserSound;
    public EventReference doubleClickSound;
    public EventReference smallBrushSound;
    public EventReference musicOne;
    public EventReference musicTwo;
    public EventReference musicThree;
    public EventReference loadingSound;
    public EventReference splooshSound;
    public EventReference tableauArrival;
    public EventReference tableauDeparture;
    public EventReference singleClickSound;
    public EventReference welcomeSound;
    public EventReference kenBoom;
    public EventReference kenSploosh;
    public EventReference windowsStart;
    public EventReference windowsStop;
    public EventReference oldComputerSound;
    public EventReference goodByeSound;
    public EventReference resultBad;
    public EventReference resultMedium;
    public EventReference resultGood;
    public EventReference tadaSound;
    public EventReference middleBrushSound;
    public EventReference bigBrushSound;
    public EventReference accelerateSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}