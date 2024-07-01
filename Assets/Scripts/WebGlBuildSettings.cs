#if UNITY_EDITOR
using UnityEditor;

public class WebGlBuildSettings
{
    [MenuItem("WebGL/Enable Embedded Resources")]
    public static void EnableEmbeddedResources()
    {
        PlayerSettings.WebGL.useEmbeddedResources = true;
    }
}
#endif