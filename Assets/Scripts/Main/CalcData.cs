
using System;
using System.Collections.Generic;
using naichilab.Scripts.Extensions;
using UnityEngine;

public static class CalcData
{

	private static List<double> Inst1ConstTable = new List<double>();
	private static List<double> Inst2ConstTable = new List<double>();
	private static List<double> Inst3ConstTable = new List<double>();
	private static List<double> Inst4ConstTable = new List<double>();
	private static List<double> Inst5ConstTable = new List<double>();
	private static List<double> Inst6ConstTable = new List<double>();
	private static List<double> Inst7ConstTable = new List<double>();
	private static List<double> Inst8ConstTable = new List<double>();
	private static List<double> Inst9ConstTable = new List<double>();
	private static List<double> Inst10ConstTable = new List<double>();
	private static List<double> Inst11ConstTable = new List<double>();
	private static List<double> Inst12ConstTable = new List<double>();
	private static List<double> Inst13ConstTable = new List<double>();
	private static List<double> Inst14ConstTable = new List<double>();
	private static List<double> Inst15ConstTable = new List<double>();
	private static List<double> Inst16ConstTable = new List<double>();
	private static List<double> Inst17ConstTable = new List<double>();
	private static List<double> Inst18ConstTable = new List<double>();
	private static List<double> Inst19ConstTable = new List<double>();
	private static List<double> Inst20ConstTable = new List<double>();


	public static Dictionary<int, double> CacheGetInst1Point = new Dictionary<int, double>();
	public static Dictionary<int, double> CacheGetInst2Point = new Dictionary<int, double>();
	public static Dictionary<int, double> CacheGetInst3Point = new Dictionary<int, double>();
	public static Dictionary<int, double> CacheGetInst4Point = new Dictionary<int, double>();
	public static Dictionary<int, double> CacheGetInst5Point = new Dictionary<int, double>();
	public static Dictionary<int, double> CacheGetInst6Point = new Dictionary<int, double>();
	public static Dictionary<int, double> CacheGetInst7Point = new Dictionary<int, double>();
	public static Dictionary<int, double> CacheGetInst8Point = new Dictionary<int, double>();
	public static Dictionary<int, double> CacheGetInst9Point = new Dictionary<int, double>();
	public static Dictionary<int, double> CacheGetInst10Point = new Dictionary<int, double>();
	public static Dictionary<int, double> CacheGetInst11Point = new Dictionary<int, double>();
	public static Dictionary<int, double> CacheGetInst12Point = new Dictionary<int, double>();
	public static Dictionary<int, double> CacheGetInst13Point = new Dictionary<int, double>();
	public static Dictionary<int, double> CacheGetInst14Point = new Dictionary<int, double>();
	public static Dictionary<int, double> CacheGetInst15Point = new Dictionary<int, double>();
	public static Dictionary<int, double> CacheGetInst16Point = new Dictionary<int, double>();
	public static Dictionary<int, double> CacheGetInst17Point = new Dictionary<int, double>();
	public static Dictionary<int, double> CacheGetInst18Point = new Dictionary<int, double>();
	public static Dictionary<int, double> CacheGetInst19Point = new Dictionary<int, double>();
	public static Dictionary<int, double> CacheGetInst20Point = new Dictionary<int, double>();
	private static Dictionary<int, double> InstAwakeTypeA = new Dictionary<int, double>() {
		{10, 2},
		{20, 2},
		{30, 2},
		{40, 2},
		{50, 3},
		{55, 2},
		{70, 5},
		{90, 2},
		{100, 10},
		{130, 2},
		{150, 2},
		{170, 2},
		{200, 5},
		{220, 2},
		{240, 2},
		{260, 2},
		{280, 2},
		{300, 5},
	};
	private static Dictionary<int, double> InstAwakeTypeB = new Dictionary<int, double>() {
		{ 5, 2},
		{10, 2},
		{20, 2},
		{35, 2},
		{50, 3},
		{55, 2},
		{60, 2},
		{70, 2},
		{80, 2},
		{100, 5},
		{150, 2},
		{200, 2},
		{250, 5},
		{300, 3},
		{350, 3},
		{400, 3},
		{450, 3},
		{500, 5},
	};

	private static bool isInit = false;

	public static void ResetInit()
	{
		isInit = false;

		Inst1ConstTable = new List<double>();
		Inst2ConstTable = new List<double>();
		Inst3ConstTable = new List<double>();
		Inst4ConstTable = new List<double>();
		Inst5ConstTable = new List<double>();
		Inst6ConstTable = new List<double>();
		Inst7ConstTable = new List<double>();
		Inst8ConstTable = new List<double>();
		Inst9ConstTable = new List<double>();
		Inst10ConstTable = new List<double>();
		Inst11ConstTable = new List<double>();
		Inst12ConstTable = new List<double>();
		Inst13ConstTable = new List<double>();
		Inst14ConstTable = new List<double>();
		Inst15ConstTable = new List<double>();
		Inst16ConstTable = new List<double>();
		Inst17ConstTable = new List<double>();
		Inst18ConstTable = new List<double>();
		Inst19ConstTable = new List<double>();
		Inst20ConstTable = new List<double>();

		CacheGetInst1Point = new Dictionary<int, double>();
		CacheGetInst2Point = new Dictionary<int, double>();
		CacheGetInst3Point = new Dictionary<int, double>();
		CacheGetInst4Point = new Dictionary<int, double>();
		CacheGetInst5Point = new Dictionary<int, double>();
		CacheGetInst6Point = new Dictionary<int, double>();
		CacheGetInst7Point = new Dictionary<int, double>();
		CacheGetInst8Point = new Dictionary<int, double>();
		CacheGetInst9Point = new Dictionary<int, double>();
		CacheGetInst10Point = new Dictionary<int, double>();
		CacheGetInst11Point = new Dictionary<int, double>();
		CacheGetInst12Point = new Dictionary<int, double>();
		CacheGetInst13Point = new Dictionary<int, double>();
		CacheGetInst14Point = new Dictionary<int, double>();
		CacheGetInst15Point = new Dictionary<int, double>();
		CacheGetInst16Point = new Dictionary<int, double>();
		CacheGetInst17Point = new Dictionary<int, double>();
		CacheGetInst18Point = new Dictionary<int, double>();
		CacheGetInst19Point = new Dictionary<int, double>();
		CacheGetInst20Point = new Dictionary<int, double>();

	}

