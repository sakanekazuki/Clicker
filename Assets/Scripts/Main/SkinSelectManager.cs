using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WaterRippleForScreens;

public class SkinSelectManager : MonoBehaviour
{
	public static SkinSelectManager Instance { get; private set; }
	[SerializeField] TabButtonController OpenButton;
	[SerializeField] TabButtonController CGOpenButton;
	//[SerializeField] TabButtonController OnOffButton;
	[SerializeField] Image GoddessImage;
	[SerializeField] List<Sprite> GoddessImages;
	[SerializeField] List<Sprite> FeverImages;
	[SerializeField] GameObject ButtonSelectPrefabs;
	[SerializeField] GameObject SelectContent;
	[SerializeField] GameObject FeverContent;
	[SerializeField] GameObject InfoWindow;
	[SerializeField] Text BonusText;
	[SerializeField] Text BonusText2;
	[SerializeField] GameObject normalGalleryObject;
	[SerializeField] GameObject feverGalleryObject;
	[SerializeField] GameObject selectMarck1; // 立ち絵ギャラリーの選択マーク
	[SerializeField] GameObject selectMarck2; // HCGギャラリーの選択マーク
	List<GameObject> buttons = new List<GameObject>();
	List<GameObject> feverButtons = new List<GameObject>();
	public MainCanvas mainCanvas;


	private bool isInit = false;

	// true = ノーマルギャラリー
	public bool isNormalGallery = true;

	// キャラクターを変更する際に出すエフェクト
	[SerializeField]
	GameObject characterChangeEffect;

	// エフェクトを出している時間
	[SerializeField]
	float changeEffectTime;

	// ボタンを選択できない状態にするオブジェクト
	[SerializeField]
	GameObject cannotButtonSelectObject;

	public bool IsEffectDisplay
	{
		get;
		private set;
	}

	private void OnEnable()
	{
		Instance = this;
		gameObject.transform.localScale = Vector3.one;
		normalGalleryObject.SetActive(isNormalGallery);
		feverGalleryObject.SetActive(!isNormalGallery);
		OnExitHideInfo();
		Init();
		cannotButtonSelectObject?.SetActive(false);
	}

	private void OnDisable()
	{
		normalGalleryObject.SetActive(false);
		feverGalleryObject.SetActive(false);
	}

	private void Init()
	{

		if (isInit) return;

		isInit = true;

		foreach (KeyValuePair<int, GoddessSkinClass> pair in GameData.mGoddesSkin)
		{
			GameObject obj = Instantiate(ButtonSelectPrefabs, SelectContent.transform, false) as GameObject;
			obj.name = pair.Key.ToString();
			obj.SetActive(true);

			SkinSelectButtonManager ssbm = obj.GetComponent<SkinSelectButtonManager>();
			ssbm.mainCanvas = mainCanvas;
			ssbm.skinSelectManager = this;
			ssbm.Init(pair.Key, GameData.GoddessOpen.Contains(pair.Key));

			ButtonStatusController bsc = obj.GetComponent<ButtonStatusController>();
			bsc.TargetPoint = pair.Value.price;

			buttons.Add(obj);
		}
		foreach (KeyValuePair<int, GoddessSkinClass> pair in GameData.mFeverSkin)
		{
			GameObject obj = Instantiate(ButtonSelectPrefabs, FeverContent.transform, false) as GameObject;
			obj.name = pair.Key.ToString();
			obj.SetActive(true);

			SkinSelectButtonManager ssbm = obj.GetComponent<SkinSelectButtonManager>();
			ssbm.mainCanvas = mainCanvas;
			ssbm.skinSelectManager = this;
			ssbm.Init(pair.Key, GameData.feverSpriteOpen.Contains(pair.Key), false);

			ButtonStatusController bsc = obj.GetComponent<ButtonStatusController>();
			bsc.TargetPoint = pair.Value.price;

			feverButtons.Add(obj);
		}

		ScrollRect s = SelectContent.transform.parent.parent.gameObject.GetComponent<ScrollRect>();
		s.verticalNormalizedPosition = 1f;

		selectMarck1.SetActive(false);
		selectMarck2.SetActive(false);
	}

	public void ResetUI()
	{
		foreach (Transform child in SelectContent.transform)
		{
			Destroy(child.gameObject);
		}
		foreach (Transform child in FeverContent.transform)
		{
			Destroy(child.gameObject);
		}
		buttons = new List<GameObject>();
		feverButtons = new List<GameObject>();
		isInit = false;
	}

	public void UpdateView()
	{
		foreach (GameObject obj in isNormalGallery ? buttons : feverButtons)
		{
			SkinSelectButtonManager ssbm = obj.GetComponent<SkinSelectButtonManager>();
			ssbm.UpdateView(isNormalGallery ?
				GameData.GoddessOpen.Contains(ssbm.GoddesID) :
				GameData.feverSpriteOpen.Contains(ssbm.GoddesID));
		}

		BonusText.text = GameData.GODDESS_POW_TEXT.Replace("@", CalcData.GetGoddessPow().ToString());
		BonusText2.text = GameData.GODDESS_POW_TEXT.Replace("@", CalcData.GetGoddessPow().ToString());
	}
	public void UpdateSelectView()
	{
		var v = isNormalGallery ? GameData.treeIndex : GameData.feverSpriteIndex;
		foreach (GameObject obj in isNormalGallery ? buttons : feverButtons)
		{
			SkinSelectButtonManager ssbm = obj.GetComponent<SkinSelectButtonManager>();
			obj.GetComponent<TabButtonController>().ChangeView(v == ssbm.GoddesID);
		}
	}

