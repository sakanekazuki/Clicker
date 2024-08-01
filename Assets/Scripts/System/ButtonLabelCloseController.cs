using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLabelCloseController : MonoBehaviour
{
    [SerializeField] Text _Label;
    public string labelText;

    public double TargetPoint = 0;
    public bool isOn = true;

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            _Label.text = (GameData.total_point >= TargetPoint) ? labelText : GameData.CLOSED_TEXT;
        }
        else
        {
            _Label.text = labelText;
        }
    }
}
