using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Speech : MonoBehaviour
{
	// セリフを表示するテキスト
	[SerializeField]
	Text speech1;
	[SerializeField]
	Text speech2;
	[SerializeField]
	Text speech3;

	// 次のセリフを表示するまでの時間
	[SerializeField]
	float speechTime = 3;

	private void OnEnable()
	{
		StartCoroutine(ESpeech());
	}

	/// <summary>
	/// セリフ更新
	/// </summary>
	void SpeechUpdate()
	{
		// セリフを移動
		speech1.text = speech2.text;
		speech2.text = speech3.text;

		// true = 同じセリフ
		bool isSame = true;

		if (WordsManager.Instance != null)
		{
			var key = FeverManager.Instance.IsFever ? GameData.feverSpriteIndex + 1 : GameData.treeIndex + 1;
			var s = WordsManager.Instance.GetCSV(key.ToString());
			var number = 0;
			// 同じセリフは出さない
			while (isSame)
			{
				number = Random.Range(0, s.Count);
				if (speech1.text != s[number] && speech2.text != s[number])
				{
					isSame = false;
				}
			}
			// セリフを表示
			speech3.text = s[number];
		}
	}

	/// <summary>
	/// セリフ表示
	/// </summary>
	IEnumerator ESpeech()
	{
		while (true)
		{
			SpeechUpdate();
			yield return new WaitForSeconds(speechTime);
		}
	}
}