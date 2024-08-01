using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScaleSync : MonoBehaviour
{
    [SerializeField] Transform TargetTransform;
    [SerializeField] int TargetWidth = 1280;
    [SerializeField] int TargetHeight = 720;
    private bool init = false;
    private float baseNum = 0f;

    private void Reset()
    {
        TargetTransform = gameObject.transform;
    }

    private void OnEnable()
    {
        Update();
    }

    void Update()
    {
        if (TargetTransform != null)
        {
            if (!init)
            {
                baseNum = (float)TargetWidth / (float)TargetHeight;
                init = true;
            }


            //Debug.Log($"{Screen.width} {Screen.height}");

            float aspect = (float)Screen.width / (float)Screen.height;

            float r = 1f;
            if (aspect > baseNum)
            {
                r = baseNum / ((float)Screen.width / (float)Screen.height);
            }
            else
            {
                r = 1f;
                //r = baseNum / ((float)Screen.width / (float)Screen.height);
            }
            TargetTransform.localScale = new Vector3(r,r,r);
        }
    }
}
