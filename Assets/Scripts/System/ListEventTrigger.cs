using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ListEventTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int num = 0;
    public Action<int> OnPointerEnterAction;
    public Action<int> OnPointerExitAction;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log($"ListEventTrigger OnPointerEnter");
        OnPointerEnterAction?.Invoke(num);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log($"ListEventTrigger OnPointerExit");
        OnPointerExitAction?.Invoke(num);
    }
}