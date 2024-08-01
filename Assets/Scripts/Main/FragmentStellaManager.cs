using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentStellaManager : MonoBehaviour
{
    [SerializeField] GameObject FragmentChild;
    [SerializeField] float RandomPositionX = 2f;
    [SerializeField] MainCanvas mainCanvas;

    private bool activeFlg = false;
    public void SetFragment()
    {
        if (!activeFlg && FragmentChild.activeSelf == false)
        {
            activeFlg = true;
            ViewFragment();
        }
    }

    public void ViewFragment()
    {
        FragmentChild.transform.localPosition = new Vector2(UnityEngine.Random.RandomRange(-RandomPositionX, RandomPositionX), 0);
        FragmentChild.SetActive(true);
    }

    public void RemoveFragment()
    {
        FragmentChild.SetActive(false);
        activeFlg = false;
    }

    public void ClickFragment()
    {
        GameData.FragmentStellaCycleCnt = 0;
        GameData.FragmentStellaStayCnt = 0;
        mainCanvas.GetFragmentStella();
        RemoveFragment();
    }

    public bool isFragmentStay()
    {
        if(activeFlg && !FragmentChild.activeSelf)
        {
            ViewFragment();
        }
        return activeFlg;
    }




}
