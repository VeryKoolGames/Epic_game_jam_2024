using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSaver : MonoBehaviour
{
    private List<Sprite> sprites = new List<Sprite>();
    private int spriteIndex = 0;
    
    public void SaveSprite(Sprite sprite)
    {
        if (sprites.Count > 2)
            sprites.Clear();

        Texture2D copiedTexture = new Texture2D(sprite.texture.width, sprite.texture.height);
    
        copiedTexture.SetPixels(sprite.texture.GetPixels());
        copiedTexture.Apply();

        Sprite copiedSprite = Sprite.Create(copiedTexture, sprite.rect, sprite.pivot);

        sprites.Add(copiedSprite);
    }

    public List<Sprite> GetSprites()
    {
        return sprites;
    }
}
