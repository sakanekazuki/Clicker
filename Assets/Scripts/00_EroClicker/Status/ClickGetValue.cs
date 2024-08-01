using UnityEngine;
using UnityEngine.UI;

public class ClickGetValue : MonoBehaviour
{
	Text clickValueTxt;

	private void Start()
	{
		clickValueTxt = GetComponent<Text>();
	}

	void Update()
	{
		clickValueTxt.text = CalcData.GetCalcClickPoint().ToString();
	}
}
