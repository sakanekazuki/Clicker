using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �M�������[�Ǘ��N���X
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
					Debug.LogError("GalleryManager�����݂��܂���");
					return null;
				}
			}
			return instance;
		}
	}

	// �J���{�^��
	[SerializeField]
	TabButtonController openBtn;

	// �V�X�^�[�̉摜
	[SerializeField]
	List<Sprite> sisterSprites = new List<Sprite>();
	// HCG�̉摜
	[SerializeField]
	List<Sprite> hcgSprites = new List<Sprite>();

	// �V�X�^�[�̉摜���Ǘ�����I�u�W�F�N�g
	[SerializeField]
	GameObject sisterContent;

	// HCG�̉摜���Ǘ�����I�u�W�F�N�g
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