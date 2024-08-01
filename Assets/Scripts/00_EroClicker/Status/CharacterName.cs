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
	/// ñºëOïœçX
	/// </summary>
	/// <param name="name"></param>
	void ICharacterName.NameChange(string name)
	{
		nameTxt.text = name;
	}
}