using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMainCamera : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    // Update is called once per frame
    void Update()
    {
        if (_canvas.worldCamera == null)
        {
            _canvas.worldCamera = Camera.main;
        }
    }
}
