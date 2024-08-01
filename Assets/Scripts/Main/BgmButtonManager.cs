using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgmButtonManager : MonoBehaviour
{
    [SerializeField] Button btn;
    [SerializeField] public Text textName;
    [SerializeField] public string Name;
    [SerializeField] GameObject StatusOn;
    [SerializeField] GameObject StatusOff;
    [SerializeField] GameObject StatusSelect;

    public AudioClip audioClip;
    public int index;
    public string streamingPass;
    public bool isCustom = false;
    public bool isMyMusic = false;

    private void Reset()
    {
        btn = GetComponent<Button>();
    }
    private void Awake()
    {
        btn = GetComponent<Button>();
        textName.text = Name;
    }
    public void Play()
    {
        StatusOn.SetActive(true);
        StatusOff.SetActive(false);
    }
    public void Stop()
    {
        StatusOn.SetActive(false);
        StatusOff.SetActive(true);
    }
    public void Select(bool b = true)
    {
        StatusSelect.SetActive(b);
    }

    public void SetButton(Action<int> action)
    {
        Button btn = GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => {
            action(index);
        });
    }
}
