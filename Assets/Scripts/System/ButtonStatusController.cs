using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStatusController : MonoBehaviour
{
	[SerializeField] Button _Button;
	[SerializeField] Text _Label;
	[SerializeField] bool UseLabelStatusColor = true;

	public double TargetPoint = 0;
	public bool SwitchMode = true;
	public bool TargetLabelOnly = false;//ラベル文字色だけ別途再度計算
	public double TargetLabelOnlyPoint = 0;//ラベル文字色だけ別途再度計算

	ColorBlock beforeColorBlock;

	private void Start()
	{
		beforeColorBlock = _Button.colors;
	}

	// Update is called once per frame
	void Update()
	{
		if (SwitchMode)
		{
			bool b = TargetPoint <= GameData.point;
			//_Button.interactable = b;
			if (!b)
			{
				var btnColor = _Button.colors;
				btnColor.normalColor = new Color(0.784f, 0.784f, 0.784f, 0.5f);
				btnColor.highlightedColor = new Color(0.784f, 0.784f, 0.784f, 0.5f);
				btnColor.pressedColor = new Color(0.784f, 0.784f, 0.784f, 0.5f);
				btnColor.selectedColor = new Color(0.784f, 0.784f, 0.784f, 0.5f);
				_Button.colors = btnColor;
			}
			else
			{
				_Button.colors = beforeColorBlock;
			}
			if (TargetLabelOnly)
			{
				b = TargetLabelOnlyPoint <= GameData.point;
			}
			if (_Label != null) _Label.color = b ? GameData.COLOR_TRUE : GameData.COLOR_FALSE;
		}
		else
		{
			_Button.interactable = false;
			if (_Label != null) _Label.color = GameData.COLOR_NORMAL;
		}
	}
}
