using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance { get; private set; }

    [SerializeField] GameObject LoadingParent;
    [SerializeField] Image ProgressBar;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void On()
    {
        LoadingParent.SetActive(true);
        ProgressBar.fillAmount = 0;
    }
    public void Off()
    {
        LoadingParent.SetActive(false);
    }

    public void Progress(float n = 0)
    {
        ProgressBar.fillAmount = n;
    }
}
