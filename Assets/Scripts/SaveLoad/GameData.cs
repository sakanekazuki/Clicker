using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class GameData
{

	//SaveDataと連動するデータ
	public static int story = 0;
	public static float AudioMasterVolume = 0.5f;//BGM
	public static float SeVolume = 0.5f;
	public static int AudioPlayMode = 0;
	public static int AudioMute = 0;
	public static int GameModePhase = 0;
	public static double click = 0;
	public static double feverClick = 0;
	public static double all_click = 0;
	public static double point = 0;//今
	public static double total_point;//今回
	public static double all_point;//
	public static double fruit = 0;
	public static double total_fruit = 0;
	public static double all_fruit = 0;
	public static double all_fruit_use = 0;
	public static double all_ragnarok;
	public static int language = 0;
	public static int treeIndex = 0;
	public static int feverSpriteIndex = 0;
	public static double total_ShardBeforest;
	public static double total_Warp;
	public static double total_Stella;
	public static double all_ShardBeforest;
	public static double all_Warp;
	public static double all_Stella;
	public static double total_gungnir;
	public static double total_wisdom;
	public static double all_gungnir;
	public static double all_wisdom;
	public static double climaxIndex = 0;

	public static List<int> InstLv = new List<int>() {
		0,
		0, //LEAF
        0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
	};
	public static List<double> InstCharge = new List<double>() {
		0,
		0.0, //LEAF
        0.0,
		0.0,
		0.0,
		0.0,
		0.0,
		0.0,
		0.0,
		0.0,
		0.0,
		0.0,
		0.0,
		0.0,
		0.0,
		0.0,
		0.0,
		0.0,
		0.0,
		0.0,
		0.0,
	};
	public static List<double> InstFruitLv = new List<double>() {
		0,
		0, //LEAF
        0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
		0,
	};
	public static List<Vector3> LeafPos = new List<Vector3>();

	public static int BgmSelect = 0;

	public static int FragmentStellaCycleCnt = 0;
	public static int FragmentStellaStayCnt = 0;

	public static int ShardBiforest = 0;
	public static int ShardBiforestStayCnt = 0;
	public static int ShardBiforestCycleCnt = 0;
	public static int ShardBiforestCycleCntLimit = 0;
	public static int ShardBiforestBoost = 0;

	public static int FruitCycleCnt = 0;

	public static double SeedYggdrasil = 0; //初期化しない
	public static double Total_SeedYggdrasil = 0; //初期化しない

	public static List<bool> RagnarokSkill = new List<bool>()
	{
		false,
		false,false,false,false,false,false,false,false,false,false,
		false,false,false,false,false,false,false,false,false,false,
		false,false,false,false,false,false,false,false,false,false,
		false,false,false,false,false,false,false,false,false,false,
		false,false,false,false,false,false,false,false,false,false,
		false,false,false,false,false,false,false,false,false,false,
		false,false,false,false,false,false,false,false,
	};
	public static List<int> AwakeLv = new List<int>();

	public static double nowtime = 0;
	public static double starttime = 0;
	public static double firststarttime = 0;
	public static double longesttime;//
	public static double playtime = 0;//プレイ時間(秒)
	public static double all_playtime = 0;//通算プレイ時間(秒)

	public static int GungnirCycleCnt = 0;
	public static int GungnirPowerTime = 0;

	public static int WisdomCycleCnt = 0;
	public static int WisdomPowerTime = 0;
	public static int WisdomClickCnt = 0;

	public static int GoddessView = 0;//1で女神様表示
	public static int GoddessViewIndex = 0;
	public static List<int> GoddessOpen = new List<int>() { 0 };
	public static List<int> feverSpriteOpen = new List<int>() { 0 };

	// 初期化
	public static void ResetSaveData(bool allreset = false)
	{
		click = 0;
		feverClick = 0;
		point = 0;
		total_point = 0;
		treeIndex = 0;
		feverSpriteIndex = 0;
		InstLv = new List<int>() {
			0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
		};
		InstCharge = new List<double>() {
			0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
		};
		LeafPos = new List<Vector3>();
		FragmentStellaCycleCnt = 0;
		FragmentStellaStayCnt = 0;
		ShardBiforestStayCnt = 0;
		ShardBiforestCycleCnt = 0;
		ShardBiforestCycleCntLimit = 0;
		ShardBiforestBoost = 0;

		FruitCycleCnt = 0;
		total_fruit = 0;
		nowtime = 0;
		starttime = 0;//要確認
		playtime = 0;
		GungnirCycleCnt = 0;
		GungnirPowerTime = 0;
		WisdomCycleCnt = 0;
		WisdomPowerTime = 0;
		WisdomClickCnt = 0;
		WisdomClickCnt = 0;
		total_ShardBeforest = 0;
		total_Warp = 0;
		total_Stella = 0;
		total_gungnir = 0;
		total_wisdom = 0;

		//永続のものはリセットしない
		//AwakeLv = new List<int>();
		List<int> new_AwakeLv = new List<int>();
		foreach (int id in AwakeLv)
		{
			if (mAwake.ContainsKey(id))
			{
				AwakeClass a = mAwake[id];
				switch (a.skillType)
				{
				case AWAKE_TYPE_INST_LV:
				case AWAKE_TYPE_TIME:
					new_AwakeLv.Add(id);
					break;
				}
			}
		}
		AwakeLv = new_AwakeLv;


		if (allreset)
		{
			//ラグナロクでは初期化しない、ゲームデータ削除でリセットされるもの
			story = 0;
			//AudioMasterVolume = 0.5f; //音量だけはメモリに保持しているものを引き継ぐ
			GameModePhase = 0;
			all_click = 0;
			all_point = 0;
			fruit = 0;//要確認
			all_fruit = 0;
			all_fruit_use = 0;
			all_ragnarok = 0;
			//language = 0;//引き継ぐ
			ShardBiforest = 0;//ラグナロクでもリセットしないことに
			all_ShardBeforest = 0;
			all_Warp = 0;
			all_Stella = 0;
			BgmSelect = 0;
			AudioPlayMode = 0;
			AudioMute = 0;
			SeedYggdrasil = 0;
			Total_SeedYggdrasil = 0;
			firststarttime = 0;
			longesttime = 0;
			all_playtime = 0;
			all_gungnir = 0;
			all_wisdom = 0;
			climaxIndex = 0;

			RagnarokSkill = new List<bool>()
			{
				false,
				false,false,false,false,false,false,false,false,false,false,
				false,false,false,false,false,false,false,false,false,false,
				false,false,false,false,false,false,false,false,false,false,
				false,false,false,false,false,false,false,false,false,false,
				false,false,false,false,false,false,false,false,false,false,
				false,false,false,false,false,false,false,false,false,false,
				false,false,false,false,false,false,false,false,
			};
			AwakeLv = new List<int>();
			//AwakeLv.Add((int)GameData.AwakeModeNum.GODDESS);

			InstFruitLv = new List<double>() {
			0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
			};

			GoddessView = 0;
			GoddessViewIndex = 0;
			GoddessOpen = new List<int>() { 0 };
			feverSpriteOpen = new List<int>() { 0 };
		}

#if UNITY_EDITOR
		//    RagnarokSkill = new List<bool>()
		//{
		//    false,
		//    false,false,false,false,false,false,false,false,false,false,
		//    false,false,false,false,false,false,false,false,false,false,
		//    false,false,false,false,false,false,false,false,false,false,
		//    false,false,false,false,false,false,false,false,false,false,
		//    false,false,false,false,false,false,false,false,false,false,
		//    false,false,false,false,false,false,false,false,false,false,
		//    false,false,false,false,false,false,false,false,
		//};
#endif

	}

	// SaveDataへ
	public static SaveData CreateSaveData()
	{
		//Debug.Log($"CreateSaveData");
		//セーブデータのインスタンス化
		SaveData saveData = new SaveData();
		//ゲームデータの値をセーブデータに代入
		saveData.story = GameData.story;
		saveData.AudioMasterVolume = GameData.AudioMasterVolume;
		saveData.SeVolume = GameData.SeVolume;
		saveData.AudioPlayMode = GameData.AudioPlayMode;
		saveData.AudioMute = GameData.AudioMute;
		saveData.GamePhase = GameData.GameModePhase;
		saveData.click = GameData.click;
		saveData.feverClick = GameData.feverClick;
		saveData.all_click = GameData.all_click;
		saveData.point = GameData.point;
		saveData.total_point = GameData.total_point;
		saveData.all_point = GameData.all_point;
		saveData.fruit = GameData.fruit;
		saveData.total_fruit = GameData.total_fruit;
		saveData.all_fruit = GameData.all_fruit;
		saveData.all_fruit_use = GameData.all_fruit_use;
		saveData.all_ragnarok = GameData.all_ragnarok;
		saveData.language = GameData.language;
		saveData.treeIndex = GameData.treeIndex;
		saveData.feverSpriteIndex = GameData.feverSpriteIndex;
		saveData.InstLv = GameData.InstLv;
		saveData.InstFruitLv = GameData.InstFruitLv;
		saveData.LeafPos = GameData.LeafPos;
		saveData.BgmSelect = GameData.BgmSelect;
		saveData.FragmentStellaCycleCnt = GameData.FragmentStellaCycleCnt;
		saveData.FragmentStellaStayCnt = GameData.FragmentStellaStayCnt;
		saveData.ShardBiforestStayCnt = GameData.ShardBiforestStayCnt;
		saveData.ShardBiforestCycleCnt = GameData.ShardBiforestCycleCnt;
		saveData.ShardBiforestCycleCntLimit = GameData.ShardBiforestCycleCntLimit;
		saveData.ShardBiforest = GameData.ShardBiforest;
		//saveData.ShardBiforestBoost = GameData.ShardBiforestBoost;
		saveData.SeedYggdrasil = GameData.SeedYggdrasil;
		saveData.Total_SeedYggdrasil = GameData.Total_SeedYggdrasil;
		saveData.RagnarokSkill = GameData.RagnarokSkill;
		saveData.AwakeLv = GameData.AwakeLv;
		saveData.FruitCycleCnt = GameData.FruitCycleCnt;
		saveData.nowtime = GameData.nowtime;
		saveData.starttime = GameData.starttime;
		saveData.firststarttime = GameData.firststarttime;
		saveData.longesttime = GameData.longesttime;
		saveData.playtime = GameData.playtime;
		saveData.all_playtime = GameData.all_playtime;
		saveData.GungnirCycleCnt = GameData.GungnirCycleCnt;
		//saveData.GungnirPowerTime = GameData.GungnirPowerTime;
		saveData.WisdomCycleCnt = GameData.WisdomCycleCnt;
		//saveData.WisdomPowerTime = GameData.WisdomPowerTime;
		saveData.WisdomClickCnt = GameData.WisdomClickCnt;

		saveData.total_ShardBeforest = GameData.total_ShardBeforest;
		saveData.total_Warp = GameData.total_Warp;
		saveData.total_Stella = GameData.total_Stella;
		saveData.all_ShardBeforest = GameData.all_ShardBeforest;
		saveData.all_Warp = GameData.all_Warp;
		saveData.all_Stella = GameData.all_Stella;
		saveData.total_gungnir = GameData.total_gungnir;
		saveData.total_wisdom = GameData.total_wisdom;
		saveData.all_gungnir = GameData.all_gungnir;
		saveData.all_wisdom = GameData.all_wisdom;
		saveData.climaxIndex = GameData.climaxIndex;

		saveData.GoddessView = GameData.GoddessView;
		saveData.GoddessViewIndex = GameData.GoddessViewIndex;
		saveData.GoddessOpen = GameData.GoddessOpen;
		saveData.feverSpriteOpen = GameData.feverSpriteOpen;

		//Debug.Log($"CreateSaveData {saveData.point}");
		return saveData;
	}

	// SaveDataを読み込んでGameDataへ
	public static void ReadData(SaveData saveData)
	{
		GameData.story = saveData.story;
		GameData.AudioMasterVolume = saveData.AudioMasterVolume;
		GameData.SeVolume = saveData.SeVolume;
		GameData.AudioPlayMode = saveData.AudioPlayMode;
		GameData.AudioMute = saveData.AudioMute;
		GameData.GameModePhase = saveData.GamePhase;
		GameData.click = saveData.click;
		GameData.feverClick = saveData.feverClick;
		GameData.all_click = saveData.all_click;
		GameData.point = saveData.point;
		GameData.total_point = saveData.total_point;
		GameData.all_point = saveData.all_point;
		GameData.fruit = saveData.fruit;
		GameData.total_fruit = saveData.total_fruit;
		GameData.all_fruit = saveData.all_fruit;
		GameData.all_fruit_use = saveData.all_fruit_use;
		GameData.all_ragnarok = saveData.all_ragnarok;
		GameData.language = 0;
		GameData.treeIndex = saveData.treeIndex;
		GameData.feverSpriteIndex = saveData.feverSpriteIndex;
		GameData.InstLv = saveData.InstLv;
		GameData.InstFruitLv = saveData.InstFruitLv;
		GameData.LeafPos = saveData.LeafPos;
		GameData.BgmSelect = saveData.BgmSelect;
		GameData.FragmentStellaCycleCnt = saveData.FragmentStellaCycleCnt;
		GameData.FragmentStellaStayCnt = saveData.FragmentStellaStayCnt;
		GameData.ShardBiforestStayCnt = saveData.ShardBiforestStayCnt;
		GameData.ShardBiforestCycleCnt = saveData.ShardBiforestCycleCnt;
		GameData.ShardBiforestCycleCntLimit = saveData.ShardBiforestCycleCntLimit;
		GameData.ShardBiforest = saveData.ShardBiforest;
		//GameData.ShardBiforestBoost = saveData.ShardBiforestBoost;
		GameData.SeedYggdrasil = saveData.SeedYggdrasil;
		GameData.Total_SeedYggdrasil = saveData.Total_SeedYggdrasil;
		GameData.RagnarokSkill = saveData.RagnarokSkill;
		GameData.AwakeLv = saveData.AwakeLv;
		GameData.FruitCycleCnt = saveData.FruitCycleCnt;
		GameData.nowtime = saveData.nowtime;
		GameData.starttime = saveData.starttime;
		GameData.firststarttime = saveData.firststarttime;
		GameData.longesttime = saveData.longesttime;
		GameData.playtime = saveData.playtime;
		GameData.all_playtime = saveData.all_playtime;
		GameData.GungnirCycleCnt = saveData.GungnirCycleCnt;
		//GameData.GungnirPowerTime = saveData.GungnirPowerTime;
		GameData.WisdomCycleCnt = saveData.WisdomCycleCnt;
		//GameData.WisdomPowerTime = saveData.WisdomPowerTime;
		GameData.WisdomClickCnt = saveData.WisdomClickCnt;

		GameData.total_ShardBeforest = saveData.total_ShardBeforest;
		GameData.total_Warp = saveData.total_Warp;
		GameData.total_Stella = saveData.total_Stella;
		GameData.all_ShardBeforest = saveData.all_ShardBeforest;
		GameData.all_Warp = saveData.all_Warp;
		GameData.all_Stella = saveData.all_Stella;
		GameData.total_gungnir = saveData.total_gungnir;
		GameData.total_wisdom = saveData.total_wisdom;
		GameData.all_gungnir = saveData.all_gungnir;
		GameData.all_wisdom = saveData.all_wisdom;
		GameData.climaxIndex = saveData.climaxIndex;

		GameData.GoddessView = saveData.GoddessView;
		GameData.GoddessViewIndex = saveData.GoddessViewIndex;
		GameData.GoddessOpen = /*new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };*/ saveData.GoddessOpen;
		GameData.feverSpriteOpen = /*new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };*/ saveData.feverSpriteOpen;

		if (!GameData.AwakeLv.Contains((int)AwakeModeNum.GODDESS))
		{
			GameData.AwakeLv.Add((int)AwakeModeNum.GODDESS);
		}
	}






























	//SaveDataと連動しないデータ
	public static double GrayScaleLimit = 1000;
	public static double NightStella = 0;
	public const double NIGHT_STELLA_CYCLE = 60 * 60 * 5;//5h
	public const double NIGHT_STELLA_POW = 5;//5%
	public const int GAME_MODE_SEED = 0;
	public const int GAME_MODE_TREE = 1;
	public const int GAME_MODE_RAGNAROK = 2;

	public const string MESSAGE_STEAM_RESET = @"Steamの実績を削除します\nよろしいですか？";
	public const string MESSAGE_STEAM_ACHIVEMENT = "実績を解放！";
	public const string MESSAGE_GAMEDATA_RESET = @"ゲームのプレイデータを削除します\nよろしいですか？";
	public const string MESSAGE_GAMEDATA_RESETED = "ゲームのプレイデータを削除しました";
	public const string MESSAGE_GAME_EXIT = "ゲームを終了しますか？";
	public const string MESSAGE_CLOSE_MENU = "失われし力を使うにはステラを集める必要があります";

	public const string STRING_PPS = "/s";

	public enum ClickCase
	{
		NONE = -1,
		CLICK = 0,
		INST,
		FRAGMENT_STELLA,
		SHARD_BIFOREST_WARP,
		CENTOR,

	}
	public enum InstCase
	{
		NONE = 0,
		INST1 = 1,
		INST2,
		INST3,
		INST4,
		INST5,
		INST6,//祭祀場
		INST7,
		INST8,
		INST9,
		INST10,
		INST11,
		INST12,
		INST13,
		INST14,
		INST15,
		INST16,
		INST17,
		INST18,
		INST19,
		INST20,
	}
	public enum RagnarokSkillIndex
	{
		NONE = 0,
		SKILL_1 = 1,
		SKILL_2,
		SKILL_3,
		SKILL_4,
		SKILL_5,
		SKILL_6,
		SKILL_7,
		SKILL_8,
		SKILL_9,
		SKILL_10,
		SKILL_11,
		SKILL_12,
		SKILL_13,
		SKILL_14,
		SKILL_15,
		SKILL_16,
		SKILL_17,
		SKILL_18,
		SKILL_19,
		SKILL_20,
		SKILL_21,
		SKILL_22,
		SKILL_23,
		SKILL_24,
		SKILL_25,
		SKILL_26,
		SKILL_27,
		SKILL_28,
		SKILL_29,
		SKILL_30,
		SKILL_31,
		SKILL_32,
		SKILL_33,
		SKILL_34,
		SKILL_35,
		SKILL_36,
		SKILL_37,
		SKILL_38,
		SKILL_39,
		SKILL_40,
		SKILL_41,
		SKILL_42,
		SKILL_43,
		SKILL_44,
		SKILL_45,
		SKILL_46,
		SKILL_47,
		SKILL_48,
		SKILL_49,
		SKILL_50,
		SKILL_51,
		SKILL_52,
		SKILL_53,
		SKILL_54,
		SKILL_55,
		SKILL_56,
		SKILL_57,
		SKILL_58,
		SKILL_59,
		SKILL_60,
		SKILL_61,
		SKILL_62,
		SKILL_63,
		SKILL_64,
		SKILL_65,
		SKILL_66,
		SKILL_67,
		SKILL_68,
	}
	public static int ADD_POINT_CLICK = 1;

	public static double MASTER_LV_BASE = 4.2;
	public static double MASTER_LVUP_RATE = 1.03;
	public static double MASTER_10LVUP_NUM = 2;
	public static double OUROBOROS_POW_MAX = 1000;

	public static List<string> INST_NAME = new List<string>() {
		"non",
		"ペンデュラム", //LEAF
        "媚薬",
		"教典",
		"ローター",
		"指輪",
		"ローション",
		"ブレスレット",
		"お香",
		"ネックレス",
		"ディルド",
		"蝋燭",
		"バイブ",
		"ハンドベル",
		"セクシー下着",
		"天使像",
		"淫紋",
		"杯",
		"十字のオブジェクト",
		"首輪",
		"教祖像",
	};
	public static List<double> INST_COST_BASE = new List<double>() {
		0,
		15, //LEAF
        100,
		1440,
		20740,
		298660,
		4300710,
		61930230,
		891795320,
		12841852610,
		184922677590,
		2662886557300,
		38345566425120,
		552176156521730,
		Math.Ceiling(552176156521730*14.4),
		Math.Ceiling(552176156521730*14.4*14.4),
		Math.Ceiling(552176156521730*14.4*14.4*14.4),
		Math.Ceiling(552176156521730*14.4*14.4*14.4*14.4),
		Math.Ceiling(552176156521730*14.4*14.4*14.4*14.4*14.4),
		Math.Ceiling(552176156521730*14.4*14.4*14.4*14.4*14.4*14.4),
		Math.Ceiling(552176156521730*14.4*14.4*14.4*14.4*14.4*14.4*14.4),
	};

	//合成ベース 1/10で記入
	public static List<double> INST_POW_BASE = new List<double>() {
		0,
		0.1, //LEAF
        1,
		7,
		48,
		327,
		2224,
		15124,
		102844,
		699340,
		4755512,
		32337482,
		219894878,
		1495285171,
		10167939163,
		Math.Ceiling(10167939163*6.8),
		Math.Ceiling(10167939163*6.8*6.8),
		Math.Ceiling(10167939163*6.8*6.8*6.8),
		Math.Ceiling(10167939163*6.8*6.8*6.8*6.8),
		Math.Ceiling(10167939163*6.8*6.8*6.8*6.8*6.8),
		Math.Ceiling(10167939163*6.8*6.8*6.8*6.8*6.8*6.8),
	};
	public static List<int> INST_LV_MAX = new List<int>() {
		0,
		1000, //LEAF
        1000,
		1000,
		1000,
		1000,
		1000,
		1000,
		1000,
		1000,
		1000,
		1000,
		1000,
		1000,
		1000,
		1000,
		1000,
		1000,
		1000,
		1000,
		1000,
	};
	public static double DEBUG_POINT = double.Parse("1000000000000000");// double.MaxValue * 0.00001;// 
	public static string PPS_TEMPLATE = "毎秒:";
	public static Color32 COLOR_TRUE = new Color32(15, 100, 0, 255);
	public static Color32 COLOR_FALSE = new Color32(130, 10, 0, 255);
	public static Color32 COLOR_NORMAL = new Color32(0, 0, 0, 255);

	public static int FRAGMENT_STELLA_CYCLE = 60 * 60 * 10;//10分
	public static int FRAGMENT_STELLA_STAY = 60 * 60 * 2;//2分
	public static int FRAGMENT_STELLA_POP_BASE = 20;//出現率ベース
	public static int FRAGMENT_STELLA_GET_BASE = 2000;//獲得量ベース

	public static int SHARD_BIFOREST_POP_BASE = 5;//出現率ベース
	public static int SHARD_BIFOREST_CYCLE = 60 * 60 * 10;//10分
	public static int SHARD_BIFOREST_STAY = 60 * 60 * 2;//2分
	public static int SHARD_BIFOREST_MAX = 7;
	public static int SHARD_BIFOREST_CYCLE_MIN = 60 * 3;//仮
	public static int SHARD_BIFOREST_CYCLE_MAX = 60 * 10;//仮
	public static int SHARD_BIFOREST_BOOST_TIME_BASE = 60 * 30;//1分
	public static double SHARD_BIFOREST_BOOST_UP_BASE = 10;
	public static string SHARD_BIFOREST_TIME_TEMPLATE = "残り:";
	public static double SHARD_BIFOREST_WARP_SECOND_BASE = 60 * 30;//30分

	public static int FRUIT_CYCLE = 60 * 60 * 60 * 24;//24時間
	public static int FRUIT_OPEN_LV = 500;//500

	public static string SEED_USER_TEMPLATE = "所持:";
	public static string SEED_GET_TEMPLATE = "獲得:";
	public static string SEED_CONFIRM = @"ラグナロクを迎え、すべてのステラが消滅し、\n○○個のユグドラシルの種に変換されます。";
	public static string SEED_ZERO_CONFIRM = @"所持している「ステラ」から交換できる「ユグドラシルの種」の数は0個です。 本当に「ラグナロク」を迎えますか？";
	public static double SEED_RATE = 1000000000000000000;

	public static int GUNGNIR_CYCLE = 60 * 60 * 60 * 1;//1時間
	public static int GUNGNIR_POWER_TIME = 60 * 30;
	public static double GUNGNIR_POW = 500;//500%

	public static int WISDOM_CYCLE = 60 * 60 * 60 * 1;//1時間
	public static int WISDOM_POWER_TIME = 60 * 60;
	public static double WISDOM_CLICK_FRAME = 60;//何フレームに一回クリックするか
	public static double WISDOM_CLICK_FRAME_MAGNIFICATION = 1;

	public static int LIGHT_MORNING = 0;
	public static int LIGHT_NOON = 1;
	public static int LIGHT_EVENING = 2;
	public static int LIGHT_NIGHT = 3;
	public static int LIGHT_MIDNIGHT = 4;
	public static float LIGHT_CHANGE_TIME = 3f;

	public static double GODDESS_POW = 25;//25%
	public static string GODDESS_POW_TEXT = "Bonus +@%";




	//データが多いマスター
	public enum AwakeModeNum
	{
		FRAGMENT_STELLA = 1002,
		BGM = 1003,
		SHARD_BEFOREST = 1005,
		FRUIT = 1007,
		RAGNAROK = 1008,
		GODDESS = 1040,
	}
	public static string CLOSED_TEXT = "？？？";
	public static int mAwakeIDLength = 10000;
	public static int mAwakeLvNum = 18;
	public static int mAwakeIDLengthTime = 2024;
	public static int mAwakeLvNumTime = 2037;
	public const int AWAKE_TYPE_INST = 1;
	public const int AWAKE_TYPE_CLICK = 2;
	public const int AWAKE_TYPE_INST_LV = 3;
	public const int AWAKE_TYPE_TIME = 4;
	public const int AWAKE_TYPE_AWAKE_NUM = 5;
	public static List<int> mAwakeTargetClick = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
	public static List<int> mAwakeTargetClickAwakeNum = new List<int>() { 101, 102, 103, 104, 105, 106, 107, 108, 110, 111, 112, 113, 114, 115, 116 };
	public static List<int> mAwakeTargetInstALL = new List<int>() { 1001, 1004, 1006, 1009, 1010, 1011, 1013, 1014, 1015, 1016, 1018, 1019, 1020, 1022, 1023, 1024, 1026, 1027, 1028, 1030, 1031, 1032, 1033, 1034, 1035, 1036, 1037 };
	public static List<int> mAwakeTargetInst1 = new List<int>() { 2001, 2002, 2003, 2007, 2008, 2009 };
	public static List<int> mAwakeTargetTime = new List<int>() { 2024, 2025, 2026, 2027, 2028, 2029, 2030, 2031, 2032, 2033, 2034, 2035, 2036, 2037 };
	public static Dictionary<int, AwakeClass> mAwake = new Dictionary<int, AwakeClass>() {
		{0,     new AwakeClass(0,       0,       0,       100,       "覚醒指定ミス",              "覚醒指定ミス")},

		{1, new AwakeClass(AWAKE_TYPE_CLICK,    10,             2,      100,                                    "クリックの旅人",      "神の恵みの効率が2倍になる")},
		{2, new AwakeClass(AWAKE_TYPE_CLICK,    100,            2,      1360,                                   "クリックの達人",      "神の恵みの効率が2倍になる")},
		{3, new AwakeClass(AWAKE_TYPE_CLICK,    500,            2,      18500,                                  "クリックのプロ",      "神の恵みの効率が2倍になる")},
		{4, new AwakeClass(AWAKE_TYPE_CLICK,    1000,           2,	    251600,                                 "クリックの猛者",        "所有する聖霊1個に付き、神の恵みの合成数+0.5する")},
		{5, new AwakeClass(AWAKE_TYPE_CLICK,    3000,           3,      3421760,                                "クリックの超人",      "神の恵みの効率が3倍になる")},
		{6, new AwakeClass(AWAKE_TYPE_CLICK,    5000,           2,      46535940,                               "クリックの闘士",      "神の恵みの効率が5倍になる")},
		{7, new AwakeClass(AWAKE_TYPE_CLICK,    10000,          5,      632888790,                              "クリックの挑戦者",    "神の恵みの効率が5倍になる")},
		{8, new AwakeClass(AWAKE_TYPE_CLICK,    15000,          2,      8607287550,                             "クリックの天才",        "ラグナロクをした回数1回に付き、神の恵みの生産率+5％")},
		{9, new AwakeClass(AWAKE_TYPE_CLICK,    30000,          10,     117059110680,                           "クリックの奇跡",      "神の恵みの効率が10倍になる")},
		{10, new AwakeClass(AWAKE_TYPE_CLICK,   50000,          2,      1592003905250,                          "クリックの聖域",      "神の恵みの効率が5倍になる" )},
		{11, new AwakeClass(AWAKE_TYPE_CLICK,   100000,         2,      21651253111400,                         "クリックの王者",      "神の恵みの効率が10倍になる")},
		{12, new AwakeClass(AWAKE_TYPE_CLICK,   1000000,        2,      294457042315040,                        "クリックの英雄",      "神の恵みの効率が5倍になる" )},
		{13, new AwakeClass(AWAKE_TYPE_CLICK,   2000000,        5,      4004615775484540,                       "クリックの伝説",      "神の恵みの効率が5倍になる" )},
		{14, new AwakeClass(AWAKE_TYPE_CLICK,   5000000,        2,      54462774546589700,                      "クリックの帝王",      "神の恵みの効率が10倍になる")},
		{15, new AwakeClass(AWAKE_TYPE_CLICK,   10000000,       2,      740693733833620000,                     "クリックの妖精",      "神の恵みの効率が10倍になる")},
		{16, new AwakeClass(AWAKE_TYPE_CLICK,   50000000,       2,      10073434780137200000,                   "クリックの聖域",      "神の恵みの効率が10倍になる")},
		{17, new AwakeClass(AWAKE_TYPE_CLICK,   100000000,      2,      double.Parse("136998713009866000000"),  "クリックの神秘",      "神の恵みの効率が10倍になる")},
		{18, new AwakeClass(AWAKE_TYPE_CLICK,   1000000000,     5,      double.Parse("1863182496934180000000"), "クリックの極致",      "神の恵みの効率が10倍になる")},

		{101, new AwakeClass(AWAKE_TYPE_AWAKE_NUM,   3,         25,     4300,                                   "覚醒の第一歩",           "神の恵みの効率が25％上昇する")},
		{102, new AwakeClass(AWAKE_TYPE_AWAKE_NUM,   5,         25,     100*Math.Pow(13.6, 1),                  "眠りから覚めし者",       "神の恵みの効率が25％上昇する")},
		{103, new AwakeClass(AWAKE_TYPE_AWAKE_NUM,   10,        25,     100*Math.Pow(13.6, 2),                  "覚醒の兆し",             "神の恵みの効率が25％上昇する")},
		{104, new AwakeClass(AWAKE_TYPE_AWAKE_NUM,   20,        25,     100*Math.Pow(13.6, 3),                  "潜在能力の開花",         "神の恵みの効率が25％上昇する")},
		{105, new AwakeClass(AWAKE_TYPE_AWAKE_NUM,   30,        25,     100*Math.Pow(13.6, 4),                  "魂の覚醒者",             "神の恵みの効率が25％上昇する")},
		{106, new AwakeClass(AWAKE_TYPE_AWAKE_NUM,   40,        25,     100*Math.Pow(13.6, 5),                  "神々の眷属となる者",     "神の恵みの効率が25％上昇する")},
		{107, new AwakeClass(AWAKE_TYPE_AWAKE_NUM,   50,        25,     100*Math.Pow(13.6, 6),                  "悟りへの道を開く",       "神の恵みの効率が25％上昇する")},
		{108, new AwakeClass(AWAKE_TYPE_AWAKE_NUM,   80,        25,     100*Math.Pow(13.6, 7),                  "夢と現実",               "神の恵みの効率が25％上昇する")},
		{109, new AwakeClass(AWAKE_TYPE_AWAKE_NUM,   100,       1,      100*Math.Pow(13.6, 8),                  "意識の領域",             "所有する聖霊10レベルに付き、神の恵みの生産率+1％する")},
		{110, new AwakeClass(AWAKE_TYPE_AWAKE_NUM,   120,       25,     100*Math.Pow(13.6, 9),                  "法則を超越する",         "神の恵みの効率が25％上昇する")},
		{111, new AwakeClass(AWAKE_TYPE_AWAKE_NUM,   150,       25,     100*Math.Pow(13.6, 10),                 "天変地異の宿命",         "神の恵みの効率が25％上昇する")},
		{112, new AwakeClass(AWAKE_TYPE_AWAKE_NUM,   180,       25,     100*Math.Pow(13.6, 11),                 "終焉の守護者",           "神の恵みの効率が25％上昇する")},
		{113, new AwakeClass(AWAKE_TYPE_AWAKE_NUM,   200,       25,     100*Math.Pow(13.6, 12),                 "崇高なる存在",           "神の恵みの効率が25％上昇する")},
		{114, new AwakeClass(AWAKE_TYPE_AWAKE_NUM,   300,       25,     100*Math.Pow(13.6, 13),                 "永劫の旅人",             "神の恵みの効率が25％上昇する")},
		{115, new AwakeClass(AWAKE_TYPE_AWAKE_NUM,   400,       25,     100*Math.Pow(13.6, 14),                 "永遠の輝き",             "神の恵みの効率が25％上昇する")},
		{116, new AwakeClass(AWAKE_TYPE_AWAKE_NUM,   500,       25,     100*Math.Pow(13.6, 15),                 "神々の証となる覚醒",     "神の恵みの効率が25％上昇する")},















































		{1001, new AwakeClass(AWAKE_TYPE_INST_LV, 50,     2,  100,                     "聖霊使い",       "ステラの生産率を+2％")},
		{1002, new AwakeClass(AWAKE_TYPE_INST_LV, 75,     0,  100*Math.Pow(15.5, 1),   "聖霊の守護者",   "ステラの欠片の機能が解除される")},
		{1003, new AwakeClass(AWAKE_TYPE_INST_LV, 100,    0,  100*Math.Pow(15.5, 2),   "聖霊の導き手",   "メディアプレイア機能解除")},
		{1004, new AwakeClass(AWAKE_TYPE_INST_LV, 200,    2,  100*Math.Pow(15.5, 3),   "聖霊の修練者",   "ステラの生産率を+2％")},
		{1005, new AwakeClass(AWAKE_TYPE_INST_LV, 300,    0,  100*Math.Pow(15.5, 4),   "聖霊の召使い",   "ビフレストの欠片の機能が解除される")},
		{1006, new AwakeClass(AWAKE_TYPE_INST_LV, 400,    2,  100*Math.Pow(15.5, 5),   "聖霊の契約者",   "ステラの生産率を+2％")},
		{1007, new AwakeClass(AWAKE_TYPE_INST_LV, 500,    0,  100*Math.Pow(15.5, 6),   "聖霊の覚醒者",   "生命の実機能が解除される")},
		{1040, new AwakeClass(AWAKE_TYPE_INST_LV, 500,    0,  4727033000,   "ヴァルハラのコレクション",   "女神のギャラリー機能が解除されます。所持する女神の肖像画を1枚に付き、ステラの合成率を+25％")},
		{1008, new AwakeClass(AWAKE_TYPE_INST_LV, 600,    0,  100*Math.Pow(15.5, 7),   "聖霊の使徒",    "ラグナロク機能解除")},
		{1009, new AwakeClass(AWAKE_TYPE_INST_LV, 700,    2,  100*Math.Pow(15.5, 8),   "聖霊の神官",    "ステラの生産率を+2％")},
		{1010, new AwakeClass(AWAKE_TYPE_INST_LV, 800,    2,  100*Math.Pow(15.5, 9),   "聖霊の輝く光",   "ステラの生産率を+2％")},
		{1011, new AwakeClass(AWAKE_TYPE_INST_LV, 900,    2,  100*Math.Pow(15.5, 10),   "聖霊の星の導き手", "ステラの生産率を+2％")},
		{1012, new AwakeClass(AWAKE_TYPE_INST_LV, 1000,   5,  100*Math.Pow(15.5, 11),   "聖霊の月光の騎士", "ラグナロクをした回数1回に付き、神の恵みの生産率を+5％")},
		{1013, new AwakeClass(AWAKE_TYPE_INST_LV, 1200,   2,  100*Math.Pow(15.5, 12),   "聖霊の光り輝く者", "ステラの生産率を+2％")},
		{1014, new AwakeClass(AWAKE_TYPE_INST_LV, 1400,   2,  100*Math.Pow(15.5, 13),   "聖霊の牧師",    "ステラの生産率を+2％")},
		{1015, new AwakeClass(AWAKE_TYPE_INST_LV, 1600,   2,  100*Math.Pow(15.5, 14),   "聖霊の司祭",    "ステラの生産率を+2％")},
		{1016, new AwakeClass(AWAKE_TYPE_INST_LV, 1800,   2,  100*Math.Pow(15.5, 15),   "聖霊の神官",    "ステラの生産率を+2％")},
		{1017, new AwakeClass(AWAKE_TYPE_INST_LV, 2000,   2,  100*Math.Pow(15.5, 16),   "聖霊の力を司る者", "ユグドラシルが成長する、生命の実は2個同時に成長する")},
		{1018, new AwakeClass(AWAKE_TYPE_INST_LV, 2500,   2,  100*Math.Pow(15.5, 17),   "聖霊の導きを受けし者",   "ステラの生産率を+2％")},
		{1019, new AwakeClass(AWAKE_TYPE_INST_LV, 3000,   2,  100*Math.Pow(15.5, 18),   "聖霊の恩恵を授かった者",  "ステラの生産率を+2％")},
		{1020, new AwakeClass(AWAKE_TYPE_INST_LV, 3500,   2,  100*Math.Pow(15.5, 19),   "聖霊の導きに従う者",    "ステラの生産率を+2％")},
		{1021, new AwakeClass(AWAKE_TYPE_INST_LV, 4000,   3,  100*Math.Pow(15.5, 20),   "聖霊の象徴",    "ユグドラシルが成長する、生命の実は3個同時に成長する")},
		{1022, new AwakeClass(AWAKE_TYPE_INST_LV, 4500,   2,  100*Math.Pow(15.5, 21),   "聖霊の継承者",   "ステラの生産率を+2％")},
		{1023, new AwakeClass(AWAKE_TYPE_INST_LV, 5000,   2,  100*Math.Pow(15.5, 22),   "聖霊の化身",    "ステラの生産率を+2％")},
		{1024, new AwakeClass(AWAKE_TYPE_INST_LV, 5500,   2,  100*Math.Pow(15.5, 23),   "聖霊の眷属",    "ステラの生産率を+2％")},
		{1025, new AwakeClass(AWAKE_TYPE_INST_LV, 6000,   4,  100*Math.Pow(15.5, 24),   "聖霊と共鳴する者", "ユグドラシルが成長する、生命の実は4個同時に成長する")},
		{1026, new AwakeClass(AWAKE_TYPE_INST_LV, 6500,   2,  100*Math.Pow(15.5, 25),   "聖霊の掌握者",   "ステラの生産率を+2％")},
		{1027, new AwakeClass(AWAKE_TYPE_INST_LV, 7000,   2,  100*Math.Pow(15.5, 26),   "聖霊の叡智を有する者",   "ステラの生産率を+2％")},
		{1028, new AwakeClass(AWAKE_TYPE_INST_LV, 7500,   2,  100*Math.Pow(15.5, 27),   "聖霊の風の使い手", "ステラの生産率を+2％")},
		{1029, new AwakeClass(AWAKE_TYPE_INST_LV, 8000,   5,  100*Math.Pow(15.5, 28),   "聖霊の雷の使い手", "ユグドラシルが成長する、生命の実は5個同時に成長する")},
		{1030, new AwakeClass(AWAKE_TYPE_INST_LV, 8500,   2,  100*Math.Pow(15.5, 29),   "聖霊の水の使い手", "ステラの生産率を+2％")},
		{1031, new AwakeClass(AWAKE_TYPE_INST_LV, 9000,   2,  100*Math.Pow(15.5, 30),   "聖霊の炎の使い手", "ステラの生産率を+2％")},
		{1032, new AwakeClass(AWAKE_TYPE_INST_LV, 9500,   2,  100*Math.Pow(15.5, 31),   "聖霊の氷の使い手", "ステラの生産率を+2％")},
		{1033, new AwakeClass(AWAKE_TYPE_INST_LV, 10000,  2,  100*Math.Pow(15.5, 32),   "聖霊の土の使い手", "ステラの生産率を+2％")},
		{1034, new AwakeClass(AWAKE_TYPE_INST_LV, 20000,  2,  100*Math.Pow(15.5, 33),   "聖霊の光の使い手", "ステラの生産率を+2％")},
		{1035, new AwakeClass(AWAKE_TYPE_INST_LV, 30000,  2,  100*Math.Pow(15.5, 34),   "聖霊の闇の使い手", "ステラの生産率を+2％")},
		{1036, new AwakeClass(AWAKE_TYPE_INST_LV, 40000,  2,  100*Math.Pow(15.5, 35),   "聖霊の空間の支配者",    "ステラの生産率を+2％")},
		{1037, new AwakeClass(AWAKE_TYPE_INST_LV, 50000,  2,  100*Math.Pow(15.5, 36),   "聖霊の時間の支配者", "ステラの生産率を+2％")},

		{2001, new AwakeClass(AWAKE_TYPE_TIME,1800,            5,  15000,                                        "木々の使者",                       "「世界樹の葉」のステラ生産率を+5％")},
		{2002, new AwakeClass(AWAKE_TYPE_TIME,1*(60*60),       5,  15000*Math.Pow(15.5, 1),                      "ふわりと舞う葉っぱ",               "「世界樹の葉」のステラ生産率を+5％")},
		{2003, new AwakeClass(AWAKE_TYPE_TIME,2*(60*60),       5,  15000*Math.Pow(15.5, 2),                      "大地を突き抜ける芽",               "「世界樹の葉」のステラ生産率を+5％")},
		{2004, new AwakeClass(AWAKE_TYPE_TIME,5*(60*60),       1,  15000*Math.Pow(15.5, 3),                      "雄大な枝葉を持つ若木",             "夜空のステラを解除される、夜空のステラが1に付き、ステラの総生産率を+5％、通算プレイ時間が5時間に付き、夜空のステラが+1される")},
		{2005, new AwakeClass(AWAKE_TYPE_TIME,10*(60*60),      1,  15000*Math.Pow(15.5, 4),                      "緑豊かな木立の一員",               "「ワタリカラス」が1レベルに付き、「世界樹の葉」のステラ生産率を+1％")},
		{2006, new AwakeClass(AWAKE_TYPE_TIME,15*(60*60),      1,  15000*Math.Pow(15.5, 5),                      "頑丈な幹を持つ老木",               "解除された実績が1個に付き、「ワタリガラス」のステラ生産率を+1％")},//■■■まだ■■
        {2007, new AwakeClass(AWAKE_TYPE_TIME,20*(60*60),      5,  15000*Math.Pow(15.5, 6),                      "大自然を象徴する樹木",             "「世界樹の葉」のステラ生産率を+5％")},
		{2008, new AwakeClass(AWAKE_TYPE_TIME,25*(60*60),      5,  15000*Math.Pow(15.5, 7),                      "世界樹の子孫",                     "「世界樹の葉」のステラ生産率を+5％")},
		{2009, new AwakeClass(AWAKE_TYPE_TIME,30*(60*60),      5,  15000*Math.Pow(15.5, 8),                      "魔法の森の主",                     "「世界樹の葉」のステラ生産率を+5％")},
		{2010, new AwakeClass(AWAKE_TYPE_TIME,50*(60*60),      1,  15000*Math.Pow(15.5, 9),                      "時を超えた存在",                   "「人族の農場」が3レベルに付き、「世界樹の葉」のステラ生産率を+1％")},
		{2011, new AwakeClass(AWAKE_TYPE_TIME,60*(60*60),      1,  15000*Math.Pow(15.5, 10),                     "空を見上げる者",                   "「ダークエルフの泉」が4レベルに付き、「世界樹の葉」のステラ生産率を+1％")},
		{2012, new AwakeClass(AWAKE_TYPE_TIME,70*(60*60),      1,  15000*Math.Pow(15.5, 11),                     "枝葉に宿る知識",                   "「ドヴェルグの鍛冶場」が5レベルに付き、「世界樹の葉」のステラ生産率を+1％")},
		{2013, new AwakeClass(AWAKE_TYPE_TIME,100*(60*60),     10, 15000*Math.Pow(15.5, 12),                     "大自然の響きを感じる者",           "解除された伝説の生き物が1種類に付き、ステラの生産率を+10％")},
		{2014, new AwakeClass(AWAKE_TYPE_TIME,150*(60*60),     1,  15000*Math.Pow(15.5, 13),                     "深い森の探検家",                   "「ヴァン神族の祭祀場」が6レベルに付き、「ワタリガラス」のステラ生産率を+1％")},
		{2015, new AwakeClass(AWAKE_TYPE_TIME,200*(60*60),     1,  15000*Math.Pow(15.5, 14),                     "夜明け前の森の番人",               "「神聖なる樹海」が7レベルに付き、「ワタリガラス」のステラ生産率を+1％")},
		{2016, new AwakeClass(AWAKE_TYPE_TIME,250*(60*60),     1,  15000*Math.Pow(15.5, 15),                     "森の住人の友",                     "「テュールの神殿」が8レベルに付き、「ワタリガラス」のステラ生産率を+1％")},
		{2017, new AwakeClass(AWAKE_TYPE_TIME,300*(60*60),     1,  15000*Math.Pow(15.5, 16),                     "自然と共に生きる者",               "「エルフ薬草園」が9レベルに付き、「ワタリガラス」のステラ生産率を+1％")},
		{2018, new AwakeClass(AWAKE_TYPE_TIME,350*(60*60),     1,  15000*Math.Pow(15.5, 17),                     "森の賢者",                         "「異世界のワープゲート」が10レベルに付き、「ワタリガラス」のステラ生産率を+1％")},
		{2019, new AwakeClass(AWAKE_TYPE_TIME,400*(60*60),     2,  15000*Math.Pow(15.5, 18),                     "創造力に満ちた森の守護者",         "ステラの欠片の出現率を+2％")},
		{2020, new AwakeClass(AWAKE_TYPE_TIME,450*(60*60),     2,  15000*Math.Pow(15.5, 19),                     "森の神話を紡ぐ者",                 "ステラの欠片の出現率を+2％")},
		{2021, new AwakeClass(AWAKE_TYPE_TIME,500*(60*60),     2,  15000*Math.Pow(15.5, 20),                     "森の繁栄を願う者",                 "ステラの欠片の出現率を+2％")},
		{2022, new AwakeClass(AWAKE_TYPE_TIME,550*(60*60),     2,  15000*Math.Pow(15.5, 21),                     "生命の輝きを見る者",               "ステラの欠片の出現率を+2％")},
		{2023, new AwakeClass(AWAKE_TYPE_TIME,600*(60*60),     2,  15000*Math.Pow(15.5, 22),                     "自然を守る者",                     "ステラの欠片の出現率を+2％")},
		{2024, new AwakeClass(AWAKE_TYPE_TIME,650*(60*60),     5,  15000*Math.Pow(15.5, 23),                     "自然界の法則",                     "ステラの生産率+5％")},
		{2025, new AwakeClass(AWAKE_TYPE_TIME,700*(60*60),     5,  15000*Math.Pow(15.5, 24),                     "ユグドラシルの探求者",             "ステラの生産率+5％")},
		{2026, new AwakeClass(AWAKE_TYPE_TIME,750*(60*60),     5,  15000*Math.Pow(15.5, 25),                     "ユグドラシルの夢想家",             "ステラの生産率+5％")},
		{2027, new AwakeClass(AWAKE_TYPE_TIME,800*(60*60),     5,  15000*Math.Pow(15.5, 26),                     "ユグドラシルの伝説",               "ステラの生産率+5％")},
		{2028, new AwakeClass(AWAKE_TYPE_TIME,850*(60*60),     5,  15000*Math.Pow(15.5, 27),                     "ユグドラシルの覇者 ",              "ステラの生産率+5％")},
		{2029, new AwakeClass(AWAKE_TYPE_TIME,900*(60*60),     5,  15000*Math.Pow(15.5, 28),                     "ユグドラシルの救世主",             "ステラの生産率+5％")},
		{2030, new AwakeClass(AWAKE_TYPE_TIME,950*(60*60),     5,  15000*Math.Pow(15.5, 29),                     "ユグドラシルの世界に身を置く者 ",  "ステラの生産率+5％")},
		{2031, new AwakeClass(AWAKE_TYPE_TIME,1000*(60*60),    5,  15000*Math.Pow(15.5, 30),                     "聖霊と共に踊る者",                 "ステラの生産率+5％")},
		{2032, new AwakeClass(AWAKE_TYPE_TIME,1200*(60*60),    5,  15000*Math.Pow(15.5, 31),                     "終わりのない冒険者",               "ステラの生産率+5％")},
		{2033, new AwakeClass(AWAKE_TYPE_TIME,1400*(60*60),    5,  15000*Math.Pow(15.5, 32),                     "ユグドラシルの聖域",               "ステラの生産率+5％")},
		{2034, new AwakeClass(AWAKE_TYPE_TIME,1600*(60*60),    5,  15000*Math.Pow(15.5, 33),                     "ユグドラシルの永遠",               "ステラの生産率+5％")},
		{2035, new AwakeClass(AWAKE_TYPE_TIME,1800*(60*60),    5,  15000*Math.Pow(15.5, 34),                     "ユグドラシルの守護神",             "ステラの生産率+5％")},
		{2036, new AwakeClass(AWAKE_TYPE_TIME,2000*(60*60),    5,  15000*Math.Pow(15.5, 35),                     "ユグドラシルの終焉を見届けた者",   "ステラの生産率+5％")},
		{2037, new AwakeClass(AWAKE_TYPE_TIME,5000*(60*60),    5,  15000*Math.Pow(15.5, 36),                     "ユグドラシルの根源",               "ステラの生産率+5％")},



		{10001, new AwakeClass(AWAKE_TYPE_INST, 10,2, 100                   ,"初めの一歩","世界樹の葉の効率が2倍になる")},
		{10002, new AwakeClass(AWAKE_TYPE_INST, 20,2, 100*Math.Pow(13.6, 1) ,"緑の翼","世界樹の葉の効率が2倍になる")},
		{10003, new AwakeClass(AWAKE_TYPE_INST, 30,2, 100*Math.Pow(13.6, 2) ,"地を這う者","世界樹の葉の効率が2倍になる")},
		{10004, new AwakeClass(AWAKE_TYPE_INST, 40,2, 100*Math.Pow(13.6, 3) ,"大地の贈り物","世界樹の葉の効率が2倍になる")},
		{10005, new AwakeClass(AWAKE_TYPE_INST, 50,3, 100*Math.Pow(13.6, 4) ,"自然の調和者 ","世界樹の葉の効率が3倍になる")},
		{10006, new AwakeClass(AWAKE_TYPE_INST, 60,2, 100*Math.Pow(13.6, 5) ,"聖霊の友","世界樹の葉の効率が2倍になる")},
		{10007, new AwakeClass(AWAKE_TYPE_INST, 70,5, 100*Math.Pow(13.6, 6) ,"風の翼","世界樹の葉の効率が5倍になる")},
		{10008, new AwakeClass(AWAKE_TYPE_INST, 80,2, 100*Math.Pow(13.6, 7) ,"光の導き手","世界樹の葉の効率が2倍になる")},
		{10009, new AwakeClass(AWAKE_TYPE_INST, 90,10,100*Math.Pow(13.6, 8) ,"聖なる力","世界樹の葉の効率が10倍になる")},
		{10010, new AwakeClass(AWAKE_TYPE_INST, 100,2,100*Math.Pow(13.6, 9) ,"鋼鉄の意志","世界樹の葉の効率が2倍になる")},
		{10011, new AwakeClass(AWAKE_TYPE_INST, 150,2,100*Math.Pow(13.6, 10),"魔法の守護者","世界樹の葉の効率が2倍になる")},
		{10012, new AwakeClass(AWAKE_TYPE_INST, 200,2,100*Math.Pow(13.6, 11),"黄金の守護者","世界樹の葉の効率が2倍になる")},
		{10013, new AwakeClass(AWAKE_TYPE_INST, 250,5,100*Math.Pow(13.6, 12),"魂の収集者","世界樹の葉の効率が5倍になる")},
		{10014, new AwakeClass(AWAKE_TYPE_INST, 300,2,100*Math.Pow(13.6, 13),"神秘の探求者","世界樹の葉の効率が2倍になる")},
		{10015, new AwakeClass(AWAKE_TYPE_INST, 400,2,100*Math.Pow(13.6, 14),"時間の支配者","世界樹の葉の効率が2倍になる")},
		{10016, new AwakeClass(AWAKE_TYPE_INST, 500,2,100*Math.Pow(13.6, 15),"不滅の意志","世界樹の葉の効率が2倍になる")},
		{10017, new AwakeClass(AWAKE_TYPE_INST, 600,2,100*Math.Pow(13.6, 16),"宇宙の探求者","世界樹の葉の効率が2倍になる")},
		{10018, new AwakeClass(AWAKE_TYPE_INST, 700,5,100*Math.Pow(13.6, 17),"世界の支配者","世界樹の葉の効率が5倍になる")},
		{20001, new AwakeClass(AWAKE_TYPE_INST, 10,2, 200                   ,"学びの旅人","ワタリカラスの効率が2倍になる")},
		{20002, new AwakeClass(AWAKE_TYPE_INST, 20,2, 200*Math.Pow(13.6, 1) ,"冒険者","ワタリカラスの効率が2倍になる")},
		{20003, new AwakeClass(AWAKE_TYPE_INST, 30,2, 200*Math.Pow(13.6, 2) ,"探究者","ワタリカラスの効率が2倍になる")},
		{20004, new AwakeClass(AWAKE_TYPE_INST, 40,2, 200*Math.Pow(13.6, 3) ,"自然の守り手","ワタリカラスの効率が2倍になる")},
		{20005, new AwakeClass(AWAKE_TYPE_INST, 50,3, 200*Math.Pow(13.6, 4) ,"勇敢な戦士","ワタリカラスの効率が3倍になる")},
		{20006, new AwakeClass(AWAKE_TYPE_INST, 60,2, 200*Math.Pow(13.6, 5) ,"天空の舞者","ワタリカラスの効率が2倍になる")},
		{20007, new AwakeClass(AWAKE_TYPE_INST, 70,2, 200*Math.Pow(13.6, 6) ,"真実の探求者","ワタリカラスの効率が2倍になる")},
		{20008, new AwakeClass(AWAKE_TYPE_INST, 80,2, 200*Math.Pow(13.6, 7) ,"幻想の冒険者","ワタリカラスの効率が2倍になる")},
		{20009, new AwakeClass(AWAKE_TYPE_INST, 90,2, 200*Math.Pow(13.6, 8) ,"天空の旅人","ワタリカラスの効率が2倍になる")},
		{20010, new AwakeClass(AWAKE_TYPE_INST, 100,5,200*Math.Pow(13.6, 9) ,"時空を超える者","ワタリカラスの効率が5倍になる")},
		{20011, new AwakeClass(AWAKE_TYPE_INST, 150,2,200*Math.Pow(13.6, 10),"魂の輝き","ワタリカラスの効率が2倍になる")},
		{20012, new AwakeClass(AWAKE_TYPE_INST, 200,2,200*Math.Pow(13.6, 11),"魂の紡ぎ手","ワタリカラスの効率が2倍になる")},
		{20013, new AwakeClass(AWAKE_TYPE_INST, 250,5,200*Math.Pow(13.6, 12),"知恵の探求者","ワタリカラスの効率が5倍になる")},
		{20014, new AwakeClass(AWAKE_TYPE_INST, 300,3,200*Math.Pow(13.6, 13),"時空を操る者","ワタリカラスの効率が3倍になる")},
		{20015, new AwakeClass(AWAKE_TYPE_INST, 400,3,200*Math.Pow(13.6, 14),"運命の導き手","ワタリカラスの効率が3倍になる")},
		{20016, new AwakeClass(AWAKE_TYPE_INST, 500,3,200*Math.Pow(13.6, 15),"宇宙の航海者","ワタリカラスの効率が3倍になる")},
		{20017, new AwakeClass(AWAKE_TYPE_INST, 600,3,200*Math.Pow(13.6, 16),"超越する者","ワタリカラスの効率が3倍になる")},
		{20018, new AwakeClass(AWAKE_TYPE_INST, 700,5,200*Math.Pow(13.6, 17),"真理の探求者","ワタリカラスの効率が5倍になる")},
		{30001, new AwakeClass(AWAKE_TYPE_INST, 10,2, 700                   ,"土壌の修復士","人族の農場の効率が2倍になる")},
		{30002, new AwakeClass(AWAKE_TYPE_INST, 20,2, 700*Math.Pow(13.6, 1) ,"収穫の達人","人族の農場の効率が2倍になる")},
		{30003, new AwakeClass(AWAKE_TYPE_INST, 30,2, 700*Math.Pow(13.6, 2) ,"農業のプロ","人族の農場の効率が2倍になる")},
		{30004, new AwakeClass(AWAKE_TYPE_INST, 40,2, 700*Math.Pow(13.6, 3) ,"緑の大地の守り手","人族の農場の効率が2倍になる")},
		{30005, new AwakeClass(AWAKE_TYPE_INST, 50,2, 700*Math.Pow(13.6, 4) ,"自給自足の生活者","人族の農場の効率が2倍になる")},
		{30006, new AwakeClass(AWAKE_TYPE_INST, 60,2, 700*Math.Pow(13.6, 5) ,"土地改良の達人","人族の農場の効率が2倍になる")},
		{30007, new AwakeClass(AWAKE_TYPE_INST, 70,2, 700*Math.Pow(13.6, 6) ,"食糧危機を救う者","人族の農場の効率が2倍になる")},
		{30008, new AwakeClass(AWAKE_TYPE_INST, 80,2, 700*Math.Pow(13.6, 7) ,"作物の研究者","人族の農場の効率が2倍になる")},
		{30009, new AwakeClass(AWAKE_TYPE_INST, 90,5, 700*Math.Pow(13.6, 8) ,"食糧問題の解決者 ","人族の農場の効率が5倍になる")},
		{30010, new AwakeClass(AWAKE_TYPE_INST, 100,2,700*Math.Pow(13.6, 9) ,"農業の継承者","人族の農場の効率が2倍になる")},
		{30011, new AwakeClass(AWAKE_TYPE_INST, 150,2,700*Math.Pow(13.6, 10),"農業の開拓者","人族の農場の効率が2倍になる")},
		{30012, new AwakeClass(AWAKE_TYPE_INST, 200,3,700*Math.Pow(13.6, 11),"革命の先駆者","人族の農場の効率が3倍になる")},
		{30013, new AwakeClass(AWAKE_TYPE_INST, 250,2,700*Math.Pow(13.6, 12),"食の豊かさを追求する存在","人族の農場の効率が2倍になる")},
		{30014, new AwakeClass(AWAKE_TYPE_INST, 300,3,700*Math.Pow(13.6, 13),"農業文化の継承者","人族の農場の効率が3倍になる")},
		{30015, new AwakeClass(AWAKE_TYPE_INST, 400,3,700*Math.Pow(13.6, 14),"未来を担う存在","人族の農場の効率が3倍になる")},
		{30016, new AwakeClass(AWAKE_TYPE_INST, 500,3,700*Math.Pow(13.6, 15),"自然の叡智","人族の農場の効率が3倍になる")},
		{30017, new AwakeClass(AWAKE_TYPE_INST, 600,3,700*Math.Pow(13.6, 16),"恵みの管理人","人族の農場の効率が3倍になる")},
		{30018, new AwakeClass(AWAKE_TYPE_INST, 700,5,700*Math.Pow(13.6, 17),"食糧生産の帝王","人族の農場の効率が5倍になる")},
		{40001, new AwakeClass(AWAKE_TYPE_INST, 10,2, 10000                   ,"森の賢者","エルフ族の泉の効率が2倍になる")},
		{40002, new AwakeClass(AWAKE_TYPE_INST, 20,2, 10000*Math.Pow(13.6, 1) ,"森の癒し手 ","エルフ族の泉の効率が2倍になる")},
		{40003, new AwakeClass(AWAKE_TYPE_INST, 30,2, 10000*Math.Pow(13.6, 2) ,"自然界の使者","エルフ族の泉の効率が2倍になる")},
		{40004, new AwakeClass(AWAKE_TYPE_INST, 40,2, 10000*Math.Pow(13.6, 3) ,"神秘の水源守り","エルフ族の泉の効率が2倍になる")},
		{40005, new AwakeClass(AWAKE_TYPE_INST, 50,2, 10000*Math.Pow(13.6, 4) ,"水の魔術師","エルフ族の泉の効率が2倍になる")},
		{40006, new AwakeClass(AWAKE_TYPE_INST, 60,2, 10000*Math.Pow(13.6, 5) ,"水の加護を授ける者","エルフ族の泉の効率が2倍になる")},
		{40007, new AwakeClass(AWAKE_TYPE_INST, 70,2, 10000*Math.Pow(13.6, 6) ,"森の守り神","エルフ族の泉の効率が2倍になる")},
		{40008, new AwakeClass(AWAKE_TYPE_INST, 80,2, 10000*Math.Pow(13.6, 7) ,"水の騎士","エルフ族の泉の効率が2倍になる")},
		{40009, new AwakeClass(AWAKE_TYPE_INST, 90,5, 10000*Math.Pow(13.6, 8) ,"水の奇跡を起こす者","エルフ族の泉の効率が5倍になる")},
		{40010, new AwakeClass(AWAKE_TYPE_INST, 100,2,10000*Math.Pow(13.6, 9) ,"水の聖者","エルフ族の泉の効率が2倍になる")},
		{40011, new AwakeClass(AWAKE_TYPE_INST, 150,2,10000*Math.Pow(13.6, 10),"泉の守護者","エルフ族の泉の効率が2倍になる")},
		{40012, new AwakeClass(AWAKE_TYPE_INST, 200,2,10000*Math.Pow(13.6, 11),"清水の聖者","エルフ族の泉の効率が2倍になる")},
		{40013, new AwakeClass(AWAKE_TYPE_INST, 250,2,10000*Math.Pow(13.6, 12),"純水の加護","エルフ族の泉の効率が2倍になる")},
		{40014, new AwakeClass(AWAKE_TYPE_INST, 300,3,10000*Math.Pow(13.6, 13),"泉の浄化者","エルフ族の泉の効率が3倍になる")},
		{40015, new AwakeClass(AWAKE_TYPE_INST, 400,3,10000*Math.Pow(13.6, 14),"流水の詠唱師","エルフ族の泉の効率が3倍になる")},
		{40016, new AwakeClass(AWAKE_TYPE_INST, 500,3,10000*Math.Pow(13.6, 15),"泉の祝福を受けし者","エルフ族の泉の効率が3倍になる")},
		{40017, new AwakeClass(AWAKE_TYPE_INST, 600,3,10000*Math.Pow(13.6, 16),"泉の瑠璃色騎士","エルフ族の泉の効率が3倍になる")},
		{40018, new AwakeClass(AWAKE_TYPE_INST, 700,5,10000*Math.Pow(13.6, 17),"湧き出る命の源","エルフ族の泉の効率が5倍になる")},
		{50001, new AwakeClass(AWAKE_TYPE_INST, 10,2, 149330                   ,"鉄の職人","ドヴェルグ族の鍛冶場の効率が2倍になる")},
		{50002, new AwakeClass(AWAKE_TYPE_INST, 20,2, 149330*Math.Pow(13.6, 1) ,"熟練の鋳造技術者","ドヴェルグ族の鍛冶場の効率が2倍になる")},
		{50003, new AwakeClass(AWAKE_TYPE_INST, 30,2, 149330*Math.Pow(13.6, 2) ,"鋳造の専門家","ドヴェルグ族の鍛冶場の効率が2倍になる")},
		{50004, new AwakeClass(AWAKE_TYPE_INST, 40,2, 149330*Math.Pow(13.6, 3) ,"鍛冶場の名工","ドヴェルグ族の鍛冶場の効率が2倍になる")},
		{50005, new AwakeClass(AWAKE_TYPE_INST, 50,2, 149330*Math.Pow(13.6, 4) ,"熟練の鋳造職人","ドヴェルグ族の鍛冶場の効率が2倍になる")},
		{50006, new AwakeClass(AWAKE_TYPE_INST, 60,2, 149330*Math.Pow(13.6, 5) ,"鋼鉄の創造者","ドヴェルグ族の鍛冶場の効率が2倍になる")},
		{50007, new AwakeClass(AWAKE_TYPE_INST, 70,2, 149330*Math.Pow(13.6, 6) ,"鋼鉄の加工者","ドヴェルグ族の鍛冶場の効率が2倍になる")},
		{50008, new AwakeClass(AWAKE_TYPE_INST, 80,2, 149330*Math.Pow(13.6, 7) ,"熟練の鍛冶職人","ドヴェルグ族の鍛冶場の効率が2倍になる")},
		{50009, new AwakeClass(AWAKE_TYPE_INST, 90,5, 149330*Math.Pow(13.6, 8) ,"鋳造の革命家","ドヴェルグ族の鍛冶場の効率が5倍になる")},
		{50010, new AwakeClass(AWAKE_TYPE_INST, 100,2,149330*Math.Pow(13.6, 9) ,"鋳造の巨匠","ドヴェルグ族の鍛冶場の効率が2倍になる")},
		{50011, new AwakeClass(AWAKE_TYPE_INST, 150,2,149330*Math.Pow(13.6, 10),"鋳造技術者","ドヴェルグ族の鍛冶場の効率が2倍になる")},
		{50012, new AwakeClass(AWAKE_TYPE_INST, 200,2,149330*Math.Pow(13.6, 11),"熔鉄の巨匠","ドヴェルグ族の鍛冶場の効率が2倍になる")},
		{50013, new AwakeClass(AWAKE_TYPE_INST, 250,2,149330*Math.Pow(13.6, 12),"鍛冶場の黒騎士","ドヴェルグ族の鍛冶場の効率が2倍になる")},
		{50014, new AwakeClass(AWAKE_TYPE_INST, 300,3,149330*Math.Pow(13.6, 13),"鍛冶場の主","ドヴェルグ族の鍛冶場の効率が3倍になる")},
		{50015, new AwakeClass(AWAKE_TYPE_INST, 400,3,149330*Math.Pow(13.6, 14),"鍛冶場の巨匠","ドヴェルグ族の鍛冶場の効率が3倍になる")},
		{50016, new AwakeClass(AWAKE_TYPE_INST, 500,3,149330*Math.Pow(13.6, 15),"炎の聖域の守護者","ドヴェルグ族の鍛冶場の効率が3倍になる")},
		{50017, new AwakeClass(AWAKE_TYPE_INST, 600,3,149330*Math.Pow(13.6, 16),"鉄の聖域の守護者","ドヴェルグ族の鍛冶場の効率が3倍になる")},
		{50018, new AwakeClass(AWAKE_TYPE_INST, 700,5,149330*Math.Pow(13.6, 17),"偉大なる錬金術師","ドヴェルグ族の鍛冶場の効率が5倍になる")},
		{60001, new AwakeClass(AWAKE_TYPE_INST, 10,2, 2150355                   ,"神の使者の召集者","ヴァン神族の祭祀場の効率が2倍になる")},
		{60002, new AwakeClass(AWAKE_TYPE_INST, 20,2, 2150355*Math.Pow(13.6, 1) ,"神秘の儀式の指導者","ヴァン神族の祭祀場の効率が2倍になる")},
		{60003, new AwakeClass(AWAKE_TYPE_INST, 30,2, 2150355*Math.Pow(13.6, 2) ,"神々の恵みの伝達者","ヴァン神族の祭祀場の効率が2倍になる")},
		{60004, new AwakeClass(AWAKE_TYPE_INST, 40,2, 2150355*Math.Pow(13.6, 3) ,"神聖なる力の使者","ヴァン神族の祭祀場の効率が2倍になる")},
		{60005, new AwakeClass(AWAKE_TYPE_INST, 50,2, 2150355*Math.Pow(13.6, 4) ,"神々の意思の伝達者","ヴァン神族の祭祀場の効率が2倍になる")},
		{60006, new AwakeClass(AWAKE_TYPE_INST, 60,2, 2150355*Math.Pow(13.6, 5) ,"神聖なる儀式の祝福者","ヴァン神族の祭祀場の効率が2倍になる")},
		{60007, new AwakeClass(AWAKE_TYPE_INST, 70,2, 2150355*Math.Pow(13.6, 6) ,"祭祀場の神聖なる契約者","ヴァン神族の祭祀場の効率が2倍になる")},
		{60008, new AwakeClass(AWAKE_TYPE_INST, 80,2, 2150355*Math.Pow(13.6, 7) ,"神々の力の解放者","ヴァン神族の祭祀場の効率が2倍になる")},
		{60009, new AwakeClass(AWAKE_TYPE_INST, 90,5, 2150355*Math.Pow(13.6, 8) ,"祭祀場の神聖なる護法者","ヴァン神族の祭祀場の効率が5倍になる")},
		{60010, new AwakeClass(AWAKE_TYPE_INST, 100,2,2150355*Math.Pow(13.6, 9) ,"霊魂の狩人","ヴァン神族の祭祀場の効率が2倍になる")},
		{60011, new AwakeClass(AWAKE_TYPE_INST, 150,2,2150355*Math.Pow(13.6, 10),"神々の恩寵の授け手","ヴァン神族の祭祀場の効率が2倍になる")},
		{60012, new AwakeClass(AWAKE_TYPE_INST, 200,2,2150355*Math.Pow(13.6, 11),"神聖なる輝きの探求者","ヴァン神族の祭祀場の効率が2倍になる")},
		{60013, new AwakeClass(AWAKE_TYPE_INST, 250,2,2150355*Math.Pow(13.6, 12),"神聖なる知識の収集者","ヴァン神族の祭祀場の効率が2倍になる")},
		{60014, new AwakeClass(AWAKE_TYPE_INST, 300,3,2150355*Math.Pow(13.6, 13),"霊魂の安息の主","ヴァン神族の祭祀場の効率が3倍になる")},
		{60015, new AwakeClass(AWAKE_TYPE_INST, 400,3,2150355*Math.Pow(13.6, 14),"神聖なる契約の締結者","ヴァン神族の祭祀場の効率が3倍になる")},
		{60016, new AwakeClass(AWAKE_TYPE_INST, 500,3,2150355*Math.Pow(13.6, 15),"神々の意志の実現者","ヴァン神族の祭祀場の効率が3倍になる")},
		{60017, new AwakeClass(AWAKE_TYPE_INST, 600,3,2150355*Math.Pow(13.6, 16),"神聖なる生命の創造者","ヴァン神族の祭祀場の効率が3倍になる")},
		{60018, new AwakeClass(AWAKE_TYPE_INST, 700,5,2150355*Math.Pow(13.6, 17),"神聖なる霊魂の輪廻者","ヴァン神族の祭祀場の効率が5倍になる")},
		{70001, new AwakeClass(AWAKE_TYPE_INST, 10,2, 30965115                   ,"樹海の自然を愛する者","神聖なる樹海の効率が2倍になる")},
		{70002, new AwakeClass(AWAKE_TYPE_INST, 20,2, 30965115*Math.Pow(13.6, 1) ,"樹海の森林浴の案内人","神聖なる樹海の効率が2倍になる")},
		{70003, new AwakeClass(AWAKE_TYPE_INST, 30,2, 30965115*Math.Pow(13.6, 2) ,"樹海の生態系の保護者","神聖なる樹海の効率が2倍になる")},
		{70004, new AwakeClass(AWAKE_TYPE_INST, 40,2, 30965115*Math.Pow(13.6, 3) ,"樹海の生態系の維持者","神聖なる樹海の効率が2倍になる")},
		{70005, new AwakeClass(AWAKE_TYPE_INST, 50,3, 30965115*Math.Pow(13.6, 4) ,"自然の息吹を感じる者","神聖なる樹海の効率が3倍になる")},
		{70006, new AwakeClass(AWAKE_TYPE_INST, 60,2, 30965115*Math.Pow(13.6, 5) ,"神聖なる森の癒し手","神聖なる樹海の効率が2倍になる")},
		{70007, new AwakeClass(AWAKE_TYPE_INST, 70,5, 30965115*Math.Pow(13.6, 6) ,"神聖なる樹海の生態学者","神聖なる樹海の効率が5倍になる")},
		{70008, new AwakeClass(AWAKE_TYPE_INST, 80,2, 30965115*Math.Pow(13.6, 7) ,"樹海の神聖なる知識の伝承者","神聖なる樹海の効率が2倍になる")},
		{70009, new AwakeClass(AWAKE_TYPE_INST, 90,10,30965115*Math.Pow(13.6, 8) ,"樹海の神聖なる秘儀の学者","神聖なる樹海の効率が10倍になる")},
		{70010, new AwakeClass(AWAKE_TYPE_INST, 100,2,30965115*Math.Pow(13.6, 9) ,"樹海の森羅万象を熟知する者","神聖なる樹海の効率が2倍になる")},
		{70011, new AwakeClass(AWAKE_TYPE_INST, 150,2,30965115*Math.Pow(13.6, 10),"樹海の生命力を宿す者","神聖なる樹海の効率が2倍になる")},
		{70012, new AwakeClass(AWAKE_TYPE_INST, 200,2,30965115*Math.Pow(13.6, 11),"生命の響きを聴く者","神聖なる樹海の効率が2倍になる")},
		{70013, new AwakeClass(AWAKE_TYPE_INST, 250,5,30965115*Math.Pow(13.6, 12),"生命の輝きを解き放つ者","神聖なる樹海の効率が5倍になる")},
		{70014, new AwakeClass(AWAKE_TYPE_INST, 300,2,30965115*Math.Pow(13.6, 13),"自然と共に生きる者","神聖なる樹海の効率が2倍になる")},
		{70015, new AwakeClass(AWAKE_TYPE_INST, 400,2,30965115*Math.Pow(13.6, 14),"生態系の守護神","神聖なる樹海の効率が2倍になる")},
		{70016, new AwakeClass(AWAKE_TYPE_INST, 500,2,30965115*Math.Pow(13.6, 15),"樹海の自然療法士","神聖なる樹海の効率が2倍になる")},
		{70017, new AwakeClass(AWAKE_TYPE_INST, 600,2,30965115*Math.Pow(13.6, 16),"神聖なる魂の発見者","神聖なる樹海の効率が2倍になる")},
		{70018, new AwakeClass(AWAKE_TYPE_INST, 700,5,30965115*Math.Pow(13.6, 17),"樹海の儀式の司祭","神聖なる樹海の効率が5倍になる")},
		{80001, new AwakeClass(AWAKE_TYPE_INST, 10,2, 445897660                   ,"法の執行者","テュールの神殿の効率が2倍になる")},
		{80002, new AwakeClass(AWAKE_TYPE_INST, 20,2, 445897660*Math.Pow(13.6, 1) ,"弱き者の守護者","テュールの神殿の効率が2倍になる")},
		{80003, new AwakeClass(AWAKE_TYPE_INST, 30,2, 445897660*Math.Pow(13.6, 2) ,"法律の散歩書","テュールの神殿の効率が2倍になる")},
		{80004, new AwakeClass(AWAKE_TYPE_INST, 40,2, 445897660*Math.Pow(13.6, 3) ,"正義の裁判官","テュールの神殿の効率が2倍になる")},
		{80005, new AwakeClass(AWAKE_TYPE_INST, 50,3, 445897660*Math.Pow(13.6, 4) ,"聖なるパラディン","テュールの神殿の効率が3倍になる")},
		{80006, new AwakeClass(AWAKE_TYPE_INST, 60,2, 445897660*Math.Pow(13.6, 5) ,"正義の勇者","テュールの神殿の効率が2倍になる")},
		{80007, new AwakeClass(AWAKE_TYPE_INST, 70,2, 445897660*Math.Pow(13.6, 6) ,"無辜の盾","テュールの神殿の効率が2倍になる")},
		{80008, new AwakeClass(AWAKE_TYPE_INST, 80,2, 445897660*Math.Pow(13.6, 7) ,"テュールの鉄槌","テュールの神殿の効率が2倍になる")},
		{80009, new AwakeClass(AWAKE_TYPE_INST, 90,2, 445897660*Math.Pow(13.6, 8) ,"真実の守護者","テュールの神殿の効率が2倍になる")},
		{80010, new AwakeClass(AWAKE_TYPE_INST, 100,5,445897660*Math.Pow(13.6, 9) ,"聖なる亀","テュールの神殿の効率が5倍になる")},
		{80011, new AwakeClass(AWAKE_TYPE_INST, 150,2,445897660*Math.Pow(13.6, 10),"正義の右腕","テュールの神殿の効率が2倍になる")},
		{80012, new AwakeClass(AWAKE_TYPE_INST, 200,2,445897660*Math.Pow(13.6, 11),"神聖なる調停者","テュールの神殿の効率が2倍になる")},
		{80013, new AwakeClass(AWAKE_TYPE_INST, 250,5,445897660*Math.Pow(13.6, 12),"裁判官と陪審員","テュールの神殿の効率が5倍になる")},
		{80014, new AwakeClass(AWAKE_TYPE_INST, 300,3,445897660*Math.Pow(13.6, 13),"英雄の伝説","テュールの神殿の効率が3倍になる")},
		{80015, new AwakeClass(AWAKE_TYPE_INST, 400,3,445897660*Math.Pow(13.6, 14),"逆転勝利","テュールの神殿の効率が3倍になる")},
		{80016, new AwakeClass(AWAKE_TYPE_INST, 500,3,445897660*Math.Pow(13.6, 15),"鉄壁の守護者","テュールの神殿の効率が3倍になる")},
		{80017, new AwakeClass(AWAKE_TYPE_INST, 600,3,445897660*Math.Pow(13.6, 16),"至高者","テュールの神殿の効率が3倍になる")},
		{80018, new AwakeClass(AWAKE_TYPE_INST, 700,5,445897660*Math.Pow(13.6, 17),"不敗神話","テュールの神殿の効率が5倍になる")},
		{90001, new AwakeClass(AWAKE_TYPE_INST, 10,2, 6420926305                   ,"緑の妖精の友","エルフ薬草園の効率が2倍になる")},
		{90002, new AwakeClass(AWAKE_TYPE_INST, 20,2, 6420926305*Math.Pow(13.6, 1) ,"薬草の専門家","エルフ薬草園の効率が2倍になる")},
		{90003, new AwakeClass(AWAKE_TYPE_INST, 30,2, 6420926305*Math.Pow(13.6, 2) ,"薬草の騎士","エルフ薬草園の効率が2倍になる")},
		{90004, new AwakeClass(AWAKE_TYPE_INST, 40,2, 6420926305*Math.Pow(13.6, 3) ,"植物の味方","エルフ薬草園の効率が2倍になる")},
		{90005, new AwakeClass(AWAKE_TYPE_INST, 50,2, 6420926305*Math.Pow(13.6, 4) ,"自然の護り手","エルフ薬草園の効率が2倍になる")},
		{90006, new AwakeClass(AWAKE_TYPE_INST, 60,2, 6420926305*Math.Pow(13.6, 5) ,"薬草園の主","エルフ薬草園の効率が2倍になる")},
		{90007, new AwakeClass(AWAKE_TYPE_INST, 70,2, 6420926305*Math.Pow(13.6, 6) ,"植物学の天才","エルフ薬草園の効率が2倍になる")},
		{90008, new AwakeClass(AWAKE_TYPE_INST, 80,2, 6420926305*Math.Pow(13.6, 7) ,"エルフの薬師","エルフ薬草園の効率が2倍になる")},
		{90009, new AwakeClass(AWAKE_TYPE_INST, 90,5, 6420926305*Math.Pow(13.6, 8) ,"草木の聖霊使い","エルフ薬草園の効率が5倍になる")},
		{90010, new AwakeClass(AWAKE_TYPE_INST, 100,2,6420926305*Math.Pow(13.6, 9) ,"毒を知る者","エルフ薬草園の効率が2倍になる")},
		{90011, new AwakeClass(AWAKE_TYPE_INST, 150,2,6420926305*Math.Pow(13.6, 10),"緑の大神官","エルフ薬草園の効率が2倍になる")},
		{90012, new AwakeClass(AWAKE_TYPE_INST, 200,3,6420926305*Math.Pow(13.6, 11),"自然の慈愛者","エルフ薬草園の効率が3倍になる")},
		{90013, new AwakeClass(AWAKE_TYPE_INST, 250,2,6420926305*Math.Pow(13.6, 12),"薬草のアルケミスト","エルフ薬草園の効率が2倍になる")},
		{90014, new AwakeClass(AWAKE_TYPE_INST, 300,3,6420926305*Math.Pow(13.6, 13),"万能薬の開発者","エルフ薬草園の効率が3倍になる")},
		{90015, new AwakeClass(AWAKE_TYPE_INST, 400,3,6420926305*Math.Pow(13.6, 14),"未知の病に立ち向かう勇敢なる者","エルフ薬草園の効率が3倍になる")},
		{90016, new AwakeClass(AWAKE_TYPE_INST, 500,3,6420926305*Math.Pow(13.6, 15),"病気の原因を見抜く力を持つ者","エルフ薬草園の効率が3倍になる")},
		{90017, new AwakeClass(AWAKE_TYPE_INST, 600,3,6420926305*Math.Pow(13.6, 16),"風邪にも負けない体質の持ち主","エルフ薬草園の効率が3倍になる")},
		{90018, new AwakeClass(AWAKE_TYPE_INST, 700,5,6420926305*Math.Pow(13.6, 17),"毒にも負けない体質の持ち主","エルフ薬草園の効率が5倍になる")},
		{100001, new AwakeClass(AWAKE_TYPE_INST,10,2, 92461338795                   ,"ワープポイントの使い手","異世界のワープゲートの効率が2倍になる")},
		{100002, new AwakeClass(AWAKE_TYPE_INST,20,2, 92461338795*Math.Pow(13.6, 1) ,"異次元空間の旅人","異世界のワープゲートの効率が2倍になる")},
		{100003, new AwakeClass(AWAKE_TYPE_INST,30,2, 92461338795*Math.Pow(13.6, 2) ,"異世界への突破者","異世界のワープゲートの効率が2倍になる")},
		{100004, new AwakeClass(AWAKE_TYPE_INST,40,2, 92461338795*Math.Pow(13.6, 3) ,"遥かなる世界の冒険者","異世界のワープゲートの効率が2倍になる")},
		{100005, new AwakeClass(AWAKE_TYPE_INST,50,2, 92461338795*Math.Pow(13.6, 4) ,"テレポーター","異世界のワープゲートの効率が2倍になる")},
		{100006, new AwakeClass(AWAKE_TYPE_INST,60,2, 92461338795*Math.Pow(13.6, 5) ,"異界のエキスパート","異世界のワープゲートの効率が2倍になる")},
		{100007, new AwakeClass(AWAKE_TYPE_INST,70,2, 92461338795*Math.Pow(13.6, 6) ,"次元の鍵","異世界のワープゲートの効率が2倍になる")},
		{100008, new AwakeClass(AWAKE_TYPE_INST,80,2, 92461338795*Math.Pow(13.6, 7) ,"平面移動の達人","異世界のワープゲートの効率が2倍になる")},
		{100009, new AwakeClass(AWAKE_TYPE_INST,90,5, 92461338795*Math.Pow(13.6, 8) ,"スペース・シフター","異世界のワープゲートの効率が5倍になる")},
		{100010, new AwakeClass(AWAKE_TYPE_INST,100,2,92461338795*Math.Pow(13.6, 9) ,"異界の鍵を握る者","異世界のワープゲートの効率が2倍になる")},
		{100011, new AwakeClass(AWAKE_TYPE_INST,150,2,92461338795*Math.Pow(13.6, 10),"ワープゲートの管理人","異世界のワープゲートの効率が2倍になる")},
		{100012, new AwakeClass(AWAKE_TYPE_INST,200,2,92461338795*Math.Pow(13.6, 11),"次元の扉番","異世界のワープゲートの効率が2倍になる")},
		{100013, new AwakeClass(AWAKE_TYPE_INST,250,2,92461338795*Math.Pow(13.6, 12),"運命の分岐点を超える者","異世界のワープゲートの効率が2倍になる")},
		{100014, new AwakeClass(AWAKE_TYPE_INST,300,3,92461338795*Math.Pow(13.6, 13),"異次元の観測者","異世界のワープゲートの効率が3倍になる")},
		{100015, new AwakeClass(AWAKE_TYPE_INST,400,3,92461338795*Math.Pow(13.6, 14),"平行世界の歩き方マスター","異世界のワープゲートの効率が3倍になる")},
		{100016, new AwakeClass(AWAKE_TYPE_INST,500,3,92461338795*Math.Pow(13.6, 15),"ゲートウォッチャー","異世界のワープゲートの効率が3倍になる")},
		{100017, new AwakeClass(AWAKE_TYPE_INST,600,3,92461338795*Math.Pow(13.6, 16),"時空を超える者","異世界のワープゲートの効率が3倍になる")},
		{100018, new AwakeClass(AWAKE_TYPE_INST,700,5,92461338795*Math.Pow(13.6, 17),"インターディメンショナル・トラベラー","異世界のワープゲートの効率が5倍になる")},
		{110001, new AwakeClass(AWAKE_TYPE_INST,10,2, 1331443278650                   ,"墓地の主","冥界への扉の効率が2倍になる")},
		{110002, new AwakeClass(AWAKE_TYPE_INST,20,2, 1331443278650*Math.Pow(13.6, 1) ,"ネクロマンサー","冥界への扉の効率が2倍になる")},
		{110003, new AwakeClass(AWAKE_TYPE_INST,30,2, 1331443278650*Math.Pow(13.6, 2) ,"亡者の道標","冥界への扉の効率が2倍になる")},
		{110004, new AwakeClass(AWAKE_TYPE_INST,40,2, 1331443278650*Math.Pow(13.6, 3) ,"幽冥の羽衣使い","冥界への扉の効率が2倍になる")},
		{110005, new AwakeClass(AWAKE_TYPE_INST,50,2, 1331443278650*Math.Pow(13.6, 4) ,"霊界の番人","冥界への扉の効率が2倍になる")},
		{110006, new AwakeClass(AWAKE_TYPE_INST,60,2, 1331443278650*Math.Pow(13.6, 5) ,"亡霊の代弁者","冥界への扉の効率が2倍になる")},
		{110007, new AwakeClass(AWAKE_TYPE_INST,70,2, 1331443278650*Math.Pow(13.6, 6) ,"冥土の案内人","冥界への扉の効率が2倍になる")},
		{110008, new AwakeClass(AWAKE_TYPE_INST,80,2, 1331443278650*Math.Pow(13.6, 7) ,"骸を司る者","冥界への扉の効率が2倍になる")},
		{110009, new AwakeClass(AWAKE_TYPE_INST,90,5, 1331443278650*Math.Pow(13.6, 8) ,"オルペウスの後継者","冥界への扉の効率が5倍になる")},
		{110010, new AwakeClass(AWAKE_TYPE_INST,100,2,1331443278650*Math.Pow(13.6, 9) ,"冥界の大賢者","冥界への扉の効率が2倍になる")},
		{110011, new AwakeClass(AWAKE_TYPE_INST,150,2,1331443278650*Math.Pow(13.6, 10),"アンダーワールドの聖職者","冥界への扉の効率が2倍になる")},
		{110012, new AwakeClass(AWAKE_TYPE_INST,200,2,1331443278650*Math.Pow(13.6, 11),"冥府の召使い","冥界への扉の効率が2倍になる")},
		{110013, new AwakeClass(AWAKE_TYPE_INST,250,2,1331443278650*Math.Pow(13.6, 12),"冥界ホテルのVIP","冥界への扉の効率が2倍になる")},
		{110014, new AwakeClass(AWAKE_TYPE_INST,300,3,1331443278650*Math.Pow(13.6, 13),"冥王の友人","冥界への扉の効率が3倍になる")},
		{110015, new AwakeClass(AWAKE_TYPE_INST,400,3,1331443278650*Math.Pow(13.6, 14),"超高速エレベーターの使い手","冥界への扉の効率が3倍になる")},
		{110016, new AwakeClass(AWAKE_TYPE_INST,500,3,1331443278650*Math.Pow(13.6, 15),"地下10000階","冥界への扉の効率が3倍になる")},
		{110017, new AwakeClass(AWAKE_TYPE_INST,600,3,1331443278650*Math.Pow(13.6, 16),"悪魔とは違うんです","冥界への扉の効率が3倍になる")},
		{110018, new AwakeClass(AWAKE_TYPE_INST,700,5,1331443278650*Math.Pow(13.6, 17),"蝕まれた死神","冥界への扉の効率が5倍になる")},
		{120001, new AwakeClass(AWAKE_TYPE_INST,10,2, 19172783212560                 ,"霊峰を仰ぎ見る者","霊峰の雲海の効率が2倍になる")},
		{120002, new AwakeClass(AWAKE_TYPE_INST,20,2, 19172783212560*Math.Pow(13.6, 1) ,"霧に包まれる冒険者","霊峰の雲海の効率が2倍になる")},
		{120003, new AwakeClass(AWAKE_TYPE_INST,30,2, 19172783212560*Math.Pow(13.6, 2) ,"天空の夢を追いかける人","霊峰の雲海の効率が2倍になる")},
		{120004, new AwakeClass(AWAKE_TYPE_INST,40,2, 19172783212560*Math.Pow(13.6, 3) ,"神秘の山頂","霊峰の雲海の効率が2倍になる")},
		{120005, new AwakeClass(AWAKE_TYPE_INST,50,2, 19172783212560*Math.Pow(13.6, 4) ,"山脈を渡る者","霊峰の雲海の効率が2倍になる")},
		{120006, new AwakeClass(AWAKE_TYPE_INST,60,2, 19172783212560*Math.Pow(13.6, 5) ,"頂上に近づく者","霊峰の雲海の効率が2倍になる")},
		{120007, new AwakeClass(AWAKE_TYPE_INST,70,2, 19172783212560*Math.Pow(13.6, 6) ,"頂上に立つ者","霊峰の雲海の効率が2倍になる")},
		{120008, new AwakeClass(AWAKE_TYPE_INST,80,2, 19172783212560*Math.Pow(13.6, 7) ,"雲海を見下ろす者","霊峰の雲海の効率が2倍になる")},
		{120009, new AwakeClass(AWAKE_TYPE_INST,90,5, 19172783212560*Math.Pow(13.6, 8) ,"悠久の山脈","霊峰の雲海の効率が5倍になる")},
		{120010, new AwakeClass(AWAKE_TYPE_INST,100,2,19172783212560*Math.Pow(13.6, 9) ,"霊峰の宝物","霊峰の雲海の効率が2倍になる")},
		{120011, new AwakeClass(AWAKE_TYPE_INST,150,2,19172783212560*Math.Pow(13.6, 10),"霊峰を統べる者","霊峰の雲海の効率が2倍になる")},
		{120012, new AwakeClass(AWAKE_TYPE_INST,200,2,19172783212560*Math.Pow(13.6, 11),"空高く舞う者","霊峰の雲海の効率が2倍になる")},
		{120013, new AwakeClass(AWAKE_TYPE_INST,250,2,19172783212560*Math.Pow(13.6, 12),"そよ風を操る者","霊峰の雲海の効率が2倍になる")},
		{120014, new AwakeClass(AWAKE_TYPE_INST,300,3,19172783212560*Math.Pow(13.6, 13),"雲を越えて","霊峰の雲海の効率が3倍になる")},
		{120015, new AwakeClass(AWAKE_TYPE_INST,400,3,19172783212560*Math.Pow(13.6, 14),"霊峰の絶頂","霊峰の雲海の効率が3倍になる")},
		{120016, new AwakeClass(AWAKE_TYPE_INST,500,3,19172783212560*Math.Pow(13.6, 15),"空の彼方へ","霊峰の雲海の効率が3倍になる")},
		{120017, new AwakeClass(AWAKE_TYPE_INST,600,3,19172783212560*Math.Pow(13.6, 16),"雲の上の王者","霊峰の雲海の効率が3倍になる")},
		{120018, new AwakeClass(AWAKE_TYPE_INST,700,5,19172783212560*Math.Pow(13.6, 17),"大空の覇者","霊峰の雲海の効率が5倍になる")},
		{130001, new AwakeClass(AWAKE_TYPE_INST,10,2, 276088078260865                   ,"宇宙の冒険家","コスモスの船の効率が2倍になる")},
		{130002, new AwakeClass(AWAKE_TYPE_INST,20,2, 276088078260865*Math.Pow(13.6, 1) ,"惑星の探検家","コスモスの船の効率が2倍になる")},
		{130003, new AwakeClass(AWAKE_TYPE_INST,30,2, 276088078260865*Math.Pow(13.6, 2) ,"時間と空間の旅人","コスモスの船の効率が2倍になる")},
		{130004, new AwakeClass(AWAKE_TYPE_INST,40,2, 276088078260865*Math.Pow(13.6, 3) ,"星間航海士","コスモスの船の効率が2倍になる")},
		{130005, new AwakeClass(AWAKE_TYPE_INST,50,3, 276088078260865*Math.Pow(13.6, 4) ,"重力の踏破者","コスモスの船の効率が3倍になる")},
		{130006, new AwakeClass(AWAKE_TYPE_INST,60,2, 276088078260865*Math.Pow(13.6, 5) ,"エイリアンの通訳","コスモスの船の効率が2倍になる")},
		{130007, new AwakeClass(AWAKE_TYPE_INST,70,5, 276088078260865*Math.Pow(13.6, 6) ,"クエーサーの軌跡","コスモスの船の効率が5倍になる")},
		{130008, new AwakeClass(AWAKE_TYPE_INST,80,2, 276088078260865*Math.Pow(13.6, 7) ,"宇宙の彷徨者","コスモスの船の効率が2倍になる")},
		{130009, new AwakeClass(AWAKE_TYPE_INST,90,10,276088078260865*Math.Pow(13.6, 8) ,"超光速の航跡","コスモスの船の効率が10倍になる")},
		{130010, new AwakeClass(AWAKE_TYPE_INST,100,2,276088078260865*Math.Pow(13.6, 9) ,"ガラクタ収集家","コスモスの船の効率が2倍になる")},
		{130011, new AwakeClass(AWAKE_TYPE_INST,150,2,276088078260865*Math.Pow(13.6, 10),"銀河のカジノの常連客","コスモスの船の効率が2倍になる")},
		{130012, new AwakeClass(AWAKE_TYPE_INST,200,2,276088078260865*Math.Pow(13.6, 11),"宇宙の猫好き","コスモスの船の効率が2倍になる")},
		{130013, new AwakeClass(AWAKE_TYPE_INST,250,5,276088078260865*Math.Pow(13.6, 12),"銀河系一周旅行者","コスモスの船の効率が5倍になる")},
		{130014, new AwakeClass(AWAKE_TYPE_INST,300,2,276088078260865*Math.Pow(13.6, 13),"エイリアン語の翻訳者","コスモスの船の効率が2倍になる")},
		{130015, new AwakeClass(AWAKE_TYPE_INST,400,2,276088078260865*Math.Pow(13.6, 14),"天体観測マニア","コスモスの船の効率が2倍になる")},
		{130016, new AwakeClass(AWAKE_TYPE_INST,500,2,276088078260865*Math.Pow(13.6, 15),"銀河のアイドル","コスモスの船の効率が2倍になる")},
		{130017, new AwakeClass(AWAKE_TYPE_INST,600,2,276088078260865*Math.Pow(13.6, 16),"星座のマスター","コスモスの船の効率が2倍になる")},
		{130018, new AwakeClass(AWAKE_TYPE_INST,700,5,276088078260865*Math.Pow(13.6, 17),"宇宙のビーガン","コスモスの船の効率が5倍になる")},
		{140001, new AwakeClass(AWAKE_TYPE_INST,10,2, 3975670000000000                   ,"星の読み手","アースガルズの預言者の効率が2倍になる")},
		{140002, new AwakeClass(AWAKE_TYPE_INST,20,2, 3975670000000000*Math.Pow(13.6, 1) ,"知識の保持者","アースガルズの預言者の効率が2倍になる")},
		{140003, new AwakeClass(AWAKE_TYPE_INST,30,2, 3975670000000000*Math.Pow(13.6, 2) ,"未来を見通す者","アースガルズの預言者の効率が2倍になる")},
		{140004, new AwakeClass(AWAKE_TYPE_INST,40,2, 3975670000000000*Math.Pow(13.6, 3) ,"天啓を受ける者","アースガルズの預言者の効率が2倍になる")},
		{140005, new AwakeClass(AWAKE_TYPE_INST,50,3, 3975670000000000*Math.Pow(13.6, 4) ,"宇宙の謎を解く者","アースガルズの預言者の効率が3倍になる")},
		{140006, new AwakeClass(AWAKE_TYPE_INST,60,2, 3975670000000000*Math.Pow(13.6, 5) ,"神秘的な訪問者","アースガルズの預言者の効率が2倍になる")},
		{140007, new AwakeClass(AWAKE_TYPE_INST,70,2, 3975670000000000*Math.Pow(13.6, 6) ,"神々の声を聞く者","アースガルズの預言者の効率が2倍になる")},
		{140008, new AwakeClass(AWAKE_TYPE_INST,80,2, 3975670000000000*Math.Pow(13.6, 7) ,"霊的感受性を持つ者","アースガルズの預言者の効率が2倍になる")},
		{140009, new AwakeClass(AWAKE_TYPE_INST,90,2, 3975670000000000*Math.Pow(13.6, 8) ,"時空を超える者","アースガルズの預言者の効率が2倍になる")},
		{140010, new AwakeClass(AWAKE_TYPE_INST,100,5,3975670000000000*Math.Pow(13.6, 9) ,"知識と英知を結集する者","アースガルズの預言者の効率が5倍になる")},
		{140011, new AwakeClass(AWAKE_TYPE_INST,150,2,3975670000000000*Math.Pow(13.6, 10),"暗黒を照らす者","アースガルズの預言者の効率が2倍になる")},
		{140012, new AwakeClass(AWAKE_TYPE_INST,200,2,3975670000000000*Math.Pow(13.6, 11),"運命を変える者","アースガルズの預言者の効率が2倍になる")},
		{140013, new AwakeClass(AWAKE_TYPE_INST,250,5,3975670000000000*Math.Pow(13.6, 12),"幸運を招く者","アースガルズの預言者の効率が5倍になる")},
		{140014, new AwakeClass(AWAKE_TYPE_INST,300,3,3975670000000000*Math.Pow(13.6, 13),"神聖なる目的を果たす者","アースガルズの預言者の効率が3倍になる")},
		{140015, new AwakeClass(AWAKE_TYPE_INST,400,3,3975670000000000*Math.Pow(13.6, 14),"神話の力を司る者","アースガルズの預言者の効率が3倍になる")},
		{140016, new AwakeClass(AWAKE_TYPE_INST,500,3,3975670000000000*Math.Pow(13.6, 15),"神の導きに従う者","アースガルズの預言者の効率が3倍になる")},
		{140017, new AwakeClass(AWAKE_TYPE_INST,600,3,3975670000000000*Math.Pow(13.6, 16),"神々の知恵を宿す者","アースガルズの預言者の効率が3倍になる")},
		{140018, new AwakeClass(AWAKE_TYPE_INST,700,5,3975670000000000*Math.Pow(13.6, 17),"伝説の予言者","アースガルズの預言者の効率が5倍になる")},
		{150001, new AwakeClass(AWAKE_TYPE_INST,10,2, 57249500000000000                   ,"知識の獲得者","伝説の法典の効率が2倍になる")},
		{150002, new AwakeClass(AWAKE_TYPE_INST,20,2, 57249500000000000*Math.Pow(13.6, 1) ,"古代の遺産を探す者","伝説の法典の効率が2倍になる")},
		{150003, new AwakeClass(AWAKE_TYPE_INST,30,2, 57249500000000000*Math.Pow(13.6, 2) ,"法典の鑑定士","伝説の法典の効率が2倍になる")},
		{150004, new AwakeClass(AWAKE_TYPE_INST,40,2, 57249500000000000*Math.Pow(13.6, 3) ,"古代の聖なる書","伝説の法典の効率が2倍になる")},
		{150005, new AwakeClass(AWAKE_TYPE_INST,50,2, 57249500000000000*Math.Pow(13.6, 4) ,"知識の集積者","伝説の法典の効率が2倍になる")},
		{150006, new AwakeClass(AWAKE_TYPE_INST,60,2, 57249500000000000*Math.Pow(13.6, 5) ,"聖典の復元者","伝説の法典の効率が2倍になる")},
		{150007, new AwakeClass(AWAKE_TYPE_INST,70,2, 57249500000000000*Math.Pow(13.6, 6) ,"聖典の復元者","伝説の法典の効率が2倍になる")},
		{150008, new AwakeClass(AWAKE_TYPE_INST,80,2, 57249500000000000*Math.Pow(13.6, 7) ,"書物の番人","伝説の法典の効率が2倍になる")},
		{150009, new AwakeClass(AWAKE_TYPE_INST,90,5, 57249500000000000*Math.Pow(13.6, 8) ,"知識の泉","伝説の法典の効率が5倍になる")},
		{150010, new AwakeClass(AWAKE_TYPE_INST,100,2,57249500000000000*Math.Pow(13.6, 9) ,"伝説の図書館員","伝説の法典の効率が2倍になる")},
		{150011, new AwakeClass(AWAKE_TYPE_INST,150,2,57249500000000000*Math.Pow(13.6, 10),"聖域の探求者","伝説の法典の効率が2倍になる")},
		{150012, new AwakeClass(AWAKE_TYPE_INST,200,3,57249500000000000*Math.Pow(13.6, 11),"法典の黄金編集者","伝説の法典の効率が3倍になる")},
		{150013, new AwakeClass(AWAKE_TYPE_INST,250,2,57249500000000000*Math.Pow(13.6, 12),"神託の解読者","伝説の法典の効率が2倍になる")},
		{150014, new AwakeClass(AWAKE_TYPE_INST,300,3,57249500000000000*Math.Pow(13.6, 13),"魔法の再創造者","伝説の法典の効率が3倍になる")},
		{150015, new AwakeClass(AWAKE_TYPE_INST,400,3,57249500000000000*Math.Pow(13.6, 14),"早読み魔人","伝説の法典の効率が3倍になる")},
		{150016, new AwakeClass(AWAKE_TYPE_INST,500,3,57249500000000000*Math.Pow(13.6, 15),"ネタバレ注意報","伝説の法典の効率が3倍になる")},
		{150017, new AwakeClass(AWAKE_TYPE_INST,600,3,57249500000000000*Math.Pow(13.6, 16),"ページめくりの達人","伝説の法典の効率が3倍になる")},
		{150018, new AwakeClass(AWAKE_TYPE_INST,700,5,57249500000000000*Math.Pow(13.6, 17),"古代魔法の継承者","伝説の法典の効率が5倍になる")},
		{160001, new AwakeClass(AWAKE_TYPE_INST,10,2, 824395000000000000                   ,"光を遮るもの","神々の光芒の効率が2倍になる")},
		{160002, new AwakeClass(AWAKE_TYPE_INST,20,2, 824395000000000000*Math.Pow(13.6, 1) ,"光と闇のバランス","神々の光芒の効率が2倍になる")},
		{160003, new AwakeClass(AWAKE_TYPE_INST,30,2, 824395000000000000*Math.Pow(13.6, 2) ,"輝きに包まれる者","神々の光芒の効率が2倍になる")},
		{160004, new AwakeClass(AWAKE_TYPE_INST,40,2, 824395000000000000*Math.Pow(13.6, 3) ,"煌めく大地","神々の光芒の効率が2倍になる")},
		{160005, new AwakeClass(AWAKE_TYPE_INST,50,2, 824395000000000000*Math.Pow(13.6, 4) ,"輝かしい瞬間","神々の光芒の効率が2倍になる")},
		{160006, new AwakeClass(AWAKE_TYPE_INST,60,2, 824395000000000000*Math.Pow(13.6, 5) ,"光を纏う者","神々の光芒の効率が2倍になる")},
		{160007, new AwakeClass(AWAKE_TYPE_INST,70,2, 824395000000000000*Math.Pow(13.6, 6) ,"光の使徒","神々の光芒の効率が2倍になる")},
		{160008, new AwakeClass(AWAKE_TYPE_INST,80,2, 824395000000000000*Math.Pow(13.6, 7) ,"光輝く守護者","神々の光芒の効率が2倍になる")},
		{160009, new AwakeClass(AWAKE_TYPE_INST,90,5, 824395000000000000*Math.Pow(13.6, 8) ,"輝きを宿す者","神々の光芒の効率が5倍になる")},
		{160010, new AwakeClass(AWAKE_TYPE_INST,100,2,824395000000000000*Math.Pow(13.6, 9) ,"天上の眩しさ","神々の光芒の効率が2倍になる")},
		{160011, new AwakeClass(AWAKE_TYPE_INST,150,2,824395000000000000*Math.Pow(13.6, 10),"天空の迷宮探検家","神々の光芒の効率が2倍になる")},
		{160012, new AwakeClass(AWAKE_TYPE_INST,200,2,824395000000000000*Math.Pow(13.6, 11),"虹色の翼","神々の光芒の効率が2倍になる")},
		{160013, new AwakeClass(AWAKE_TYPE_INST,250,2,824395000000000000*Math.Pow(13.6, 12),"神のお気に入り","神々の光芒の効率が2倍になる")},
		{160014, new AwakeClass(AWAKE_TYPE_INST,300,3,824395000000000000*Math.Pow(13.6, 13),"超越者","神々の光芒の効率が3倍になる")},
		{160015, new AwakeClass(AWAKE_TYPE_INST,400,3,824395000000000000*Math.Pow(13.6, 14),"光の化身","神々の光芒の効率が3倍になる")},
		{160016, new AwakeClass(AWAKE_TYPE_INST,500,3,824395000000000000*Math.Pow(13.6, 15),"光の約束を果たす者","神々の光芒の効率が3倍になる")},
		{160017, new AwakeClass(AWAKE_TYPE_INST,600,3,824395000000000000*Math.Pow(13.6, 16),"光の信念を持つ者","神々の光芒の効率が3倍になる")},
		{160018, new AwakeClass(AWAKE_TYPE_INST,700,5,824395000000000000*Math.Pow(13.6, 17),"光栄なる使徒","神々の光芒の効率が5倍になる")},
		{170001, new AwakeClass(AWAKE_TYPE_INST,10,2, 11871300000000000000                   ,"悪魔の苦手な人","地獄浄化装置の効率が2倍になる")},
		{170002, new AwakeClass(AWAKE_TYPE_INST,20,2, 11871300000000000000*Math.Pow(13.6, 1) ,"初心者の浄化士","地獄浄化装置の効率が2倍になる")},
		{170003, new AwakeClass(AWAKE_TYPE_INST,30,2, 11871300000000000000*Math.Pow(13.6, 2) ,"罪の清算者","地獄浄化装置の効率が2倍になる")},
		{170004, new AwakeClass(AWAKE_TYPE_INST,40,2, 11871300000000000000*Math.Pow(13.6, 3) ,"罪の洗濯機","地獄浄化装置の効率が2倍になる")},
		{170005, new AwakeClass(AWAKE_TYPE_INST,50,2, 11871300000000000000*Math.Pow(13.6, 4) ,"悪霊を祓う者","地獄浄化装置の効率が2倍になる")},
		{170006, new AwakeClass(AWAKE_TYPE_INST,60,2, 11871300000000000000*Math.Pow(13.6, 5) ,"修羅場の整備士","地獄浄化装置の効率が2倍になる")},
		{170007, new AwakeClass(AWAKE_TYPE_INST,70,2, 11871300000000000000*Math.Pow(13.6, 6) ,"地獄の浄化者","地獄浄化装置の効率が2倍になる")},
		{170008, new AwakeClass(AWAKE_TYPE_INST,80,2, 11871300000000000000*Math.Pow(13.6, 7) ,"獄卒の救世主","地獄浄化装置の効率が2倍になる")},
		{170009, new AwakeClass(AWAKE_TYPE_INST,90,5, 11871300000000000000*Math.Pow(13.6, 8) ,"魔界の清掃スタッフ","地獄浄化装置の効率が5倍になる")},
		{170010, new AwakeClass(AWAKE_TYPE_INST,100,2,11871300000000000000*Math.Pow(13.6, 9) ,"悪魔退治のエキスパート","地獄浄化装置の効率が2倍になる")},
		{170011, new AwakeClass(AWAKE_TYPE_INST,150,2,11871300000000000000*Math.Pow(13.6, 10),"地獄の扉番長","地獄浄化装置の効率が2倍になる")},
		{170012, new AwakeClass(AWAKE_TYPE_INST,200,2,11871300000000000000*Math.Pow(13.6, 11),"魔物ハンター","地獄浄化装置の効率が2倍になる")},
		{170013, new AwakeClass(AWAKE_TYPE_INST,250,2,11871300000000000000*Math.Pow(13.6, 12),"魔物ハンター最高位","地獄浄化装置の効率が2倍になる")},
		{170014, new AwakeClass(AWAKE_TYPE_INST,300,3,11871300000000000000*Math.Pow(13.6, 13),"魔界のマリー・コンドー","地獄浄化装置の効率が3倍になる")},
		{170015, new AwakeClass(AWAKE_TYPE_INST,400,3,11871300000000000000*Math.Pow(13.6, 14),"サタンの散歩係","地獄浄化装置の効率が3倍になる")},
		{170016, new AwakeClass(AWAKE_TYPE_INST,500,3,11871300000000000000*Math.Pow(13.6, 15),"神の左手","地獄浄化装置の効率が3倍になる")},
		{170017, new AwakeClass(AWAKE_TYPE_INST,600,3,11871300000000000000*Math.Pow(13.6, 16),"悪魔の調教師","地獄浄化装置の効率が3倍になる")},
		{170018, new AwakeClass(AWAKE_TYPE_INST,700,5,11871300000000000000*Math.Pow(13.6, 17),"地獄の大掃除人","地獄浄化装置の効率が5倍になる")},
		{180001, new AwakeClass(AWAKE_TYPE_INST,10,2, double.Parse("170946500000000000000")                   ,"次元の探検家","外の世界のトモタチの効率が2倍になる")},
		{180002, new AwakeClass(AWAKE_TYPE_INST,20,2, double.Parse("170946500000000000000")*Math.Pow(13.6, 1) ,"銀河の旅人","外の世界のトモタチの効率が2倍になる")},
		{180003, new AwakeClass(AWAKE_TYPE_INST,30,2, double.Parse("170946500000000000000")*Math.Pow(13.6, 2) ,"遥かなる世界の言語マスター","外の世界のトモタチの効率が2倍になる")},
		{180004, new AwakeClass(AWAKE_TYPE_INST,40,2, double.Parse("170946500000000000000")*Math.Pow(13.6, 3) ,"世界を開拓する者","外の世界のトモタチの効率が2倍になる")},
		{180005, new AwakeClass(AWAKE_TYPE_INST,50,2, double.Parse("170946500000000000000")*Math.Pow(13.6, 4) ,"自然観察家","外の世界のトモタチの効率が2倍になる")},
		{180006, new AwakeClass(AWAKE_TYPE_INST,60,2, double.Parse("170946500000000000000")*Math.Pow(13.6, 5) ,"法則の研究者","外の世界のトモタチの効率が2倍になる")},
		{180007, new AwakeClass(AWAKE_TYPE_INST,70,2, double.Parse("170946500000000000000")*Math.Pow(13.6, 6) ,"次元を超える者","外の世界のトモタチの効率が2倍になる")},
		{180008, new AwakeClass(AWAKE_TYPE_INST,80,2, double.Parse("170946500000000000000")*Math.Pow(13.6, 7) ,"生命体の研究者","外の世界のトモタチの効率が2倍になる")},
		{180009, new AwakeClass(AWAKE_TYPE_INST,90,5, double.Parse("170946500000000000000")*Math.Pow(13.6, 8) ,"星間航行士","外の世界のトモタチの効率が5倍になる")},
		{180010, new AwakeClass(AWAKE_TYPE_INST,100,2,double.Parse("170946500000000000000")*Math.Pow(13.6, 9) ,"文明の専門家","外の世界のトモタチの効率が2倍になる")},
		{180011, new AwakeClass(AWAKE_TYPE_INST,150,2,double.Parse("170946500000000000000")*Math.Pow(13.6, 10),"生命進化論","外の世界のトモタチの効率が2倍になる")},
		{180012, new AwakeClass(AWAKE_TYPE_INST,200,2,double.Parse("170946500000000000000")*Math.Pow(13.6, 11),"星間交流使節","外の世界のトモタチの効率が2倍になる")},
		{180013, new AwakeClass(AWAKE_TYPE_INST,250,2,double.Parse("170946500000000000000")*Math.Pow(13.6, 12),"次元の歴史","外の世界のトモタチの効率が2倍になる")},
		{180014, new AwakeClass(AWAKE_TYPE_INST,300,3,double.Parse("170946500000000000000")*Math.Pow(13.6, 13),"宇宙ハッカー","外の世界のトモタチの効率が3倍になる")},
		{180015, new AwakeClass(AWAKE_TYPE_INST,400,3,double.Parse("170946500000000000000")*Math.Pow(13.6, 14),"文明の発祥地","外の世界のトモタチの効率が3倍になる")},
		{180016, new AwakeClass(AWAKE_TYPE_INST,500,3,double.Parse("170946500000000000000")*Math.Pow(13.6, 15),"歴史の石壁","外の世界のトモタチの効率が3倍になる")},
		{180017, new AwakeClass(AWAKE_TYPE_INST,600,3,double.Parse("170946500000000000000")*Math.Pow(13.6, 16),"エメラルド・タブレットの解読者","外の世界のトモタチの効率が3倍になる")},
		{180018, new AwakeClass(AWAKE_TYPE_INST,700,5,double.Parse("170946500000000000000")*Math.Pow(13.6, 17),"異星人コミュニケーター","外の世界のトモタチの効率が5倍になる")},
		{190001, new AwakeClass(AWAKE_TYPE_INST,10,2, double.Parse("2461630000000000000000")                   ,"ドルイドの弟子","神秘の渦巻の効率が2倍になる")},
		{190002, new AwakeClass(AWAKE_TYPE_INST,20,2, double.Parse("2461630000000000000000")*Math.Pow(13.6, 1) ,"シーウィッチの見習い","神秘の渦巻の効率が2倍になる")},
		{190003, new AwakeClass(AWAKE_TYPE_INST,30,2, double.Parse("2461630000000000000000")*Math.Pow(13.6, 2) ,"シーリーの保護者","神秘の渦巻の効率が2倍になる")},
		{190004, new AwakeClass(AWAKE_TYPE_INST,40,2, double.Parse("2461630000000000000000")*Math.Pow(13.6, 3) ,"タラの丘の守護者","神秘の渦巻の効率が2倍になる")},
		{190005, new AwakeClass(AWAKE_TYPE_INST,50,3, double.Parse("2461630000000000000000")*Math.Pow(13.6, 4) ,"シド・フィンナの探求者","神秘の渦巻の効率が3倍になる")},
		{190006, new AwakeClass(AWAKE_TYPE_INST,60,2, double.Parse("2461630000000000000000")*Math.Pow(13.6, 5) ,"シャーマニックな旅人","神秘の渦巻の効率が2倍になる")},
		{190007, new AwakeClass(AWAKE_TYPE_INST,70,5, double.Parse("2461630000000000000000")*Math.Pow(13.6, 6) ,"ガルヌスの召使い","神秘の渦巻の効率が5倍になる")},
		{190008, new AwakeClass(AWAKE_TYPE_INST,80,2, double.Parse("2461630000000000000000")*Math.Pow(13.6, 7) ,"タルテュの信奉者","神秘の渦巻の効率が2倍になる")},
		{190009, new AwakeClass(AWAKE_TYPE_INST,90,10,double.Parse("2461630000000000000000")*Math.Pow(13.6, 8) ,"モリガンの使徒","神秘の渦巻の効率が10倍になる")},
		{190010, new AwakeClass(AWAKE_TYPE_INST,100,2,double.Parse("2461630000000000000000")*Math.Pow(13.6, 9) ,"ルーの戦士","神秘の渦巻の効率が2倍になる")},
		{190011, new AwakeClass(AWAKE_TYPE_INST,150,2,double.Parse("2461630000000000000000")*Math.Pow(13.6, 10),"ニアドの神託者","神秘の渦巻の効率が2倍になる")},
		{190012, new AwakeClass(AWAKE_TYPE_INST,200,2,double.Parse("2461630000000000000000")*Math.Pow(13.6, 11),"ダナの信奉者","神秘の渦巻の効率が2倍になる")},
		{190013, new AwakeClass(AWAKE_TYPE_INST,250,5,double.Parse("2461630000000000000000")*Math.Pow(13.6, 12),"パーガンの呪術師","神秘の渦巻の効率が5倍になる")},
		{190014, new AwakeClass(AWAKE_TYPE_INST,300,2,double.Parse("2461630000000000000000")*Math.Pow(13.6, 13),"グウィネットの勇者","神秘の渦巻の効率が2倍になる")},
		{190015, new AwakeClass(AWAKE_TYPE_INST,400,2,double.Parse("2461630000000000000000")*Math.Pow(13.6, 14),"アビソンの探究者","神秘の渦巻の効率が2倍になる")},
		{190016, new AwakeClass(AWAKE_TYPE_INST,500,2,double.Parse("2461630000000000000000")*Math.Pow(13.6, 15),"聖なる木の守り手","神秘の渦巻の効率が2倍になる")},
		{190017, new AwakeClass(AWAKE_TYPE_INST,600,2,double.Parse("2461630000000000000000")*Math.Pow(13.6, 16),"大地と繋がる者","神秘の渦巻の効率が2倍になる")},
		{190018, new AwakeClass(AWAKE_TYPE_INST,700,5,double.Parse("2461630000000000000000")*Math.Pow(13.6, 17),"天界と地界の調停者","神秘の渦巻の効率が5倍になる")},
		{200001, new AwakeClass(AWAKE_TYPE_INST,10,2, double.Parse("35447450000000000000000")                   ,"時空の旅人","原初の特異点の効率が2倍になる")},
		{200002, new AwakeClass(AWAKE_TYPE_INST,20,2, double.Parse("35447450000000000000000")*Math.Pow(13.6, 1) ,"特異な存在の観察者","原初の特異点の効率が2倍になる")},
		{200003, new AwakeClass(AWAKE_TYPE_INST,30,2, double.Parse("35447450000000000000000")*Math.Pow(13.6, 2) ,"特異点の維持者","原初の特異点の効率が2倍になる")},
		{200004, new AwakeClass(AWAKE_TYPE_INST,40,2, double.Parse("35447450000000000000000")*Math.Pow(13.6, 3) ,"時間の継承者","原初の特異点の効率が2倍になる")},
		{200005, new AwakeClass(AWAKE_TYPE_INST,50,3, double.Parse("35447450000000000000000")*Math.Pow(13.6, 4) ,"運命を変える者","原初の特異点の効率が3倍になる")},
		{200006, new AwakeClass(AWAKE_TYPE_INST,60,2, double.Parse("35447450000000000000000")*Math.Pow(13.6, 5) ,"可能性の追求者","原初の特異点の効率が2倍になる")},
		{200007, new AwakeClass(AWAKE_TYPE_INST,70,2, double.Parse("35447450000000000000000")*Math.Pow(13.6, 6) ,"始まりの証人","原初の特異点の効率が2倍になる")},
		{200008, new AwakeClass(AWAKE_TYPE_INST,80,2, double.Parse("35447450000000000000000")*Math.Pow(13.6, 7) ,"特異点の解析者","原初の特異点の効率が2倍になる")},
		{200009, new AwakeClass(AWAKE_TYPE_INST,90,2, double.Parse("35447450000000000000000")*Math.Pow(13.6, 8) ,"過去と未来の観察者","原初の特異点の効率が2倍になる")},
		{200010, new AwakeClass(AWAKE_TYPE_INST,100,5,double.Parse("35447450000000000000000")*Math.Pow(13.6, 9) ,"真理の追求者","原初の特異点の効率が5倍になる")},
		{200011, new AwakeClass(AWAKE_TYPE_INST,150,2,double.Parse("35447450000000000000000")*Math.Pow(13.6, 10),"超次元の意識体","原初の特異点の効率が2倍になる")},
		{200012, new AwakeClass(AWAKE_TYPE_INST,200,2,double.Parse("35447450000000000000000")*Math.Pow(13.6, 11),"真実を知る者","原初の特異点の効率が2倍になる")},
		{200013, new AwakeClass(AWAKE_TYPE_INST,250,5,double.Parse("35447450000000000000000")*Math.Pow(13.6, 12),"始まりの目撃者","原初の特異点の効率が5倍になる")},
		{200014, new AwakeClass(AWAKE_TYPE_INST,300,3,double.Parse("35447450000000000000000")*Math.Pow(13.6, 13),"真理の為に","原初の特異点の効率が3倍になる")},
		{200015, new AwakeClass(AWAKE_TYPE_INST,400,3,double.Parse("35447450000000000000000")*Math.Pow(13.6, 14),"創造の源泉","原初の特異点の効率が3倍になる")},
		{200016, new AwakeClass(AWAKE_TYPE_INST,500,3,double.Parse("35447450000000000000000")*Math.Pow(13.6, 15),"原初存在の交渉者","原初の特異点の効率が3倍になる")},
		{200017, new AwakeClass(AWAKE_TYPE_INST,600,3,double.Parse("35447450000000000000000")*Math.Pow(13.6, 16),"特異点の統括者","原初の特異点の効率が3倍になる")},
		{200018, new AwakeClass(AWAKE_TYPE_INST,700,5,double.Parse("35447450000000000000000")*Math.Pow(13.6, 17),"全知全能のオリジン","原初の特異点の効率が5倍になる")},


};

	public static int mYggdrasilSkillNum = 68;
	public static Dictionary<int, YggdrasilSkillClass> mYggdrasilSkill = new Dictionary<int, YggdrasilSkillClass>() {
		{0,     new YggdrasilSkillClass(0,       0,                 "指定ミス",                         "覚醒指定ミス", 0)},
{1, new YggdrasilSkillClass(1, 1, "ウロボロス", "獲得した「ユグドラシルの種」1に付き、ステラの総生産率+1", 2)},
{2, new YggdrasilSkillClass(1, 5, "ヨルムンガンドLv1", "ヨルムンガンドが現れ、ステラの生産率を+1％", 2)},
{3, new YggdrasilSkillClass(2, 750, "ヨルムンガンドLv2", "ステラの生産率をさらに+2％", 2)},
{4, new YggdrasilSkillClass(3, 48000, "ヨルムンガンドLv3", "ステラの生産率をさらに+3％", 2)},
{5, new YggdrasilSkillClass(4, 8000000, "ヨルムンガンドLv4", "ステラの生産率をさらに+4％", 2)},
{6, new YggdrasilSkillClass(5, 800000000, "ヨルムンガンドLv5", "ステラの生産率をさらに+5％", 2)},
{7, new YggdrasilSkillClass(10, 10, "労働者", "クリックの効果を+10％", 0)},
{8, new YggdrasilSkillClass(10, 50, "予言者", "クリックの効果をさらに+10％", 0)},
{9, new YggdrasilSkillClass(20, 200, "創造主", "クリックの効果をさらに+20％", 0)},
{10, new YggdrasilSkillClass(-3, 1500, "ドヴェルグの雑貨屋", "聖霊購入時の値段-3％", 4)},
{11, new YggdrasilSkillClass(-3, 1500000, "ドヴェルグの商店", "聖霊購入時の値段-3％", 4)},
{12, new YggdrasilSkillClass(-4, 15000000, "ドヴェルグの商人ギルド", "聖霊購入時の値段-4％", 4)},
{13, new YggdrasilSkillClass(5, 33, "流れ星", "ステラの欠片の出現率を+5％", 12)},
{14, new YggdrasilSkillClass(5, 33333, "流れ惑星", "ステラの欠片の出現率をさらに+5％", 12)},
{15, new YggdrasilSkillClass(5, 333333333, "青い鴉", "ステラの欠片の出現率をさらに+5％", 12)},
{16, new YggdrasilSkillClass(5, 333, "渡り橋", "ビフレストの出現を率+5％", 9)},
{17, new YggdrasilSkillClass(5, 3333333, "世界の果てへ", "ビフレストの出現率をさらに+5％", 9)},
{18, new YggdrasilSkillClass(5, 333333333, "赤い鴉", "ビフレストの出現率をさらに+5％", 9)},
{19, new YggdrasilSkillClass(60*10, 66666, "イマは何時？", "超越ワープの時間を+10分", 7)},
{20, new YggdrasilSkillClass(20, 6666666, "これで間に合う？", "特殊イベントの継続時間を+20％", 7)},
{21, new YggdrasilSkillClass(1, 1500000, "手を貸そう", "クリック時のステラ生産回数を+1", 0)},
{22, new YggdrasilSkillClass(1, 23000000000, "三本目だ", "クリック時のステラ生産回数を+1", 0)},
{23, new YggdrasilSkillClass(50, 550000, "夜はこれから", "ハティが現れる、18時から5時の間の「ワタリカラス」の生産率+50％", 2)},
{24, new YggdrasilSkillClass(50, 5500000000, "しごとの始まり", "スコルが現れる、6時から17時の間の「世界樹の葉」の生産率+50％", 2)},
{25, new YggdrasilSkillClass(10, 5500000000000, "二対の狼", "すべての聖霊の生産率を+10％", 1)},
{26, new YggdrasilSkillClass(0.1, 800, "繁栄の証", "聖霊が100レベルに付き総生産率+0.1％", 1)},
{27, new YggdrasilSkillClass(500, 333, "神の力", "「グングニル」が使えるようになる、30秒の間、ステラの生産率+500％", 11)},
{28, new YggdrasilSkillClass(-5, 500000, "落下する速度", "「グングニル」の使用間隔を-5％", 11)},
{29, new YggdrasilSkillClass(-5, 15000000, "落下する速度+", "「グングニル」の使用間隔をさらに-5％", 11)},
{30, new YggdrasilSkillClass(-10, 2000000000000, "落下する速度++", "「グングニル」の使用間隔をさらに-10％", 11)},
{31, new YggdrasilSkillClass(5, 500000, "光る長さ", "「グングニル」の継続時間+5秒", 11)},
{32, new YggdrasilSkillClass(5, 15000000, "光る長さ+", "「グングニル」の継続時間さらに+5秒", 11)},
{33, new YggdrasilSkillClass(10, 2000000000000, "光る長さ++", "「グングニル」の継続時間さらに+10秒", 11)},
{34, new YggdrasilSkillClass(10, 17000000000000, "光る長さ+++", "「グングニル」の継続時間さらに+10秒", 11)},
{35, new YggdrasilSkillClass(100, 2500000, "倍率こそスピード", "「グングニル」発動時、ステラの生産率を+100％", 11)},
{36, new YggdrasilSkillClass(100, 5000000000, "倍率こそスピード+", "「グングニル」発動時、ステラの生産率をさらに+100％", 11)},
{37, new YggdrasilSkillClass(100, 100000000000000, "倍率こそスピード++", "「グングニル」発動時、ステラの生産率をさらに+100％", 11)},
{38, new YggdrasilSkillClass(60*60, 333, "頭が復活した！", "「ミーミルの知恵」が使えるようになる、1分間の間、クリックが自動で行う", 3)},
{39, new YggdrasilSkillClass(15, 1500000, "知識こそバワー", "「ミーミルの知恵」の継続時間を＋15秒", 3)},
{40, new YggdrasilSkillClass(15, 200000000, "知識こそバワー+", "「ミーミルの知恵」の継続時間をさらに＋15秒", 3)},
{41, new YggdrasilSkillClass(30, 7000000000000, "知識こそバワー+", "「ミーミルの知恵」の継続時間をさらに＋30秒", 3)},
{42, new YggdrasilSkillClass(60*60*120, 999999, "肥料購入", "生命の実が2時間早く成熟する", 8)},
{43, new YggdrasilSkillClass(60*60*120, 99999999999999, "栄養素", "生命の実がさらに2時間早く成熟する", 8)},
{44, new YggdrasilSkillClass(60*60*2, 5, "木の壺", "ゲームが閉じている間、合計2時間ステラを生産し続ける", 6)},
{45, new YggdrasilSkillClass(60*60*2, 10, "銅の壺", "ゲームを閉じている間、ステラの生産時間が2倍になる　合計4時", 6)},
{46, new YggdrasilSkillClass(60*60*4, 130, "銀の壺", "ゲームを閉じている間、ステラの生産時間が2倍になる　合計8時", 6)},
{47, new YggdrasilSkillClass(60*60*8, 1400, "金の壺", "ゲームを閉じている間、ステラの生産時間が2倍になる　合計16時", 6)},
{48, new YggdrasilSkillClass(60*60*16, 15000, "白金の壺", "ゲームを閉じている間、ステラの生産時間が2倍になる　合計32時", 6)},
{49, new YggdrasilSkillClass(1, 9999999, "現れる悪狼", "フェンリル現れる、聖霊が100レベルに付き総生産率+1％", 2)},
{50, new YggdrasilSkillClass(1, 15000000, "誰の手？", "クリック時のステラ生産回数を+1", 0)},
{51, new YggdrasilSkillClass(15, 1000000, "ギャァァァ", "タングリスニが現れる、ステラの欠片とビフレストの欠片の表示時間を+15％", 2)},
{52, new YggdrasilSkillClass(15, 15000000, "グヤァァァァ", "タングニョーストが現れる、ステラの欠片とビフレストの欠片の表示時間を更に+15％", 2)},
{53, new YggdrasilSkillClass(20, 600000000, "見逃さない注意力", "ステラの欠片とビフレストの欠片の表示時間を更に+20％", 12)},
{54, new YggdrasilSkillClass(2, 65, "塵も積もれば山となる", "聖霊の生産速度2％早くなる　※（9.8秒）", 13)},
{55, new YggdrasilSkillClass(2, 6500, "塵も積もれば山となる+", "聖霊の生産速度更に2％早くなる※（9.6秒）", 13)},
{56, new YggdrasilSkillClass(2, 650000, "塵も積もれば山となる++", "聖霊の生産速度更に2％早くなる※（9.4秒）", 13)},
{57, new YggdrasilSkillClass(2, 6500000, "塵も積もれば山となる+++", "聖霊の生産速度更に2％早くなる※（9.2秒）", 13)},
{58, new YggdrasilSkillClass(2, 65000000, "塵も積もれば山となる++++", "聖霊の生産速度更に2％早くなる※（9秒）", 13)},
{59, new YggdrasilSkillClass(2, 650000000, "塵も積もれば山となる+++++", "聖霊の生産速度更に2％早くなる※（8.8秒）", 13)},
{60, new YggdrasilSkillClass(2, 6500000000, "塵も積もれば山となる++++++", "聖霊の生産速度更に2％早くなる※（8.6秒）", 13)},
{61, new YggdrasilSkillClass(2, 65000000000, "塵も積もれば山となる+++++++", "聖霊の生産速度更に2％早くなる※（8.4秒）", 13)},
{62, new YggdrasilSkillClass(2, 650000000000, "塵も積もれば山となる++++++++", "聖霊の生産速度更に2％早くなる※（8.2秒）", 13)},
{63, new YggdrasilSkillClass(2, 6500000000000, "石の上にも三年", "聖霊の生産速度更に2％早くなる※（8秒）", 13)},
{64, new YggdrasilSkillClass(-5, 1500000000, "商人法則", "聖霊購入時の値段-5％", 5)},
{65, new YggdrasilSkillClass(-5, 150000000000, "値引きの達人", "聖霊購入時の値段-5％", 5)},
{66, new YggdrasilSkillClass(-10, 50000, "知識の宝物庫", "「ミーミルの知恵」の使用間隔を-10％", 3)},
{67, new YggdrasilSkillClass(-10, 5000000, "知識の宝物庫+", "「ミーミルの知恵」の使用間隔をさらに-10％", 3)},
{68, new YggdrasilSkillClass(-20, 50000000000, "知識の宝物庫++", "「ミーミルの知恵」の使用間隔をさらに-20％", 3)},
	};

	public static Dictionary<int, RecordClass> mRecord = new Dictionary<int, RecordClass>() {
{1, new RecordClass("催眠日数")},
{2, new RecordClass("催眠度")},
{3, new RecordClass("奉仕回数")},
{4, new RecordClass("経験人数")},
{5, new RecordClass("自慰回数")},
{6, new RecordClass("絶頂回数")},
{7, new RecordClass("膣内射精回数")},
{8, new RecordClass("精飲回数")},
{9, new RecordClass("潮吹き回数")},
{10, new RecordClass("総精液量")},
{11, new RecordClass("膣内射精量")},
{12, new RecordClass("精飲量")},
{13, new RecordClass("潮吹き量")},
{14, new RecordClass("総プレイ時間")},
{15, new RecordClass("総クリック数")},
{16, new RecordClass("フィーバー発生回数")},
{17, new RecordClass("総生産催眠P")},
{18, new RecordClass("消費催眠P")},
{19, new RecordClass("累計アイテム強化")},
//{20, new RecordClass("累計アイテムレベル")},
	};






	public const int ACHIVEMENT_TYPE_CLICK = 1;
	public const int ACHIVEMENT_TYPE_POINT = 2;
	public const int ACHIVEMENT_TYPE_PPS = 3;
	public const int ACHIVEMENT_TYPE_INST_LV = 4;
	public const int ACHIVEMENT_TYPE_ALL_TIME = 5;
	public const int ACHIVEMENT_TYPE_TIME = 6;
	public const int ACHIVEMENT_TYPE_FRAGMENT = 7;
	public const int ACHIVEMENT_TYPE_BEFOREST = 8;
	public const int ACHIVEMENT_TYPE_FRUIT = 9;
	public const int ACHIVEMENT_TYPE_RAGNAROK = 10;
	public static List<int> mAchivement_Click = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
	public static List<int> mAchivement_Point = new List<int>() { 101, 102, 103, 104, 105, 106, 107 };
	public static List<int> mAchivement_PPS = new List<int>() { 201, 202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 214, 215 };
	public static List<int> mAchivement_InstLv = new List<int>() { 301, 302, 303, 304, 305, 306, 307, 308, 309, 310, 311, 312, 313, 314, 315, 316, 317 };
	public static List<int> mAchivement_AllTime = new List<int>() { 401, 402, 403, 404, 405, 406, 407, 408, 409, 410 };
	public static List<int> mAchivement_Time = new List<int>() { 501, 502, 503, 504, 505, 506, 507, 508, 509, 510, 511, 512 };
	public static List<int> mAchivement_Fragment = new List<int>() { 601, 602, 603, 604, 605, 606, 607 };
	public static List<int> mAchivement_Beforest = new List<int>() { 701, 702, 703, 704, 705, 706, 707 };
	public static List<int> mAchivement_Fruit = new List<int>() { 801, 802, 803, 804, 805, 806, 807, 808, 809, 810 };
	public static List<int> mAchivement_Ragnarok = new List<int>() { 901, 902, 903, 904, 905, 906, 907 };

	public static Dictionary<int, AchivementClass> mAchivementClick = new Dictionary<int, AchivementClass>() {
		{1,  new AchivementClass("AC_CLICK_01" , ACHIVEMENT_TYPE_CLICK, 1                     )},
		{2,  new AchivementClass("AC_CLICK_02" , ACHIVEMENT_TYPE_CLICK, 10                    )},
		{3,  new AchivementClass("AC_CLICK_03" , ACHIVEMENT_TYPE_CLICK, 100                   )},
		{4,  new AchivementClass("AC_CLICK_04" , ACHIVEMENT_TYPE_CLICK, 1000                  )},
		{5,  new AchivementClass("AC_CLICK_05" , ACHIVEMENT_TYPE_CLICK, 10000                 )},
		{6,  new AchivementClass("AC_CLICK_06" , ACHIVEMENT_TYPE_CLICK, 100000                )},
		{7,  new AchivementClass("AC_CLICK_07" , ACHIVEMENT_TYPE_CLICK, 1000000               )},
		{8,  new AchivementClass("AC_CLICK_08" , ACHIVEMENT_TYPE_CLICK, 10000000              )},
		{9,  new AchivementClass("AC_CLICK_09" , ACHIVEMENT_TYPE_CLICK, 100000000             )},
		{10, new AchivementClass("AC_CLICK_10" , ACHIVEMENT_TYPE_CLICK, 1000000000            )},
		{11, new AchivementClass("AC_CLICK_11" , ACHIVEMENT_TYPE_CLICK, 10000000000           )},
		{12, new AchivementClass("AC_CLICK_12" , ACHIVEMENT_TYPE_CLICK, 100000000000          )},

		{101, new AchivementClass("AC_STELA_PRODUCT_01" , ACHIVEMENT_TYPE_POINT, 10                  )},
		{102, new AchivementClass("AC_STELA_PRODUCT_02" , ACHIVEMENT_TYPE_POINT, 1000                )},
		{103, new AchivementClass("AC_STELA_PRODUCT_03" , ACHIVEMENT_TYPE_POINT, 1000000             )},
		{104, new AchivementClass("AC_STELA_PRODUCT_04" , ACHIVEMENT_TYPE_POINT, 1000000000          )},
		{105, new AchivementClass("AC_STELA_PRODUCT_05" , ACHIVEMENT_TYPE_POINT, 1000000000000       )},
		{106, new AchivementClass("AC_STELA_PRODUCT_06" , ACHIVEMENT_TYPE_POINT, 1000000000000000    )},
		{107, new AchivementClass("AC_STELA_PRODUCT_07" , ACHIVEMENT_TYPE_POINT, 1000000000000000000 )},

		{201, new AchivementClass("AC_PRODUCT_SEC_01" , ACHIVEMENT_TYPE_PPS, 1               )},
		{202, new AchivementClass("AC_PRODUCT_SEC_02" , ACHIVEMENT_TYPE_PPS, 10              )},
		{203, new AchivementClass("AC_PRODUCT_SEC_03" , ACHIVEMENT_TYPE_PPS, 100             )},
		{204, new AchivementClass("AC_PRODUCT_SEC_04" , ACHIVEMENT_TYPE_PPS, 1000            )},
		{205, new AchivementClass("AC_PRODUCT_SEC_05" , ACHIVEMENT_TYPE_PPS, 10000           )},
		{206, new AchivementClass("AC_PRODUCT_SEC_06" , ACHIVEMENT_TYPE_PPS, 100000          )},
		{207, new AchivementClass("AC_PRODUCT_SEC_07" , ACHIVEMENT_TYPE_PPS, 1000000         )},
		{208, new AchivementClass("AC_PRODUCT_SEC_08" , ACHIVEMENT_TYPE_PPS, 10000000        )},
		{209, new AchivementClass("AC_PRODUCT_SEC_09" , ACHIVEMENT_TYPE_PPS, 100000000       )},
		{210, new AchivementClass("AC_PRODUCT_SEC_10" , ACHIVEMENT_TYPE_PPS, 1000000000      )},
		{211, new AchivementClass("AC_PRODUCT_SEC_11" , ACHIVEMENT_TYPE_PPS, 10000000000     )},
		{212, new AchivementClass("AC_PRODUCT_SEC_12" , ACHIVEMENT_TYPE_PPS, 100000000000    )},
		{213, new AchivementClass("AC_PRODUCT_SEC_13" , ACHIVEMENT_TYPE_PPS, 1000000000000   )},
		{214, new AchivementClass("AC_PRODUCT_SEC_14" , ACHIVEMENT_TYPE_PPS, 10000000000000  )},
		{215, new AchivementClass("AC_PRODUCT_SEC_15" , ACHIVEMENT_TYPE_PPS, 100000000000000 )},

		{301, new AchivementClass("AC_INSTI_LV_01" , ACHIVEMENT_TYPE_INST_LV, 50   )},
		{302, new AchivementClass("AC_INSTI_LV_02" , ACHIVEMENT_TYPE_INST_LV, 100  )},
		{303, new AchivementClass("AC_INSTI_LV_03" , ACHIVEMENT_TYPE_INST_LV, 150  )},
		{304, new AchivementClass("AC_INSTI_LV_04" , ACHIVEMENT_TYPE_INST_LV, 200  )},
		{305, new AchivementClass("AC_INSTI_LV_05" , ACHIVEMENT_TYPE_INST_LV, 250  )},
		{306, new AchivementClass("AC_INSTI_LV_06" , ACHIVEMENT_TYPE_INST_LV, 300  )},
		{307, new AchivementClass("AC_INSTI_LV_07" , ACHIVEMENT_TYPE_INST_LV, 400  )},
		{308, new AchivementClass("AC_INSTI_LV_08" , ACHIVEMENT_TYPE_INST_LV, 500  )},
		{309, new AchivementClass("AC_INSTI_LV_09" , ACHIVEMENT_TYPE_INST_LV, 1000 )},
		{310, new AchivementClass("AC_INSTI_LV_10" , ACHIVEMENT_TYPE_INST_LV, 1500 )},
		{311, new AchivementClass("AC_INSTI_LV_11" , ACHIVEMENT_TYPE_INST_LV, 2000 )},
		{312, new AchivementClass("AC_INSTI_LV_12" , ACHIVEMENT_TYPE_INST_LV, 2500 )},
		{313, new AchivementClass("AC_INSTI_LV_13" , ACHIVEMENT_TYPE_INST_LV, 3000 )},
		{314, new AchivementClass("AC_INSTI_LV_14" , ACHIVEMENT_TYPE_INST_LV, 3500 )},
		{315, new AchivementClass("AC_INSTI_LV_15" , ACHIVEMENT_TYPE_INST_LV, 4000 )},
		{316, new AchivementClass("AC_INSTI_LV_16" , ACHIVEMENT_TYPE_INST_LV, 4500 )},
		{317, new AchivementClass("AC_INSTI_LV_17" , ACHIVEMENT_TYPE_INST_LV, 5000 )},

		{401, new AchivementClass("AC_ALL_PLAY_TIME_01" , ACHIVEMENT_TYPE_ALL_TIME, 1   )},
		{402, new AchivementClass("AC_ALL_PLAY_TIME_02" , ACHIVEMENT_TYPE_ALL_TIME, 5   )},
		{403, new AchivementClass("AC_ALL_PLAY_TIME_03" , ACHIVEMENT_TYPE_ALL_TIME, 10  )},
		{404, new AchivementClass("AC_ALL_PLAY_TIME_04" , ACHIVEMENT_TYPE_ALL_TIME, 24  )},
		{405, new AchivementClass("AC_ALL_PLAY_TIME_05" , ACHIVEMENT_TYPE_ALL_TIME, 48  )},
		{406, new AchivementClass("AC_ALL_PLAY_TIME_06" , ACHIVEMENT_TYPE_ALL_TIME, 100 )},
		{407, new AchivementClass("AC_ALL_PLAY_TIME_07" , ACHIVEMENT_TYPE_ALL_TIME, 200 )},
		{408, new AchivementClass("AC_ALL_PLAY_TIME_08" , ACHIVEMENT_TYPE_ALL_TIME, 300 )},
		{409, new AchivementClass("AC_ALL_PLAY_TIME_09" , ACHIVEMENT_TYPE_ALL_TIME, 400 )},
		{410, new AchivementClass("AC_ALL_PLAY_TIME_10" , ACHIVEMENT_TYPE_ALL_TIME, 500 )},

		{501, new AchivementClass("AC_ONE_PLAY_TIME_01" , ACHIVEMENT_TYPE_TIME, 0.5 )},
		{502, new AchivementClass("AC_ONE_PLAY_TIME_02" , ACHIVEMENT_TYPE_TIME, 2   )},
		{503, new AchivementClass("AC_ONE_PLAY_TIME_03" , ACHIVEMENT_TYPE_TIME, 3   )},
		{504, new AchivementClass("AC_ONE_PLAY_TIME_04" , ACHIVEMENT_TYPE_TIME, 4   )},
		{505, new AchivementClass("AC_ONE_PLAY_TIME_05" , ACHIVEMENT_TYPE_TIME, 12  )},
		{506, new AchivementClass("AC_ONE_PLAY_TIME_06" , ACHIVEMENT_TYPE_TIME, 24  )},
		{507, new AchivementClass("AC_ONE_PLAY_TIME_07" , ACHIVEMENT_TYPE_TIME, 48  )},
		{508, new AchivementClass("AC_ONE_PLAY_TIME_08" , ACHIVEMENT_TYPE_TIME, 72  )},
		{509, new AchivementClass("AC_ONE_PLAY_TIME_09" , ACHIVEMENT_TYPE_TIME, 96  )},
		{510, new AchivementClass("AC_ONE_PLAY_TIME_10" , ACHIVEMENT_TYPE_TIME, 120 )},
		{511, new AchivementClass("AC_ONE_PLAY_TIME_11" , ACHIVEMENT_TYPE_TIME, 144 )},
		{512, new AchivementClass("AC_ONE_PLAY_TIME_12" , ACHIVEMENT_TYPE_TIME, 168 )},

		{601, new AchivementClass("AC_ALL_STELA_PRODUCT_01" , ACHIVEMENT_TYPE_FRAGMENT, 1   )},
		{602, new AchivementClass("AC_ALL_STELA_PRODUCT_02" , ACHIVEMENT_TYPE_FRAGMENT, 3   )},
		{603, new AchivementClass("AC_ALL_STELA_PRODUCT_03" , ACHIVEMENT_TYPE_FRAGMENT, 5   )},
		{604, new AchivementClass("AC_ALL_STELA_PRODUCT_04" , ACHIVEMENT_TYPE_FRAGMENT, 10  )},
		{605, new AchivementClass("AC_ALL_STELA_PRODUCT_05" , ACHIVEMENT_TYPE_FRAGMENT, 20  )},
		{606, new AchivementClass("AC_ALL_STELA_PRODUCT_06" , ACHIVEMENT_TYPE_FRAGMENT, 30  )},
		{607, new AchivementClass("AC_ALL_STELA_PRODUCT_07" , ACHIVEMENT_TYPE_FRAGMENT, 50  )},

		{701, new AchivementClass("AC_BIFROST_GET_01" , ACHIVEMENT_TYPE_BEFOREST, 1   )},
		{702, new AchivementClass("AC_BIFROST_GET_02" , ACHIVEMENT_TYPE_BEFOREST, 3   )},
		{703, new AchivementClass("AC_BIFROST_GET_03" , ACHIVEMENT_TYPE_BEFOREST, 5   )},
		{704, new AchivementClass("AC_BIFROST_GET_04" , ACHIVEMENT_TYPE_BEFOREST, 10  )},
		{705, new AchivementClass("AC_BIFROST_GET_05" , ACHIVEMENT_TYPE_BEFOREST, 20  )},
		{706, new AchivementClass("AC_BIFROST_GET_06" , ACHIVEMENT_TYPE_BEFOREST, 30  )},
		{707, new AchivementClass("AC_BIFROST_GET_07" , ACHIVEMENT_TYPE_BEFOREST, 50  )},

		{801, new AchivementClass("AC_LIFE_FRUITS_GET_01" , ACHIVEMENT_TYPE_FRUIT, 1   )},
		{802, new AchivementClass("AC_LIFE_FRUITS_GET_02" , ACHIVEMENT_TYPE_FRUIT, 3   )},
		{803, new AchivementClass("AC_LIFE_FRUITS_GET_03" , ACHIVEMENT_TYPE_FRUIT, 5   )},
		{804, new AchivementClass("AC_LIFE_FRUITS_GET_04" , ACHIVEMENT_TYPE_FRUIT, 7   )},
		{805, new AchivementClass("AC_LIFE_FRUITS_GET_05" , ACHIVEMENT_TYPE_FRUIT, 9   )},
		{806, new AchivementClass("AC_LIFE_FRUITS_GET_06" , ACHIVEMENT_TYPE_FRUIT, 10  )},
		{807, new AchivementClass("AC_LIFE_FRUITS_GET_07" , ACHIVEMENT_TYPE_FRUIT, 15  )},
		{808, new AchivementClass("AC_LIFE_FRUITS_GET_08" , ACHIVEMENT_TYPE_FRUIT, 20  )},
		{809, new AchivementClass("AC_LIFE_FRUITS_GET_09" , ACHIVEMENT_TYPE_FRUIT, 25  )},
		{810, new AchivementClass("AC_LIFE_FRUITS_GET_10" , ACHIVEMENT_TYPE_FRUIT, 30  )},

		{901, new AchivementClass("AC_RAGNAROK_TIME_01" , ACHIVEMENT_TYPE_RAGNAROK, 1   )},
		{902, new AchivementClass("AC_RAGNAROK_TIME_02" , ACHIVEMENT_TYPE_RAGNAROK, 2   )},
		{903, new AchivementClass("AC_RAGNAROK_TIME_03" , ACHIVEMENT_TYPE_RAGNAROK, 3   )},
		{904, new AchivementClass("AC_RAGNAROK_TIME_04" , ACHIVEMENT_TYPE_RAGNAROK, 4   )},
		{905, new AchivementClass("AC_RAGNAROK_TIME_05" , ACHIVEMENT_TYPE_RAGNAROK, 5   )},
		{906, new AchivementClass("AC_RAGNAROK_TIME_06" , ACHIVEMENT_TYPE_RAGNAROK, 10  )},
		{907, new AchivementClass("AC_RAGNAROK_TIME_07" , ACHIVEMENT_TYPE_RAGNAROK, 15  )},




        //{10, new AchivementClass("ACH_WIN_ONE_GAME" , ACHIVEMENT_TYPE_CLICK, 3                     )},//test
        //{11, new AchivementClass("ACH_WIN_100_GAMES" , ACHIVEMENT_TYPE_CLICK, 4                    )},//test
        //{12, new AchivementClass("ACH_HEAVY_FIRE" , ACHIVEMENT_TYPE_CLICK, 5                   )},//test
        //{13, new AchivementClass("ACH_TRAVEL_FAR_ACCUM" , ACHIVEMENT_TYPE_CLICK, 6                   )},//test
        //{14, new AchivementClass("ACH_TRAVEL_FAR_SINGLE" , ACHIVEMENT_TYPE_CLICK, 7                   )},//test
    };


	public static Dictionary<int, GoddessSkinClass> mGoddesSkin = new Dictionary<int, GoddessSkinClass>()
	{
		{ 0, new GoddessSkinClass(0, 0,                            "1")},
		{ 1, new GoddessSkinClass(0, double.Parse("100"),          "2")},
		{ 2, new GoddessSkinClass(0, double.Parse("10000"),        "3")},
		{ 3, new GoddessSkinClass(0, double.Parse("1000000"),      "4")},
		{ 4, new GoddessSkinClass(0, double.Parse("100000000"),    "5")},
		{ 5, new GoddessSkinClass(0, double.Parse("10000000000"),  "6")},
		{ 6, new GoddessSkinClass(0, double.Parse("1000000000000"),"7")},
        //{ 8, new GoddessSkinClass(1200, double.Parse("334000000000000000"),"フリナ")},
        //{ 9, new GoddessSkinClass(1300, double.Parse("763000000000000000"),"エオストレ")},
        //{ 10, new GoddessSkinClass(1400, double.Parse("1090000000000000000"),"スカディ")},
    };

	public static Dictionary<int, GoddessSkinClass> mFeverSkin = new Dictionary<int, GoddessSkinClass>()
	{
		{ 0, new GoddessSkinClass(0, 0,                               "CG1")},
		{ 1, new GoddessSkinClass(0, double.Parse("1000"),            "CG2")},
		{ 2, new GoddessSkinClass(0, double.Parse("100000"),          "CG3")},
		{ 3, new GoddessSkinClass(0, double.Parse("10000000"),        "CG4")},
		{ 4, new GoddessSkinClass(0, double.Parse("1000000000"),      "CG5")},
		{ 5, new GoddessSkinClass(0, double.Parse("100000000000"),    "CG6")},
		{ 6, new GoddessSkinClass(0, double.Parse("10000000000000"),  "CG7")},
		{ 7, new GoddessSkinClass(0, double.Parse("100000000000000"), "CG8")},
		{ 8, new GoddessSkinClass(0, double.Parse("1000000000000000"),"CG9")},
	};
}