	private static void init()
	{
		if (isInit) return;

		//Debug.Log("CalcData init");

		//string tt = "";
		//for (int lv=1; lv<=GameData.INST_LV_MAX[(int)GameData.InstCase.INST1]; lv++)
		//{
		//    double num = 0;
		//    if (lv == 1)
		//    {
		//        num = GameData.INST_COST_BASE[(int)GameData.InstCase.INST1];
		//    }
		//    else
		//    {
		//        num = Math.Ceiling((Inst1ConstTable[lv - 2] + GameData.MASTER_LV_BASE) * Math.Round(Math.Pow(GameData.MASTER_LVUP_RATE, (double)((double)(lv - 1) / (double)10)), 9) * ((double)1 +GameData.MASTER_LV_BASE/ (double)100)-1);
		//    }
		//    Inst1ConstTable.Add(num);
		//}

		CostCalc(ref Inst1ConstTable, GameData.INST_COST_BASE[(int)GameData.InstCase.INST1], GameData.INST_LV_MAX[(int)GameData.InstCase.INST1]);
		CostCalc(ref Inst2ConstTable, GameData.INST_COST_BASE[(int)GameData.InstCase.INST2], GameData.INST_LV_MAX[(int)GameData.InstCase.INST2]);
		CostCalc(ref Inst3ConstTable, GameData.INST_COST_BASE[(int)GameData.InstCase.INST3], GameData.INST_LV_MAX[(int)GameData.InstCase.INST3]);
		CostCalc(ref Inst4ConstTable, GameData.INST_COST_BASE[(int)GameData.InstCase.INST4], GameData.INST_LV_MAX[(int)GameData.InstCase.INST4]);
		CostCalc(ref Inst5ConstTable, GameData.INST_COST_BASE[(int)GameData.InstCase.INST5], GameData.INST_LV_MAX[(int)GameData.InstCase.INST5]);
		CostCalc(ref Inst6ConstTable, GameData.INST_COST_BASE[(int)GameData.InstCase.INST6], GameData.INST_LV_MAX[(int)GameData.InstCase.INST6]);
		CostCalc(ref Inst7ConstTable, GameData.INST_COST_BASE[(int)GameData.InstCase.INST7], GameData.INST_LV_MAX[(int)GameData.InstCase.INST7]);
		CostCalc(ref Inst8ConstTable, GameData.INST_COST_BASE[(int)GameData.InstCase.INST8], GameData.INST_LV_MAX[(int)GameData.InstCase.INST8]);
		CostCalc(ref Inst9ConstTable, GameData.INST_COST_BASE[(int)GameData.InstCase.INST9], GameData.INST_LV_MAX[(int)GameData.InstCase.INST9]);
		CostCalc(ref Inst10ConstTable, GameData.INST_COST_BASE[(int)GameData.InstCase.INST10], GameData.INST_LV_MAX[(int)GameData.InstCase.INST10]);
		CostCalc(ref Inst11ConstTable, GameData.INST_COST_BASE[(int)GameData.InstCase.INST11], GameData.INST_LV_MAX[(int)GameData.InstCase.INST11]);
		CostCalc(ref Inst12ConstTable, GameData.INST_COST_BASE[(int)GameData.InstCase.INST12], GameData.INST_LV_MAX[(int)GameData.InstCase.INST12]);
		CostCalc(ref Inst13ConstTable, GameData.INST_COST_BASE[(int)GameData.InstCase.INST13], GameData.INST_LV_MAX[(int)GameData.InstCase.INST13]);
		CostCalc(ref Inst14ConstTable, GameData.INST_COST_BASE[(int)GameData.InstCase.INST14], GameData.INST_LV_MAX[(int)GameData.InstCase.INST14]);
		CostCalc(ref Inst15ConstTable, GameData.INST_COST_BASE[(int)GameData.InstCase.INST15], GameData.INST_LV_MAX[(int)GameData.InstCase.INST15]);
		CostCalc(ref Inst16ConstTable, GameData.INST_COST_BASE[(int)GameData.InstCase.INST16], GameData.INST_LV_MAX[(int)GameData.InstCase.INST16]);
		CostCalc(ref Inst17ConstTable, GameData.INST_COST_BASE[(int)GameData.InstCase.INST17], GameData.INST_LV_MAX[(int)GameData.InstCase.INST17]);
		CostCalc(ref Inst18ConstTable, GameData.INST_COST_BASE[(int)GameData.InstCase.INST18], GameData.INST_LV_MAX[(int)GameData.InstCase.INST18]);
		CostCalc(ref Inst19ConstTable, GameData.INST_COST_BASE[(int)GameData.InstCase.INST19], GameData.INST_LV_MAX[(int)GameData.InstCase.INST19]);
		CostCalc(ref Inst20ConstTable, GameData.INST_COST_BASE[(int)GameData.InstCase.INST20], GameData.INST_LV_MAX[(int)GameData.InstCase.INST20]);

		isInit = true;

	}
	private static void CostCalc(ref List<double> _pointaList, double _CostBase, int _LvMax)
	{
		for (int lv = 1; lv <= _LvMax; lv++)
		{
			double num = 0;
			if (lv == 1)
			{
				num = _CostBase;
			}
			else
			{
				num = Math.Ceiling((_pointaList[lv - 2] + GameData.MASTER_LV_BASE) * Math.Round(Math.Pow(GameData.MASTER_LVUP_RATE, (double)((double)(lv - 1) / (double)10)), 9) * ((double)1 + GameData.MASTER_LV_BASE / (double)100) - 1);
			}
			if (num >= double.MaxValue) num = double.MaxValue;
			_pointaList.Add(num);
			//Debug.Log($"lv:{lv} {_pointaList.Count} {num}");
		}
	}


	public static double GetBiforestBoostPower()
	{
		return GameData.SHARD_BIFOREST_BOOST_UP_BASE;
	}
	private static double CalcBiforestBoost(double point)
	{
		if (GameData.ShardBiforestBoost > 0)
		{
			point = point * GetBiforestBoostPower();
		}
		return point;
	}

