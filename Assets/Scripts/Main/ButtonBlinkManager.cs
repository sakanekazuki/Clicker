using System.Collections;
using System.Collections.Generic;
using Coffee.UIEffects;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBlinkManager : MonoBehaviour
{

    [SerializeField] UIShiny uiShiny;
    [SerializeField] Button button;
    public bool isBlink = false;

    // Update is called once per frame
    void Update()
    {
        if (isBlink)
        {
            uiShiny.enabled = button.interactable;
        }
        else
        {
            uiShiny.enabled = false;
        }
    }
}
