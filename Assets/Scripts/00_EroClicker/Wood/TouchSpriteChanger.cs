using System.Collections.Generic;
using UnityEngine;

public class TouchSpriteChanger : MonoBehaviour
{
	// �ύX����X�v���C�g�̔ԍ�
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
		// �����_���ŉ摜�̔ԍ����擾
		var v = Random.Range(0, spriteNumbers.Count);
		// �摜�ύX
		GameObject.FindObjectOfType<TreeManager>().FacialExpressionChange(spriteNumbers[v] - 1);
	}
}
