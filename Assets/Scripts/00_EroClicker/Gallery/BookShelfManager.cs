using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// HCG��������A�����G���������茩�邱�Ƃ��ł���i�{�I�j
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
					Debug.LogError("BookShelf�����݂��܂���");
					return null;
				}
			}
			return instance;
		}
	}

	// 
	[SerializeField]
	GameObject bookGroup;
	// �{�^�����i�[����I�u�W�F�N�g(�{�I)
	[SerializeField]
	GameObject bookShelf;

	[SerializeField]
	GameObject hbookGroup;
	// HCG�p�̃{�^�����i�[����I�u�W�F�N�g
	[SerializeField]
	GameObject hbookShelf;

	// �摜�O���[�v
	[SerializeField]
	GameObject illustGroup;

	// �����G��I�ԉ�ʂɐ؂�ւ���{�^��
	[SerializeField]
	GameObject changeBtn1;
	// HCG��I�ԉ�ʂɐ؂�ւ���{�^��
	[SerializeField]
	GameObject changeBtn2;

	// �摜��\������Image
	[SerializeField]
	Image illustImg;

	// �摜�̍���
	List<Sprite> illustDifference = new List<Sprite>();

	// �\�����Ă��鍷���̔ԍ�
	int differenceNumber = 0;

	// �L�����N�^�[�I���{�^��
	[SerializeField]
	GameObject characterSelectPrefab;

	// true = �J���Ă���
	bool isOpen = false;

	// �{�^���Ǘ�
	List<BookSelectManager> normalBookSelectManagers = new List<BookSelectManager>();
	List<BookSelectManager> feverBookSelectManagers = new List<BookSelectManager>();
	// �ʏ�M�������[
	bool isNormal = true;

	// ���ʂ̉摜��I�����Ă��鎞�ɕ\������鍷����؂�ւ���{�^��
	[SerializeField]
	GameObject normalButtonGroup;
	// �t�B�[�o�[�̉摜��I�����Ă���Ƃ��ɕ\������鍷����؂�ւ���{�^��
	[SerializeField]
	GameObject feverButtonGroup;

	// �摜�T�C�Y
	Vector2 spriteSize = Vector2.zero;
	Vector2 feverSpriteSize = Vector2.zero;

	// �����G�I����ʂɂ��Ă���ۂɃ^�u�ɕ\������I�u�W�F�N�g
	[SerializeField]
	GameObject normalTabSelectMark;
	// HCG�I����ʂɂ��Ă���ۂɃ^�u�ɕ\������I�u�W�F�N�g
	[SerializeField]
	GameObject feverTabSelectMark;

	// ����
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
		// �f�[�^�폜�Ή�
		if (normalBookSelectManagers.Count != 0)
		{
			// �K�v�Ȃ��I�u�W�F�N�g�폜
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

		// �L�����N�^�[�Ǘ��N���X�擾
		var tree = GameObject.FindObjectOfType<TreeManager>();
		// �����G
		for (int i = 0; i < tree?.TreeNum(); ++i)
		{
			// �{�^������
			var obj = Instantiate(characterSelectPrefab, bookShelf.transform);
			var bsm = obj.GetComponent<BookSelectManager>();
			// �L�����N�^�[ID�ݒ�
			bsm.Init(i, SkinSelectManager.Instance.GetGoddesSprite(i), true, GameData.GoddessOpen.Contains(i));
			normalBookSelectManagers.Add(bsm);
			ntabBtnCon.Add(bsm.GetComponent<TabButtonController>());
		}
		// HCG
		for (int i = 0; i < tree?.FeverNum(); ++i)
		{
			// �{�^������
			var obj = Instantiate(characterSelectPrefab, hbookShelf.transform);
			var bsm = obj.GetComponent<BookSelectManager>();
			// �L�����N�^�[ID�ݒ�
			bsm.Init(i, SkinSelectManager.Instance.GetGoddesSprite(i, false), false, GameData.feverSpriteOpen.Contains(i));
			feverBookSelectManagers.Add(bsm);
			ftabBtnCon.Add(bsm.GetComponent<TabButtonController>());
		}
		if (tree != null)
		{
			// �摜�T�C�Y�ݒ�
			//spriteSize = normalGallerySprites[0].sprites[0].bounds.size * 100;
			feverSpriteSize = feverGallerySprites_jp[0].sprites[0].bounds.size * 100;
		}
		// �傫�������Ƃɖ߂�
		transform.localScale = Vector3.zero;
	}

	/// <summary>
	/// �L�����N�^�[�J��
	/// </summary>
	/// <param name="number">�J������L�����N�^�[�̔ԍ�</param>
	/// <param name="isNormal">�����G</param>
	public void CharacterOpen(int number, bool isNormal)
	{
		var v = isNormal ? normalBookSelectManagers[number] : feverBookSelectManagers[number];
		v.CharacterOpen();
	}

	/// <summary>
	/// �J��
	/// </summary>
	public void OpenClose()
	{
		// �I�[�v����Ԕ��]
		isOpen = !isOpen;
		// �O���[�v�̃A�N�e�B�u��Ԃ�ύX
		bookGroup.SetActive(isOpen);
		// �^�u�{�^���̃A�N�e�B�u��Ԃ�ύX
		changeBtn1.SetActive(isOpen);
		changeBtn2.SetActive(isOpen);
		// HCG�̃O���[�v�͔�\��
		hbookGroup.SetActive(false);
		// �����G�����̃{�^���O���[�v�͕\��
		normalButtonGroup.SetActive(true);
		// HCG�����{�^���͔�\��
		feverButtonGroup.SetActive(false);
		// �^�u�{�^���̉����̃A�N�e�B�u��Ԃ�ύX
		normalTabSelectMark.SetActive(isOpen);
		feverTabSelectMark.SetActive(false);
		//illustGroup.SetActive(false);
		// �I�[�v����Ԃł���΃T�C�Y��1�ɂ���
		transform.localScale = isOpen ? Vector3.one : Vector3.zero;

		// �M�������[�ɕ\��������̂�I������{�^���̃A�N�e�B�u��Ԃ�ύX
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
			// �ݒ�E�B���h�E�����
			mainCanvas.OpenSettingWindow(false);
			// ���R�[�h�E�B���h�E�����
			mainCanvas.OpenRecordWindow(false);
			// �����G�ACG�؂�ւ���ʂ����
			SkinSelectManager.Instance.Hide();
		}
	}

	/// <summary>
	/// ����
	/// </summary>
	public void Close()
	{
		// �I�[�v����Ԕ��]
		isOpen = false;
		// �O���[�v�̃A�N�e�B�u��Ԃ�ύX
		bookGroup.SetActive(false);
		// �^�u�{�^���̃A�N�e�B�u��Ԃ�ύX
		changeBtn1.SetActive(false);
		changeBtn2.SetActive(false);
		// HCG�̃O���[�v�͔�\��
		hbookGroup.SetActive(false);
		// �����G�����̃{�^���O���[�v�͕\��
		normalButtonGroup.SetActive(true);
		// HCG�����{�^���͔�\��
		feverButtonGroup.SetActive(false);
		// �^�u�{�^���̉����̃A�N�e�B�u��Ԃ�ύX
		normalTabSelectMark.SetActive(isOpen);
		feverTabSelectMark.SetActive(false);
		//illustGroup.SetActive(false);
		// �I�[�v����Ԃł���΃T�C�Y��1�ɂ���
		transform.localScale = Vector3.zero;

		bookButton.GetComponent<TabButtonController>().ChangeView(false);

		// �M�������[�ɕ\��������̂�I������{�^���̃A�N�e�B�u��Ԃ�ύX
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
	/// �ڂ�������
	/// </summary>
	/// <param name="characterNumber">�ڂ�������L�����N�^�[�̔ԍ�</param>
	/// <param name="isNormal">�����G���ǂ���</param>
	public void Expansion(int characterNumber = 0, bool isNormal = true)
	{
		this.isNormal = isNormal;
		// �J������Ă��Ȃ��摜����
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
		// ����L�����N�^�[�̉摜�擾
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
		// ������0�Ԗڂ̃{�^���ɐ؂�ւ���
		normalSelectButtonGroups.Change(0);
		feverSelectButtonGroups.Change(0);
		// �{�^����\��
		//bookGroup.SetActive(false);
		//hbookGroup.SetActive(false);
		//changeBtn1.SetActive(false);
		//changeBtn2.SetActive(false);
		// �L�����N�^�[�摜�\��
		//illustGroup.SetActive(true);
		// �L�����N�^�[�\��
		SetDifferenceNumber(0);
	}

	/// <summary>
	/// ����
	/// </summary>
	public void Reduction()
	{
		// �{�^���\��
		bookGroup.SetActive(true);
		hbookGroup.SetActive(false);
		changeBtn1.SetActive(true);
		changeBtn2.SetActive(true);
		isNormal = true;
		// �L�����N�^�[�摜��\��
		illustGroup.gameObject.SetActive(false);
	}

	/// <summary>
	/// �����̔ԍ��ݒ�
	/// </summary>
	/// <param name="number">�ݒ肷��ԍ�</param>
	public void SetDifferenceNumber(int number)
	{
		// �ԍ��ݒ�
		differenceNumber = number;

		// �X�v���C�g�̃f�[�^�擾
		var s = illustDifference[differenceNumber];

		// �摜�̃T�C�Y�ɑΉ�
		var rect = illustImg.transform as RectTransform;

		// �摜�̃T�C�Y�ύX
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
		// �C���X�g�X�V
		illustImg.sprite = s;
	}

	/// <summary>
	/// �M�������[�ݒ�
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