	/// <summary>
	/// 各種補正を計算して返す クリック版
	/// </summary>
	public static double GetCalcClickPoint()
	{
		double point = GameData.ADD_POINT_CLICK;

		double nanbai = 2;
		double par = 0;
		double tasu = 0;
		double _instLv = GetAllInstLv();
		/*foreach (int id in GameData.mAwakeTargetClick)
        {
            if (GameData.AwakeLv.Contains(id))
            {
                nanbai *= GameData.mAwake[id].power;
            }
        }*/

		//if (GameData.AwakeLv.Contains(4))
		//{
		//    //Debug.Log($"tasu:{GameData.mAwake[4].power} { _instLv} {GameData.mAwake[4].power * _instLv}");
		//    tasu += GameData.mAwake[4].power * _instLv;
		//}

		//if (GameData.AwakeLv.Contains(109))
		//{
		//    //Debug.Log($"_instLv:{_instLv} { Math.Floor(_instLv / 10)} {(GameData.mAwake[109].power * Math.Floor(_instLv / 10))}");
		//    par += (GameData.mAwake[109].power * Math.Floor(_instLv / 10));
		//}
		//if (GameData.AwakeLv.Contains(8))
		//{
		//    par += (GameData.all_ragnarok * GameData.mAwake[8].power);
		//}
		//if (GameData.AwakeLv.Contains(1012))
		//{
		//    par += (GameData.all_ragnarok * GameData.mAwake[1012].power);
		//}
		//foreach (int id in GameData.mAwakeTargetClickAwakeNum)
		//{
		//    if (GameData.AwakeLv.Contains(id))
		//    {
		//        par += (GameData.mAwake[id].power);
		//    }
		//}


		//ラグナロクスキル
		//if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_7])
		//    par += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_7].power;
		//if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_8])
		//    par += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_8].power;
		//if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_9])
		//    par += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_9].power;

		//女神
		//par += GetGoddessPow();

		/*point += tasu;
        point = point * nanbai;
        point = point * (1 + (0.01 * par));*/

		//ウロボロス
		//point = point * GetOuroborosPow();

		////グングニル発動中
		//if (GameData.GungnirPowerTime > 0)
		//{
		//    point = point * (0.01 * GetGungnirPower());
		//}

		//Debug.Log($"click {point} {tasu} {nanbai} {par}");

		foreach (int id in GameData.mAwakeTargetClick)
		{
			if (GameData.AwakeLv.Contains(id))
			{
				nanbai *= GameData.mAwake[id].power;
			}
		}
		//List<int> lvs = new List<int>()
		//{
		//	10,20,30,40,50,60,70,80,90,100,
		//	150,200,300,400,500,600,700
		//};

		//var lv = lvs.FindLastIndex((int i) => i < GameData.InstLv[1]);
		//if (lv == -1)
		//{
		//	lv = 0;
		//}
		point = (nanbai / 10) * GameData.InstLv[1] + point;

		double result = CalcBiforestBoost(point);
		//result *= 10;
		//result = Math.Floor(result);
		//result *= 0.1f;
		//result = Math.Round(result, 0);

		if (result >= double.MaxValue) result = double.MaxValue;
		return result;
	}

	/// <summary>
	/// 各種補正を計算して返す 施設版
	/// </summary>
	public static double GetCalcInstPoint(double point)
	{
		double result = CalcBiforestBoost(point);
		if (result >= double.MaxValue) result = double.MaxValue;
		return result;
	}



