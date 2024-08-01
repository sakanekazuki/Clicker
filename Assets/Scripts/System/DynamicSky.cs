using UnityEngine;
using System;
using System.Collections.Generic;

public class DynamicSky : MonoBehaviour
{
    //[SerializeField]
    private AnimationCurve Curve_R, Curve_G, Curve_B;

    public bool isManual = false;
    [Range(0f, 1f)] public float ManualValue;
    public float hourValue;

    private Material mat;

    [SerializeField] private List<Light> light;

    void Start()
    {
        GetColorRGB();

        //mat = new Material(RenderSettings.skybox);
        //mat = new Material(Camera.main.gameObject.GetComponent<Skybox>().material);
        Camera.main.gameObject.GetComponent<Skybox>().material = mat;
        RenderSettings.skybox = mat;

        ChangeLightColor();
    }

    private void Update()
    {
        ChangeLightColor();
    }

    private void ChangeLightColor()
    {
        DateTime now = DateTime.Now;
        hourValue = (now.Hour * 60f + now.Minute) / (24 * 60);

        if (isManual)
        {
            hourValue = ManualValue;
        }
        
        Color col = new Color(Curve_R.Evaluate(hourValue), Curve_G.Evaluate(hourValue), Curve_B.Evaluate(hourValue));

        if(light.Count > 0)
        {
            foreach (Light _light in light)
            {
                _light.color = col;
                _light.intensity = 2f;//明るめに調整
            }
        }

        //mat.SetColor("_Tint", col);
    }

    private void GetColorRGB()
    {

        Keyframe[] frames_R = new Keyframe[7];
        Keyframe[] frames_G = new Keyframe[7];
        Keyframe[] frames_B = new Keyframe[7];

        frames_R[0] = new Keyframe(0f, 0f, 0f, 0f);
        frames_R[1] = new Keyframe(0.16f, 0f, 0f, 3f);
        frames_R[2] = new Keyframe(0.28f, 0.38f, 3f, 1f);
        frames_R[3] = new Keyframe(0.5f, 0.5f, 0.2f, 0.05f);
        frames_R[4] = new Keyframe(0.7f, 0.5f, -0.05f, -3f);
        frames_R[5] = new Keyframe(0.8f, 0.1f, -3f, -1f);
        frames_R[6] = new Keyframe(1f, 0f, -0.1f, 0f);

        frames_G[0] = new Keyframe(0f, 0.1f, 0, 0);
        frames_G[1] = new Keyframe(0.16f, 0.1f, 0f, 3f);
        frames_G[2] = new Keyframe(0.28f, 0.42f, 3f, 1f);
        frames_G[3] = new Keyframe(0.5f, 0.5f, 0.1f, -0.05f);
        //frames_G[4] = new Keyframe(0.7f, 0.45f, -0.5f, -3f);
        frames_G[4] = new Keyframe(0.7f, 0.37f, -0.5f, -3f);
        frames_G[5] = new Keyframe(0.8f, 0.15f, -3f, -0.8f);
        frames_G[6] = new Keyframe(1f, 0.1f, -0.1f, 0f);

        frames_B[0] = new Keyframe(0f, 0.17f, 0f, 0f);
        frames_B[1] = new Keyframe(0.16f, 0.17f, 0f, 3f);
        frames_B[2] = new Keyframe(0.28f, 0.41f, 2.5f, 1f);
        frames_B[3] = new Keyframe(0.5f, 0.5f, 0.1f, -0.07f);
        //frames_B[4] = new Keyframe(0.7f, 0.4f, -0.7f, -3f);
        frames_B[4] = new Keyframe(0.7f, 0.25f, -0.7f, -3f);
        frames_B[5] = new Keyframe(0.8f, 0.2f, -3f, -0.5f);
        frames_B[6] = new Keyframe(1f, 0.17f, -0.1f, 0f);

        // カーブにキーフレームを設定する
        Curve_R = new AnimationCurve(frames_R);
        Curve_G = new AnimationCurve(frames_G);
        Curve_B = new AnimationCurve(frames_B);
    }
}