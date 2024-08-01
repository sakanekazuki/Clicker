using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class WordsManager : MonoBehaviour
{
	// セリフを保存しているテキストファイル
	[SerializeField]
	TextAsset[] csv;

	private int index_ja = (int)LanguageTranslation.LanguageType.ja * 2;
	private int index_en = (int)LanguageTranslation.LanguageType.en * 2;
	private int index_tw = (int)LanguageTranslation.LanguageType.tw * 2;
	private int index_kr = (int)LanguageTranslation.LanguageType.kr * 2;
	private int index_cn = (int)LanguageTranslation.LanguageType.cn * 2;

	Dictionary<int, List<string>> words = new Dictionary<int, List<string>>();

	static WordsManager instance;
	public static WordsManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<WordsManager>();
				if (instance == null)
				{
					Debug.Log("WordsManagerが存在しません");
					return null;
				}
			}
			return instance;
		}
	}

	private void Awake()
	{
		instance = this;
		for (int i = 0; i < csv.Length; ++i)
		{
			LoadCSV(csv[i].name, csv[i].text);
		}
	}

	private void OnDestroy()
	{
		instance = null;
	}

	public void LoadCSV(string key, string textFile)
	{
		var reader = new StringReader(textFile);

		var lineNumber = 0;

		while (reader.Peek() != -1)
		{
			// 一行読む
			var line = reader.ReadLine();
			// タブ区切りでリストに追加
			var s = line.Split('\t');

			if (lineNumber == 0)
			{
				++lineNumber;
				continue;
			}
			else if (lineNumber == 1)
			{
				++lineNumber;
				continue;
			}
			// キャラクターID
			var id = "";
			// ID抽出
			for (int i = 0; i < 4; ++i)
			{
				id += s[1][i];
			}

			// とりあえずのエラー対策
			if (s[2] == "")
			{
				break;
			}

			// 通常状態のセリフ
			if (!words.ContainsKey(int.Parse(id)))
			{
				words.Add(int.Parse(id), new List<string>());
			}
			words[int.Parse(id)].Add(s[2]);
			lineNumber++;
		}
	}

	public List<string> GetCSV(string key = "")
	{
		if (key == "")
		{
			return new List<string>();
		}
		return GetCSVLang(key, GameData.language, FeverManager.Instance.IsFever);
	}

	public List<string> GetCSVLang(string key, int languageList, bool isfever)
	{
		// セリフのID計算
		var v = int.Parse(key);
		v *= 1000;
		if (isfever)
		{
			v += 10;
		}
		v += languageList;

		//return t;
		return this.words[v];
	}
}