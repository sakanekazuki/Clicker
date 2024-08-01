using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ギャラリー管理クラス
public class GalleryManager : MonoBehaviour
{
	static GalleryManager instance;
	public static GalleryManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<GalleryManager>();
				if (instance == null)
				{
					Debug.LogError("GalleryManagerが存在しません");
					return null;
				}
			}
			return instance;
		}
	}

	// 開くボタン
	[SerializeField]
	TabButtonController openBtn;

	// シスターの画像
	[SerializeField]
	List<Sprite> sisterSprites = new List<Sprite>();
	// HCGの画像
	[SerializeField]
	List<Sprite> hcgSprites = new List<Sprite>();

	// シスターの画像を管理するオブジェクト
	[SerializeField]
	GameObject sisterContent;

	// HCGの画像を管理するオブジェクト
	[SerializeField]
	GameObject hcgContent;



	private void Awake()
	{
		instance = this;
	}

	private void OnDestroy()
	{
		instance = null;
	}


}