using System.Collections;
using System.Collections.Generic;
using Coffee.UIEffects;
using naichilab.Scripts.Extensions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelectButtonManager : MonoBehaviour
{
	[SerializeField] GameObject button;
	[SerializeField] List<GameObject> OpenViews;
	[SerializeField] List<GameObject> CloseViews;
	[SerializeField] Image image;
	public SkinSelectManager skinSelectManager;
	[SerializeField] Text priceText;
	[SerializeField] Text InstLvText;
	[SerializeField] Text NameText;
	[SerializeField] AudioClip click_clip;
	public int GoddesID = 0;
	private bool isInit = false;
	public MainCanvas mainCanvas;
	public double TargetInstLv;
	private UIGradient uiEffect;
	private Button ParentButton;

	public void Init(int _goddessID = 0, bool bought = true, bool isNormal = true)
	{
		//Debug.Log($"GoddessOpen {_goddessID} {bought}");
		GoddesID = _goddessID;
		image.sprite = skinSelectManager.GetGoddesSprite(GoddesID, isNormal);
		//if(isNormal)
		//{
		//var s = image.sprite;
		//var ssize = s.bounds.size * 3;
		//var rect = image.transform as RectTransform;
		//rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ssize.x);
		//rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ssize.y);
		//}
		uiEffect = image.gameObject.GetComponent<UIGradient>();
		ParentButton = button.GetComponent<Button>();


		GoddessSkinClass goddessGameData = isNormal ? GameData.mGoddesSkin[GoddesID] : GameData.mFeverSkin[GoddesID];
		priceText.text = goddessGameData.price.ToReadableString();
		//InstLvText.text = goddessGameData.OpenInstLv.ToReadableString();
		//InstLvText.text = "";

		TargetInstLv = goddessGameData.OpenInstLv;

		LanguageTranslation languageTranslation = NameText.gameObject.GetComponent<LanguageTranslation>();
		NameText.text = goddessGameData.name;
		languageTranslation.TranslationKey = goddessGameData.name;

		UpdateView(bought);

		isInit = true;
	}
	public void UpdateView(bool bought = true)
	{
		foreach (GameObject obj in OpenViews)
		{
			obj.SetActive(bought);
		}
		foreach (GameObject obj in CloseViews)
		{
			obj.SetActive(!bought);
		}
		uiEffect.enabled = !bought;
		ParentButton.enabled = true;
		if (bought)
		{
			button.GetComponent<ButtonStatusController>().TargetPoint = 0;
		}
	}

	private void UpdateIconColor()
	{
		bool bought =
			SkinSelectManager.Instance.isNormalGallery ?
				GameData.GoddessOpen.Contains(GoddesID) : GameData.feverSpriteOpen.Contains(GoddesID);
		if (!bought)
		{
			//Debug.Log($"UpdateIconColor {GoddesID} {mainCanvas.total_inst_lv} < {TargetInstLv}");
			//if (mainCanvas.total_inst_lv < TargetInstLv)
			//{
			//黒
			uiEffect.enabled = true;
			ParentButton.enabled = true;
			//InstLvText.color = GameData.COLOR_FALSE;
			//}
			//else
			//{
			//黒解除
			//uiEffect.enabled = false;
			//if (!ParentButton.enabled) ParentButton.enabled = true;
			//InstLvText.color = GameData.COLOR_TRUE;
			//}
		}
		else
		{
			UpdateView(bought);
		}
	}

	private void Update()
	{
		if (isInit)
		{
			UpdateIconColor();
		}
	}

	public void ButtonClick()
	{
		var isOpen =
			SkinSelectManager.Instance.isNormalGallery ?
				GameData.GoddessOpen.Contains(GoddesID) : GameData.feverSpriteOpen.Contains(GoddesID);

		GoddessSkinClass goddessGameData = SkinSelectManager.Instance.isNormalGallery ?
			GameData.mGoddesSkin[GoddesID] : GameData.mFeverSkin[GoddesID];

		if (
			!isOpen
			//&& mainCanvas.total_inst_lv >= goddessGameData.OpenInstLv
			&& GameData.point >= goddessGameData.price
			)
		{
			//買える
			GameData.point -= goddessGameData.price;
			if (SkinSelectManager.Instance.isNormalGallery)
			{
				GameData.GoddessOpen.Add(GoddesID);
			}
			else
			{
				GameData.feverSpriteOpen.Add(GoddesID);
			}
			BookShelfManager.Instance.CharacterOpen(GoddesID, SkinSelectManager.Instance.isNormalGallery);
			DataManager.Instance.Save();

			UpdateView(true);
			skinSelectManager.UpdateView();
		}
		else if (isOpen)
		{
			//切り替え
			skinSelectManager.SetSprite(GoddesID);

		}
		//SE
		SoundManager.Instance.PlaySe(click_clip);

		skinSelectManager.UpdateSelectView();
	}
}
