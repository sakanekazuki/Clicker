using UnityEngine;
using WaterRippleForScreens;

public class RippleEffectTest : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Camera cam = Camera.main;
			Vector2 target = new Vector2(0, 0);//Unity��̍��W
			target = cam.WorldToScreenPoint(target);//�J�������W�ɕϊ�
			target.y = Screen.height - target.y;//WRFS�p�ɍ��W��ϊ�
			cam.GetComponent<RippleEffect>().SetNewRipplePosition(target); //�V�����g��𐶐�
		}
	}
}
