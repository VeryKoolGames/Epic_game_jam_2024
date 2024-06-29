using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UIElements;

public class CompareManager : MonoBehaviour
{
    public FollowCursor followCursor;
    public PaintingParser paintingParser;
    private Color[] paintToCopy;
    private Color[] paintFinal;
    private float nCorrect;
    private float pCorrect;
    private float compensation;
    
    public void GetPercentageCorrect()
    {
        paintToCopy = paintingParser.GetPaintColors();
        paintFinal = followCursor.GetAllPixels();
        Debug.Log("First pixel of paint: " + paintToCopy[0]);
        Debug.Log("First pixel of final: " + paintFinal[0]);
        nCorrect = paintFinal.Where((x, i) => ColorsAreSimilar(x, paintToCopy[i])).Count();
        compensation = HasPlayed() ? 5 : 0; // added 5% more to be happy
        pCorrect = (float)nCorrect / paintFinal.Length * 100 + compensation;
        float ret = (pCorrect > 100 ? 100 : pCorrect);
        Debug.Log("Correct: " + ret);
        // return ret;
    }

    public bool ColorsAreSimilar(Color a, Color b, float tolerance = 0.05f)
    {
        return Mathf.Abs(a.r - b.r) < tolerance &&
               Mathf.Abs(a.g - b.g) < tolerance &&
               Mathf.Abs(a.b - b.b) < tolerance &&
               Mathf.Abs(a.a - b.a) < tolerance;
    }

    private bool HasPlayed()
    {
        int totalWhite = paintFinal.Where((x, i) => x.Equals(Color.white)).Count();
        return(totalWhite != paintFinal.Length);
    }
}