public class AwakeClass
{
	public int skillType;
	public int lv;
	public double power;
	public double price;
	public string name;
	public string dis;

	public AwakeClass(int _skillType, int _lv, double _power, double _price, string _name, string _dis)
	{
		skillType = _skillType;
		lv = _lv;
		power = _power;
		price = _price;
		name = _name;
		dis = _dis;
	}
}
public class PermanenceAwakeClass
{
	public int skillType;
	public int lv;
	public double power;
	public double price;
	public string name;
	public string dis;

	public PermanenceAwakeClass(int _skillType, int _lv, double _power, double _price, string _name, string _dis)
	{
		skillType = _skillType;
		lv = _lv;
		power = _power;
		price = _price;
		name = _name;
		dis = _dis;
	}
}
public class YggdrasilSkillClass
{
	public double power;
	public double price;
	public string name;
	public string dis;
	public int icon;

	public YggdrasilSkillClass(double _power, double _price, string _name, string _dis, int _icon)
	{
		power = _power;
		price = _price;
		name = _name;
		dis = _dis;
		icon = _icon;
	}
}
public class RecordClass
{
	public string name;

	public RecordClass(string _name)
	{
		name = _name;
	}
}
public class AchivementClass
{
	public string apikey;
	public int ahivementType;
	public double value;

	public AchivementClass(string _apikey, int _ahivementType, double _value)
	{
		apikey = _apikey;
		ahivementType = _ahivementType;
		value = _value;
	}
}
public class GoddessSkinClass
{
	public string name;
	public double OpenInstLv;
	public double price;

	public GoddessSkinClass(double _OpenInstLv, double _price, string _name)
	{
		OpenInstLv = _OpenInstLv;
		price = _price;
		name = _name;
	}
}
