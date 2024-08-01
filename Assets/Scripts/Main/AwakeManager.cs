using naichilab.Scripts.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AwakeManager : MonoBehaviour
{
    [SerializeField] MainCanvas mainCanvas;
    [SerializeField] GameObject infoWindow;
    [SerializeField] Text infoWindowName;
    [SerializeField] Text infoWindowPrice;
    [SerializeField] Text infoWindowInfo;
    List<GameObject> buttons = new List<GameObject>();
    [SerializeField] GameObject prefabAwakeButton;
    [SerializeField] GameObject AwakeButtonContent;

    private int total_awaked = 0;

    private void Init()
    {
        HideInfoWindow();

        double MaxPrice = 0;

        int CreateChild = GameData.mAwake.Count;
        foreach (KeyValuePair<int,AwakeClass> pair in GameData.mAwake)
        {
            if (pair.Key == 0) continue;


            GameObject obj = Instantiate(prefabAwakeButton, AwakeButtonContent.transform, false) as GameObject;
            obj.name = pair.Key.ToString();
            obj.SetActive(false);

            AwakeButtonController abc = obj.GetComponent<AwakeButtonController>();
            abc.mainCanvas = mainCanvas;
            abc.mAwakeID = pair.Key;
            abc.Init();

            //EventTrigger et = obj.GetComponent<EventTrigger>();

            //EventTrigger.Entry entry = new EventTrigger.Entry();
            //entry.eventID = EventTriggerType.PointerEnter;
            //entry.callback.AddListener((eventDate) => { abc.OnAwakeButtonPointerEnter((mAwakeID) => {
            //    ShowInfoWindow(mAwakeID);
            //}); });
            //et.triggers.Add(entry);

            //entry = new EventTrigger.Entry();
            //entry.eventID = EventTriggerType.PointerExit;
            //entry.callback.AddListener((eventDate) => { abc.OnAwakeButtonPointerExit((mAwakeID) => {
            //    HideInfoWindow();
            //}); });
            //et.triggers.Add(entry);

            ListEventTrigger listEventTrigger = obj.GetComponent<ListEventTrigger>();
            listEventTrigger.num = pair.Key;
            listEventTrigger.OnPointerEnterAction = (mAwakeID) => { ShowInfoWindow(mAwakeID); };
            listEventTrigger.OnPointerExitAction = (mAwakeID) => { HideInfoWindow(); };

            buttons.Add(obj);

            if(MaxPrice < GameData.mAwake[abc.mAwakeID].price)
            {
                MaxPrice = GameData.mAwake[abc.mAwakeID].price;
            }
        }

        //最高桁数のぶんだけ0をつなげる
        int maxPriceLength = MaxPrice.ToString("0").Length;
        string maxPriceZero = "";
        for (int i=0; i<maxPriceLength; i++)
        {
            maxPriceZero = maxPriceZero + "0";
        }


        //安い順にソート
        List<Transform> objList = new List<Transform>();

        // 子階層のGameObject取得
        var childCount = AwakeButtonContent.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            objList.Add(AwakeButtonContent.transform.GetChild(i));
        }

        // オブジェクトソート
        objList.Sort((obj1, obj2) => string.Compare(
            (GameData.mAwake[obj1.GetComponent<AwakeButtonController>().mAwakeID].price).ToString(maxPriceZero),
            GameData.mAwake[obj2.GetComponent<AwakeButtonController>().mAwakeID].price.ToString(maxPriceZero))
        );

        // ソート結果順にGameObjectの順序を反映
        foreach (var obj in objList)
        {
            obj.SetSiblingIndex(childCount - 1);

            //Debug.Log($"_sort:{GameData.mAwake[obj.GetComponent<AwakeButtonController>().mAwakeID].price.ToString(maxPriceZero)}:({obj.GetComponent<AwakeButtonController>().mAwakeID})");
        }

    }
    public void UpdateAwakeButtonView()
    {
        if(buttons.Count == 0)
        {
            Init();
        }
        total_awaked = 0;

        int _instAllLv = 0;
        int _AwakeAllNum = GameData.AwakeLv.Count;
        foreach (int n in GameData.InstLv)
        {
            _instAllLv += n;
        }

        foreach (GameObject obj in buttons)
        {
            AwakeButtonController abc = obj.GetComponent<AwakeButtonController>();
            if (GameData.AwakeLv.Contains(abc.GetAwakeID()))
            {
                total_awaked++;
                obj.SetActive(false);
                continue;
            }

            AwakeClass awake = abc.GetAwakeData();
            switch (awake.skillType) {
                case GameData.AWAKE_TYPE_INST:
                    if (awake.lv <= GameData.InstLv[abc.GetInstID()])
                    {
                        obj.SetActive(true);
                    }
                    else
                    {
                        obj.SetActive(false);
                    }
                    break;
                case GameData.AWAKE_TYPE_CLICK:
                    obj.SetActive(awake.lv <= GameData.click);
                    break;
                case GameData.AWAKE_TYPE_INST_LV:
                    obj.SetActive(awake.lv <= _instAllLv);
                    break;
                case GameData.AWAKE_TYPE_AWAKE_NUM:
                    obj.SetActive(awake.lv <= _AwakeAllNum);
                    break;
                case GameData.AWAKE_TYPE_TIME:
                    if (GameData.firststarttime == 0)
                    {
                        GameData.firststarttime = Math.Round((double)DateTime.Now.Ticks / (1000 * 1000 * 10));
                    }
                    double diff_sec = Math.Round((double)DateTime.Now.Ticks / (1000 * 1000 * 10)) - GameData.firststarttime;
                    obj.SetActive(awake.lv <= diff_sec);
#if UNITY_EDITOR
                    //obj.SetActive(true);
#endif
                    //Debug.Log($"diff_sec {diff_sec}");
                    break;
            }
        }
    }

    public void ShowInfoWindow(int mAwakeID = 0)
    {
        AwakeClass awake = GameData.mAwake[mAwakeID];
        infoWindowName.text = LanguageCSVparams.Instance.GetCSV(awake.name, LanguageParamsClass.Field.name);
        infoWindowPrice.text = $"{awake.price.ToReadableString()}";
        infoWindowPrice.color = (GameData.point >= awake.price) ? GameData.COLOR_TRUE : GameData.COLOR_FALSE;
        infoWindowInfo.text = LanguageCSVparams.Instance.GetCSV(awake.name, LanguageParamsClass.Field.dis, awake.dis);
        infoWindow.SetActive(true);
    }
    public void HideInfoWindow(int mAwakeID = 0)
    {
        infoWindow.SetActive(false);
    }

    public int GetTotalAwaked()
    {
        return total_awaked;
    }

}
