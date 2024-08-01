using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour, ITree
{
	[SerializeField] List<Sprite> textureTree = new List<Sprite>();
	[SerializeField] SpriteRenderer spriteTree;

	// 半分進んだときに表示される画像
	[SerializeField]
	List<Sprite> halfProgressSprites = new List<Sprite>();

	// 移動速度
	[SerializeField, Tooltip("移動速度")]
	float moveSpeed = 0.1f;

	// 反転する距離
	[SerializeField, Tooltip("反転する距離")]
	float inversionDistance = 0.4f;

	// フィーバー中のスプライト
	[SerializeField]
	List<Sprite> feverSprites = new List<Sprite>();

	// 表情
	[SerializeField]
	List<SpriteList> facialExpressionLists = new List<SpriteList>();

	// 
	[SerializeField]
	List<SpriteList> feverDifference = new List<SpriteList>();
	[SerializeField]
	List<SpriteList> feverDifference_jp = new List<SpriteList>();
	int feverSpriteNumber = 0;

	// フィーバー中に表示するオブジェクト
	[SerializeField]
	GameObject frameObj;
	[SerializeField]
	GameObject frameBGObj;

	// 普通のクリックで出る画像の番号
	List<int> normalClickSpriteNumbers = new List<int>();
	public List<int> NormalClickSpriteNumvers
	{
		get => normalClickSpriteNumbers;
	}

	// true = フィーバー中の画像切替がランダム
	bool isFeverSpriteRandome = false;

	// フィーバー中に切り替えようとした場合trueになる
	//bool isFeverSriteChange = false;
	//int characterNumber = 0;

	//private void Update()
	//{
	//	if(FeverManager.Instance.IsFever && isFeverSriteChange)
	//	{
	//		GameData.feverSpriteIndex = characterNumber;
	//	}
	//}

	public void Init(int index = 0)
	{
		if (index >= textureTree.Count)
		{
			index = 0;
		}
		frameObj.SetActive(false);
		frameBGObj.SetActive(false);
		spriteTree.sprite = textureTree[index];

		spriteTree.maskInteraction = SpriteMaskInteraction.None;
	}

	/// <summary>
	/// 半分進んだときに画像を切替える
	/// </summary>
	/// <param name="number">切り替える画像の番号</param>
	public void Half(int number)
	{
		if (number >= halfProgressSprites.Count || number < 0)
		{
			number = 0;
		}
		spriteTree.sprite = halfProgressSprites[number];
	}

	/// <summary>
	/// 表情変更
	/// </summary>
	/// <param name="number">変更する番号</param>
	public void FacialExpressionChange(int number = 0)
	{
		if (facialExpressionLists[GameData.treeIndex].sprites.Count <= number || number < 0)
		{
			number = 0;
		}
		spriteTree.sprite = facialExpressionLists[GameData.treeIndex].sprites[number];
	}

	/// <summary>
	/// フィーバー状態の画像を表示
	/// </summary>
	/// <param name="number"></param>
	public void Fever(int number = 0)
	{
		if (number >= feverSprites.Count)
		{
			number = 0;
		}
		// フィーバー状態であれば切り替えない
		//if (FeverManager.Instance.IsFever)
		//{
		//	isFeverSriteChange = true;
		//	characterNumber = number;
		//	return;
		//}
		
		if (GameData.language == 1)
		{
			spriteTree.sprite = feverSprites[number];
		}
		else
		{
			spriteTree.sprite = feverDifference_jp[number].sprites[0];
		}
		frameObj.SetActive(true);
		frameBGObj.SetActive(true);
		spriteTree.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
	}

	/// <summary>
	/// 次のフィーバー画像に変える
	/// </summary>
	public void NextFeverSprite()
	{
		if (isFeverSpriteRandome)
		{
			var n = Random.Range(0, feverDifference[GameData.feverSpriteIndex].sprites.Count);
			spriteTree.sprite = (GameData.language == 1) ? feverDifference[GameData.feverSpriteIndex].sprites[n] :
				feverDifference_jp[GameData.feverSpriteIndex].sprites[n];
		}
		else
		{
			// 次の画像の番号を入れる
			++feverSpriteNumber;
			// 最後の画像の次が最初の画像になるようにあまりを代入
			feverSpriteNumber %= feverDifference[GameData.feverSpriteIndex].sprites.Count;
			// 画像を設定
			spriteTree.sprite = (GameData.language == 1) ? feverDifference[GameData.feverSpriteIndex].sprites[feverSpriteNumber] :
				feverDifference_jp[GameData.feverSpriteIndex].sprites[feverSpriteNumber];
		}
	}

	/// <summary>
	/// 表情の数取得
	/// </summary>
	/// <returns></returns>
	public int FacialExpressionNum()
	{
		return facialExpressionLists[GameData.treeIndex].sprites.Count;
	}

	/// <summary>
	/// フィーバーの画像数取得
	/// </summary>
	/// <returns>画像数</returns>
	public int FeverNum()
	{
		return feverSprites.Count;
	}

	public int TreeNum()
	{
		return textureTree.Count;
	}

	/// <summary>
	/// 立ち絵の画像取得
	/// </summary>
	/// <param name="characterNumber">取得するキャラクターの番号</param>
	/// <returns>キャラクターの画像</returns>
	public List<Sprite> GetNormalGallerySprites(int characterNumber)
	{
		// キャラクターの番号がキャラクター数より多い場合番号を0にする
		if (characterNumber >= facialExpressionLists.Count)
		{
			characterNumber = 0;
		}
		return facialExpressionLists[characterNumber].sprites;
	}

	/// <summary>
	/// HCG画像取得
	/// </summary>
	/// <param name="characterNumber">取得するキャラクターの番号</param>
	/// <returns>取得した画像</returns>
	public List<Sprite> GetFeverGallerySprites(int characterNumber)
	{
		// キャラクターの番号がキャラクター数より多い場合番号を0にする
		if (characterNumber >= feverDifference.Count)
		{
			characterNumber = 0;
		}
		if (GameData.language == 1)
		{
			return feverDifference[characterNumber].sprites;
		}
		else
		{
			return feverDifference_jp[characterNumber].sprites;
		}
	}

	/// <summary>
	/// 普通のクリックで出現する番号の設定
	/// </summary>
	/// <param name="number">設定する番号</param>
	/// <param name="isAppear">出現するものかどうか</param>
	public void SetNormalClickSpriteNumber(int number, bool isAppear = true)
	{
		// 設定する番号が入っているかどうか
		var b = normalClickSpriteNumbers.Contains(number);
		// 出現する番号の場合は設定されていない場合のみ設定
		if (isAppear)
		{
			if (!b)
			{
				normalClickSpriteNumbers.Add(number);
			}
		}
		// 出現しない番号の場合は設定されている場合のみ削除
		else
		{
			if (b)
			{
				normalClickSpriteNumbers.Remove(number);
			}
		}
	}

	/// <summary>
	/// フィーバー中の画像切替状態切替　(「ランダム⇔連番」を切り替える)
	/// </summary>
	public void FeverSpriteRandom()
	{
		isFeverSpriteRandome = !isFeverSpriteRandome;
	}

	// true = 移動している
	bool isMove = false;

	IEnumerator ESway()
	{
		// 初期位置取得
		var pos = transform.parent.position;
		bool isRight = true;
		bool isStop = false;
		isMove = true;
		while (true)
		{
			if (isStop)
			{
				// 元の位置に戻す
				transform.parent.position += new Vector3(moveSpeed, 0);
				if (pos.x <= transform.parent.position.x)
				{
					// 元の位置を超えると元の位置に戻す
					transform.parent.position = pos;
					break;
				}
			}
			else
			{
				// 右に動かす
				if (isRight)
				{
					transform.parent.position += new Vector3(moveSpeed, 0);
				}
				// 左に動かす
				else
				{
					transform.parent.position -= new Vector3(moveSpeed, 0);
				}
				// 指定した距離離れたら反対に移動する
				if (Vector3.Distance(pos, transform.parent.position) >= inversionDistance)
				{
					// 動かす方向の反転
					isRight = !isRight;
					if (isRight)
					{
						isStop = true;
					}
				}
			}
			// 位置フレーム待つ
			yield return null;
		}
		isMove = false;

	}

	void ITree.Sway()
	{
		if (!isMove)
		{
			StartCoroutine(ESway());
		}
	}
}