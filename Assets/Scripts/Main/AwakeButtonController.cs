using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AwakeButtonController : MonoBehaviour
{
    [SerializeField] public MainCanvas mainCanvas;
    [SerializeField] public int mAwakeID;
    [SerializeField] Button button;
    [SerializeField] Image imageIcon;
    [SerializeField] ButtonStatusController buttonStatusController;
    [SerializeField] SourceImageManager FrameSourceImageManager;
    [SerializeField] List<SourceImageManager> iconInstSourceImageManager;
    [SerializeField] SourceImageManager iconClickSourceImageManager;

    private AwakeClass awake;
    private int InstID;

    public void Init()
    {
        awake = GameData.mAwake[mAwakeID];
        InstID = Mathf.FloorToInt((float)mAwakeID / (float)GameData.mAwakeIDLength);

        if (mAwakeID >= GameData.mAwakeIDLength)
        {
            imageIcon.enabled = true;
            FrameSourceImageManager.SetSprite(1);

            for (int _instID=1; _instID <= iconInstSourceImageManager.Count; _instID++)
            {
                if (_instID > iconInstSourceImageManager.Count) break;
                if (iconInstSourceImageManager[(_instID - 1)] == null) break;

                if (InstID == _instID)
                {
                    int ImageIndex = (int)((double)mAwakeID % (double)GameData.mAwakeIDLength) - 1;
                    //Debug.Log($"Init InstID {InstID} {ImageIndex}");
                    iconInstSourceImageManager[(_instID - 1)].SetSprite(ImageIndex);
                }
            }
        }
        else if (mAwakeID == (int)GameData.AwakeModeNum.FRAGMENT_STELLA)
        {
            FrameSourceImageManager.SetSprite(2);
            iconClickSourceImageManager.SetSprite(1);
        }
        else if (mAwakeID == (int)GameData.AwakeModeNum.BGM)
        {
            FrameSourceImageManager.SetSprite(2);
            iconClickSourceImageManager.SetSprite(3);//その他的な
        }
        else if (mAwakeID == (int)GameData.AwakeModeNum.SHARD_BEFOREST)
        {
            FrameSourceImageManager.SetSprite(2);
            iconClickSourceImageManager.SetSprite(2);
        }
        else if (mAwakeID == (int)GameData.AwakeModeNum.FRUIT)
        {
            FrameSourceImageManager.SetSprite(2);
            iconClickSourceImageManager.SetSprite(3);//一旦仮
        }
        else if (mAwakeID == (int)GameData.AwakeModeNum.RAGNAROK)
        {
            FrameSourceImageManager.SetSprite(2);
            iconClickSourceImageManager.SetSprite(3);
        }
        else if (mAwakeID == (int)GameData.AwakeModeNum.GODDESS)
        {
            FrameSourceImageManager.SetSprite(2);
            iconClickSourceImageManager.SetSprite(3);
        }
        else if (awake.skillType == GameData.AWAKE_TYPE_TIME)
        {
            FrameSourceImageManager.SetSprite(3);
            iconClickSourceImageManager.SetSprite(3);
        }
        else
        {
            FrameSourceImageManager.SetSprite(0);
            iconClickSourceImageManager.SetSprite(0);
        }

        buttonStatusController.TargetPoint = awake.price;
    }
    public void ButtonClick()
    {
        //覚醒解放
        Debug.Log($"ButtonClick {mAwakeID}");
        mainCanvas.ButtonAwake(mAwakeID);
    }

    private void Update()
    {
    }

    public AwakeClass GetAwakeData()
    {
        return awake;
    }
    public int GetInstID()
    {
        return InstID;
    }
    public int GetAwakeID()
    {
        return mAwakeID;
    }
    public Button GetButton()
    {
        return button;
    }


    public void OnAwakeButtonPointerEnter(Action<int> act)
    {
        //Debug.Log($"OnAwakeButtonPointerEnter {gameObject.name} {mAwakeID}");
        act?.Invoke(mAwakeID);
    }
    public void OnAwakeButtonPointerExit(Action<int> act)
    {
        //Debug.Log($"OnAwakeButtonPointerExit {gameObject.name} {mAwakeID}");
        act?.Invoke(mAwakeID);
    }
}
