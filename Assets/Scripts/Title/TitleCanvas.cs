using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCanvas : CanvasBase
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInit()
    {
        base.OnInit();

        Debug.Log("Title OnInit");
    }
}
