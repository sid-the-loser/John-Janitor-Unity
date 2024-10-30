using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScreenCameraShader : MonoBehaviour
{
//public Shader awesomeShader = null;
    public Material ColorLUTShader;
    public Material ColorLUTShader1;
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, ColorLUTShader);
        Graphics.Blit(source, destination, ColorLUTShader1);
    }
}