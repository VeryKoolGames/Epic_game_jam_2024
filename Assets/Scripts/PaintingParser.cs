using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class PaintingParser : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private Sprite sprite;
    private Color[] colors;
    public Color redColor;
    public Color blueColor;
    public Color yellowColor; // Changed from green to yellow
    public Color defaultColor = new Color(0, 0, 0, 0);
    public Color unlockColor1;
    public Color unlockColor2;
    public Color unlockColor3;
    public Color unlockColor4;
    [SerializeField] private OnColorParsedEvent onColorParsedEvent;

    private float closestDistanceToRed = float.MaxValue;
    private float closestDistanceToBlue = float.MaxValue;
    private float closestDistanceToYellow = float.MaxValue;

    private Color closestToRedColor = Color.black;
    private Color closestToBlueColor = Color.black;
    private Color closestToYellowColor = Color.black;

    void Start()
    {
        sprite = spriteRenderer.sprite;
        // if (sprite != null)
        // {
        //     ParseSpriteColors();
        // }
    }
    
    public void ResetAll()
    {
        closestDistanceToRed = float.MaxValue;
        closestDistanceToBlue = float.MaxValue;
        closestDistanceToYellow = float.MaxValue;
        unlockColor1 = defaultColor;
        unlockColor2 = defaultColor;
        unlockColor3 = defaultColor;
        unlockColor4 = defaultColor;

        closestToRedColor = Color.black;
        closestToBlueColor = Color.black;
        closestToYellowColor = Color.black;
    }

    private void OnDisable()
    {
        ResetAll();
    }

    void ParseSpriteColors()
    {
        Texture2D texture = sprite.texture;
        colors = texture.GetPixels();
        List<Color> dominantColors = GetDominantColors(texture, 7);
        
        foreach (Color color in dominantColors)
        {
            GetClosestColor(color);
        }
        foreach (Color color in dominantColors)
        {
            getLastColorToUnlock(color);
        }

        redColor = closestToRedColor;
        blueColor = closestToBlueColor;
        yellowColor = closestToYellowColor;

        onColorParsedEvent.Raise(redColor);
        onColorParsedEvent.Raise(yellowColor);
        onColorParsedEvent.Raise(blueColor);
        onColorParsedEvent.Raise(unlockColor1);
        onColorParsedEvent.Raise(unlockColor2);
        onColorParsedEvent.Raise(unlockColor3);
        onColorParsedEvent.Raise(unlockColor4);
    }

    private List<Color> GetDominantColors(Texture2D tex, int numberOfColors)
    {
        Color[] pixels = tex.GetPixels();

        // Convert colors to a simplified form to reduce the number of unique colors
        List<Color> colorList = new List<Color>();
        foreach (Color pixel in pixels)
        {
            colorList.Add(new Color(
                Mathf.Round(pixel.r * 10) / 10.0f,
                Mathf.Round(pixel.g * 10) / 10.0f,
                Mathf.Round(pixel.b * 10) / 10.0f,
                pixel.a));
        }

        // Group by color and count occurrences
        var colorGroups = colorList.GroupBy(color => color).OrderByDescending(group => group.Count()).Take(numberOfColors);

        // Return the most frequent colors
        return colorGroups.Select(group => group.Key).ToList();
    }

    private void getLastColorToUnlock(Color newColor)
    {
        if (newColor == Color.white)
        {
            return;
        }
        if (newColor == closestToRedColor)
        {
            return;
        }
        if (newColor == closestToBlueColor)
        {
            return;
        }
        if (newColor == closestToYellowColor)
        {
            return;
        }
        
        if (unlockColor1 == defaultColor)
        {
            unlockColor1 = newColor;
            return;
        }
        
        if (unlockColor2 == defaultColor)
        {
            unlockColor2 = newColor;
            return;
        }
        
        if (unlockColor3 == defaultColor)
        {
            unlockColor3 = newColor;
            return;
        }
        
        if (unlockColor4 == defaultColor)
        {
            unlockColor4 = Color.white;
            return;
        }
    }
    
    public void parsePainting()
    {
        Debug.Log("Parsing painting");
        if (spriteRenderer.sprite == null)
        {
            Debug.Log("No sprite found");
            return;
        }
        ResetAll();
        sprite = spriteRenderer.sprite;
        ParseSpriteColors();
    }

    private void GetClosestColor(Color newColor)
    {
        if (newColor == Color.white)
        {
            return;
        }

        float distanceToRed = ColorDistance(newColor, Color.red);
        if (distanceToRed < closestDistanceToRed)
        {
            closestDistanceToRed = distanceToRed;
            closestToRedColor = newColor;
        }

        float distanceToBlue = ColorDistance(newColor, Color.blue);
        if (distanceToBlue < closestDistanceToBlue)
        {
            closestDistanceToBlue = distanceToBlue;
            closestToBlueColor = newColor;
        }

        float distanceToYellow = ColorDistance(newColor, new Color32( 255 , 230 , 153 , 255 ));
        if (distanceToYellow < closestDistanceToYellow)
        {
            closestDistanceToYellow = distanceToYellow;
            closestToYellowColor = newColor;
        }
    }

    private float ColorDistance(Color c1, Color c2)
    {
        float rDiff = c1.r - c2.r;
        float gDiff = c1.g - c2.g;
        float bDiff = c1.b - c2.b;
        return Mathf.Sqrt(rDiff * rDiff + gDiff * gDiff + bDiff * bDiff);
    }

    public Color[] GetPaintColors()
    {
        return colors;
    }
}