	public static double GetInstCost(int instCase, int lv)
	{
		init();

		double n = 0;

		//Debug.Log($"instCase:{instCase} lv:{lv}");
		switch (instCase)
		{
		default:
		case (int)GameData.InstCase.INST1: n = Inst1ConstTable[lv - 1]; break;
		case (int)GameData.InstCase.INST2: n = Inst2ConstTable[lv - 1]; break;
		case (int)GameData.InstCase.INST3: n = Inst3ConstTable[lv - 1]; break;
		case (int)GameData.InstCase.INST4: n = Inst4ConstTable[lv - 1]; break;
		case (int)GameData.InstCase.INST5: n = Inst5ConstTable[lv - 1]; break;
		case (int)GameData.InstCase.INST6: n = Inst6ConstTable[lv - 1]; break;
		case (int)GameData.InstCase.INST7: n = Inst7ConstTable[lv - 1]; break;
		case (int)GameData.InstCase.INST8: n = Inst8ConstTable[lv - 1]; break;
		case (int)GameData.InstCase.INST9: n = Inst9ConstTable[lv - 1]; break;
		case (int)GameData.InstCase.INST10: n = Inst10ConstTable[lv - 1]; break;
		case (int)GameData.InstCase.INST11: n = Inst11ConstTable[lv - 1]; break;
		case (int)GameData.InstCase.INST12: n = Inst12ConstTable[lv - 1]; break;
		case (int)GameData.InstCase.INST13: n = Inst13ConstTable[lv - 1]; break;
		case (int)GameData.InstCase.INST14: n = Inst14ConstTable[lv - 1]; break;
		case (int)GameData.InstCase.INST15: n = Inst15ConstTable[lv - 1]; break;
		case (int)GameData.InstCase.INST16: n = Inst16ConstTable[lv - 1]; break;
		case (int)GameData.InstCase.INST17: n = Inst17ConstTable[lv - 1]; break;
		case (int)GameData.InstCase.INST18: n = Inst18ConstTable[lv - 1]; break;
		case (int)GameData.InstCase.INST19: n = Inst19ConstTable[lv - 1]; break;
		case (int)GameData.InstCase.INST20: n = Inst20ConstTable[lv - 1]; break;
		}

		//ラグナロクスキル
		double RagnarokSkill_ShopPrice = 0;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_10])
			RagnarokSkill_ShopPrice += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_10].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_11])
			RagnarokSkill_ShopPrice += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_11].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_12])
			RagnarokSkill_ShopPrice += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_12].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_64])
			RagnarokSkill_ShopPrice += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_64].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_65])
			RagnarokSkill_ShopPrice += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_65].power;

		if (RagnarokSkill_ShopPrice < 0)
		{
			n = n * (1 + (0.01 * RagnarokSkill_ShopPrice));
		}

		n = Math.Round(n);

		if (n >= double.MaxValue) n = double.MaxValue;
		return n;
	}





	/// <summary>
	/// 施設の生産ポイント取得 1秒分
	/// </summary>
	public static double GetInstPoint(int instCase = 0, int lv = 0, bool adj = true)
	{
		if (lv == 0) return 0;

		double result = 0;
		int baseIndex = GameData.mAwakeIDLength * instCase;
		double m = 0;
		switch (instCase)
		{
		default:
		case (int)GameData.InstCase.INST1:/* result = CalcGetPoint(ref CacheGetInst1Point, GameData.INST_POW_BASE[(int)GameData.InstCase.INST1], InstAwakeTypeA, GameData.INST_LV_MAX[(int)GameData.InstCase.INST1], lv);*/ break;
		case (int)GameData.InstCase.INST2: result = CalcGetPoint(ref CacheGetInst2Point, GameData.INST_POW_BASE[(int)GameData.InstCase.INST2], InstAwakeTypeB, GameData.INST_LV_MAX[(int)GameData.InstCase.INST2], lv); break;
		case (int)GameData.InstCase.INST3: result = CalcGetPoint(ref CacheGetInst3Point, GameData.INST_POW_BASE[(int)GameData.InstCase.INST3], InstAwakeTypeB, GameData.INST_LV_MAX[(int)GameData.InstCase.INST3], lv); break;
		case (int)GameData.InstCase.INST4: result = CalcGetPoint(ref CacheGetInst4Point, GameData.INST_POW_BASE[(int)GameData.InstCase.INST4], InstAwakeTypeB, GameData.INST_LV_MAX[(int)GameData.InstCase.INST4], lv); break;
		case (int)GameData.InstCase.INST5: result = CalcGetPoint(ref CacheGetInst5Point, GameData.INST_POW_BASE[(int)GameData.InstCase.INST5], InstAwakeTypeB, GameData.INST_LV_MAX[(int)GameData.InstCase.INST5], lv); break;
		case (int)GameData.InstCase.INST6: result = CalcGetPoint(ref CacheGetInst6Point, GameData.INST_POW_BASE[(int)GameData.InstCase.INST6], InstAwakeTypeB, GameData.INST_LV_MAX[(int)GameData.InstCase.INST6], lv); break;
		case (int)GameData.InstCase.INST7: result = CalcGetPoint(ref CacheGetInst7Point, GameData.INST_POW_BASE[(int)GameData.InstCase.INST7], InstAwakeTypeB, GameData.INST_LV_MAX[(int)GameData.InstCase.INST7], lv); break;
		case (int)GameData.InstCase.INST8: result = CalcGetPoint(ref CacheGetInst8Point, GameData.INST_POW_BASE[(int)GameData.InstCase.INST8], InstAwakeTypeB, GameData.INST_LV_MAX[(int)GameData.InstCase.INST8], lv); break;
		case (int)GameData.InstCase.INST9: result = CalcGetPoint(ref CacheGetInst9Point, GameData.INST_POW_BASE[(int)GameData.InstCase.INST9], InstAwakeTypeB, GameData.INST_LV_MAX[(int)GameData.InstCase.INST9], lv); break;
		case (int)GameData.InstCase.INST10: result = CalcGetPoint(ref CacheGetInst10Point, GameData.INST_POW_BASE[(int)GameData.InstCase.INST10], InstAwakeTypeB, GameData.INST_LV_MAX[(int)GameData.InstCase.INST10], lv); break;
		case (int)GameData.InstCase.INST11: result = CalcGetPoint(ref CacheGetInst11Point, GameData.INST_POW_BASE[(int)GameData.InstCase.INST11], InstAwakeTypeB, GameData.INST_LV_MAX[(int)GameData.InstCase.INST11], lv); break;
		case (int)GameData.InstCase.INST12: result = CalcGetPoint(ref CacheGetInst12Point, GameData.INST_POW_BASE[(int)GameData.InstCase.INST12], InstAwakeTypeB, GameData.INST_LV_MAX[(int)GameData.InstCase.INST12], lv); break;
		case (int)GameData.InstCase.INST13: result = CalcGetPoint(ref CacheGetInst13Point, GameData.INST_POW_BASE[(int)GameData.InstCase.INST13], InstAwakeTypeB, GameData.INST_LV_MAX[(int)GameData.InstCase.INST13], lv); break;
		case (int)GameData.InstCase.INST14: result = CalcGetPoint(ref CacheGetInst14Point, GameData.INST_POW_BASE[(int)GameData.InstCase.INST14], InstAwakeTypeB, GameData.INST_LV_MAX[(int)GameData.InstCase.INST14], lv); break;
		case (int)GameData.InstCase.INST15: result = CalcGetPoint(ref CacheGetInst15Point, GameData.INST_POW_BASE[(int)GameData.InstCase.INST15], InstAwakeTypeB, GameData.INST_LV_MAX[(int)GameData.InstCase.INST15], lv); break;
		case (int)GameData.InstCase.INST16: result = CalcGetPoint(ref CacheGetInst16Point, GameData.INST_POW_BASE[(int)GameData.InstCase.INST16], InstAwakeTypeB, GameData.INST_LV_MAX[(int)GameData.InstCase.INST16], lv); break;
		case (int)GameData.InstCase.INST17: result = CalcGetPoint(ref CacheGetInst17Point, GameData.INST_POW_BASE[(int)GameData.InstCase.INST17], InstAwakeTypeB, GameData.INST_LV_MAX[(int)GameData.InstCase.INST17], lv); break;
		case (int)GameData.InstCase.INST18: result = CalcGetPoint(ref CacheGetInst18Point, GameData.INST_POW_BASE[(int)GameData.InstCase.INST18], InstAwakeTypeB, GameData.INST_LV_MAX[(int)GameData.InstCase.INST18], lv); break;
		case (int)GameData.InstCase.INST19: result = CalcGetPoint(ref CacheGetInst19Point, GameData.INST_POW_BASE[(int)GameData.InstCase.INST19], InstAwakeTypeB, GameData.INST_LV_MAX[(int)GameData.InstCase.INST19], lv); break;
		case (int)GameData.InstCase.INST20: result = CalcGetPoint(ref CacheGetInst20Point, GameData.INST_POW_BASE[(int)GameData.InstCase.INST20], InstAwakeTypeB, GameData.INST_LV_MAX[(int)GameData.InstCase.INST20], lv); break;
		}

		//覚醒効果
		for (int i = 1; i <= GameData.mAwakeLvNum; i++)
		{
			int id = baseIndex + i;
			if (GameData.AwakeLv.Contains(id)) m += GameData.mAwake[id].power;
		}
		if (m != 0) result = result * m;

		//ラグナロクスキル
		double RagnarokSkill_InstPow = 0;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_2])
			RagnarokSkill_InstPow += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_2].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_3])
			RagnarokSkill_InstPow += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_3].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_4])
			RagnarokSkill_InstPow += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_4].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_5])
			RagnarokSkill_InstPow += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_5].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_6])
			RagnarokSkill_InstPow += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_6].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_25])
			RagnarokSkill_InstPow += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_25].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_26])
		{
			RagnarokSkill_InstPow += Math.Floor((double)lv / 100) * 0.1;
		}
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_49])
		{
			RagnarokSkill_InstPow += Math.Floor((double)lv / 100);
		}

		switch (instCase)
		{
		default:
		case (int)GameData.InstCase.INST1:
			if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_24])
			{
				int h = DateTime.Now.Hour;
				if (6 <= h && h < 17)
				{
					RagnarokSkill_InstPow += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_24].power;
				}
			}

			//永続覚醒分 べたに書く…
			foreach (int id in GameData.mAwakeTargetInst1)
			{
				if (GameData.AwakeLv.Contains(id))
				{
					RagnarokSkill_InstPow += GameData.mAwake[id].power;
				}
			}

			if (GameData.AwakeLv.Contains(2005))
			{
				//「ワタリカラス」が1レベルに付き、「世界樹の葉」のステラ生産率を+1％
				RagnarokSkill_InstPow += GameData.InstLv[(int)GameData.InstCase.INST2];
				//Debug.Log($"RagnarokSkill_InstPow ワタリカラス [{instCase}] {GameData.InstLv[(int)GameData.InstCase.INST2]}");
			}
			if (GameData.AwakeLv.Contains(2010))
			{
				//「人族の農場」が3レベルに付き、「世界樹の葉」のステラ生産率を+1％
				RagnarokSkill_InstPow += Math.Floor((double)GameData.InstLv[(int)GameData.InstCase.INST3] / 3);
				//Debug.Log($"RagnarokSkill_InstPow 人族の農場 [{instCase}] {Math.Floor((double)GameData.InstLv[(int)GameData.InstCase.INST3] / 3)}");
			}
			if (GameData.AwakeLv.Contains(2011))
			{
				//「ダークエルフの泉」が4レベルに付き、「世界樹の葉」のステラ生産率を+1％
				RagnarokSkill_InstPow += Math.Floor((double)GameData.InstLv[(int)GameData.InstCase.INST4] / 4);
				//Debug.Log($"RagnarokSkill_InstPow ダークエルフの泉 [{instCase}] {Math.Floor((double)GameData.InstLv[(int)GameData.InstCase.INST4] / 4)}");
			}
			if (GameData.AwakeLv.Contains(2012))
			{
				//「ドヴェルグの鍛冶場」が5レベルに付き、「世界樹の葉」のステラ生産率を+1％
				RagnarokSkill_InstPow += Math.Floor((double)GameData.InstLv[(int)GameData.InstCase.INST5] / 5);
				//Debug.Log($"RagnarokSkill_InstPow ドヴェルグの鍛冶場 [{instCase}] {Math.Floor((double)GameData.InstLv[(int)GameData.InstCase.INST5] / 5)}");
			}

			break;

		case (int)GameData.InstCase.INST2:
			if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_23])
			{
				int h = DateTime.Now.Hour;
				if (0 <= h && h < 5 || 18 <= h && h <= 24)
				{
					RagnarokSkill_InstPow += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_23].power;
				}
			}
			if (GameData.AwakeLv.Contains(2014))
			{
				//「ヴァン神族の祭祀場」が6レベルに付き、「ワタリガラス」のステラ生産率を+1％
				RagnarokSkill_InstPow += Math.Floor((double)GameData.InstLv[(int)GameData.InstCase.INST6] / 6);
				//Debug.Log($"RagnarokSkill_InstPow ヴァン神族の祭祀場 [{instCase}] {Math.Floor((double)GameData.InstLv[(int)GameData.InstCase.INST6] / 6)}");
			}
			if (GameData.AwakeLv.Contains(2015))
			{
				//「神聖なる樹海」が7レベルに付き、「ワタリガラス」のステラ生産率を+1％		
				RagnarokSkill_InstPow += Math.Floor((double)GameData.InstLv[(int)GameData.InstCase.INST7] / 7);
				//Debug.Log($"RagnarokSkill_InstPow 神聖なる樹海 [{instCase}] {Math.Floor((double)GameData.InstLv[(int)GameData.InstCase.INST7] / 7)}");
			}
			if (GameData.AwakeLv.Contains(2016))
			{
				//「テュールの神殿」が8レベルに付き、「ワタリガラス」のステラ生産率を+1％
				RagnarokSkill_InstPow += Math.Floor((double)GameData.InstLv[(int)GameData.InstCase.INST8] / 8);
				//Debug.Log($"RagnarokSkill_InstPow テュールの神殿 [{instCase}] {Math.Floor((double)GameData.InstLv[(int)GameData.InstCase.INST8] / 8)}");
			}
			if (GameData.AwakeLv.Contains(2017))
			{
				//「エルフ薬草園」が9レベルに付き、「ワタリガラス」のステラ生産率を+1％
				RagnarokSkill_InstPow += Math.Floor((double)GameData.InstLv[(int)GameData.InstCase.INST9] / 9);
				//Debug.Log($"RagnarokSkill_InstPow エルフ薬草園 [{instCase}] {Math.Floor((double)GameData.InstLv[(int)GameData.InstCase.INST9] / 9)}");
			}
			if (GameData.AwakeLv.Contains(2018))
			{
				//「異世界のワープゲート」が10レベルに付き、「ワタリガラス」のステラ生産率を+1％	
				RagnarokSkill_InstPow += Math.Floor((double)GameData.InstLv[(int)GameData.InstCase.INST10] / 10);
				//Debug.Log($"RagnarokSkill_InstPow 異世界のワープゲート [{instCase}] {Math.Floor((double)GameData.InstLv[(int)GameData.InstCase.INST10] / 10)}");
			}
			break;
		case (int)GameData.InstCase.INST3:
			break;
		}
		//永続覚醒も混ぜる
		foreach (int id in GameData.mAwakeTargetInstALL)
		{
			//ステラの生産率アップ系
			if (GameData.AwakeLv.Contains(id))
			{
				RagnarokSkill_InstPow += GameData.mAwake[id].power;
			}
		}
		foreach (int id in GameData.mAwakeTargetTime)
		{
			if (GameData.AwakeLv.Contains(id))
			{
				RagnarokSkill_InstPow += GameData.mAwake[id].power;
			}
		}
		if (GameData.AwakeLv.Contains(2013))
		{
			if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_2])
				RagnarokSkill_InstPow += GameData.mAwake[2013].power;
			if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_23])
				RagnarokSkill_InstPow += GameData.mAwake[2013].power;
			if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_24])
				RagnarokSkill_InstPow += GameData.mAwake[2013].power;
			if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_49])
				RagnarokSkill_InstPow += GameData.mAwake[2013].power;
			if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_51])
				RagnarokSkill_InstPow += GameData.mAwake[2013].power;
			if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_52])
				RagnarokSkill_InstPow += GameData.mAwake[2013].power;
		}

		//夜空のステラ
		if (GameData.AwakeLv.Contains(2004))
		{
			RagnarokSkill_InstPow += (GameData.NightStella * GameData.NIGHT_STELLA_POW);
		}

		//女神
		RagnarokSkill_InstPow += GetGoddessPow();

		//最終計算
		if (RagnarokSkill_InstPow > 0)
		{
			result = result * (1 /*+ (0.01 * RagnarokSkill_InstPow)*/);
		}

		//生命の実補正
		double FruitPow = 0;
		//Debug.Log($"FruitPow[{instCase}] {FruitPow} {GameData.InstFruitLv[instCase]}");
		FruitPow += GameData.InstFruitLv[instCase] * 10;
		//Debug.Log($"FruitPow[{instCase}] {FruitPow} {GameData.InstFruitLv[instCase]}");
		if (FruitPow > 0)
		{
			result = result * (1 + (0.01 * FruitPow));
		}



		//ウロボロス
		result = result * GetOuroborosPow();

		//グングニル発動中
		if (GameData.GungnirPowerTime > 0)
		{
			result = result * (0.01 * GetGungnirPower());
		}

		//ブースト補正
		if (adj) result = GetCalcInstPoint(result);


		result = Math.Round(result, 2);

		if (result >= double.MaxValue) result = double.MaxValue;
		return result;
	}

	/// <summary>
	/// 全施設の生産ポイントを取得
	/// </summary>
	/// <returns></returns>
	public static double GetAllInstPoint()
	{
		double result = 0;
		foreach (int instCase in Enum.GetValues(typeof(GameData.InstCase)))
		{
			result += GetInstPoint(instCase, (GameData.InstLv.Count > instCase ? GameData.InstLv[instCase] : 0), false);
		}
		result = Math.Round(result, 2);
		return result;
	}

	public static double GetAllInstLv()
	{
		double _instLv = 0;

		foreach (int _lv in GameData.InstLv)
		{
			_instLv += _lv;
		}

		return _instLv;
	}

	/// <summary>
	/// ワープで加算されるポイント
	/// </summary>
	/// <returns></returns>
	public static double GetShardBiforestWarpPoint()
	{
		double result = GameData.SHARD_BIFOREST_WARP_SECOND_BASE;
		double n = 0;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_19])
			n += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_19].power;

		result = Math.Round(GetAllInstPoint() * (result + n));
		//Debug.Log($"GetShardBiforestWarpPoint {result} {n}");

		return result;
	}
	/// <summary>
	/// 復帰時獲得ポイント
	/// </summary>
	/// <returns></returns>
	public static double GetResumePoint(out double _diff_sec)
	{

		double result = 0;
		double diff_sec = Math.Round((double)DateTime.Now.Ticks / (1000 * 1000 * 10)) - GameData.nowtime;
		_diff_sec = diff_sec;
		if (diff_sec > 0)
		{
			double n = 0;
			if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_44])
				n += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_44].power;
			if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_45])
				n += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_45].power;
			if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_46])
				n += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_46].power;
			if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_47])
				n += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_47].power;
			if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_48])
				n += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_48].power;

			if (diff_sec > n)
			{
				diff_sec = n;
			}
			double p = GetAllInstPoint() / CalcData.GetAdjInstCycle();
			result = Math.Round(p * (diff_sec));

			//Debug.Log($"GetResumePoint {result} {diff_sec} {n}");
		}

		if (result >= double.MaxValue) result = double.MaxValue;
		return result;
	}

	private static double CalcGetPoint(ref Dictionary<int, double> _pointaDic, double _powBase, Dictionary<int, double> _AwakeType, int _lvMax, int lv)
	{


		if (_pointaDic.ContainsKey(lv))
		{
			//キャッシュ
			return _pointaDic[lv] * lv;
		}
		_pointaDic.Add(0, 0);
		for (int i = 1; i <= _lvMax; i++)
		{
			double n = 0;
			if (i == 1)
			{
				//べーすを書くINSTごとできめる
				n = _powBase;
				//Debug.Log($"powBase:{_powBase.ToString("0")}");
			}
			else
			{
				if (_AwakeType.ContainsKey(i))
				{
					n = _pointaDic[i - 1] * _AwakeType[i];
				}
				else
				{
					n = _pointaDic[i - 1];
				}
			}
			if (!_pointaDic.ContainsKey(i))
			{
				_pointaDic.Add(i, 0);
			}
			_pointaDic[i] = n;
		}

		return _pointaDic[lv] * lv;
	}



	public static double GetAdjInstCycle()
	{
		double result = 1;

		double RagnarokSkill_Time = 0;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_54])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_54].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_55])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_55].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_56])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_56].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_57])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_57].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_58])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_58].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_59])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_59].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_60])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_60].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_61])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_61].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_62])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_62].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_63])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_63].power;

		result = result * (1 - (0.01 * RagnarokSkill_Time));
		//Debug.Log($"GetAdjInstCycle {result} {RagnarokSkill_Time}");

		return result;
	}




	public static double GetFragmentStellaPoint()
	{
		//総生産秒の2000%がベース
		double n = GameData.FRAGMENT_STELLA_GET_BASE;

		double allp = 0;
		for (int instCase = 1; instCase < (GameData.INST_COST_BASE.Count); instCase++)
		{
			allp += CalcData.GetInstPoint(instCase, GameData.InstLv[instCase]);
		}

		double result = allp * (0.01 * n);

		return result;
	}
	public static int GetFragmentStellaCycle()
	{
		//最初は出る
		if (GameData.all_Stella == 0)
		{
			return 0;
		}
		return GameData.FRAGMENT_STELLA_CYCLE;
	}
	public static int GetFragmentStellaStay()
	{
		int n = GameData.FRAGMENT_STELLA_STAY;

		//ラグナロクスキル
		double RagnarokSkill_StayTime = 0;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_51])
			RagnarokSkill_StayTime += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_51].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_52])
			RagnarokSkill_StayTime += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_52].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_53])
			RagnarokSkill_StayTime += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_53].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_20])
			RagnarokSkill_StayTime += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_20].power;

		double result = (double)n * (1 + (0.01 * RagnarokSkill_StayTime));

		//Debug.Log($"GetFragmentStellaStay {result} {n} {RagnarokSkill_StayTime}");

		return (int)result;
	}
	public static bool GetFragmentStellaJudge()
	{
		//仮
		int r = 10000;
		int pop_line = GameData.FRAGMENT_STELLA_POP_BASE;


		//ラグナロクスキル
		double RagnarokSkill_Stella_pop = 0;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_13])
			RagnarokSkill_Stella_pop += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_13].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_14])
			RagnarokSkill_Stella_pop += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_14].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_15])
			RagnarokSkill_Stella_pop += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_15].power;
		if (GameData.AwakeLv.Contains(2019))
			RagnarokSkill_Stella_pop += GameData.mAwake[2019].power;
		if (GameData.AwakeLv.Contains(2020))
			RagnarokSkill_Stella_pop += GameData.mAwake[2020].power;
		if (GameData.AwakeLv.Contains(2021))
			RagnarokSkill_Stella_pop += GameData.mAwake[2021].power;
		if (GameData.AwakeLv.Contains(2022))
			RagnarokSkill_Stella_pop += GameData.mAwake[2022].power;
		if (GameData.AwakeLv.Contains(2023))
			RagnarokSkill_Stella_pop += GameData.mAwake[2023].power;

		int rr = UnityEngine.Random.RandomRange(0, r);
		bool j = rr < (100 * (pop_line + RagnarokSkill_Stella_pop));

		Debug.Log($"GetFragmentStellaJudge {rr} {j} {pop_line} {RagnarokSkill_Stella_pop}");
		//#if UNITY_EDITOR
		//        j = true;
		//#endif

		//最初は出る
		if (GameData.all_Stella == 0)
		{
			j = true;
		}

		return j;
	}


	//生命の実
	public static int GetFruitCycle()
	{
		int result = GameData.FRUIT_CYCLE;
		if (GameData.all_fruit == 0)
		{
			return 0;
		}
		return result;
	}
	public static int GetAdjFruitCycle()
	{
		int result = 0;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_42])
			result += (int)GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_42].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_43])
			result += (int)GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_43].power;

		//Debug.Log($"GetFruitCycle {result}");

		return result;
	}
	public static double GetFruitNum()
	{
		double result = 1;

		if (GameData.AwakeLv.Contains(1017))
			result += GameData.mAwake[1017].power;
		if (GameData.AwakeLv.Contains(1021))
			result += GameData.mAwake[1021].power;
		if (GameData.AwakeLv.Contains(1025))
			result += GameData.mAwake[1025].power;
		if (GameData.AwakeLv.Contains(1029))
			result += GameData.mAwake[1029].power;

		return result;
	}



	public static int GetShardBiforestCycle()
	{
		if (GameData.all_ShardBeforest == 0)
		{
			return 0;
		}
		return GameData.SHARD_BIFOREST_CYCLE;
	}
	public static bool GetShardBiforestJudge()
	{
		//仮
		int r = 10000;
		int pop_line = GameData.SHARD_BIFOREST_POP_BASE;


		//ラグナロクスキル
		double RagnarokSkill_Shard_pop = 0;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_16])
			RagnarokSkill_Shard_pop += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_16].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_17])
			RagnarokSkill_Shard_pop += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_17].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_18])
			RagnarokSkill_Shard_pop += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_18].power;

		int rr = UnityEngine.Random.RandomRange(0, r);
		bool j = rr < (100 * (pop_line + RagnarokSkill_Shard_pop));

		if (GameData.all_ShardBeforest == 0)
		{
			j = true;
		}
		//#if UNITY_EDITOR
		//        j = true;
		//#endif
		Debug.Log($"GetShardBiforestJudge {rr} {j} {pop_line} {RagnarokSkill_Shard_pop}");
		return j;
	}
	public static int GetShardBiforestStay()
	{
		int n = GameData.SHARD_BIFOREST_STAY;

		//ラグナロクスキル
		double RagnarokSkill_StayTime = 0;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_51])
			RagnarokSkill_StayTime += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_51].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_52])
			RagnarokSkill_StayTime += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_52].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_53])
			RagnarokSkill_StayTime += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_53].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_20])
			RagnarokSkill_StayTime += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_20].power;

		double result = (double)n * (1 + (0.01 * RagnarokSkill_StayTime));

		//Debug.Log($"GetShardBiforestStay {result} {n} {RagnarokSkill_StayTime}");

		return (int)result;
	}
	public static int GetShardBiforestLimit()
	{
		return UnityEngine.Random.RandomRange(GameData.SHARD_BIFOREST_CYCLE_MIN, GameData.SHARD_BIFOREST_CYCLE_MAX);
	}
	public static int GetShardBiforestBoostTime()
	{
		int result = GameData.SHARD_BIFOREST_BOOST_TIME_BASE;

		double n = 0;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_20])
			n += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_20].power;

		result = (int)((double)result * (1 + (0.01 * n)));
		//Debug.Log($"GetShardBiforestBoostTime {result} {n}");

		return result;
	}






	public static double GetSeedNum(out float par)
	{
		string format = "F5";
		bool flg_par_cal = true;
		par = 0f;
		double result = 0;
		double p = GameData.total_point;
		double calc_base = 0;
		calc_base = double.Parse("100000000000000");
		if (p >= calc_base)
		{
			double a = Math.Floor(p / calc_base);
			if (a >= 500)
			{
				a = 500;
				flg_par_cal = true;
			}
			p -= (a * calc_base);
			result += a;
			if (p <= calc_base)
			{
				par = float.Parse((p / calc_base).ToString(format));
				flg_par_cal = false;
			}
			//Debug.Log($"a {GameData.total_point} result:{result} a:{a} p:{p}");
		}
		else if (flg_par_cal)
		{
			par = float.Parse((p / calc_base).ToString(format));
			flg_par_cal = false;
		}
		calc_base = double.Parse("1000000000000000");
		if (p >= calc_base)
		{
			double a = Math.Floor(p / calc_base);
			if (a >= 500)
			{
				a = 500;
				flg_par_cal = true;
			}
			p -= (a * calc_base);
			result += a;
			if (p <= calc_base)
			{
				par = float.Parse((p / calc_base).ToString(format));
				flg_par_cal = false;
			}
			//Debug.Log($"b {GameData.total_point} result:{result} a:{a} p:{p} par:{par}");
		}
		else if (flg_par_cal)
		{
			par = float.Parse((p / calc_base).ToString(format));
			flg_par_cal = false;
		}
		calc_base = double.Parse("10000000000000000");
		if (p >= calc_base)
		{
			double a = Math.Floor(p / calc_base);
			if (a >= 1000)
			{
				a = 1000;
				flg_par_cal = true;
			}
			p -= (a * calc_base);
			result += a;
			if (p <= calc_base)
			{
				par = float.Parse((p / calc_base).ToString(format));
				flg_par_cal = false;
			}
			//Debug.Log($"c {GameData.total_point} result:{result} a:{a} p:{p} par:{par}");
		}
		else if (flg_par_cal)
		{
			par = float.Parse((p / calc_base).ToString(format));
			flg_par_cal = false;
		}
		calc_base = double.Parse("100000000000000000");
		if (p >= calc_base)
		{
			double a = Math.Floor(p / calc_base);
			if (a >= 8000)
			{
				a = 8000;
				flg_par_cal = true;
			}
			p -= (a * calc_base);
			result += a;
			if (p <= calc_base)
			{
				par = float.Parse((p / calc_base).ToString(format));
				flg_par_cal = false;
			}
			//Debug.Log($"d {GameData.total_point} result:{result} a:{a} p:{p} par:{par}");
		}
		else if (flg_par_cal)
		{
			par = float.Parse((p / calc_base).ToString(format));
			flg_par_cal = false;
		}
		calc_base = double.Parse("100000000000000000000");
		if (p >= calc_base)
		{
			double a = Math.Floor(p / calc_base);
			p -= (a * calc_base);
			result += a;
			par = float.Parse((p / calc_base).ToString(format));
			//Debug.Log($"e {p}");
			//Debug.Log($"e {calc_base}");
			//Debug.Log($"e {(p / calc_base).ToString(format)}");
			//Debug.Log($"e {par}");
			//Debug.Log($"e {GameData.total_point} result:{result} a:{a} p:{p} par:{par}");
		}
		else if (flg_par_cal)
		{
			par = float.Parse((p / calc_base).ToString(format));
			flg_par_cal = false;
		}
		//Debug.Log($"e {GameData.total_point} result:{result} par:{par}");

		//double result = Math.Floor(GameData.total_point / GameData.SEED_RATE);
		if (!CalcData.CheckAwakeToMode(GameData.AwakeModeNum.RAGNAROK))
		{
			result = 0;
		}

		if (result >= double.MaxValue) result = double.MaxValue;
		return result;
	}




	public static int GetGungnirTime()
	{
		double result = GameData.GUNGNIR_CYCLE;

		double RagnarokSkill_Time = 0;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_28])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_28].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_29])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_29].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_30])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_30].power;

		result = result * (1 + (0.01 * RagnarokSkill_Time));
		//Debug.Log($"GetGungnirTime {(int)result} {RagnarokSkill_Time}");

		if (GameData.all_gungnir == 0)
		{
			return 1;
		}

		return (int)result;
	}
	public static int GetGungnirPowerTime()
	{
		int result = GameData.GUNGNIR_POWER_TIME;

		double RagnarokSkill_Time = 0;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_31])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_31].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_32])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_32].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_33])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_33].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_34])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_34].power;

		result += (int)(RagnarokSkill_Time * 60);
		//Debug.Log($"GetGungnirPowerTime {result} {(int)(RagnarokSkill_Time * 60)}");
		return result;
	}
	public static double GetGungnirPower()
	{
		double result = GameData.GUNGNIR_POW;

		double RagnarokSkill_Time = 0;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_35])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_35].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_36])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_36].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_37])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_37].power;

		result = result * (1 + (0.01 * RagnarokSkill_Time));
		//Debug.Log($"GetGungnirPower {(int)result} {RagnarokSkill_Time}");

		return result;
	}


	public static double GetOuroborosPow()
	{
		double result = 1;

		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_1])
			result = (1 + (0.01 * GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_1].power * GameData.Total_SeedYggdrasil));


		if (result >= GameData.OUROBOROS_POW_MAX)
		{
			result = GameData.OUROBOROS_POW_MAX;
		}

		//Debug.Log($"GetOuroborosPow {result}倍  {GameData.Total_SeedYggdrasil}");

		return result;
	}



	public static int GetWisdomTime()
	{
		double result = GameData.WISDOM_CYCLE;

		double RagnarokSkill_Time = 0;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_66])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_66].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_67])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_67].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_68])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_68].power;

		result = result * (1 + (0.01 * RagnarokSkill_Time));
		//Debug.Log($"GetWisdomTime {(int)result} {RagnarokSkill_Time}");

		if (GameData.all_wisdom == 0)
		{
			return 1;
		}
		return (int)result;
	}
	public static int GetWisdomPowerTime()
	{
		int result = GameData.WISDOM_POWER_TIME;

		double RagnarokSkill_Time = 0;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_39])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_39].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_40])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_40].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_41])
			RagnarokSkill_Time += GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_41].power;

		result += (int)(RagnarokSkill_Time * 60);
		//Debug.Log($"GetWisdomPowerTime {result} {(int)(RagnarokSkill_Time * 60)}");

		return result;
	}
	public static double GetWisdomClickCycle()
	{
		double result = GameData.WISDOM_CLICK_FRAME * GameData.WISDOM_CLICK_FRAME_MAGNIFICATION;
		return result;
	}

	public static double GetGoddessPow()
	{
		double result = (GameData.GODDESS_POW * (GameData.GoddessOpen.Count + GameData.feverSpriteOpen.Count - 2));
		return result;
	}



	public static bool CheckAwakeToMode(GameData.AwakeModeNum awakeType)
	{
#if UNITY_EDITOR
		//if (awakeType == GameData.AwakeModeNum.RAGNAROK)
		//{
		//    return true;
		//}
#endif
		return true;
	}
}
