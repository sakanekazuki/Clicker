using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [SerializeField] GameObject FruitChild;
    [SerializeField] float RandomPositionX = 1.5f;
    [SerializeField] float RandomPositionY = 0.5f;
    [SerializeField] MainCanvas mainCanvas;

    private bool activeFlg = false;
    public void SetFruit()
    {
        if (!activeFlg && FruitChild.activeSelf == false)
        {
            activeFlg = true;
            ViewFruit();
        }
    }

    public void ViewFruit()
    {
        FruitChild.transform.localPosition = new Vector2(UnityEngine.Random.RandomRange(-RandomPositionX, RandomPositionX), UnityEngine.Random.RandomRange(-RandomPositionY, RandomPositionY));
        //FruitChild.SetActive(true);
    }

    public void RemoveFruit()
    {
        FruitChild.SetActive(false);
        activeFlg = false;
    }

    public void ClickFruit()
    {
        GameData.FruitCycleCnt = 0;
        mainCanvas.GetFruit();
        RemoveFruit();
    }

    public bool isFruitStay()
    {
        if(activeFlg && !FruitChild.activeSelf)
        {
            ViewFruit();
        }
        return activeFlg;
    }




}
