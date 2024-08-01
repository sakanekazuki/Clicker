using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabButtonController : MonoBehaviour
{
    [SerializeField] List<GameObject> SelectedImage = new List<GameObject>();
    [SerializeField] List<GameObject> NormalImage = new List<GameObject>();
    [SerializeField] List<GameObject> SelectedViewWindow = new List<GameObject>();
    private bool isClose = true;

    public void ChangeView(bool select)
    {
        isClose = select;

        foreach (GameObject obj in SelectedImage)
        {
            obj.SetActive(select);
        }
        foreach (GameObject obj in NormalImage)
        {
            obj.SetActive(!select);
        }
        foreach (GameObject obj in SelectedViewWindow)
        {
            obj.SetActive(select);
        }
    }

    public void ChangeViewOrClose()
    {
        isClose = !isClose;

        foreach (GameObject obj in SelectedImage)
        {
            obj.SetActive(isClose);
        }
        foreach (GameObject obj in NormalImage)
        {
            obj.SetActive(!isClose);
        }
        foreach (GameObject obj in SelectedViewWindow)
        {
            obj.SetActive(isClose);
        }
    }
}
