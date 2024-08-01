using System.Collections.Generic;
using UnityEngine;

public class TouchSpriteChanger : MonoBehaviour
{
	// 変更するスプライトの番号
	[SerializeField]
	List<int> spriteNumbers = new List<int>();

	//private void Start()
	//{
	//	foreach (var number in spriteNumbers)
	//	{
	//		GameObject.FindObjectOfType<TreeManager>().SetNormalClickSpriteNumber(number, false);
	//	}
	//}

	public void SpriteChange()
	{
		// ランダムで画像の番号を取得
		var v = Random.Range(0, spriteNumbers.Count);
		// 画像変更
		GameObject.FindObjectOfType<TreeManager>().FacialExpressionChange(spriteNumbers[v] - 1);
	}
}
