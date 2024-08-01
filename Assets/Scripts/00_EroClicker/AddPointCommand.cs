using UnityEngine;

// ポイントを追加するコマンド
public class AddPointCommand : CommandBase
{
	// 追加するポイント
	[SerializeField]
	double addPointValue = 10000;

	protected override void Command()
	{
		GameData.point += addPointValue;
	}
}
