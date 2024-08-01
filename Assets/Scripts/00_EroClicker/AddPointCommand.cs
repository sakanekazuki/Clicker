using UnityEngine;

// �|�C���g��ǉ�����R�}���h
public class AddPointCommand : CommandBase
{
	// �ǉ�����|�C���g
	[SerializeField]
	double addPointValue = 10000;

	protected override void Command()
	{
		GameData.point += addPointValue;
	}
}
