using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpWindowController : MonoBehaviour
{
    public Action buttonAction;
    [SerializeField] public GameObject MainButtonObj;
    [SerializeField] public Text Label;
    [SerializeField] public Text LabelDis;
    [SerializeField] public Text LabelPrice;
    [SerializeField] public Text LabelPriceUI;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ButtonAction()
    {
        buttonAction?.Invoke();
    }

    public void SetLabel(string s)
    {
        if (Label != null) Label.text = s;
    }
    public void SetLabelDis(string s)
    {
        if (LabelDis != null) LabelDis.text = s;
    }
    public void SetLabelPrice(string s)
    {
        if (LabelPrice != null) LabelPrice.text = s;
    }
    public void SetButtonAction(Action a)
    {
        buttonAction = null;
        buttonAction = a;
    }
}
