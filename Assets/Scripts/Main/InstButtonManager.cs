using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstButtonManager : MonoBehaviour
{
    [SerializeField] Button btn;
    [SerializeField] Text textLv;
    [SerializeField] Text textCost;
    [SerializeField] Text textName;
    [SerializeField] Text textFruitLv;
    [SerializeField] Text textPower;
    [SerializeField] SourceImageManager sourceImage;
    [SerializeField] Text textMulti;

    private void Reset()
    {
        btn = GetComponent<Button>();
    }

    public void SetName(string s)
    {
        textName.text = LanguageCSV.Instance.GetCSV(s);

        //ローカライズ対応
        LanguageTranslation languageTranslation = GetComponent<LanguageTranslation>();
        languageTranslation.TranslationKey = s;
        languageTranslation.translationType = LanguageTranslation.TranslationType.KeyValue;
        languageTranslation.ResetData();

        //Debug.Log($"SetName:{textName.text}:{s}");
    }

    public void SetIcon(int index)
    {
        sourceImage.SetSprite(index);
    }

    public void SetButtonStatus(int lv, string cost, string power, int nextLv, int maxLv)
    {
        if(nextLv > maxLv)
        {
            //これ以上アップデートできない
            btn.interactable = false;
            UpdateLabel(lv.ToString(), "---", power);
        }
        else
        {
            btn.interactable = true;
            UpdateLabel(lv.ToString(), cost, power);
        }
    }
    public void UpdateLabel(string lv, string cost, string power)
    {
        textLv.text = lv;
        textCost.text = cost;
        textPower.text = $"{power} {GameData.STRING_PPS}";
    }

    public void UpdateFruitLv(string lv)
    {
        textFruitLv.text = lv;
    }
    public void ShowFruitLv(bool b)
    {
        //textFruitLv.gameObject.SetActive(b);
    }

    public void ShowMulti(string m = "")
    {
        textMulti.text = m;
        textMulti.gameObject.SetActive(m.Length != 0);
    }
}
