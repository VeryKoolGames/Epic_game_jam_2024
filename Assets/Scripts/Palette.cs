using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Palette : MonoBehaviour
{
    private bool isMouseInZone;
    private bool isMouseButtonDown;
    private ColorsEnum currentColor = ColorsEnum.WHITE;
    private Color currentPaintColor = Color.white;
    private Color lastMix;
    private Color[] selectedColors = new Color[20];
    private int index = 0;
    [SerializeField] private OnColorChoiceListener onColorChoiceListener;
    private bool hasBlended;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Texture2D texture;
    private Vector2Int textureSize;
    private int id;

    private void Start()
    {
        texture = new Texture2D(spriteRenderer.sprite.texture.width, spriteRenderer.sprite.texture.height, spriteRenderer.sprite.texture.format, false);
        Graphics.CopyTexture(spriteRenderer.sprite.texture, texture);

        if (!texture.isReadable)
        {
            return;
        }

        textureSize = new Vector2Int(texture.width, texture.height);
        spriteRenderer.sprite = Sprite.Create(texture, spriteRenderer.sprite.rect, new Vector2(0.5f, 0.5f));
        onColorChoiceListener.Response.AddListener(SetCurrentColor);
    }
    
    public void SetCurrentColor(Color color)
    {
        hasBlended = false;
        currentPaintColor = color;
        if (selectedColors.Contains(color) == false)
        {
            selectedColors[index] = color;
            index++;
        } 
    }

    void Update()
    {
        if (isMouseInZone)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isMouseButtonDown = true;
            }
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMouseButtonDown = false;
            hasBlended = false;
        }
        if (isMouseButtonDown)
        {
            PaintSquares(transform.position);
        }
    }
    
    private Texture2D DuplicateTexture(Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
            source.width,
            source.height,
            0,
            RenderTextureFormat.ARGB32,
            RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        
        return readableText;
    }

    private void PaintSquares(Vector3 position)
    {
        Vector2 localPoint;
        if (GetMousePixelPosition(out localPoint))
        {
            PaintPixels(localPoint, currentPaintColor, 20);
        }
    }
    
    private bool GetMousePixelPosition(out Vector2 pixelPos)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 localPos = spriteRenderer.transform.InverseTransformPoint(mouseWorldPos);

        Rect spriteRect = spriteRenderer.sprite.rect;
        Vector2 pivot = spriteRenderer.sprite.pivot;

        float unitsPerPixel = spriteRect.width / spriteRenderer.sprite.bounds.size.x;
        Vector2 localPixelPos = new Vector2(localPos.x * unitsPerPixel + pivot.x, localPos.y * unitsPerPixel + pivot.y);

        if (localPixelPos.x >= 0 && localPixelPos.x < textureSize.x && localPixelPos.y >= 0 && localPixelPos.y < textureSize.y)
        {
            pixelPos = new Vector2(localPixelPos.x, localPixelPos.y);
            return true;
        }

        pixelPos = Vector2.zero;
        return false;
    }

    private void PaintPixels(Vector2 pixelPos, Color color, int brushSize)
    {
        int radius = brushSize;
        int cx = (int)pixelPos.x;
        int cy = (int)pixelPos.y;

        for (int y = -radius; y <= radius; y++)
        {
            for (int x = -radius; x <= radius; x++)
            {
                if (x * x + y * y <= radius * radius) // Check if the pixel is within the circle
                {
                    int pixelX = cx + x;
                    int pixelY = cy + y;

                    if (pixelX >= 0 && pixelX < textureSize.x && pixelY >= 0 && pixelY < textureSize.y)
                    {
                        if (hasBlended)
                        {
                            texture.SetPixel(pixelX, pixelY, lastMix);
                            continue;
                        }
                        
                        Color mix = color;
                        if (selectedColors.Contains(texture.GetPixel(pixelX, pixelY)))
                        {
                            mix = BlendColors(pixelX, pixelY, color);
                            lastMix = mix;
                        }

                        texture.SetPixel(pixelX, pixelY, mix);
                    }
                }
            }
        }
        texture.Apply();
    }
    
    private Color BlendColors(int x, int y, Color colorSelected)
    {
        Color colorPanel = texture.GetPixel(x, y);

        if (colorPanel == Color.white || colorPanel == colorSelected)
        {
            return colorSelected;
        }
        if (new Color(0.863f, 0.529f, 0.286f, 1.000f) == colorPanel)
        {
            return colorSelected;
        }

        float blendFactor = 0.5f;
        float r = Mathf.Lerp(colorPanel.r, colorSelected.r, blendFactor);
        float g = Mathf.Lerp(colorPanel.g, colorSelected.g, blendFactor);
        float b = Mathf.Lerp(colorPanel.b, colorSelected.b, blendFactor);
        float a = Mathf.Lerp(colorPanel.a, colorSelected.a, blendFactor);
        Color ret = new Color(r, g, b, a);
        hasBlended = true;
        return ret;
    }
    
    public void SetIsMouseInZone(bool value)
    {
        if (!value)
        {
            isMouseButtonDown = false;
        }
        isMouseInZone = value;
    }
    
}
