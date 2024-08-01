using UnityEngine;
using UnityEngine.UI;

public class PendulumText : MonoBehaviour
{
	// テキスト
	Text text;

	void Start()
	{
		text = GetComponent<Text>();
	}

	private void LateUpdate()
	{
		text.text += " /click";
	}
}