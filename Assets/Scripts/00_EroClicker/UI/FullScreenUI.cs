using UnityEngine;

public class FullScreenUI : MonoBehaviour
{
	TabButtonController controller;

	private void Start()
	{
		controller = GetComponent<TabButtonController>();
	}

	void Update()
	{
		controller.ChangeView(Screen.fullScreen);
	}
}