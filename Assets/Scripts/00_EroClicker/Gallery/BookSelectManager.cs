using Coffee.UIEffects;
using UnityEngine;
using UnityEngine.UI;

public class BookSelectManager : MonoBehaviour
{
	// キャラクターの番号
	int characterID = 0;
	// true = 普通のギャラリー
	bool isNormal = false;
	// true = ギャラリーを見ることができる
	bool isOpen = false;

	// 画像
	[SerializeField]
	Image buttonImg;

	UIGradient gradient;

	/// <summary>
	/// 初期化
	/// </summary>
	/// <param name="characterID">キャラクターID</param>
	/// <param name="sprite">画像</param>
	/// <param name="isNormal">通常のボタンかCGのボタンか</param>
	public void Init(int characterID, Sprite sprite, bool isNormal, bool isOpen)
	{
		// ID設定
		this.characterID = characterID;
		// スプライトの設定
		buttonImg.sprite = sprite;
		// フラグ設定
		this.isNormal = isNormal;
		// 見ることができるかどうか
		this.isOpen = isOpen;

		//// RectTransformに変換
		//var rect = buttonImg.transform as RectTransform;
		//// 画像サイズ修正
		//rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sprite.bounds.size.x * 3);
		//rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sprite.bounds.size.y * 3);

		// 黒くしたり、色をつけたり
		gradient = buttonImg.GetComponent<UIGradient>();
		gradient.enabled = !isOpen;
	}

	/// <summary>
	/// キャラクター開放
	/// </summary>
	public void CharacterOpen()
	{
		isOpen = true;
		gradient.enabled = !isOpen;
	}

	/// <summary>
	/// ギャラリーを開く
	/// </summary>
	public void Open()
	{
		BookShelfManager.Instance.Expansion(characterID, isNormal);
	}
}