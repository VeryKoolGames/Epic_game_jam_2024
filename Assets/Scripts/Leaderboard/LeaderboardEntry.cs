using System;
using UnityEngine;

[System.Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public float completionPercentage;
    public string spriteBase64;

    public LeaderboardEntry(string playerName, float completionPercentage, Sprite sprite)
    {
        this.playerName = playerName;
        this.completionPercentage = completionPercentage;
        this.spriteBase64 = SpriteToBase64(sprite);
    }

    public static string SpriteToBase64(Sprite sprite)
    {
        if (sprite == null) return null;

        Texture2D texture = sprite.texture;
        byte[] bytes = texture.EncodeToPNG();
        return Convert.ToBase64String(bytes);
    }

    public static Sprite Base64ToSprite(string base64)
    {
        if (string.IsNullOrEmpty(base64)) return null;

        byte[] bytes = Convert.FromBase64String(base64);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}