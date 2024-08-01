using System.Collections.Generic;
using UnityEngine;

public class SelectButtonGroups : MonoBehaviour
{
	// 管理するボタン
	[SerializeField]
	List<TabButtonController> tabButtonControllers = new List<TabButtonController>();

	private void OnEnable()
	{
		foreach (var controller in tabButtonControllers)
		{
			controller.ChangeView(tabButtonControllers.IndexOf(controller) == 0);
		}
	}

	/// <summary>
	/// 選択しているボタン変更
	/// </summary>
	/// <param name="number">変更するボタンの番号</param>
	public void Change(int number)
	{
		for (int i = 0; i < tabButtonControllers.Count; ++i)
		{
			tabButtonControllers[i].ChangeView(i == number);
		}
	}
}