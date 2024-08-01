using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LanguageParamsClass
{
    public enum Field { 
        name,
        dis
    }
    public string name;
    public string dis;

    public LanguageParamsClass(string _name, string _dis)
    {
        name = _name;
        dis = _dis;
    }
}
public class LanguageCSVparams : MonoBehaviour
{
    //CSVのKeyValue系

    public static LanguageCSVparams Instance { get; private set; }

    private string split = "\t";
    [SerializeField] private TextAsset[] csvFile; // CSVファイル
    private Dictionary<string, LanguageParamsClass> dic_ja = new Dictionary<string, LanguageParamsClass>();
    private Dictionary<string, LanguageParamsClass> dic_en = new Dictionary<string, LanguageParamsClass>();
    private Dictionary<string, LanguageParamsClass> dic_tw = new Dictionary<string, LanguageParamsClass>();
    private Dictionary<string, LanguageParamsClass> dic_kr = new Dictionary<string, LanguageParamsClass>();
    private Dictionary<string, LanguageParamsClass> dic_cn = new Dictionary<string, LanguageParamsClass>();

    private int index_ja = (int)LanguageTranslation.LanguageType.ja + 1;
    private int index_en = (int)LanguageTranslation.LanguageType.en + 2;
    private int index_tw = (int)LanguageTranslation.LanguageType.tw + 3;
    private int index_kr = (int)LanguageTranslation.LanguageType.kr + 4;
    private int index_cn = (int)LanguageTranslation.LanguageType.cn + 5;


    private void Awake()
    {
        Instance = this;

        for (int i=0; i< csvFile.Length; i++)
        {
            LoadCSV(csvFile[i].text);
        }
    }


    public void LoadCSV(string textFile)
    {
        //csvFile = Resources.Load(FilePath) as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(textFile);

        //どの言語がどのkeyか判定用
        List<int> indexList = new List<int>();
        int _line = 0;

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string d = "";//確認用
            string line = reader.ReadLine(); // 一行ずつ読み込み
            //string[] s = line.Split(split);// , 区切りでリストに追加
            string[] s = line.Split('\t');// , 区切りでリストに追加

            //ja,ja,en,enとなるもの
            //ja,enとなるもの
            if (_line == 0)
            {
                //jaとかenとかをセットするフェーズ
                _line++;
            }
            else if (_line == 1)
            {
                //覚醒名とか説明文とかの見出し部分なのでスキップ
                _line++;
            }
            else
            {
                string key = s[index_ja];
                if (s.Length > index_ja && !dic_ja.ContainsKey(key)) dic_ja.Add(key, new LanguageParamsClass(s[index_ja], s[index_ja + 1]));
                if (s.Length > index_en && !dic_en.ContainsKey(key)) dic_en.Add(key, new LanguageParamsClass(s[index_en], s[index_en + 1]));
                if (s.Length > index_tw && !dic_tw.ContainsKey(key)) dic_tw.Add(key, new LanguageParamsClass(s[index_tw], s[index_tw + 1]));
                if (s.Length > index_kr && !dic_kr.ContainsKey(key)) dic_kr.Add(key, new LanguageParamsClass(s[index_kr], s[index_kr + 1]));
                if (s.Length > index_cn && !dic_cn.ContainsKey(key)) dic_cn.Add(key, new LanguageParamsClass(s[index_cn], s[index_cn + 1]));
                _line++;

                d = s[1] + $"({s.Length})";
            }

            //if (d != "") Debug.Log($"language:{d}");
        }

    }

    public string GetCSV(string key="", LanguageParamsClass.Field field = LanguageParamsClass.Field.name, string orgText = "")
    {
        if (key == "") return ""; 
        if (orgText == "") orgText = key;
        return GetCSVLang(key, GameData.language, field, orgText);
    }
    public string GetCSVLang(string key, int languageList, LanguageParamsClass.Field field, string orgText)
    {
        string t = key;

        Dictionary<string, LanguageParamsClass> dic;
        switch (languageList)
        {
            default:
            case 0:
                dic = dic_ja;
                break;
            case 1:
                dic = dic_en;
                break;
            case 2:
                dic = dic_tw;
                break;
            case 3:
                dic = dic_kr;
                break;
            case 4:
                dic = dic_cn;
                break;
        }

        if (dic.ContainsKey(key))
        {
            //Debug.Log($"dic.ContainsKey(key)[{languageList}]:{key}:{field}");
            switch (field)
            {
                case LanguageParamsClass.Field.name:
                    t = dic[key].name;
                    break;
                case LanguageParamsClass.Field.dis:
                    t = dic[key].dis;
                    break;
            }
        }
        else
        {
            Debug.LogError($"language params miss [{key}]");
        }

        if (t.Length == 0 && field == LanguageParamsClass.Field.name)
        {
            t = key;
        }
        else if(t.Length == 0)
        {
            t = orgText;
        }

        return t;
    }

}