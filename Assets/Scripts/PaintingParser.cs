using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PaintingParser : MonoBehaviour
{
    public Sprite sprite;
    private Color[] colors;
    public Color redColor;
    public Color blueColor;
    public Color greenColor;
    [SerializeField] private OnColorParsedEvent onColorParsedEvent;

    void Start()
    {
        if (sprite != null)
        {
            ParseSpriteColors();
        }
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
            GetHighestRValue(color);
        }
        onColorParsedEvent.Raise(redColor);
        onColorParsedEvent.Raise(greenColor);
        onColorParsedEvent.Raise(blueColor);
    }
    
    private void GetHighestRValue(Color newColor)
    {
        if (newColor.r > redColor.r)
        {
            redColor = newColor;
        }
        else if (newColor.g > greenColor.g)
        {
            greenColor = newColor;
        }
        else if (newColor.b > blueColor.b)
        {
            blueColor = newColor;
        }
    }
}
