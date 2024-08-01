using UnityEngine;
using UnityEngine.UI;

public class CharacterName : MonoBehaviour, ICharacterName
{
	Text nameTxt;

	private void Start()
	{
		nameTxt = GetComponent<Text>();
	}

	/// <summary>
	/// ���O�ύX
	/// </summary>
	/// <param name="name"></param>
	void ICharacterName.NameChange(string name)
	{
		nameTxt.text = name;
	}
}