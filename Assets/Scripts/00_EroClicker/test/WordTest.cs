using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordTest : MonoBehaviour
{
	[SerializeField]
	TextAsset assets;

	List<string> words = new List<string>();

	private void Start()
	{
		Cheack();
	}

	[ContextMenu("Cheack")]
	void Cheack()
	{
		var reader = new StringReader(assets.text);

		while (reader.Peek() != -1)
		{
			words.Add(reader.ReadLine());
		}
		foreach (var word in words)
		{
			if (word.Contains("♡"))
			{
				Debug.Log(words.IndexOf(word) + "行目");
			}
		}
	}
}
