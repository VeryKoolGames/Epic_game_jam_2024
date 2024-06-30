using System.Collections;
using System.Collections.Generic;
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
        if (sprite != null)
        {
            ParseSpriteColors();
        }
    }
    
    private void ResetAll()
    {
        closestDistanceToRed = float.MaxValue;
        closestDistanceToBlue = float.MaxValue;
        closestDistanceToYellow = float.MaxValue;

        closestToRedColor = Color.black;
        closestToBlueColor = Color.black;
        closestToYellowColor = Color.black;
    }

    void ParseSpriteColors()
    {
        Texture2D texture = sprite.texture;

        colors = texture.GetPixels((int)sprite.textureRect.x,
            (int)sprite.textureRect.y,
            (int)sprite.textureRect.width,
            (int)sprite.textureRect.height);

        foreach (Color color in colors)
        {
            GetClosestColor(color);
        }

        redColor = closestToRedColor;
        blueColor = closestToBlueColor;
        yellowColor = closestToYellowColor;

        Debug.Log("Red: " + redColor);
        Debug.Log("Yellow: " + yellowColor);
        Debug.Log("Blue: " + blueColor);

        onColorParsedEvent.Raise(redColor);
        onColorParsedEvent.Raise(yellowColor);
        onColorParsedEvent.Raise(blueColor);
    }
    
    public void parsePainting()
    {
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

        float distanceToYellow = ColorDistance(newColor, new Color(1.0f, 1.0f, 0.0f));
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
