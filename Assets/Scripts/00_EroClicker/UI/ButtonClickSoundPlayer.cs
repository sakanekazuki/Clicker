using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSoundPlayer : MonoBehaviour
{
	// –Â‚ç‚·‰¹
	[SerializeField]
	AudioClip clickSound;

	/// <summary>
	/// ƒNƒŠƒbƒNŽž‚É‰¹‚ð–Â‚ç‚·
	/// </summary>
	public void Click()
	{
		SoundManager.Instance.PlaySe(clickSound);
	}
}