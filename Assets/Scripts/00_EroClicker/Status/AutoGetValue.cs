using System;
using UnityEngine;
using UnityEngine.UI;
using naichilab.Scripts.Extensions;

public class AutoGetValue : MonoBehaviour
{
	Text autoValueTxt;

	// Start is called before the first frame update
	void Start()
	{
		autoValueTxt = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update()
	{
		double i = 0;
		for (int instCase = 1; instCase < (GameData.INST_COST_BASE.Count); instCase++)
		{
			i += CalcData.GetInstPoint(instCase, GameData.InstLv[instCase]);
		}
		i /= CalcData.GetAdjInstCycle();

		var e = GetBigNumberString(Math.Round(i, 1));
		autoValueTxt.text = $"{LanguageCSV.Instance.GetCSV(GameData.PPS_TEMPLATE)}{e}";
	}

	private string GetBigNumberString(double n)
	{
		string result = "";
		if (n < 1000000)
		{
			result = n.ToString();
		}
		else
		{
			result = n.ToReadableString();
		}
		return result;
	}
}
