using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LanguageCSV : MonoBehaviour
{
    //CSVのKeyValue系

    public static LanguageCSV Instance { get; private set; }

    private string split = ",";

    [SerializeField] private TextAsset[] csvFile; // CSVファイル
    private Dictionary<string, Dictionary<int, string>> csvDatas = new Dictionary<string, Dictionary<int, string>>(); // CSVの中身を入れるリスト;



    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < csvFile.Length; i++)
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
            string[] s = line.Split('\t');// タブ区切りでリストに追加
            if (_line == 0)
            {
                //jaとかenとかの位置を記録する
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] != "")
                    {
                        indexList.Add(i);
                    }

                    d = d + "," + s[i] + $"({i})";
                }
                _line++;
            }
            else
            {
                int _cnt = 0;
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] != "")
                    {

                        //keyはja(indexが1)でやる
                        if (!csvDatas.ContainsKey(s[indexList[0]]))
                        {
                            csvDatas.Add(s[indexList[0]], new Dictionary<int, string>());
                        }

                        if (csvDatas[s[indexList[0]]].Count <= _cnt)
                        {
                            csvDatas[s[indexList[0]]].Add(_cnt, s[i]);
                        }

                        d = d + "," + s[i] + $"({_cnt})";
                        _cnt++;
                    }
                }
                _line++;
            }

            //if (d != "") Debug.Log($"language:{d}");
        }

    }

    public string GetCSV(string key)
    {
        return GetCSVLang(key, GameData.language);
    }
    public string GetCSVLang(string key, int languageList)
    {
        string t = key;
        int lang_index = languageList;
        if (csvDatas.ContainsKey(key))
        {
            if (csvDatas[key].ContainsKey(lang_index))
            {
                t = csvDatas[key][lang_index];
            }
            else
            {
                Debug.LogError($"language miss lang_index {lang_index} [{key}]");
            }
        }
        else
        {
            //Debug.LogError($"language miss [{key}]");
        }
        t = t.Replace(@"\n", "\n");

        return t;
    }


}