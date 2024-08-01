using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// フィーバー
public sealed class FeverManager : MonoBehaviour
{
	static FeverManager instance;
	public static FeverManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<FeverManager>();
				if (instance == null)
				{
					Debug.LogError("FeverManagerが存在しません");
					return null;
				}
			}
			return instance;
		}
	}

	// フィーバーになる値
	[SerializeField]
	float max = 100;
	// 進捗
	float progress = 0;
	public float Progress
	{
		get
		{
			return progress;
		}
	}
	public bool isHalf
	{
		get;
		private set;
	}
	// true = フィーバー状態
	bool isFever = false;
	public bool IsFever { get => isFever; }

	// フィーバーの際の進捗
	float feverProgress = 0;

	// 進捗を表すスライダー
	[SerializeField]
	Slider progressSlider;

	// フィーバー中に表示しないボタン
	[SerializeField]
	List<GameObject> noneFeverBtn = new List<GameObject>();

	[SerializeField]
	Sprite normalHandleSprite;
	[SerializeField]
	Sprite feverHandleSprite;

	// ハンドル
	[SerializeField]
	Image handle;

	private void Awake()
	{
		instance = this;
		isHalf = false;
		progressSlider.value = 0;
		handle.sprite = normalHandleSprite;
	}

	private void OnDestroy()
	{
		instance = null;
	}

	private void Update()
	{
		if(IsFever)
		{
			progressSlider.value = (float)GameData.ShardBiforestBoost / (float)GameData.SHARD_BIFOREST_BOOST_TIME_BASE;
		}
	}

	public void Init()
	{
		progressSlider.value = 0;
		feverProgress = 0;
		progress = 0;
		isFever = false;
	}

	/// <summary>
	/// 進行
	/// </summary>
	/// <param name="value">進んだ量</param>
	public void AddProgress(float value)
	{
		// フィーバー状態なら進めない
		if (isFever)
		{
			// フィーバー中なら進める
			feverProgress += value;
			
			// 20の倍数で次の画像に
			if (feverProgress % 20 == 0)
			{
				GameObject.FindObjectOfType<TreeManager>().NextFeverSprite();
				//if (((feverProgress / 20) % 5) == 4)
				//{
				//	++GameData.climaxIndex;
				//}
			}
			return;
		}
		// 進捗を追加
		progress += value;
		// 進捗度合いをスライダーに反映する
		progressSlider.value = progress / max;

		// 半分進んだら画像切替
		if (progressSlider.value >= 0.75f)
		{
			isHalf = true;
			GameObject.FindObjectOfType<TreeManager>().Half(GameData.treeIndex);
		}

		// 最大になったらフィーバー開始
		if (progress >= max)
		{
			FeverStart();
		}
	}

	/// <summary>
	/// フィーバー開始
	/// </summary>
	public void FeverStart()
	{
		isHalf = false;
		progress = 0;
		progressSlider.value = 100;
		isFever = true;
		// フィーバー(ブースト)状態にする
		//Debug.Log("フィーバー");
		GameObject.FindObjectOfType<ShardBiforestManager>().ClickShard();

		// 画像をフィーバーの画像に変更
		GameObject.FindObjectOfType<TreeManager>().Fever(GameData.feverSpriteIndex);
		handle.sprite = feverHandleSprite;
		foreach (var v in noneFeverBtn)
		{
			v.SetActive(false);
		}
	}

	/// <summary>
	/// フィーバー終了
	/// </summary>
	public void FeverEnd()
	{
		isFever = false;
		//Debug.Log("フィーバー終了");

		// フィーバー状態ではない画像に変更
		GameObject.FindObjectOfType<TreeManager>().Init(GameData.treeIndex);
		handle.sprite = normalHandleSprite;
		foreach (var v in noneFeverBtn)
		{
			v.SetActive(true);
		}
	}
}
