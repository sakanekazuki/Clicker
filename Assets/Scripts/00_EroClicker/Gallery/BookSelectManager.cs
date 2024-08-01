using Coffee.UIEffects;
using UnityEngine;
using UnityEngine.UI;

public class BookSelectManager : MonoBehaviour
{
	// �L�����N�^�[�̔ԍ�
	int characterID = 0;
	// true = ���ʂ̃M�������[
	bool isNormal = false;
	// true = �M�������[�����邱�Ƃ��ł���
	bool isOpen = false;

	// �摜
	[SerializeField]
	Image buttonImg;

	UIGradient gradient;

	/// <summary>
	/// ������
	/// </summary>
	/// <param name="characterID">�L�����N�^�[ID</param>
	/// <param name="sprite">�摜</param>
	/// <param name="isNormal">�ʏ�̃{�^����CG�̃{�^����</param>
	public void Init(int characterID, Sprite sprite, bool isNormal, bool isOpen)
	{
		// ID�ݒ�
		this.characterID = characterID;
		// �X�v���C�g�̐ݒ�
		buttonImg.sprite = sprite;
		// �t���O�ݒ�
		this.isNormal = isNormal;
		// ���邱�Ƃ��ł��邩�ǂ���
		this.isOpen = isOpen;

		//// RectTransform�ɕϊ�
		//var rect = buttonImg.transform as RectTransform;
		//// �摜�T�C�Y�C��
		//rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sprite.bounds.size.x * 3);
		//rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sprite.bounds.size.y * 3);

		// ����������A�F��������
		gradient = buttonImg.GetComponent<UIGradient>();
		gradient.enabled = !isOpen;
	}

	/// <summary>
	/// �L�����N�^�[�J��
	/// </summary>
	public void CharacterOpen()
	{
		isOpen = true;
		gradient.enabled = !isOpen;
	}

	/// <summary>
	/// �M�������[���J��
	/// </summary>
	public void Open()
	{
		BookShelfManager.Instance.Expansion(characterID, isNormal);
	}
}