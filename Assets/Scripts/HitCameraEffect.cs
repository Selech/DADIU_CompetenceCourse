using UnityEngine;
using System.Collections;

public class HitCameraEffect : MonoBehaviour {

    private Material mat;
    public Shader CustomShader;
    public float amount;

    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (mat == null)
        {
            mat = new Material(CustomShader);
            mat.SetFloat("_Amount", 0);
        }

        mat.SetFloat("_Amount",amount);
        Graphics.Blit(source, destination, mat);
    }

    void Update()
    {
        if (amount > 0) amount -= 0.005f;
    }

    public void TriggerGrayScale()
    {
        amount = 1.0f;
    }
}
