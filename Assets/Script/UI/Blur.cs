using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Blur : MonoBehaviour
{
    public Material BlurMaterial;
    [Range(0, 10)]
    public int Iteration;

    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        RenderTexture rt = RenderTexture.GetTemporary(src.width, src.height);
        Graphics.Blit(src, rt);

        for (int i  = 0; i < Iteration; i++)
        {
            RenderTexture rt2 = RenderTexture.GetTemporary(rt.width, rt.height);
            Graphics.Blit(rt, rt2, BlurMaterial);
        }

        Graphics.Blit(rt, dst);
        RenderTexture.ReleaseTemporary(rt);
    }
}
