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

	//ひたすら分岐で書いていく
	void Update()
	{
		double n = -1;
		string t = "";
		switch (RecordCase)
		{
		case 1: // 催眠日数
			// プレイ時間 / 300
			n = GameData.all_playtime / 300;
			break;
		case 2: // 催眠度
			// フィーバーの進捗
			n = FeverManager.Instance.Progress;
			break;
		case 3: // 奉仕回数
			// フィーバー中のクリック数 / 20
			n = GameData.feverClick / 20;
			break;
		case 4: // 経験人数
			// フィーバー中のクリック数 / 40
			n = GameData.feverClick / 40;
			break;
		case 5: // 自慰回数
			// フィーバー中のクリック数 / 45
			n = GameData.feverClick / 45;
			break;
		case 6: // 絶頂回数
			// フィーバー中のクリック数 / 100
			n = GameData.feverClick / 100;
			break;
		case 7: // 膣内射精回数
			// フィーバー中のクリック数 / 40 / 2
			n = GameData.feverClick / 40 / 2;
			break;
		case 8: // 精飲回数
			// フィーバー中のクリック数 / 20 / 2
			n = GameData.feverClick / 20 / 2;
			break;
		case 9: // 潮吹き回数
			// フィーバー中のクリック数 / 45 / 2 + 絶頂回数
			n = (GameData.feverClick / 45) / 2 + GameData.climaxIndex;
			break;
		case 10: // 総精液量
			// (フィーバー中のクリック数 / 40 / 2 + フィーバー中のクリック数 / 20 / 2 * 1.46) * 1.46
			n = ((GameData.feverClick / 40 / 2) + (GameData.feverClick / 20 / 2) * 1.46) * 1.46;
			break;
		case 11: // 膣内射精量
			// フィーバー中のクリック数 / 40 / 2 * 1.46
			n = (GameData.feverClick / 40 / 2) * 1.46;
			break;
		case 12: // 精飲量
			// フィーバー中のクリック数 / 20 / 2 * 1.46
			n = (GameData.feverClick / 20 / 2) * 1.46;
			break;
		case 13: // 潮吹き量
			// (フィーバー中のクリック数 / 45 / 2 + 絶頂回数) * 2.61
			n = ((GameData.feverClick / 45) / 2 + GameData.climaxIndex) * 2.61;
			break;
		case 14: // プレイ時間
			t = SecondsToText((GameData.nowtime - GameData.starttime));
			break;
		case 15: // 総クリック数
			n = GameData.all_click;
			break;
		case 16: // フィーバー発生回数
			n = GameData.all_ShardBeforest;
			break;
		case 17: // 総生産催眠P
			n = GameData.all_point;
			break;
		case 18: // 総消費催眠P
			n = GameData.all_point - GameData.point;
			break;
		case 19: // 累計アイテム強化
			n = 0;
			foreach (var v in GameData.InstLv)
			{
				n += v;
			}
			break;
		case 20: // 累計アイテムレベル
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
