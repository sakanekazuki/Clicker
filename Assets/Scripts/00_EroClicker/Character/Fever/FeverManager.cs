using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �t�B�[�o�[
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
					Debug.LogError("FeverManager�����݂��܂���");
					return null;
				}
			}
			return instance;
		}
	}

	// �t�B�[�o�[�ɂȂ�l
	[SerializeField]
	float max = 100;
	// �i��
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
	// true = �t�B�[�o�[���
	bool isFever = false;
	public bool IsFever { get => isFever; }

	// �t�B�[�o�[�̍ۂ̐i��
	float feverProgress = 0;

	// �i����\���X���C�_�[
	[SerializeField]
	Slider progressSlider;

	// �t�B�[�o�[���ɕ\�����Ȃ��{�^��
	[SerializeField]
	List<GameObject> noneFeverBtn = new List<GameObject>();

	[SerializeField]
	Sprite normalHandleSprite;
	[SerializeField]
	Sprite feverHandleSprite;

	// �n���h��
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
	/// �i�s
	/// </summary>
	/// <param name="value">�i�񂾗�</param>
	public void AddProgress(float value)
	{
		// �t�B�[�o�[��ԂȂ�i�߂Ȃ�
		if (isFever)
		{
			// �t�B�[�o�[���Ȃ�i�߂�
			feverProgress += value;
			
			// 20�̔{���Ŏ��̉摜��
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
		// �i����ǉ�
		progress += value;
		// �i���x�������X���C�_�[�ɔ��f����
		progressSlider.value = progress / max;

		// �����i�񂾂�摜�ؑ�
		if (progressSlider.value >= 0.75f)
		{
			isHalf = true;
			GameObject.FindObjectOfType<TreeManager>().Half(GameData.treeIndex);
		}

		// �ő�ɂȂ�����t�B�[�o�[�J�n
		if (progress >= max)
		{
			FeverStart();
		}
	}

	/// <summary>
	/// �t�B�[�o�[�J�n
	/// </summary>
	public void FeverStart()
	{
		isHalf = false;
		progress = 0;
		progressSlider.value = 100;
		isFever = true;
		// �t�B�[�o�[(�u�[�X�g)��Ԃɂ���
		//Debug.Log("�t�B�[�o�[");
		GameObject.FindObjectOfType<ShardBiforestManager>().ClickShard();

		// �摜���t�B�[�o�[�̉摜�ɕύX
		GameObject.FindObjectOfType<TreeManager>().Fever(GameData.feverSpriteIndex);
		handle.sprite = feverHandleSprite;
		foreach (var v in noneFeverBtn)
		{
			v.SetActive(false);
		}
	}

	/// <summary>
	/// �t�B�[�o�[�I��
	/// </summary>
	public void FeverEnd()
	{
		isFever = false;
		//Debug.Log("�t�B�[�o�[�I��");

		// �t�B�[�o�[��Ԃł͂Ȃ��摜�ɕύX
		GameObject.FindObjectOfType<TreeManager>().Init(GameData.treeIndex);
		handle.sprite = normalHandleSprite;
		foreach (var v in noneFeverBtn)
		{
			v.SetActive(true);
		}
	}
}
