using System.Collections.Generic;
using UnityEngine;

public class SelectButtonGroups : MonoBehaviour
{
	// �Ǘ�����{�^��
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
	/// �I�����Ă���{�^���ύX
	/// </summary>
	/// <param name="number">�ύX����{�^���̔ԍ�</param>
	public void Change(int number)
	{
		for (int i = 0; i < tabButtonControllers.Count; ++i)
		{
			tabButtonControllers[i].ChangeView(i == number);
		}
	}
}