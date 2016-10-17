using UnityEngine;
using System.Collections;

public class GrayScaleEffect : MonoBehaviour {

    public Material mat;
    public float amount;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
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
