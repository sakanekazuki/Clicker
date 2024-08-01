using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLightManager : MonoBehaviour
{
    [SerializeField] private Light[] lights;
    private int NowIndex = -1;
    private int NextIndex = -1;
    private bool isChange = false;
    private bool first = true;

    public void init(int index = 0)
    {
        ChangeLightMethod(-1);
        LightChange(index);
    }

    public void LightChange(int AfterIndex = 0, float time=0f)
    {
        if (NowIndex == AfterIndex) return;
        if (isChange) return;
        isChange = true;

        if (first)
        {
            ChangeLightMethod(-1);
            time = 0f;
            first = false;
        }

        NextIndex = AfterIndex;
        Debug.Log($"LightChange {NowIndex} ¨ {NextIndex}");

        Hashtable hash = new Hashtable(){
            {"from", 0f},
            {"to", 1f},
            {"time", time},
            {"easeType",iTween.EaseType.linear},
            {"onupdate", "OnUpdateValue"},
            {"oncomplete", "OnComplete"},
            {"onupdatetarget", gameObject},
        };
        iTween.ValueTo(gameObject, hash);
    }
    void OnUpdateValue(float a)
    {
        if (NextIndex >= 0)
        {
            lights[NextIndex].intensity = a;
        }
        if (NowIndex >= 0)
        {
            lights[NowIndex].intensity = 1f - a;
        }
    }

    void OnComplete()
    {
        NowIndex = NextIndex;
        isChange = false;
    }

    private void ChangeLightMethod(int index = 0)
    {
        for (int i=0; i<lights.Length; i++)
        {
            lights[i].intensity = (index == i) ? 1f : 0f;
        }
    }

    public int GetLightNum()
    {
        return lights.Length;
    }

    public int GetNowIndex()
    {
        return NowIndex;
    }

}
