using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSoundPlayer : MonoBehaviour
{
	// �炷��
	[SerializeField]
	AudioClip clickSound;

	/// <summary>
	/// �N���b�N���ɉ���炷
	/// </summary>
	public void Click()
	{
		SoundManager.Instance.PlaySe(clickSound);
	}
}