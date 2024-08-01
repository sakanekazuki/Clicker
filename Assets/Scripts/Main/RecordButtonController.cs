using naichilab.Scripts.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordButtonController : MonoBehaviour
{
	[SerializeField] public Text TextName;
	[SerializeField] public Text TextParams;
	[SerializeField] public int RecordCase;
	public MainCanvas mainCanvas;

	//�Ђ����番��ŏ����Ă���
	void Update()
	{
		double n = -1;
		string t = "";
		switch (RecordCase)
		{
		case 1: // �Ö�����
			// �v���C���� / 300
			n = GameData.all_playtime / 300;
			break;
		case 2: // �Ö��x
			// �t�B�[�o�[�̐i��
			n = FeverManager.Instance.Progress;
			break;
		case 3: // ��d��
			// �t�B�[�o�[���̃N���b�N�� / 20
			n = GameData.feverClick / 20;
			break;
		case 4: // �o���l��
			// �t�B�[�o�[���̃N���b�N�� / 40
			n = GameData.feverClick / 40;
			break;
		case 5: // ���ԉ�
			// �t�B�[�o�[���̃N���b�N�� / 45
			n = GameData.feverClick / 45;
			break;
		case 6: // �Ⓒ��
			// �t�B�[�o�[���̃N���b�N�� / 100
			n = GameData.feverClick / 100;
			break;
		case 7: // �S���ː���
			// �t�B�[�o�[���̃N���b�N�� / 40 / 2
			n = GameData.feverClick / 40 / 2;
			break;
		case 8: // ������
			// �t�B�[�o�[���̃N���b�N�� / 20 / 2
			n = GameData.feverClick / 20 / 2;
			break;
		case 9: // ��������
			// �t�B�[�o�[���̃N���b�N�� / 45 / 2 + �Ⓒ��
			n = (GameData.feverClick / 45) / 2 + GameData.climaxIndex;
			break;
		case 10: // �����t��
			// (�t�B�[�o�[���̃N���b�N�� / 40 / 2 + �t�B�[�o�[���̃N���b�N�� / 20 / 2 * 1.46) * 1.46
			n = ((GameData.feverClick / 40 / 2) + (GameData.feverClick / 20 / 2) * 1.46) * 1.46;
			break;
		case 11: // �S���ː���
			// �t�B�[�o�[���̃N���b�N�� / 40 / 2 * 1.46
			n = (GameData.feverClick / 40 / 2) * 1.46;
			break;
		case 12: // ������
			// �t�B�[�o�[���̃N���b�N�� / 20 / 2 * 1.46
			n = (GameData.feverClick / 20 / 2) * 1.46;
			break;
		case 13: // ��������
			// (�t�B�[�o�[���̃N���b�N�� / 45 / 2 + �Ⓒ��) * 2.61
			n = ((GameData.feverClick / 45) / 2 + GameData.climaxIndex) * 2.61;
			break;
		case 14: // �v���C����
			t = SecondsToText((GameData.nowtime - GameData.starttime));
			break;
		case 15: // ���N���b�N��
			n = GameData.all_click;
			break;
		case 16: // �t�B�[�o�[������
			n = GameData.all_ShardBeforest;
			break;
		case 17: // �����Y�Ö�P
			n = GameData.all_point;
			break;
		case 18: // ������Ö�P
			n = GameData.all_point - GameData.point;
			break;
		case 19: // �݌v�A�C�e������
			n = 0;
			foreach (var v in GameData.InstLv)
			{
				n += v;
			}
			break;
		case 20: // �݌v�A�C�e�����x��
			n = 0;
			break;
		}
		if (n > -1) TextParams.text = (n <= 1000) ? n.ToReadableString() : n.ToReadableString();
		else if (t != "") TextParams.text = t;
	}

	private string SecondsToText(double _s)
	{
		double h = 0;
		double m = 0;
		double s = 0;

		h = Math.Floor(_s / 3600);
		if (h > 0) _s = _s - (h * 3600);

		m = Math.Floor(_s / 60);
		if (m > 0) _s = _s - (m * 60);

		s = _s;

		string r = "";

		if (h > 0) r = r + h.ToString("00") + ":";
		if (m > 0) r = r + m.ToString("00") + ":";
		r = r + s.ToString("00");

		return r;
	}

}
