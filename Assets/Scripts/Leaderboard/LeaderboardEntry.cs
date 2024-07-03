using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public float completionPercentage;
    public List<string> spriteBase64One;

    public LeaderboardEntry(string playerName, float completionPercentage, List<Sprite> sprite)
    {
        spriteBase64One = new List<string>();
        this.playerName = playerName;
        this.completionPercentage = completionPercentage;
        foreach (Sprite s in sprite)
        {
            this.spriteBase64One.Add(SpriteToBase64(s));
        }
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
        Texture2D texture = new Texture2D(492, 459, TextureFormat.RGBA32, false);
        texture.LoadImage(bytes);
        var ret = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        return ret;
    }
}