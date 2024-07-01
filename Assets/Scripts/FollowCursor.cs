using System;
using System.Collections;
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
    private EventInstance brushSound;
    private int brushSize = 20;
    [SerializeField] private Animator playerAnimator;
    private bool canDraw = true;

    private void Awake()
    {
        texture = DuplicateTexture(spriteRenderer.sprite.texture);
        originalPixels = texture.GetPixels();

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
        brushSound = AudioManager.Instance.CreateInstance(FmodEvents.Instance.middleBrushSound);
    }

    private void OnEnable()
    {
        ResetTexture();
    }
    
    public void ResetBrushColor()
    {
        currentPaintColor = Color.white;
    }

    public void UpdateBrushSize(int size)
    {
        if (size < 15)
        {
            brushSound = AudioManager.Instance.CreateInstance(FmodEvents.Instance.smallBrushSound);
        }
        else if (size < 25)
        {
            brushSound = AudioManager.Instance.CreateInstance(FmodEvents.Instance.middleBrushSound);
        }
        else
        {
            brushSound = AudioManager.Instance.CreateInstance(FmodEvents.Instance.bigBrushSound);
        }
        brushSize = size;
    }

    public void ResetTexture()
    {
        texture.SetPixels(originalPixels);
        // Color[] whitePixels = new Color[t(extureSize.x * textureSize.y];
        // for (int i = 0; i < whitePixels.Length; i++)
        // {
        //     whitePixels[i] = Color.white;
        // }
        //
        // texture.SetPixels(whitePixels); /)/ Set all pixels to white
        texture.Apply();
    }
    
    public Color[] GetAllPixels()
    {
        return texture.GetPixels();
    }
    
    public void SetCurrentColor(Color color)
    {
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
                brushSound.start();
                playerAnimator.SetBool("isPainting", true);
            }
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            brushSound.stop(STOP_MODE.IMMEDIATE);
            isMouseButtonDown = false;
            playerAnimator.SetBool("isPainting", false);
        }
        if (isMouseButtonDown && canDraw)
        {
            PaintSquares(transform.position);
        }
    }

    public void delayCanDraw()
    {
        canDraw = false;
        StartCoroutine(waitForPopUp());
    }
    
    IEnumerator waitForPopUp()
    {
        yield return new WaitForSeconds(1);
        canDraw = true;
    }
    
    public void SetCanDraw(bool value)
    {
        canDraw = value;
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
            brushSound.stop(STOP_MODE.IMMEDIATE);
        }
        isMouseInZone = value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.45f);
    }
}
