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
    
    public float GetPercentageCorrect()
    {
        paintToCopy = paintingParser.GetPaintColors();
        paintFinal = followCursor.GetAllPixels();
        nCorrect = paintFinal.Where((x, i) => x.Equals(paintToCopy[i])).Count();
        compensation = HasPlayed() ? 5 : 0; // added 5% more to be happy
        pCorrect = nCorrect / paintFinal.Length * 100 + compensation;
        return (pCorrect > 100 ? 100 : pCorrect);
    }

    private bool HasPlayed()
    {
        int totalWhite = paintFinal.Where((x, i) => x.Equals(Color.white)).Count();
        return(totalWhite != paintFinal.Length);
    }
}
