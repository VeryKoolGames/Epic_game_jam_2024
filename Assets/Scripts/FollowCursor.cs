using System;
using DefaultNamespace;
using DG.Tweening;
using FMOD.Studio;
using Unity.VisualScripting;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GridStateManager gridStateManager;
    private bool isMouseInZone;
    private bool isMouseButtonDown;
    private ColorsEnum currentColor = ColorsEnum.WHITE;
    private Color currentPaintColor = Color.white;
    [SerializeField] private OnColorChoiceListener onColorChoiceListener;
    private bool hasBlended;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Texture2D texture;
    private Color[] originalPixels; // To store the original pixels
    private Vector2Int textureSize;
    private int id;
    private EventInstance smallBrushSound;
    private int brushSize = 20;
    [SerializeField] private Animator playerAnimator;

    private void Awake()
    {
        texture = DuplicateTexture(spriteRenderer.sprite.texture);
        originalPixels = texture.GetPixels(); // Store the original pixels

        if (!texture.isReadable)
        {
            return;
        }
        textureSize = new Vector2Int(texture.width, texture.height);
        spriteRenderer.sprite = Sprite.Create(texture, spriteRenderer.sprite.rect, new Vector2(0.5f, 0.5f));
        onColorChoiceListener.Response.AddListener(SetCurrentColor);
    }

    private void Start()
    {
        smallBrushSound = AudioManager.Instance.CreateInstance(FmodEvents.Instance.smallBrushSound);
    }

    public void UpdateBrushSize(int size)
    {
        brushSize = size;
    }

    public void ResetTexture()
    {
        Debug.Log(texture);
        Color[] whitePixels = new Color[textureSize.x * textureSize.y];
        for (int i = 0; i < whitePixels.Length; i++)
        {
            whitePixels[i] = Color.white;
        }

        texture.SetPixels(whitePixels); // Set all pixels to white
        texture.Apply();
    }
    
    public Color[] GetAllPixels()
    {
        return texture.GetPixels();
    }
    
    public void SetCurrentColor(Color color)
    {
        Debug.Log("Setting current color");
        hasBlended = false;
        currentPaintColor = color;
    }

    void Update()
    {
        LerpToMouse();
        if (isMouseInZone)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isMouseButtonDown = true;
                smallBrushSound.start();
                playerAnimator.SetBool("isPainting", true);
            }
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            smallBrushSound.stop(STOP_MODE.IMMEDIATE);
            isMouseButtonDown = false;
            playerAnimator.SetBool("isPainting", false);
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
            RenderTextureFormat.Default,
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
            Debug.Log("Painting");
            PaintPixels(localPoint, currentPaintColor, brushSize);
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
                if (x * x + y * y <= radius * radius)
                {
                    int pixelX = cx + x;
                    int pixelY = cy + y;

                    if (pixelX >= 0 && pixelX < textureSize.x && pixelY >= 0 && pixelY < textureSize.y)
                    {
                        texture.SetPixel(pixelX, pixelY, color);
                    }
                }
            }
        }
        texture.Apply();
    }
    
    private Color BlendColors(Color color1, Color color2)
    {
        if (color2 == Color.white || color1 == color2)
        {
            return color1;
        }
        float blendFactor = 0.5f;
        float r = Mathf.Lerp(color1.r, color2.r, blendFactor);
        float g = Mathf.Lerp(color1.g, color2.g, blendFactor);
        float b = Mathf.Lerp(color1.b, color2.b, blendFactor);
        Color ret = new Color(r, g, b, 1.0f);
        hasBlended = true;
        return ret;
    }

    private void LerpToMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;
    }
    
    public void SetIsMouseInZone(bool value)
    {
        if (!value)
        {
            isMouseButtonDown = false;
            smallBrushSound.stop(STOP_MODE.IMMEDIATE);
        }
        isMouseInZone = value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.45f);
    }
}
