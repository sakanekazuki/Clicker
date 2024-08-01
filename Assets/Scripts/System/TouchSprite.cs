using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSprite : MonoBehaviour
{
    private Rigidbody2D rbody2D;

    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
    }

    public void OnClick()
    {
        Debug.Log($"OnClick {name}");
    }
}
