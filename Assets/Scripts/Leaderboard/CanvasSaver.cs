using UnityEngine;
using System.Collections.Generic;

public class CanvasSaver : MonoBehaviour
{
    public List<GridNode> gridNodes;
    public int canvasWidth = 100;
    public int canvasHeight = 100;
    public Leaderboard leaderboard;
    [SerializeField] private GridStateManager gridStateManager;

    private void Start()
    {
        gridNodes = gridStateManager.gridNodes;
    }

    public void SaveCanvasToLeaderboard(string playerName, float completionPercentage)
    {
        Texture2D texture = new Texture2D(canvasWidth, canvasHeight, TextureFormat.RGBA32, false);

        Color[] backgroundColor = new Color[canvasWidth * canvasHeight];
        for (int i = 0; i < backgroundColor.Length; i++)
        {
            backgroundColor[i] = Color.white; // or any other base color
        }
        texture.SetPixels(backgroundColor);

        foreach (GridNode node in gridNodes)
        {
            SpriteRenderer sr = node.spriteRenderer;
            if (sr != null)
            {
                Rect rect = sr.sprite.textureRect;
                Texture2D spriteTexture = sr.sprite.texture;
                Color[] spritePixels = spriteTexture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);

                Vector3 localPos = sr.transform.localPosition;
                int startX = Mathf.FloorToInt((localPos.x + canvasWidth / 2) - rect.width / 2);
                int startY = Mathf.FloorToInt((localPos.y + canvasHeight / 2) - rect.height / 2);

                for (int y = 0; y < rect.height; y++)
                {
                    for (int x = 0; x < rect.width; x++)
                    {
                        Color color = spritePixels[y * (int)rect.width + x];
                        if (color.a > 0) // Only copy non-transparent pixels
                        {
                            texture.SetPixel(startX + x, startY + y, color);
                        }
                    }
                }
            }
        }

        texture.Apply();

        Sprite canvasSprite = Sprite.Create(texture, new Rect(0, 0, canvasWidth, canvasHeight), new Vector2(0.5f, 0.5f));
        leaderboard.AddEntry(playerName, completionPercentage, canvasSprite);
    }
}
