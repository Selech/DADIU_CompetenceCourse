using UnityEngine;
using System.Collections;

public class BloomScript : MonoBehaviour
{
    private Material mat;
    public Shader CustomShader;

    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (mat == null)
        {
            mat = new Material(CustomShader);
        }

        Graphics.Blit(source, destination, mat);
    }
}
