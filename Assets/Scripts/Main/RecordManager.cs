using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager : MonoBehaviour
{
	[SerializeField] GameObject RecordContent;
	[SerializeField] GameObject Content;
	[SerializeField] MainCanvas mainCanvas;

	private bool inInit = false;

	private void OnEnable()
	{
		//表示
		if (!inInit) init();

		//StartCoroutine(EOpen());

	}

	private void OnDisable()
	{
		//transform.localScale = new Vector3(1, 0, 1);
	}

	public void init()
	{
		//GameDataのマスターで初期化


		int CreateChild = GameData.mRecord.Count;
		foreach (KeyValuePair<int, RecordClass> pair in GameData.mRecord)
		{
			if (pair.Key == 0) continue;


			GameObject obj = Instantiate(RecordContent, Content.transform, false) as GameObject;
			obj.name = pair.Key.ToString();
			obj.SetActive(true);

			RecordButtonController rbc = obj.GetComponent<RecordButtonController>();
			rbc.TextName.text = pair.Value.name;
			rbc.TextParams.text = "0";
			rbc.RecordCase = pair.Key;

			LanguageTranslation _lang = obj.GetComponentInChildren<LanguageTranslation>();
			_lang.TranslationKey = pair.Value.name;
			_lang.translationType = LanguageTranslation.TranslationType.KeyValue;

			rbc.mainCanvas = mainCanvas;
		}


		inInit = true;
	}

	/// <summary>
	/// 開く
	/// </summary>
	/// <returns></returns>
	IEnumerator EOpen()
	{
		while (true)
		{
			transform.localScale += new Vector3(0, 1f / 60, 0);
			if (transform.localScale.y >= 1)
			{
				transform.localScale = Vector3.one;
				break;
			}
			yield return null;
		}
	}
}
