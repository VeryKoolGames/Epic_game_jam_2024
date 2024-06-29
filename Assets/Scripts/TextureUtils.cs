using UnityEngine;

public static class TextureUtils
{
    public static Texture2D CreateReadableTexture(Texture2D original)
    {
        Texture2D readableTexture = new Texture2D(original.width, original.height, original.format, false);
        
        // Copy the original texture's pixels to the new texture
        Graphics.CopyTexture(original, readableTexture);
        
        // Apply the changes to the new texture
        readableTexture.Apply();
        
        return readableTexture;
    }
}