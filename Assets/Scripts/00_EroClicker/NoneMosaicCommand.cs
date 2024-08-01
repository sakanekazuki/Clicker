using UnityEngine;

// コマンド入力
public class NoneMosaicCommand : CommandBase
{
	/// <summary>
	/// コマンド入力をしたときの処理
	/// </summary>
	protected override void Command()
	{
		Debug.Log("モザイクなし");
	}
}