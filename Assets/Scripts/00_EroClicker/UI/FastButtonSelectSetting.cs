using UnityEngine;

public class FastButtonSelectSetting : MonoBehaviour
{
	void OnEnable()
	{
		GetComponent<TabButtonController>().ChangeView(false);
	}
}
