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
			Vector2 target = new Vector2(0, 0);//Unity上の座標
			target = cam.WorldToScreenPoint(target);//カメラ座標に変換
			target.y = Screen.height - target.y;//WRFS用に座標を変換
			cam.GetComponent<RippleEffect>().SetNewRipplePosition(target); //新しい波紋を生成
		}
	}
}
