using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverSpriteRandomCommand : CommandBase
{
	protected override void Command()
	{
		GameObject.FindObjectOfType<TreeManager>().FeverSpriteRandom();
	}
}
