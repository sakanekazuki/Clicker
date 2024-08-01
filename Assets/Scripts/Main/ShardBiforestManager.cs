using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShardBiforestManager : MonoBehaviour
{

    [SerializeField] GameObject ShardChild;
    [SerializeField] List<GameObject> ShardUIChild;
    [SerializeField] float RandomPositionX = 2f;
    [SerializeField] MainCanvas mainCanvas;
    [SerializeField] GameObject ButtonWarp;
    [SerializeField] GameObject BoostEffectGroup;
    [SerializeField] Text BoostTimeText;
    [SerializeField] Text BoostPowerText;
    [SerializeField] GameObject InfoWindow;
    [SerializeField] GameObject ParticleObj;

    private bool isBoost = false;
    private bool activeFlg = false;

    private void Awake()
    {
        InfoWindow.SetActive(false);
    }

    public void SetShard()
    {
        if (!activeFlg && ShardChild.activeSelf == false)
        {
            activeFlg = true;
            ViewShard();
        }
    }

    public void ViewShard()
    {
        ShardChild.transform.localPosition = new Vector2(UnityEngine.Random.RandomRange(-RandomPositionX, RandomPositionX), 0);
        ShardChild.SetActive(false);
        //Debug.Log($"ViewShard");
    }

    public void RemoveShard()
    {
        ShardChild.SetActive(false);
        activeFlg = false;
    }

    public void ClickShard()
    {
        if (GameData.ShardBiforest < GameData.SHARD_BIFOREST_MAX)
        {
            GameData.ShardBiforest++;
#if UNITY_EDITOR
            //GameData.ShardBiforest += GameData.SHARD_BIFOREST_MAX;
#endif
        }

        mainCanvas.ClickBeforest();

        ViewShardBiforestUI();
        ResetShardCycle();

        RemoveShard();

        //ブースト開始
        GameData.ShardBiforestBoost = CalcData.GetShardBiforestBoostTime();
        StartBoostEffect();
    }

    private void ResetShardCycle()
    {
        GameData.ShardBiforestCycleCnt = 0;
        GameData.ShardBiforestCycleCntLimit = 0;
    }
    public bool isShardStay()
    {
        if (activeFlg && !ShardChild.activeSelf)
        {
            ViewShard();
        }
        return activeFlg;
    }


    public void ViewShardBiforestUI()
    {
        for (int i=0; i < ShardUIChild.Count; i++)
        {
            //ShardUIChild[i].SetActive(i < GameData.ShardBiforest);
            ShardUIChild[i].SetActive(true);
            ShardUIChild[i].GetComponent<CanvasGroup>().alpha = (i < GameData.ShardBiforest) ? 1f : 0.25f;
        }

        ButtonWarp.GetComponent<CanvasGroup>().alpha = (GameData.ShardBiforest >= GameData.SHARD_BIFOREST_MAX) ? 1 : 0;
    }


    public void UpdateBoostTime()
    {
        BoostTimeText.text = $"{LanguageCSV.Instance.GetCSV(GameData.SHARD_BIFOREST_TIME_TEMPLATE)}{((float)GameData.ShardBiforestBoost / 60f).ToString("f2")}";
    }
    public void StartBoostEffect()
    {
        if (isBoost) return;

        isBoost = true;
        BoostEffectGroup.SetActive(true);
        ParticleObj.SetActive(true);

        BoostTimeText.text = $"";
        BoostPowerText.text = $"×{(CalcData.GetBiforestBoostPower() * 100)}%!";

        ButtonWarp.GetComponent<Button>().interactable = false;

        mainCanvas.musicPlayermanager.PlayBGMBoost(true);

    }
    public void EndBoostEffect()
    {
        isBoost = false;
        if (BoostEffectGroup.activeSelf)
        {
            BoostEffectGroup.SetActive(false);
            ParticleObj.SetActive(false);
        }

        ButtonWarp.GetComponent<Button>().interactable = true;
    }



    public void ButtonStartWarp()
    {
        Debug.Log($"ButtonStartWarp {GameData.ShardBiforest} {GameData.SHARD_BIFOREST_MAX} {ButtonWarp.GetComponent<CanvasGroup>().alpha}");
        if (GameData.ShardBiforest >= GameData.SHARD_BIFOREST_MAX && ButtonWarp.GetComponent<CanvasGroup>().alpha == 1)
        {
            //ワープOK

            //30分分のポイントゲット
            mainCanvas.GetShardBiforestWarp();

            //リセット
            ResetShardCycle();

            //欠片なくなる
            GameData.ShardBiforest = 0;
            ViewShardBiforestUI();
        }
    }


    public void OnEnterShowInfo()
    {
        InfoWindow.SetActive(true);
    }
    public void OnExitHideInfo()
    {
        InfoWindow.SetActive(false);
    }
}
