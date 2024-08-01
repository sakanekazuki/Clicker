using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// HCGを見たり、立ち絵をじっくり見ることができる（本棚）
public class BookShelfManager : MonoBehaviour
{
	static BookShelfManager instance;
	public static BookShelfManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<BookShelfManager>();
				if (instance == null)
				{
					Debug.LogError("BookShelfが存在しません");
					return null;
				}
			}
			return instance;
		}
	}

	// 
	[SerializeField]
	GameObject bookGroup;
	// ボタンを格納するオブジェクト(本棚)
	[SerializeField]
	GameObject bookShelf;

	[SerializeField]
	GameObject hbookGroup;
	// HCG用のボタンを格納するオブジェクト
	[SerializeField]
	GameObject hbookShelf;

	// 画像グループ
	[SerializeField]
	GameObject illustGroup;

	// 立ち絵を選ぶ画面に切り替えるボタン
	[SerializeField]
	GameObject changeBtn1;
	// HCGを選ぶ画面に切り替えるボタン
	[SerializeField]
	GameObject changeBtn2;

	// 画像を表示するImage
	[SerializeField]
	Image illustImg;

	// 画像の差分
	List<Sprite> illustDifference = new List<Sprite>();

	// 表示している差分の番号
	int differenceNumber = 0;

	// キャラクター選択ボタン
	[SerializeField]
	GameObject characterSelectPrefab;

	// true = 開いている
	bool isOpen = false;

	// ボタン管理
	List<BookSelectManager> normalBookSelectManagers = new List<BookSelectManager>();
	List<BookSelectManager> feverBookSelectManagers = new List<BookSelectManager>();
	// 通常ギャラリー
	bool isNormal = true;

	// 普通の画像を選択している時に表示される差分を切り替えるボタン
	[SerializeField]
	GameObject normalButtonGroup;
	// フィーバーの画像を選択しているときに表示される差分を切り替えるボタン
	[SerializeField]
	GameObject feverButtonGroup;

	// 画像サイズ
	Vector2 spriteSize = Vector2.zero;
	Vector2 feverSpriteSize = Vector2.zero;

	// 立ち絵選択画面にしている際にタブに表示するオブジェクト
	[SerializeField]
	GameObject normalTabSelectMark;
	// HCG選択画面にしている際にタブに表示するオブジェクト
	[SerializeField]
	GameObject feverTabSelectMark;

	// 下線
	List<TabButtonController> ntabBtnCon = new List<TabButtonController>();
	List<TabButtonController> ftabBtnCon = new List<TabButtonController>();

	[SerializeField]
	GameObject bookButton;

	[SerializeField]
	List<SpriteList> normalGallerySprites = new List<SpriteList>();

	[SerializeField]
	List<SpriteList> feverGallerySprites_en = new List<SpriteList>();
	[SerializeField]
	List<SpriteList> feverGallerySprites_jp = new List<SpriteList>();

	[SerializeField]
	SelectButtonGroups normalSelectButtonGroups;

	[SerializeField]
	SelectButtonGroups feverSelectButtonGroups;

	private void Awake()
	{
		instance = this;
	}

	public void Init()
	{
		// データ削除対応
		if (normalBookSelectManagers.Count != 0)
		{
			// 必要ないオブジェクト削除
			foreach (var manager in normalBookSelectManagers)
			{
				Destroy(manager.gameObject);
			}
			normalBookSelectManagers.Clear();

			foreach (var manager in feverBookSelectManagers)
			{
				Destroy(manager.gameObject);
			}
			feverBookSelectManagers.Clear();

			ntabBtnCon.Clear();
			ftabBtnCon.Clear();
		}

		// キャラクター管理クラス取得
		var tree = GameObject.FindObjectOfType<TreeManager>();
		// 立ち絵
		for (int i = 0; i < tree?.TreeNum(); ++i)
		{
			// ボタン生成
			var obj = Instantiate(characterSelectPrefab, bookShelf.transform);
			var bsm = obj.GetComponent<BookSelectManager>();
			// キャラクターID設定
			bsm.Init(i, SkinSelectManager.Instance.GetGoddesSprite(i), true, GameData.GoddessOpen.Contains(i));
			normalBookSelectManagers.Add(bsm);
			ntabBtnCon.Add(bsm.GetComponent<TabButtonController>());
		}
		// HCG
		for (int i = 0; i < tree?.FeverNum(); ++i)
		{
			// ボタン生成
			var obj = Instantiate(characterSelectPrefab, hbookShelf.transform);
			var bsm = obj.GetComponent<BookSelectManager>();
			// キャラクターID設定
			bsm.Init(i, SkinSelectManager.Instance.GetGoddesSprite(i, false), false, GameData.feverSpriteOpen.Contains(i));
			feverBookSelectManagers.Add(bsm);
			ftabBtnCon.Add(bsm.GetComponent<TabButtonController>());
		}
		if (tree != null)
		{
			// 画像サイズ設定
			//spriteSize = normalGallerySprites[0].sprites[0].bounds.size * 100;
			feverSpriteSize = feverGallerySprites_jp[0].sprites[0].bounds.size * 100;
		}
		// 大きさをもとに戻す
		transform.localScale = Vector3.zero;
	}

	/// <summary>
	/// キャラクター開放
	/// </summary>
	/// <param name="number">開放するキャラクターの番号</param>
	/// <param name="isNormal">立ち絵</param>
	public void CharacterOpen(int number, bool isNormal)
	{
		var v = isNormal ? normalBookSelectManagers[number] : feverBookSelectManagers[number];
		v.CharacterOpen();
	}

	/// <summary>
	/// 開く
	/// </summary>
	public void OpenClose()
	{
		// オープン状態反転
		isOpen = !isOpen;
		// グループのアクティブ状態を変更
		bookGroup.SetActive(isOpen);
		// タブボタンのアクティブ状態を変更
		changeBtn1.SetActive(isOpen);
		changeBtn2.SetActive(isOpen);
		// HCGのグループは非表示
		hbookGroup.SetActive(false);
		// 立ち絵差分のボタングループは表示
		normalButtonGroup.SetActive(true);
		// HCG差分ボタンは非表示
		feverButtonGroup.SetActive(false);
		// タブボタンの下線のアクティブ状態を変更
		normalTabSelectMark.SetActive(isOpen);
		feverTabSelectMark.SetActive(false);
		//illustGroup.SetActive(false);
		// オープン状態であればサイズを1にする
		transform.localScale = isOpen ? Vector3.one : Vector3.zero;

		// ギャラリーに表示するものを選択するボタンのアクティブ状態を変更
		foreach (var v in ntabBtnCon)
		{
			v.ChangeView(ntabBtnCon.IndexOf(v) == 0);
		}
		foreach (var v in ftabBtnCon)
		{
			v.ChangeView(false);
		}
		Expansion();
		if (isOpen)
		{
			var mainCanvas = GameObject.FindObjectOfType<MainCanvas>();
			// 設定ウィンドウを閉じる
			mainCanvas.OpenSettingWindow(false);
			// レコードウィンドウを閉じる
			mainCanvas.OpenRecordWindow(false);
			// 立ち絵、CG切り替え画面を閉じる
			SkinSelectManager.Instance.Hide();
		}
	}

	/// <summary>
	/// 閉じる
	/// </summary>
	public void Close()
	{
		// オープン状態反転
		isOpen = false;
		// グループのアクティブ状態を変更
		bookGroup.SetActive(false);
		// タブボタンのアクティブ状態を変更
		changeBtn1.SetActive(false);
		changeBtn2.SetActive(false);
		// HCGのグループは非表示
		hbookGroup.SetActive(false);
		// 立ち絵差分のボタングループは表示
		normalButtonGroup.SetActive(true);
		// HCG差分ボタンは非表示
		feverButtonGroup.SetActive(false);
		// タブボタンの下線のアクティブ状態を変更
		normalTabSelectMark.SetActive(isOpen);
		feverTabSelectMark.SetActive(false);
		//illustGroup.SetActive(false);
		// オープン状態であればサイズを1にする
		transform.localScale = Vector3.zero;

		bookButton.GetComponent<TabButtonController>().ChangeView(false);

		// ギャラリーに表示するものを選択するボタンのアクティブ状態を変更
		foreach (var v in ntabBtnCon)
		{
			v.ChangeView(ntabBtnCon.IndexOf(v) == 0);
		}
		foreach (var v in ftabBtnCon)
		{
			v.ChangeView(false);
		}
		Expansion();
	}

	/// <summary>
	/// 詳しく見る
	/// </summary>
	/// <param name="characterNumber">詳しく見るキャラクターの番号</param>
	/// <param name="isNormal">立ち絵かどうか</param>
	public void Expansion(int characterNumber = 0, bool isNormal = true)
	{
		this.isNormal = isNormal;
		// 開放されていない画像判定
		if (isNormal)
		{
			if (!GameData.GoddessOpen.Contains(characterNumber))
			{
				return;
			}
		}
		else
		{
			if (!GameData.feverSpriteOpen.Contains(characterNumber))
			{
				return;
			}
		}

		var tree = GameObject.FindObjectOfType<TreeManager>();
		// 見るキャラクターの画像取得
		illustDifference = isNormal ?
			normalGallerySprites[characterNumber].sprites :
			GameData.language == 1 ?
				feverGallerySprites_en[characterNumber].sprites :
				feverGallerySprites_jp[characterNumber].sprites;

		normalButtonGroup.SetActive(isNormal);
		feverButtonGroup.SetActive(!isNormal);

		if (isNormal)
		{
			foreach (var b in ntabBtnCon)
			{
				b.ChangeView(ntabBtnCon.IndexOf(b) == characterNumber);
			}
			foreach (var b in ftabBtnCon)
			{
				b.ChangeView(false);
			}
		}
		else
		{
			foreach (var b in ntabBtnCon)
			{
				b.ChangeView(false);
			}
			foreach (var b in ftabBtnCon)
			{
				b.ChangeView(ftabBtnCon.IndexOf(b) == characterNumber);
			}
		}
		// 下線を0番目のボタンに切り替える
		normalSelectButtonGroups.Change(0);
		feverSelectButtonGroups.Change(0);
		// ボタン非表示
		//bookGroup.SetActive(false);
		//hbookGroup.SetActive(false);
		//changeBtn1.SetActive(false);
		//changeBtn2.SetActive(false);
		// キャラクター画像表示
		//illustGroup.SetActive(true);
		// キャラクター表示
		SetDifferenceNumber(0);
	}

	/// <summary>
	/// 閉じる
	/// </summary>
	public void Reduction()
	{
		// ボタン表示
		bookGroup.SetActive(true);
		hbookGroup.SetActive(false);
		changeBtn1.SetActive(true);
		changeBtn2.SetActive(true);
		isNormal = true;
		// キャラクター画像非表示
		illustGroup.gameObject.SetActive(false);
	}

	/// <summary>
	/// 差分の番号設定
	/// </summary>
	/// <param name="number">設定する番号</param>
	public void SetDifferenceNumber(int number)
	{
		// 番号設定
		differenceNumber = number;

		// スプライトのデータ取得
		var s = illustDifference[differenceNumber];

		// 画像のサイズに対応
		var rect = illustImg.transform as RectTransform;

		// 画像のサイズ変更
		if (isNormal)
		{
			rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, s.bounds.size.x * 100);
			rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, s.bounds.size.y * 100);
		}
		else
		{
			rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
				feverSpriteSize.x);
			rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, feverSpriteSize.y);
		}
		// イラスト更新
		illustImg.sprite = s;
	}

	/// <summary>
	/// ギャラリー設定
	/// </summary>
	/// <param name="isNormal"></param>
	public void ChangeGallery(bool isNormal)
	{
		this.isNormal = isNormal;
		bookGroup.SetActive(isNormal);
		hbookGroup.SetActive(!isNormal);

		//normalButtonGroup.SetActive(isNormal);
		//feverButtonGroup.SetActive(!isNormal);

		normalTabSelectMark.SetActive(isNormal);
		feverTabSelectMark.SetActive(!isNormal);
	}
}