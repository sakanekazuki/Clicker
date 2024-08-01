using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Speech : MonoBehaviour
{
	// �Z���t��\������e�L�X�g
	[SerializeField]
	Text speech1;
	[SerializeField]
	Text speech2;
	[SerializeField]
	Text speech3;

	// ���̃Z���t��\������܂ł̎���
	[SerializeField]
	float speechTime = 3;

	private void OnEnable()
	{
		StartCoroutine(ESpeech());
	}

	/// <summary>
	/// �Z���t�X�V
	/// </summary>
	void SpeechUpdate()
	{
		// �Z���t���ړ�
		speech1.text = speech2.text;
		speech2.text = speech3.text;

		// true = �����Z���t
		bool isSame = true;

		if (WordsManager.Instance != null)
		{
			var key = FeverManager.Instance.IsFever ? GameData.feverSpriteIndex + 1 : GameData.treeIndex + 1;
			var s = WordsManager.Instance.GetCSV(key.ToString());
			var number = 0;
			// �����Z���t�͏o���Ȃ�
			while (isSame)
			{
				number = Random.Range(0, s.Count);
				if (speech1.text != s[number] && speech2.text != s[number])
				{
					isSame = false;
				}
			}
			// �Z���t��\��
			speech3.text = s[number];
		}
	}

	/// <summary>
	/// �Z���t�\��
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