	public void OpenSkinWindow(bool isNormalGallery)
	{
		// 普通のギャラリーとHCGギャラリーの変更
		var isChange = this.isNormalGallery != isNormalGallery;
		this.isNormalGallery = isNormalGallery;
		if (CalcData.CheckAwakeToMode(GameData.AwakeModeNum.GODDESS))
		{
			OpenButton.ChangeViewOrClose();
			if (isChange && (!normalGalleryObject.activeSelf && !feverGalleryObject.activeSelf))
			{
				OpenButton.ChangeViewOrClose();
			}
			selectMarck1.SetActive(normalGalleryObject.activeSelf);
			selectMarck2.SetActive(feverGalleryObject.activeSelf);
			//OnOffButton.ChangeView(GameData.GoddessView == 1);
			UpdateView();
			UpdateSelectView();

			if (normalGalleryObject.activeSelf || feverGalleryObject.activeSelf)
			{
				var mainCanvas = GameObject.FindObjectOfType<MainCanvas>();
				// 設定ウィンドウを閉じる
				mainCanvas.OpenSettingWindow(false);
				// レコードウィンドウを閉じる
				mainCanvas.OpenRecordWindow(false);
				// ギャラリーを閉じる
				BookShelfManager.Instance.Close();
			}
		}
		else
		{
			MessageManager.Instance.PlayMessage(new List<string>() { LanguageCSV.Instance.GetCSV(GameData.MESSAGE_CLOSE_MENU) });
		}

	}
	public void Show()
	{
		OpenButton.ChangeView(true);
	}
	public void Hide()
	{
		OpenButton.ChangeView(false);
		CGOpenButton.ChangeView(false);
		selectMarck1.SetActive(false);
		selectMarck2.SetActive(false);
	}


	public void ButtonChangeOnOff()
	{
		//ViewImage(GameData.GoddessView == 0);

		//GameData.GoddessView = GameData.GoddessView == 0 ? 1 : 0;
		//DataManager.Instance.Save();
	}

	public void ViewImage(bool b = true)
	{
		GoddessImage.gameObject.SetActive(b);
		//OnOffButton.ChangeView(b);
	}
	public void SetSprite(int _index = 0, bool isInit = true)
	{
		if (_index >= (this.isNormalGallery ? GoddessImages.Count : FeverImages.Count))
		{
			_index = 0;
		}

		//GoddessImage.sprite = GoddessImages[_index];

		if (isNormalGallery)
		{
			GameData.treeIndex = _index;
			if (isInit && !FeverManager.Instance.IsFever)
			{
				StartCoroutine(SpawnCharacterChangeEffect());
			}
		}
		else
		{
			GameData.feverSpriteIndex = _index;
			if (FeverManager.Instance.IsFever)
			{
				GameObject.FindObjectOfType<TreeManager>().Fever(_index);
			}
		}


		DataManager.Instance.Save();
	}

	public Sprite GetGoddesSprite(int _index = 0, bool isNormal = true)
	{
		if (_index >= (isNormal ? GoddessImages.Count : FeverImages.Count))
		{
			_index = 0;
		}
		if (!CalcData.CheckAwakeToMode(GameData.AwakeModeNum.GODDESS))
		{
			_index = 0;
		}

		if (isNormal)
		{
			return GoddessImages[_index];
		}
		else
		{
			return FeverImages[_index];
		}
	}

	public void OnEnterShowInfo()
	{
		//InfoWindow.SetActive(true);
	}
	public void OnExitHideInfo()
	{
		//InfoWindow.SetActive(false);
	}

	IEnumerator SpawnCharacterChangeEffect()
	{
		if (IsEffectDisplay || FeverManager.Instance.IsFever)
		{
			yield break;
		}

		cannotButtonSelectObject.SetActive(true);

		// メインカメラ取得
		var c = Camera.main;
		// 波紋生成するコンポーネント取得
		var rippleGene = c.GetComponent<RippleGenerator>();
		var rippleEff = c.GetComponent<RippleEffect>();
		// 波紋生成開始
		rippleGene.enabled = true;
		// エフェクトが出ている状態にする
		IsEffectDisplay = true;
		// エフェクトが終わるまでの時間を待つ(エフェクトを出したい時間 - 1つのエフェクトが消える時間)
		yield return new WaitForSeconds(changeEffectTime - rippleEff.waveTime);
		// 波紋生成終了
		rippleGene.enabled = false;
		// 波紋が消えるまで待つ
		yield return new WaitForSeconds(rippleEff.waveTime);

		cannotButtonSelectObject.SetActive(false);

		IsEffectDisplay = false;

		if (!FeverManager.Instance.IsFever)
		{
			if (!FeverManager.Instance.isHalf)
			{
				GameObject.FindObjectOfType<TreeManager>().Init(GameData.treeIndex);
			}
		}
		//else
		//{
		//	GameObject.FindObjectOfType<TreeManager>().Fever(GameData.feverSpriteIndex);
		//}
	}
}
