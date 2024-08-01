using UnityEngine;

public class StatusGroup : MonoBehaviour
{
	// キャラクターの名前を表示しているオブジェクト
	[SerializeField]
	GameObject characterName;

	/// <summary>
	/// キャラクターの名前変更
	/// </summary>
	void CharacterNameChange()
	{
		// characterName.GetComponent<ICharacterName>().NameChange(キャラクターの名前を取得);
	}
}