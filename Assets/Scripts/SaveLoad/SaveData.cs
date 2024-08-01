using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //C:\Users\PC-0098-Admin\AppData\LocalLow\NEOLAVI\YGGDRASILL

    //セーブ対象
    public int story = 0;
    public float AudioMasterVolume = 0.5f;
    public float SeVolume = 0.5f;
    public int AudioPlayMode = 0;
    public int AudioMute = 0;
    public int GamePhase;
    public double click;
    public double feverClick;
    public double all_click;
    public double point;
    public double total_point;
    public double all_point;
    public double fruit;
    public double total_fruit;
    public double all_fruit;
    public double all_fruit_use;
    public double all_ragnarok;
    public int language;
    public int treeIndex;
    public int feverSpriteIndex;
    public double all_ShardBeforest;
    public double all_Warp;
    public double all_Stella;
    public double total_ShardBeforest;
    public double total_Warp;
    public double total_Stella;
    public double total_gungnir;
    public double total_wisdom;
    public double all_gungnir;
    public double all_wisdom;
    public double climaxIndex;

	public List<int> InstLv = new List<int>() {
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
    };
    public List<double> InstFruitLv = new List<double>() {
            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
    };
    public List<Vector3> LeafPos = new List<Vector3>();
    public int BgmSelect;
    public int FragmentStellaCycleCnt;
    public int FragmentStellaStayCnt;
    public int ShardBiforest;
    public int ShardBiforestStayCnt;
    public int ShardBiforestCycleCnt;
    public int ShardBiforestCycleCntLimit;
    public int ShardBiforestBoost;
    public int FruitCycleCnt;
    public double SeedYggdrasil = 0;
    public double Total_SeedYggdrasil = 0;
    public List<bool> RagnarokSkill = new List<bool>()
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
    public List<int> AwakeLv = new List<int>();
    public double nowtime = 0;
    public double starttime = 0;
    public double firststarttime = 0;
    public double longesttime = 0;
    public double playtime = 0;
    public double all_playtime = 0;
    public int GungnirCycleCnt = 0;
    public int GungnirPowerTime = 0;
    public int WisdomCycleCnt = 0;
    public int WisdomPowerTime = 0;
    public int WisdomClickCnt = 0;

    public int GoddessView = 0;
    public int GoddessViewIndex = 0;
    public List<int> GoddessOpen = new List<int>() { 0 };
    public List<int> feverSpriteOpen = new List<int>() { 0 };
}