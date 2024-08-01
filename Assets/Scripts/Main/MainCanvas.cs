using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Coffee.UIEffects;
using naichilab.Scripts.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class MainCanvas : CanvasBase
{
	[SerializeField] private Canvas ParentCanvas;
	[SerializeField] private Text textUserPoint;
	[SerializeField] private Text textUserFruit;

	private string _textUserPoint = "";
	private int intervalSave = 0;
	private int intervalSave_max = 60;
	private Vector3 _mousePosition = Vector3.one;

	[SerializeField] private PostProcessVolume postProcessVolume;

	[SerializeField] private GameObject Wood;
	[SerializeField] private GameObject GameModeSeed;
	[SerializeField] private GameObject GameModeTree;
	[SerializeField] private GameObject GameModeRagnarok;

	[SerializeField] private GameObject SeedClickText;
	[SerializeField] private Animator SeedObj;
	[SerializeField] private RuntimeAnimatorController SeedAnimFront;
	[SerializeField] private RuntimeAnimatorController SeedAnimAfter;
	[SerializeField] private Animator WoodAnimator;
	[SerializeField] private RuntimeAnimatorController WoodClickAnim;
	[SerializeField] private RuntimeAnimatorController WoodAwakeAnim;
	[SerializeField] private GameObject[] leafArea;
	[SerializeField] private GameObject leafPrefab;
	[SerializeField] private GameObject leafParent;
	[SerializeField] private List<InstButtonManager> instButtonManager;
	[SerializeField] private Text ppsText;
	[SerializeField] private GameObject bgmButton;
	[SerializeField] private BgmButtonGroupManager bgmButtonGroupManager;
	[SerializeField] public MusicPlayerManager musicPlayermanager;
	[SerializeField] private GameObject musicPlayerObj;
	//[SerializeField] private FragmentStellaManager fragmentStellaManager;
	[SerializeField] private ShardBiforestManager shardBiforestManager;
	[SerializeField] private GameObject shardBiforestObj;
	//[SerializeField] private FruitManager fruitManager;
	[SerializeField] private List<Image> AudioVolumeBar;
	[SerializeField] private List<Image> SfxVolumeBar;
	[SerializeField] private List<TabButtonController> ButtonMuteImages;
	[SerializeField] private ResetGameManager resetGameManager;
	[SerializeField] private AwakeManager awakeManager;
	[SerializeField] private List<GameObject> InstViewButtons;
	[SerializeField] private GameObject ButtonSettingGroup;// 設定
	[SerializeField] private GameObject ButtonSuperFastGroup;
	[SerializeField] private GameObject ButtonRecordGroup;// レコード
	[SerializeField] private GameObject YggdrasilGroup;
	[SerializeField] private Image YggdrasilSeedBar;
	[SerializeField] private Text YggdrasilNum;
	[SerializeField] private GameObject ragnarokButton;
	[SerializeField] private RagnarokSkillManager ragnarokSkillManager;
	[SerializeField] private GameObject FruitInfoWindow;
	[SerializeField] private GameObject NightStellaInfoWindow;
	[SerializeField] private GameObject GungnirInfoWindow;
	[SerializeField] private GameObject WisdomInfoWindow;
	[SerializeField] private GameObject RagnarokInfoWindow;
	//[SerializeField] private GameObject ButtonModeFruit;
	[SerializeField] private List<GameObject> ButtonMultis;
	[SerializeField] private SkinSelectManager skinSelectManager;
	[SerializeField] private GameObject ButtonSkin;

	[SerializeField] private GameObject ButtonGungnirObj;
	[SerializeField] private GameObject ButtonGungnirObjBg;
	[SerializeField] private Image ButtonGungnirObjImage;
	[SerializeField] private Button ButtonGungnirObjButton;
	[SerializeField] private Text GungnirTimeText;

	[SerializeField] private GameObject ButtonWisdomObj;
	[SerializeField] private GameObject ButtonWisdomObjBg;
	[SerializeField] private Image ButtonWisdomObjImage;
	[SerializeField] private Button ButtonWisdomObjButton;
	[SerializeField] private Text WisdomTimeText;

	[SerializeField] private GameObject NightStellaGroup;
	[SerializeField] private Text NightStellaText;

	[SerializeField] private TreeManager treeManager;

	[SerializeField] private PopupMessageManager popupMessageManager;
	[SerializeField] private PopUpWindowController settingSystemPopUpWindowController;
	[SerializeField] private List<GameObject> LanguageButtons;

	[SerializeField] private Transform SpriteSun;
	[SerializeField] private Transform SpriteMoon;
	[SerializeField] private float test_h = 0f;

	[SerializeField] private IconLoopManager iconLoopManager;
	[SerializeField] private LegendsIconManager legendsIconManager;

	[SerializeField] private AudioClip SE_Awake;
	[SerializeField] private AudioClip SE_BiforestPopup;
	[SerializeField] private AudioClip SE_Ragnarok;
	[SerializeField] private AudioClip SE_RagnarokOn;
	[SerializeField] private AudioClip SE_Inst;
	[SerializeField] private AudioClip SE_StellaPopup;
	[SerializeField] private AudioClip SE_AchievementGet;
	[SerializeField] private AudioClip SE_BiforestGet;
	[SerializeField] private AudioClip SE_FruitGet;
	[SerializeField] private AudioClip SE_StellaGet;

	[SerializeField] private AudioClip SE_CanNotSelectButton;

	//[SerializeField] private DirectionalLightManager directionalLightManager;

	private bool isInit = false;
	private Dictionary<int, Coroutine> InstCoroutinePointa = new Dictionary<int, Coroutine>();

	private bool isGameStop = false;
	private const float ppsSpan = 1f;
	private float currentTime = ppsSpan;
	private double ppsPool = 0;
	private const double FPS = 0.0166666666666667;
	private const float FontEffectRandAdjPos = 5f;
	private int _instAllLv = -1;
	private int _ButtonLvupMulti = 1;
	private bool _ModeUseFruit = false;
#if UNITY_EDITOR
	private bool debugMode = false;
#else
    private bool debugMode = false;
#endif
	private bool debugAutClick = false;
	private int debugAutClickCnt = 0;
	private int debugAutClickCntCycle = 1;

	private int lightIndex = 0;

	public string stringPPS;
	public int total_inst_lv;
	public int total_awaked
	{
		get { return awakeManager.GetTotalAwaked(); }
	}

	// true = オート
	public bool isAuto = false;

	// true = 超速モード
	public bool isSuperFast = false;

	//デバッグ
	public void ButtonDebug()
	{
#if UNITY_EDITOR
		AddPoint(GameData.ClickCase.CLICK, GameData.DEBUG_POINT);

		//GameData.SeedYggdrasil += 1;
		//GameData.Total_SeedYggdrasil += 1;

		//GameData.firststarttime = 1;

		AddFruit(10);
		popupMessageManager.SetMessage("デバッグ用です\nデバッグ用です");

		//popupMessageManager.SetMessage("音テスト", 1);
#endif
	}
	/// <summary>
	/// デバッグ用自動クリック
	/// </summary>
	private void UpdateDebugAutoClick()
	{
		if (!debugAutClick) return;

		debugAutClickCnt++;
		if (debugAutClickCnt >= debugAutClickCntCycle)
		{
			debugAutClickCnt = 0;

			//自動クリック
			MainClick();
		}
	}
	public void ButtonDebugAutoClick()
	{
		debugAutClick = !debugAutClick;
	}
	public void ButtonDebugSeed()
	{
#if UNITY_EDITOR
		GameData.SeedYggdrasil += 1000000000;
		GameData.Total_SeedYggdrasil += 1000000000;
#endif
	}









	public override void Start()
	{
		base.Start();


		//Debug.Log($"double:{double.MaxValue.ToString()}");
		//Debug.Log($"double:{double.MaxValue.ToString("0")}");
		//Debug.Log($"decimal:{decimal.MaxValue.ToString()}");
		//Debug.Log($"decimal:{decimal.MaxValue.ToString("0")}");

	}

	public override void OnInit()
	{
		base.OnInit();

		//Debug.Log("Main OnInit");

		//StartCoroutine(CorOnInit());

		switch (GameData.GameModePhase)
		{
		case GameData.GAME_MODE_SEED:
			SoundManager.Instance.AllStop();
			SeedClickText.SetActive(true);
			SeedObj.runtimeAnimatorController = SeedAnimFront;
			GameModeSeed.SetActive(true);
			GameModeTree.SetActive(false);
			GameModeRagnarok.SetActive(false);

			initSetting();
			break;
		case GameData.GAME_MODE_TREE:
			musicPlayermanager.Init(GameData.BgmSelect, GameData.AudioPlayMode);

			GameModeSeed.SetActive(false);
			GameModeTree.SetActive(true);
			GameModeRagnarok.SetActive(false);
			break;
		case GameData.GAME_MODE_RAGNAROK:
			SoundManager.Instance.AllStop();
			//musicPlayermanager.PlayBGMRagnarok();

			GameModeSeed.SetActive(false);
			GameModeTree.SetActive(false);
			GameModeRagnarok.SetActive(true);

			initSetting();
			break;
		}


	}

	//private IEnumerator CorOnInit()
	//{
	//    //一旦どのフェーズでもなくする
	//    int tmp_phase = GameData.GameModePhase;
	//    GameData.GameModePhase = -1;

	//    loadingManager.On();
	//    yield return new WaitForSeconds(0.1f);

	//    //画面を消す
	//    GameModeSeed.SetActive(false);
	//    GameModeTree.SetActive(false);
	//    GameModeRagnarok.SetActive(false);

	//    //BGMロード
	//    bool wait = true;
	//    int max = 0;
	//    //StartCoroutine(musicPlayermanager.corLoadCustomSounds(() => {
	//    //    wait = false;
	//    //},
	//    //(_max) => {
	//    //    max = _max;
	//    //    loadingManager.Progress(0);
	//    //},
	//    //(_progress) => {
	//    //    loadingManager.Progress((float)_progress / (float)max);
	//    //}));

	//    _ = musicPlayermanager.taskLoadCustomSounds(() =>
	//    {
	//        wait = false;
	//    },
	//    (_max) =>
	//    {
	//        max = _max;
	//        loadingManager.Progress(0);
	//    },
	//    (_progress) =>
	//    {
	//        loadingManager.Progress((float)_progress / (float)max);
	//    });

	//    while (wait) yield return null;

	//    //画面を写す
	//    GameData.GameModePhase = tmp_phase;
	//    switch (GameData.GameModePhase)
	//    {
	//        case GameData.GAME_MODE_SEED:
	//            SoundManager.Instance.AllStop();
	//            SeedClickText.SetActive(true);
	//            SeedObj.runtimeAnimatorController = SeedAnimFront;
	//            GameModeSeed.SetActive(true);
	//            GameModeTree.SetActive(false);
	//            GameModeRagnarok.SetActive(false);

	//            initSetting();
	//            break;
	//        case GameData.GAME_MODE_TREE:
	//            musicPlayermanager.Init(GameData.BgmSelect);

	//            GameModeSeed.SetActive(false);
	//            GameModeTree.SetActive(true);
	//            GameModeRagnarok.SetActive(false);
	//            break;
	//        case GameData.GAME_MODE_RAGNAROK:
	//            SoundManager.Instance.AllStop();

	//            GameModeSeed.SetActive(false);
	//            GameModeTree.SetActive(false);
	//            GameModeRagnarok.SetActive(true);

	//            initSetting();
	//            break;
	//    }


	//    yield return new WaitForSeconds(0.5f);
	//    loadingManager.Off();

	//    yield break;
	//}


	//表示物初期化
	private void MainCanvasInit()
	{
		if (isInit) return;
		//if (!GameData.AwakeLv.Contains(1))
		//{
		//	GameData.AwakeLv.Add(1);
		//}
		//GameData.mAwake[1].power = GetInstLv(1);
		//if (GameData.mAwake[1].power == 0)
		//{
		//	GameData.mAwake[1].power = 1;
		//}

		//施設整える
		foreach (int instCase in Enum.GetValues(typeof(GameData.InstCase)))
		{
			SetInst(instCase);
		}
		//iconLoopManager.Init();
		//legendsIconManager.Init();

		//treeManager.Init(GameData.treeIndex);
		ParticleManager.Instance.AllStop();
		ppsText.text = "";
		AddPoint(GameData.ClickCase.NONE);
		AddFruit(0);
		CalcData.ResetInit();
		bgmButton.GetComponent<Button>().interactable = CalcData.CheckAwakeToMode(GameData.AwakeModeNum.BGM);
		//bgmButtonGroupManager.ButtonClose();//廃止予定
		//bgmButtonGroupManager.PlayBGM(GameData.BgmSelect);//廃止予定
		musicPlayerObj.SetActive(CalcData.CheckAwakeToMode(GameData.AwakeModeNum.BGM));
		RemoveGrayScale(YggdrasilGroup, CalcData.CheckAwakeToMode(GameData.AwakeModeNum.RAGNAROK));
		//ragnarokButton.GetComponent<Button>().interactable = CalcData.CheckAwakeToMode(GameData.AwakeModeNum.RAGNAROK);
		//fragmentStellaManager.RemoveFragment();
		shardBiforestManager.ViewShardBiforestUI();
		RemoveGrayScale(shardBiforestObj, CalcData.CheckAwakeToMode(GameData.AwakeModeNum.SHARD_BEFOREST));
		resetGameManager.Hide();
		skinSelectManager.ResetUI();
		skinSelectManager.ViewImage(GameData.GoddessView == 1);
		skinSelectManager.SetSprite(GameData.treeIndex, isInit);
		skinSelectManager.Hide();
		RemoveGrayScale(ButtonSkin, CalcData.CheckAwakeToMode(GameData.AwakeModeNum.GODDESS));
		//ButtonGungnirObj.GetComponent<Button>().interactable = GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_27];
		//ButtonWisdomObj.GetComponent<Button>().interactable = GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_38];
		RemoveGrayScale(ButtonGungnirObj, GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_27]);
		RemoveGrayScale(ButtonGungnirObjBg, GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_27]);
		RemoveGrayScale(ButtonWisdomObj, GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_38]);
		RemoveGrayScale(ButtonWisdomObjBg, GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_38]);
		ClickFontEffectManager.Instance.AllReset();
		_instAllLv = -1;
		InstWindowView(0);
		awakeManager.UpdateAwakeButtonView();
		awakeManager.HideInfoWindow();
		//fruitManager.RemoveFruit();
		OnExitHideFruitInfo();
		OnExitHideNightStellaInfo();
		OnExitHideGungnirInfo();
		OnExitHideWisdomInfo();
		OnExitHideRagnarokInfo();
		ButtonChangeButtonMulti(1);
		ChangeUseFruit(false);
		FeverManager.Instance.Init();
		isSuperFast = false;
		GameData.WISDOM_CLICK_FRAME_MAGNIFICATION = 1;
		isAuto = false;
		Wood.GetComponent<TreeManager>().Init(GameData.treeIndex);
		ButtonSuperFastGroup.SetActive(false);
		ButtonSuperFastGroup.SetActive(true);
		BookShelfManager.Instance.Init();
		FeverManager.Instance.FeverEnd();

		initSetting();

		isInit = true;
		// 言語変更
		ButtonChangeLanguage(GameData.language);
		//OpenAwake(1);
	}

	private void initSetting()
	{
		//setting関係こちら
		UpdateAudioVolume(0f);
		UpdateSfxVolume(0f);
		SetButtonMute();
		OpenSettingWindow(false);
		OpenRecordWindow(false);
		ButtonChangeLanguage(GameData.language);
	}

	void Update()
	{
		//GameData.GameModePhase = 0;
		switch (GameData.GameModePhase)
		{
		case GameData.GAME_MODE_SEED:
		case GameData.GAME_MODE_RAGNAROK:
			break;
		case GameData.GAME_MODE_TREE:
			if (!isGameStop)
			{
				if (!isInit)
				{
					MainCanvasInit();

					//復帰時ポイント獲得
					double diff_sec = 0;
					double resume_point = CalcData.GetResumePoint(out diff_sec);
					if (resume_point > 0)
					{
						AddPoint(GameData.ClickCase.CENTOR, resume_point);

					}
					if (diff_sec > 0)
					{
						int add_num = (int)diff_sec * 60;
						GameData.FragmentStellaCycleCnt += add_num;
						GameData.ShardBiforestCycleCnt += add_num;
						GameData.ShardBiforestStayCnt += add_num;
						GameData.FruitCycleCnt += add_num;
						GameData.GungnirCycleCnt += add_num;
						GameData.WisdomCycleCnt += add_num;
						//Debug.Log($"diff_sec:{diff_sec} add_num:{add_num}");
					}
				}

				UpdatePPS();

				UpdateFragmentStella();

				UpdateFruit();

				UpdateShardBiforest();

				UpdateShardBiforestBoostTime();

				UpdateGungnirTime();

				UpdateWisdomTime();

				UpdateNightStella();

				UpdateDebugAutoClick();

				AutoSave();

				UpdateKeyEvent();
			}
			break;
		}


		UpdateEnviroment();

	}

	private void UpdateKeyEvent()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			ButtonGameExit();
		}
	}


	/// <summary>
	/// 自動セーブ
	/// </summary>
	private void AutoSave()
	{
		intervalSave++;
		if (intervalSave >= intervalSave_max)
		{
			intervalSave = 0;

			if (GameData.starttime == 0)
			{
				GameData.starttime = Math.Round((double)DateTime.Now.Ticks / (1000 * 1000 * 10));
			}
			if (GameData.firststarttime == 0)
			{
				GameData.firststarttime = Math.Round((double)DateTime.Now.Ticks / (1000 * 1000 * 10));
			}

			GameData.nowtime = Math.Round((double)DateTime.Now.Ticks / (1000 * 1000 * 10));
			//Debug.Log($"GameData.nowtime:{GameData.nowtime}");

			GameData.playtime++;//何秒経過か
			GameData.all_playtime++;

			if (DataManager.Instance != null) DataManager.Instance.Save();
		}
	}

	/// <summary>
	/// PPS表示
	/// </summary>
	private void UpdatePPS()
	{
		currentTime += Time.deltaTime;
		if (currentTime > ppsSpan)
		{
			currentTime = 0f;

			total_inst_lv = 0;
			for (int instCase = 1; instCase < (GameData.INST_COST_BASE.Count); instCase++)
			{
				ppsPool += CalcData.GetInstPoint(instCase, GameData.InstLv[instCase]);
				total_inst_lv += GameData.InstLv[instCase];
			}

			ppsPool /= CalcData.GetAdjInstCycle();

			stringPPS = GetBigNumberString(Math.Round(ppsPool, 1));
			ppsText.text = $"{LanguageCSV.Instance.GetCSV(GameData.PPS_TEMPLATE)}{stringPPS}";

			//実績解除も兼ねる
			SendSteamAchivement(GameData.ACHIVEMENT_TYPE_PPS);
			SendSteamAchivement(GameData.ACHIVEMENT_TYPE_ALL_TIME);

			ppsPool = 0;
		}
	}

	/// <summary>
	/// ステラの欠片
	/// </summary>
	private void UpdateFragmentStella()
	{
		//ステラの欠片
		if (!CalcData.CheckAwakeToMode(GameData.AwakeModeNum.FRAGMENT_STELLA))
		{
			GameData.FragmentStellaCycleCnt = 0;
			//fragmentStellaManager.RemoveFragment();
			return;
		}

		GameData.FragmentStellaCycleCnt++;
#if UNITY_EDITOR
		if (debugMode) GameData.FragmentStellaCycleCnt = CalcData.GetFragmentStellaCycle();
#endif
		//Debug.Log($"GameData.FragmentStellaCycleCnt {GameData.FragmentStellaCycleCnt} CalcData.GetFragmentStellaCycle() {CalcData.GetFragmentStellaCycle()}");
		//if (GameData.FragmentStellaCycleCnt >= CalcData.GetFragmentStellaCycle())
		//{
		//if (!fragmentStellaManager.isFragmentStay())
		//{
		//	//確率で出現
		//	if (CalcData.GetFragmentStellaJudge())
		//	{
		//		//出現
		//		fragmentStellaManager.SetFragment();
		//		GameData.FragmentStellaStayCnt = 0;

		//		//SE
		//		SoundManager.Instance.PlaySe(SE_StellaPopup);
		//	}
		//	else
		//	{
		//		//再度待つ
		//		GameData.FragmentStellaCycleCnt = 0;
		//		fragmentStellaManager.RemoveFragment();
		//	}
		//}
		//else
		//{
		//	//出現済み
		//	GameData.FragmentStellaStayCnt++;
		//	if (GameData.FragmentStellaStayCnt >= CalcData.GetFragmentStellaStay())
		//	{
		//		//消える
		//		GameData.FragmentStellaCycleCnt = 0;
		//		fragmentStellaManager.RemoveFragment();
		//	}
		//}
		//}
	}

	/// <summary>
	/// ビフレストの欠片
	/// </summary>
	private void UpdateShardBiforest()
	{
		if (!CalcData.CheckAwakeToMode(GameData.AwakeModeNum.SHARD_BEFOREST))
		{
			GameData.ShardBiforestCycleCnt = 0;
			shardBiforestManager.RemoveShard();
			return;
		}

		//ビフレストの欠片
		if (GameData.ShardBiforest >= GameData.SHARD_BIFOREST_MAX || GameData.ShardBiforestBoost > 0)
		{
			//ワープ出現中
			//もしくはブースト中は出ない
			shardBiforestManager.RemoveShard();
		}
		else
		{
			if (GameData.ShardBiforestCycleCnt == 0 && GameData.ShardBiforestCycleCntLimit == 0)
			{
				//次の出現タイミングを決める
				GameData.ShardBiforestCycleCntLimit = CalcData.GetShardBiforestLimit();
			}
			if (GameData.ShardBiforestCycleCntLimit > 0)
			{
				GameData.ShardBiforestCycleCnt++;
				if (GameData.ShardBiforestCycleCnt >= GameData.ShardBiforestCycleCntLimit)
				{
					//出現
					shardBiforestManager.SetShard();
				}
			}

			GameData.ShardBiforestCycleCnt++;
#if UNITY_EDITOR
			if (debugMode) GameData.ShardBiforestCycleCnt = CalcData.GetShardBiforestCycle();
#endif
			//Debug.Log($"GameData.ShardBiforestCycleCnt {GameData.ShardBiforestCycleCnt} CalcData.GetShardBiforestCycle() {CalcData.GetShardBiforestCycle()}");
			if (GameData.ShardBiforestCycleCnt >= CalcData.GetShardBiforestCycle())
			{

				//Debug.Log($"GshardBiforestManager.isShardStay() {shardBiforestManager.isShardStay()} GameData.ShardBiforestStayCnt {GameData.ShardBiforestStayCnt}");
				if (!shardBiforestManager.isShardStay())
				{
					//確率で出現
					if (CalcData.GetShardBiforestJudge())
					{
						//出現
						shardBiforestManager.SetShard();
						GameData.ShardBiforestStayCnt = 0;

						//SE
						SoundManager.Instance.PlaySe(SE_BiforestPopup);
					}
					else
					{
						//再度待つ
						GameData.ShardBiforestCycleCnt = 0;
						shardBiforestManager.RemoveShard();
					}
				}
				else
				{
					//出現済み
					//Debug.Log($"GameData.ShardBiforestStayCnt {GameData.ShardBiforestStayCnt} CalcData.GetShardBiforestStay() {CalcData.GetShardBiforestStay()}");
					GameData.ShardBiforestStayCnt++;
					if (GameData.ShardBiforestStayCnt >= CalcData.GetShardBiforestStay())
					{
						//消える
						GameData.ShardBiforestCycleCnt = 0;
						shardBiforestManager.RemoveShard();
					}
				}
			}
			else
			{
				shardBiforestManager.RemoveShard();
			}

		}
	}

	/// <summary>
	/// ビフレストの欠片ブースト
	/// </summary>
	private void UpdateShardBiforestBoostTime()
	{
		if (GameData.ShardBiforestBoost > 0)
		{
			shardBiforestManager.StartBoostEffect();
			GameData.ShardBiforestBoost--;
			shardBiforestManager.UpdateBoostTime();

			if (GameData.ShardBiforestBoost == 0)
			{
				musicPlayermanager.PlayBGMBoost(false);
				FeverManager.Instance.FeverEnd();
			}
		}
		else
		{
			shardBiforestManager.EndBoostEffect();
		}
	}

	/// <summary>
	/// 生命の実
	/// </summary>
	private void UpdateFruit()
	{
		if (_instAllLv == -1)
		{
			CalcInstAllLv();

			//パーティクル
			ParticleManager.Instance.SetInstParticleCnt(_instAllLv);
		}

		if (!CalcData.CheckAwakeToMode(GameData.AwakeModeNum.FRUIT))
		{
			//fruitManager.RemoveFruit();
			return;
		}

		if (_instAllLv > 0 && _instAllLv >= GameData.FRUIT_OPEN_LV)
		{
			GameData.FruitCycleCnt++;
#if UNITY_EDITOR
			if (debugMode) GameData.FruitCycleCnt = CalcData.GetFruitCycle();
#endif
			//Debug.Log($"GameData.FruitCycleCnt {GameData.FruitCycleCnt} CalcData.GetFruitCycle() {CalcData.GetFruitCycle()}");
			//if ((GameData.FruitCycleCnt + CalcData.GetAdjFruitCycle()) >= CalcData.GetFruitCycle())
			//{
			//	if (!fruitManager.isFruitStay())
			//	{
			//		//出現
			//		fruitManager.SetFruit();
			//	}
			//}
		}
	}
	private void CalcInstAllLv()
	{
		_instAllLv = 0;
		foreach (int n in GameData.InstLv)
		{
			_instAllLv += n;
		}
		if (_instAllLv == GameData.FRUIT_OPEN_LV)
		{
			//リセットで開始
			GameData.FruitCycleCnt = 0;
		}

		//レベル可視化
		ShowInstFruitLv(_instAllLv >= GameData.FRUIT_OPEN_LV);


	}



	/// <summary>
	/// グングニルの使用可能時間
	/// </summary>
	private void UpdateGungnirTime()
	{
		if (debugMode || GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_27])
		{

			if (GameData.GungnirPowerTime <= 0)
			{
				GameData.GungnirCycleCnt++;
				if (debugMode)
				{
					//GameData.GungnirCycleCnt = CalcData.GetGungnirTime();
				}
				//Debug.Log($"GameData.GungnirCycleCnt {GameData.GungnirCycleCnt} CalcData.GetGungnirTime() {CalcData.GetGungnirTime()}");
				ButtonGungnirObjImage.fillAmount = (float)GameData.GungnirCycleCnt / (float)CalcData.GetGungnirTime();
				ButtonGungnirObjButton.enabled = ButtonGungnirObjImage.fillAmount >= 1f;
				ButtonGungnirObjButton.interactable = true;
				GungnirTimeText.text = "";
			}
			else
			{
				GameData.GungnirPowerTime--;
				ButtonGungnirObjButton.interactable = false;
				GungnirTimeText.text = $"{((float)GameData.GungnirPowerTime / 60f).ToString("f2")}";
			}
		}
		else
		{
			GungnirTimeText.text = "";
			ButtonGungnirObjImage.fillAmount = 0f;
		}
	}

	/// <summary>
	/// ミーミルの知識の使用可能時間
	/// </summary>
	private void UpdateWisdomTime()
	{
		if (debugMode || GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_38])
		{

			if (GameData.WisdomPowerTime <= 0)
			{
				GameData.WisdomCycleCnt++;
				if (debugMode)
				{
					//GameData.WisdomCycleCnt = CalcData.GetWisdomTime();
				}
				//Debug.Log($"GameData.WisdomCycleCnt {GameData.WisdomCycleCnt} CalcData.GetWisdomTime() {CalcData.GetWisdomTime()}");
				ButtonWisdomObjImage.fillAmount = (float)GameData.WisdomCycleCnt / (float)CalcData.GetWisdomTime();
				ButtonWisdomObjButton.enabled = ButtonWisdomObjImage.fillAmount >= 1f;
				ButtonWisdomObjButton.interactable = true;
				WisdomTimeText.text = "";
			}
			else
			{
				GameData.WisdomPowerTime--;
				ButtonWisdomObjButton.interactable = false;
				WisdomTimeText.text = $"{((float)GameData.WisdomPowerTime / 60f).ToString("f2")}";

				GameData.WisdomClickCnt++;
				if (GameData.WisdomClickCnt >= CalcData.GetWisdomClickCycle())
				{
					GameData.WisdomClickCnt = 0;

					//自動クリック
					MainClick(GameData.ClickCase.CENTOR);
				}
			}
		}
		else
		{
			WisdomTimeText.text = "";
			ButtonWisdomObjImage.fillAmount = 0f;
		}
		if (isAuto)
		{
			GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_38] = true;
			GameData.WisdomPowerTime = CalcData.GetWisdomPowerTime();
		}
	}


	private void UpdateNightStella()
	{

		//夜空のステラ計算
		if (GameData.AwakeLv.Contains(2004))
		{
			double diff_sec = Math.Round((double)DateTime.Now.Ticks / (1000 * 1000 * 10)) - GameData.firststarttime;
			GameData.NightStella = Math.Floor(diff_sec / GameData.NIGHT_STELLA_CYCLE);

			NightStellaText.text = $"{GameData.NightStella}";
		}
		else
		{
			GameData.NightStella = 0;
			NightStellaText.text = "";
		}
	}











	public void test_loading()
	{
		StartCoroutine(test_cor());
	}
	IEnumerator test_cor()
	{

		LoadingManager.Instance.On();

		yield return new WaitForSeconds(1f);


		LoadingManager.Instance.Off();

		yield break;
	}



	/// <summary>
	/// 進行止めたり
	/// </summary>
	public void isMainGameStop()
	{
		isGameStop = true;
	}
	public void isMainGameStopResume()
	{
		isGameStop = false;
	}


	/// <summary>
	/// データリセット
	/// </summary>
	public void ButtonResetData()
	{
		if (CalcData.CheckAwakeToMode(GameData.AwakeModeNum.RAGNAROK))
		{
			resetGameManager.Show();

			//SE
			SoundManager.Instance.PlaySe(SE_Ragnarok);
		}
		else
		{
			MessageManager.Instance.PlayMessage(new List<string>() { LanguageCSV.Instance.GetCSV(GameData.MESSAGE_CLOSE_MENU) });
		}
	}
	public void GameReset()
	{

		if (CalcData.GetSeedNum(out float _) == 0)
		{
			settingSystemPopUpWindowController.SetLabel(LanguageCSV.Instance.GetCSV(GameData.SEED_ZERO_CONFIRM));
			settingSystemPopUpWindowController.SetButtonAction(() =>
			{
				settingSystemPopUpWindowController.Hide();

				GameResetMethod();

			});
			settingSystemPopUpWindowController.Show();
		}
		else
		{
			GameResetMethod();
		}

	}

	private void GameResetMethod()
	{

		resetGameManager.Close();
		isMainGameStop();

		//SE
		SoundManager.Instance.PlaySe(SE_RagnarokOn);

		//演出入れるなら入れたい
		TransitionRuleController.Instance.On(() =>
		{


			isMainGameStopResume();


			//生命の実獲得
			float _par = 0;
			if (CalcData.GetSeedNum(out _par) > 0)
			{
				GameData.SeedYggdrasil += CalcData.GetSeedNum(out _par);
				GameData.Total_SeedYggdrasil += CalcData.GetSeedNum(out _par);
			}
			GameData.all_ragnarok++;

			//最大プレイ時間
			double ptime = GameData.nowtime - GameData.starttime;
			if (GameData.longesttime < ptime)
			{
				GameData.longesttime = ptime;
			}

			//実績
			SendSteamAchivement(GameData.ACHIVEMENT_TYPE_CLICK);
			SendSteamAchivement(GameData.ACHIVEMENT_TYPE_POINT);
			SendSteamAchivement(GameData.ACHIVEMENT_TYPE_TIME);
			SendSteamAchivement(GameData.ACHIVEMENT_TYPE_RAGNAROK);

			//データリセット
			GameData.ResetSaveData();

			InstObjectReset();

			isInit = false;
			MainCanvasInit();
			AddPoint(GameData.ClickCase.NONE);

			ppsPool = 0;
			currentTime = ppsSpan;

			//fragmentStellaManager.RemoveFragment();

			shardBiforestManager.RemoveShard();
			shardBiforestManager.ViewShardBiforestUI();
			shardBiforestManager.EndBoostEffect();



			OpenRagnarokPhase();
			OnInit();

			DataManager.Instance.Save();





			TransitionRuleController.Instance.Off(() =>
			{
			}, 0, 2);
		}, 0, 2);
	}

	private void InstObjectReset()
	{
		foreach (Transform child in leafParent.transform)
		{
			Destroy(child.gameObject);
		}
	}


	public void OpenRagnarokPhase()
	{
		GameData.GameModePhase = GameData.GAME_MODE_RAGNAROK;
	}
	public void CloseRagnarokPhase()
	{
		OpenSettingWindow(false);

		//演出入れるなら入れたい
		TransitionRuleController.Instance.On(() =>
		{






			GameData.GameModePhase = GameData.GAME_MODE_TREE;

			SetTreeIndex();

			//グングニルとラグナロクはラグナロク後使える
			if (debugMode || GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_27]) GameData.GungnirCycleCnt = CalcData.GetGungnirTime();
			if (debugMode || GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_38]) GameData.WisdomCycleCnt = CalcData.GetWisdomTime();

			//セーブ
			DataManager.Instance.Save();

			isInit = false;
			OnInit();





			TransitionRuleController.Instance.Off(() =>
			{
			}, 0, 2);
		}, 0, 2);



	}






	/// <summary>
	/// メイン領域クリック
	/// </summary>
	public void ButtonMainClick()
	{
		MainClick();

		//木を光らせる
		//WoodAnimator.runtimeAnimatorController = null;
		//WoodAnimator.runtimeAnimatorController = WoodClickAnim;

		//パーティクル
		ParticleManager.Instance.PlayParticle_MouseClick(GetParticleMousePos(Input.mousePosition));
	}
	/// <summary>
	/// メイン領域クリック 種を植える
	/// </summary>
	public void ButtonMainClickSeed()
	{
		OpenSettingWindow(false);
		SeedClickText.SetActive(false);
		SeedObj.runtimeAnimatorController = SeedAnimAfter;

		//演出入れるなら入れたい
		TransitionRuleController.Instance.On(() =>
		{

			GameData.GameModePhase = GameData.GAME_MODE_TREE;

			SetTreeIndex();

			//セーブ
			DataManager.Instance.Save();


			OnInit();

			TransitionRuleController.Instance.Off(() =>
			{

				//ストーリー
				Story(0);

			}, 0, 2);
		}, 0, 2);
	}
	private void SetTreeIndex()
	{
		//GameData.treeIndex = UnityEngine.Random.RandomRange(0, treeManager.TreeNum());
		treeManager.Init(GameData.treeIndex);
	}






	/// <summary>
	/// クリックでポイント
	/// </summary>
	private void MainClick(GameData.ClickCase clickCase = GameData.ClickCase.CLICK)
	{
		int num = 1;

		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_21])
			num += (int)GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_21].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_22])
			num += (int)GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_22].power;
		if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_50])
			num += (int)GameData.mYggdrasilSkill[(int)GameData.RagnarokSkillIndex.SKILL_50].power;

		for (int i = 0; i < num; i++)
		{
			GameData.click++;
			GameData.all_click++;
			if (FeverManager.Instance.IsFever)
			{
				++GameData.feverClick;
				GameData.climaxIndex = GameData.feverClick / 100;
			}
			// 表情変更
			var treeManager = Wood.GetComponent<TreeManager>();
			//if (!FeverManager.Instance.IsFever)
			//{
			//	treeManager.FacialExpressionChange(UnityEngine.Random.Range(0, treeManager.FacialExpressionNum()));
			//}
			awakeManager.UpdateAwakeButtonView();
			AddPoint(clickCase, CalcData.GetCalcClickPoint(), i);

			// フィーバーまでの値を追加
			FeverManager.Instance.AddProgress(1);

			//if (!FeverManager.Instance.IsFever)
			//{
			// 木を揺らす
			//Wood.GetComponent<ITree>().Sway();
			//}

			//パーティクル
			ParticleManager.Instance.PlayParticle_Click(1);

			if (GameData.all_click == 1 || (GameData.all_click % 10 == 0))
			{
				//さすがにかわいそうなので10回に1回の送信にする
				SendSteamAchivement(GameData.ACHIVEMENT_TYPE_CLICK);
			}
		}

		//ストーリー

		//switch (GameData.story)
		//{
		//case 1:
		//	if (GameData.all_click >= 3)
		//	{
		//		Story(1); //３回ほどクリック
		//	}
		//	break;
		//case 2:
		//	if (GameData.total_point >= GameData.GrayScaleLimit)
		//	{
		//		Story(2); //色が戻る
		//	}
		//	break;
		//}


	}
	/// <summary>
	/// ステラの欠片でポイント
	/// </summary>
	public void GetFragmentStella()
	{
		GameData.total_Stella++;
		GameData.all_Stella++;
		AddPoint(GameData.ClickCase.FRAGMENT_STELLA, CalcData.GetFragmentStellaPoint());

		SendSteamAchivement(GameData.ACHIVEMENT_TYPE_FRAGMENT);

		//パーティクル
		ParticleManager.Instance.PlayParticle_Fragment(GetParticleMousePos(Input.mousePosition));

		//SE
		SoundManager.Instance.PlaySe(SE_StellaGet);
	}

	/// <summary>
	/// ビフレストの欠片クリック時のその他処理
	/// </summary>
	public void ClickBeforest()
	{
		GameData.total_ShardBeforest++;
		GameData.all_ShardBeforest++;

		SendSteamAchivement(GameData.ACHIVEMENT_TYPE_BEFOREST);

		//パーティクル
		ParticleManager.Instance.PlayParticle_Beforest(GetParticleMousePos(Input.mousePosition));

		//SE
		SoundManager.Instance.PlaySe(SE_BiforestGet);
	}
	/// <summary>
	/// ワープでポイント加算
	/// </summary>
	public void GetShardBiforestWarp()
	{
		GameData.total_Warp++;
		GameData.all_Warp++;
		AddPoint(GameData.ClickCase.SHARD_BIFOREST_WARP, CalcData.GetShardBiforestWarpPoint());

		//パーティクル
		ParticleManager.Instance.PlayParticle_Warp(Wood.transform.position);

		//SE
		SoundManager.Instance.PlaySe(SE_BiforestGet);
	}
	/// <summary>
	/// 生命の実獲得
	/// </summary>
	public void GetFruit()
	{
		AddFruit(CalcData.GetFruitNum());

		SendSteamAchivement(GameData.ACHIVEMENT_TYPE_FRUIT);

		//パーティクル
		ParticleManager.Instance.PlayParticle_Fruit(GetParticleMousePos(Input.mousePosition));

		//SE
		SoundManager.Instance.PlaySe(SE_FruitGet);
	}

	/// <summary>
	/// //生命の実加算
	/// </summary>
	private void AddFruit(double AddNum = 0)
	{
		GameData.fruit += AddNum;
		GameData.total_fruit += AddNum;
		GameData.all_fruit += AddNum;
		UpdateTextUserFruit();
		ViewInstButton();
	}

	/// <summary>
	/// //ポイント加算
	/// </summary>
	/// <param name="clickCase"></param>
	private void AddPoint(GameData.ClickCase clickCase, double AddNum = 0, int adjPos = 0)
	{
		//ポイント加算
		double _addPoint = 0;
		switch (clickCase)
		{
		case GameData.ClickCase.CLICK:
		case GameData.ClickCase.FRAGMENT_STELLA:
			_addPoint += AddNum;
			ppsPool += AddNum;

			//+n表示
			ViewAddPointFontEffect(Input.mousePosition, $"+{GetBigNumberString(AddNum)}", (clickCase == GameData.ClickCase.FRAGMENT_STELLA), adjPos);

			break;
		case GameData.ClickCase.SHARD_BIFOREST_WARP:
		case GameData.ClickCase.CENTOR:
			_addPoint += AddNum;
			ppsPool += AddNum;

			//+n表示 中心
			//ViewAddPointFontEffect(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f), $"+{GetBigNumberString(AddNum)}");
			ViewAddPointFontEffectDirect(new Vector3(200.0f, -150f, 0f), $"+{GetBigNumberString(AddNum)}", (clickCase == GameData.ClickCase.SHARD_BIFOREST_WARP), adjPos);

			break;
		case GameData.ClickCase.INST:
			_addPoint += AddNum;

			break;
		default:
			break;
		}

		if (GameData.point + _addPoint >= double.MaxValue)
		{
			GameData.point = double.MaxValue;
		}
		else
		{
			GameData.point += _addPoint;
			GameData.point = Math.Round(GameData.point);
		}

		if (_addPoint >= 0)
		{
			GameData.total_point += _addPoint;
			GameData.all_point += _addPoint;
		}


		//描画更新
		UpdateTextUserPoint();

		//ユグドラシルの種
		UpdateYggdrasilSeedBar();

	}

	private void ViewAddPointFontEffect(Vector3 v, string num, bool big = false, int adjPos = 0)
	{
		var canvas = ParentCanvas;
		var canvasRect = canvas.GetComponent<RectTransform>();
		Vector2 localpoint;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, v, canvas.worldCamera, out localpoint);
		localpoint.x += UnityEngine.Random.RandomRange(-FontEffectRandAdjPos, FontEffectRandAdjPos);
		localpoint.y += UnityEngine.Random.RandomRange(-FontEffectRandAdjPos, FontEffectRandAdjPos);
		ClickFontEffectManager.Instance.View(num, localpoint.x, localpoint.y, big, adjPos);
	}
	private void ViewAddPointFontEffectDirect(Vector3 localpoint, string num, bool big = false, int adjPos = 0)
	{
		localpoint.x += UnityEngine.Random.RandomRange(-FontEffectRandAdjPos, FontEffectRandAdjPos);
		localpoint.y += UnityEngine.Random.RandomRange(-FontEffectRandAdjPos, FontEffectRandAdjPos);
		ClickFontEffectManager.Instance.View(num, localpoint.x, localpoint.y, big, adjPos);
	}
	private Vector3 GetParticleMousePos(Vector3 v)
	{
		var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Camera.main.transform.forward * 10);
		return (Vector3)pos;
	}


	//描画更新
	private void UpdateTextUserPoint()
	{
		string t = GetBigNumberString(GameData.point);
		if (_textUserPoint != t)
		{
			textUserPoint.text = t;
		}
	}
	//描画更新
	private void UpdateTextUserFruit()
	{
		string t = GameData.fruit.ToReadableString();// GetBigNumberString(GameData.fruit);
		textUserFruit.text = t;
	}

	//数値表示記号調整
	private string GetBigNumberString(double n)
	{
		string result = "";
		if (n < 1000000)
		{
			result = n.ToString();
		}
		else
		{
			result = n.ToReadableString();
		}
		return result;
	}


	/// <summary>
	/// ユグドラシルの種ゲージ
	/// </summary>
	private void UpdateYggdrasilSeedBar()
	{
		float par = 0;
		double num = CalcData.GetSeedNum(out par);
		YggdrasilSeedBar.fillAmount = par;

		//獲得予定ユグドラシルの種
		YggdrasilNum.text = $"+{num.ToReadableString()}";
	}






	//葉っぱ生成座標ランダム取得
	private Vector2 GetCirclePos()
	{
		GameObject w = leafArea[UnityEngine.Random.Range(0, leafArea.Length)];
		float radius = w.transform.localScale.x * 0.5f;
		var circlePos = radius * UnityEngine.Random.insideUnitCircle;

		circlePos.x += w.transform.position.x;
		circlePos.y += w.transform.position.y;

		circlePos.x = MathF.Round(circlePos.x, 1);
		circlePos.y = MathF.Round(circlePos.y, 1);

		return circlePos;
	}

	//葉っぱ新規生成処理
	private void CreateLeafObj(int isNewLv = 0)
	{
		return;

		if (GameData.LeafPos.Count < GameData.InstLv[(int)GameData.InstCase.INST1])
		{
			for (int i = (GameData.InstLv[(int)GameData.InstCase.INST1] - GameData.LeafPos.Count); i >= 0; i--)
			{
				GameData.LeafPos.Add(Vector3.zero);
			}
		}

		GameObject obj = Instantiate(leafPrefab, leafParent.transform, false) as GameObject;
		Vector3 v = GetCirclePos();

		if (isNewLv == 0)
		{
			//新規座標登録
			obj.transform.localPosition = new Vector2(v.x, v.y);
			obj.transform.eulerAngles = new Vector3(0, 0, UnityEngine.Random.RandomRange(0, 360));
			GameData.LeafPos[GameData.InstLv[(int)GameData.InstCase.INST1] - 1] = obj.transform.localPosition;
		}
		else
		{
			//座標復元
			obj.transform.localPosition = new Vector2(GameData.LeafPos[isNewLv - 1].x, GameData.LeafPos[isNewLv - 1].y);
			obj.transform.eulerAngles = new Vector3(0, 0, GameData.LeafPos[isNewLv - 1].z);
		}

		//Debug.Log($"leaf {obj.transform.localPosition}");
	}






	//言語切り替え
	public void ButtonChangeLanguage(int index)
	{
		GameData.language = index;
		LanguageButtonView(index);

		currentTime = ppsSpan;//毎秒のところをすぐ変えるため
	}
	public void LanguageButtonView(int v = 0)
	{
		for (int i = 0; i < LanguageButtons.Count; i++)
		{
			LanguageButtons[i].GetComponent<TabButtonController>().ChangeView(v == i);
		}
	}





	private void StartInstCoroutine(int instCase)
	{

		//自動処理開始
		if (!InstCoroutinePointa.ContainsKey(instCase))
		{
			InstCoroutinePointa.Add(instCase, null);
		}
		if (InstCoroutinePointa[instCase] != null)
		{
			StopCoroutine(InstCoroutinePointa[instCase]);
			InstCoroutinePointa[instCase] = null;
		}
		if (GetInstLv(instCase) > 0) InstCoroutinePointa[instCase] = StartCoroutine(InstCoroutine(instCase));
	}

	///施設レベル取得
	public int GetInstLv(int instCase)
	{
		if (GameData.InstLv.Count <= instCase)
		{
			for (int i = ((instCase + 1) - GameData.InstLv.Count); i >= 0; i--)
			{
				GameData.InstLv.Add(0);
			}
		}
		return GameData.InstLv[instCase];
	}
	public void AddInstLv(int instCase, int add)
	{
		if (GameData.InstLv.Count < instCase)
		{
			return;
		}
		GameData.InstLv[instCase] += add;
	}
	///
	///施設アップデート
	public void ButtonUpdateInst(int instCase)
	{
		if (_ModeUseFruit)
		{
			UpdateInstFruit(instCase);
		}
		else
		{
			UpdateInst(instCase);
		}

		SendSteamAchivement(GameData.ACHIVEMENT_TYPE_INST_LV);
	}

	double NextCostCalculation(int instCase)
	{
		return CalcData.GetInstCost(instCase, GetInstLv(instCase) + 1);
	}
	double CostCalculation(int instCase)
	{
		double cost = 0.0f;

		for (int _lv = 1; _lv <= _ButtonLvupMulti; _lv++)
		{
			if (GetInstLv(instCase) + _lv <= GameData.INST_LV_MAX[instCase])
			{
				cost += CalcData.GetInstCost(instCase, GetInstLv(instCase) + _lv);
			}
			else
			{
				break;
			}
		}

		return cost;
	}
	/// <summary>
	/// 施設ボタン
	/// </summary>
	public void SetInst(int instCase)
	{
		if (instCase <= 0) return;
		if (instButtonManager.Count <= (instCase - 1)) return;

		double cost = 0;
		double next_cost = 0;
		next_cost = NextCostCalculation(instCase);
		cost = CostCalculation(instCase);
		double power = CalcData.GetInstPoint(instCase, GetInstLv(instCase));
		if (FeverManager.Instance.IsFever)
		{
			power /= 10;
		}

		//ボタン見た目
		instButtonManager[instCase - 1].SetName(GameData.INST_NAME[instCase]);
		instButtonManager[instCase - 1].SetIcon(instCase - 1);
		instButtonManager[instCase - 1].SetButtonStatus(
			GetInstLv(instCase),
			cost.ToReadableString(),//GetBigNumberString(cost),
			GetBigNumberString(power),
			GetInstLv(instCase) + 1,
			GameData.INST_LV_MAX[instCase]);

		//ボタンラベル見た目
		ButtonBlinkManager bbm = instButtonManager[instCase - 1].gameObject.GetComponent<ButtonBlinkManager>();
		ButtonStatusController bsc = instButtonManager[instCase - 1].gameObject.GetComponent<ButtonStatusController>();
		if (_ModeUseFruit)
		{
			//生命の実モード
			bsc.TargetPoint = 0;
			bsc.TargetLabelOnly = false;
			bsc.SwitchMode = GameData.fruit > 0 && GetInstLv(instCase) > 0;
			instButtonManager[instCase - 1].ShowMulti("");
			bbm.isBlink = false;
		}
		else
		{
			//通常レベルアップ
			bsc.TargetPoint = next_cost;
			bsc.TargetLabelOnly = true;
			bsc.TargetLabelOnlyPoint = cost;
			bsc.SwitchMode = cost > 0;

			switch (_ButtonLvupMulti)
			{
			case 1:
				instButtonManager[instCase - 1].ShowMulti("");
				break;
			default:
				instButtonManager[instCase - 1].ShowMulti($"×{_ButtonLvupMulti}");
				break;
			}

			if (GetInstLv(instCase) == 0)
			{
				bbm.isBlink = true;
			}
			else
			{
				bbm.isBlink = false;
			}
		}


		ButtonLabelCloseController blcc = instButtonManager[instCase - 1].gameObject.GetComponent<ButtonLabelCloseController>();
		blcc.labelText = GameData.INST_NAME[instCase];
		blcc.TargetPoint = next_cost;
		blcc.isOn = GetInstLv(instCase) == 0;

		//生命の実
		instButtonManager[instCase - 1].UpdateFruitLv(GameData.InstFruitLv[instCase].ToReadableString());

		//そもそも条件を満たしていないと表示されない
		//とりあえずひとつ前の解放かどうか
		if (instCase > 1)
		{
			instButtonManager[instCase - 1].gameObject.SetActive(GetInstLv(instCase - 1) > 0);
		}

		//自動処理開始
		StartInstCoroutine(instCase);
	}

	/// <summary>
	/// 聖霊Lvアップ
	/// </summary>
	/// <param name="instCase"></param>
	public void UpdateInst(int instCase)
	{
		if (instCase <= 0) return;
		if (instButtonManager.Count <= (instCase - 1)) return;
		if (!isUpdateCheck(instCase))
		{
			SoundManager.Instance.PlaySe(SE_CanNotSelectButton);
			return;
		}

		for (int i = 0; i < _ButtonLvupMulti; i++)
		{
			bool b = isUpdateCheck(instCase);
			if (b)
			{

				GameData.point -= CalcData.GetInstCost(instCase, GetInstLv(instCase) + 1);
				AddInstLv(instCase, 1);
				var isInst = true;
				switch (instCase)
				{
				case (int)GameData.InstCase.INST1:

					List<int> lvs = new List<int>()
					{
						10,20,30,40,50,55,70,90,100,130,
						150,170,200,220,240,260,280,300
					};
					foreach (var v in lvs)
					{
						if (GameData.InstLv[instCase] == v)
						{
							// Lv10上がるごとに覚醒開放
							OpenAwake(lvs.IndexOf(v) + 1);
						}
					}
					break;
				case (int)GameData.InstCase.INST2:
				case (int)GameData.InstCase.INST3:
				case (int)GameData.InstCase.INST4:
				case (int)GameData.InstCase.INST5:
				case (int)GameData.InstCase.INST6:
				case (int)GameData.InstCase.INST7:
				case (int)GameData.InstCase.INST8:
				case (int)GameData.InstCase.INST9:
				case (int)GameData.InstCase.INST10:
				case (int)GameData.InstCase.INST11:
				case (int)GameData.InstCase.INST12:
				case (int)GameData.InstCase.INST13:
				case (int)GameData.InstCase.INST14:
				case (int)GameData.InstCase.INST15:
				case (int)GameData.InstCase.INST16:
				case (int)GameData.InstCase.INST17:
				case (int)GameData.InstCase.INST18:
				case (int)GameData.InstCase.INST19:
				case (int)GameData.InstCase.INST20:
					break;
				}
				if (isInst)
				{
					SetInst(instCase);
				}
				//switch (instCase)
				//{
				//    case (int)GameData.InstCase.INST1:
				//        //葉っぱをはやす
				//        CreateLeafObj();
				//        break;
				//}

				//回るアイコンを表示化
				//if (GetInstLv(instCase) == 1)
				//{
				//	iconLoopManager.SetIcon();
				//}
				//else
				//{
				//	if (iconLoopManager.CheckGetPos(instCase)) ParticleManager.Instance.PlayParticle_InstLvUp(iconLoopManager.GetPos(instCase));
				//	iconLoopManager.Flash(instCase);
				//}
			}

			//次のボタン可視化のために実行
			SetInst(instCase + 1);

			//描画更新
			UpdateTextUserPoint();
			currentTime = ppsSpan;

			awakeManager.UpdateAwakeButtonView();

			//生命の実カウント開始
			CalcInstAllLv();

			if (!b) break;
		}

		//SE
		SoundManager.Instance.PlaySe(SE_Inst);

		//パーティクル
		ParticleManager.Instance.SetInstParticleCnt(_instAllLv);

		//セーブ
		DataManager.Instance.Save();
	}
	public void ViewInstButton()
	{
		//施設整える
		foreach (int instCase in Enum.GetValues(typeof(GameData.InstCase)))
		{
			SetInst(instCase);
		}

	}
	/// <summary>
	/// 生命の実を与える
	/// </summary>
	public void UpdateInstFruit(int instCase)
	{
		if (instCase <= 0) return;
		if (instButtonManager.Count <= (instCase - 1)) return;

		for (int i = 0; i < _ButtonLvupMulti; i++)
		{
			bool b = GameData.fruit > 0;
			if (b)
			{
				GameData.fruit -= 1;
				GameData.all_fruit_use += 1;
				GameData.InstFruitLv[instCase]++;
			}

			//生命の実
			instButtonManager[instCase - 1].UpdateFruitLv(GameData.InstFruitLv[instCase].ToReadableString());

			UpdateTextUserFruit();

			if (!b) break;
		}

		ViewInstButton();

		//セーブ
		DataManager.Instance.Save();
	}
	public bool isUpdateCheck(int instCase)
	{
		if (instCase <= 0) return false;
		if (instButtonManager.Count <= (instCase - 1)) return false;

		bool b = false;
		switch (instCase)
		{
		default:
			//Debug.Log($"isUpdateCheck[{instCase}] {GetInstLv(instCase)} < {GameData.INST_LV_MAX[instCase]}  {GameData.point} >= {CalcData.GetInstCost(instCase, GetInstLv(instCase) + 1)}");
			if (GetInstLv(instCase) < GameData.INST_LV_MAX[instCase] && GameData.point >= CalcData.GetInstCost(instCase, GetInstLv(instCase) + 1))
			{
				b = true;
			}
			break;
		}

		return b;
	}




	//施設処理
	IEnumerator InstCoroutine(int instCase)
	{
		//1fごとに実行する
		while (true)
		{
			switch (GameData.GameModePhase)
			{
			case GameData.GAME_MODE_TREE:
				if (!isGameStop)
				{
					double _addPoint = (CalcData.GetInstPoint(instCase, GameData.InstLv[instCase]) * FPS / GameData.WISDOM_CLICK_FRAME_MAGNIFICATION / CalcData.GetAdjInstCycle());
					GameData.InstCharge[instCase] += _addPoint;
					double addNum = Math.Floor(GameData.InstCharge[instCase]);
					if (addNum >= 1)
					{
						GameData.InstCharge[instCase] -= addNum;
						AddPoint(GameData.ClickCase.INST, addNum);
						UpdateTextUserPoint();
					}
					//Debug.Log($"InstCoroutine({instCase}) {GameData.InstCharge[instCase]}");
				}
				break;
			}

			yield return null;
		}

		yield break;
	}







	private void UpdateAudioVolume(float n = 0f)
	{
		GameData.AudioMasterVolume = GameData.AudioMasterVolume + n;
		if (GameData.AudioMasterVolume >= 1f) GameData.AudioMasterVolume = 1f;
		if (GameData.AudioMasterVolume <= 0f) GameData.AudioMasterVolume = 0f;
		if (DataManager.Instance != null) DataManager.Instance.Save();

		SoundManager.BgmVolume = GameData.AudioMasterVolume;
		foreach (Image img in AudioVolumeBar)
		{
			img.fillAmount = GameData.AudioMasterVolume;
		}
	}
	public void ButtonAudioVolumePlus()
	{
		UpdateAudioVolume(0.1f);
	}
	public void ButtonAudioVolumeMinus()
	{
		UpdateAudioVolume(-0.1f);
	}

	private void UpdateSfxVolume(float n = 0f)
	{
		GameData.SeVolume = GameData.SeVolume + n;
		if (GameData.SeVolume >= 1f) GameData.SeVolume = 1f;
		if (GameData.SeVolume <= 0f) GameData.SeVolume = 0f;
		if (DataManager.Instance != null) DataManager.Instance.Save();

		SoundManager.SeVolume = GameData.SeVolume;
		foreach (Image img in SfxVolumeBar)
		{
			img.fillAmount = GameData.SeVolume;
		}
	}
	public void ButtonSfxVolumePlus()
	{
		UpdateSfxVolume(0.1f);
	}
	public void ButtonSfxVolumeMinus()
	{
		UpdateSfxVolume(-0.1f);
	}
	public void ButtonMute()
	{
		GameData.AudioMute = (GameData.AudioMute == 0) ? 1 : 0;
		SetButtonMute();
		if (DataManager.Instance != null) DataManager.Instance.Save();
	}
	public void SetButtonMute()
	{
		SoundManager.Instance.setVolumeMuteBgm(GameData.AudioMute == 1);
		foreach (TabButtonController tab in ButtonMuteImages)
		{
			tab.ChangeView(SoundManager.Instance.getVolumeMuteBgm());
		}
	}



	public void OpenRagnarokSkill(int skillIndex)
	{
		if (GameData.mYggdrasilSkill.ContainsKey(skillIndex) && GameData.mYggdrasilSkill[skillIndex].price <= GameData.SeedYggdrasil)
		{
			if (!GameData.RagnarokSkill[skillIndex])
			{
				GameData.RagnarokSkill[skillIndex] = true;
				GameData.SeedYggdrasil -= GameData.mYggdrasilSkill[skillIndex].price;
			}
		}
		ragnarokSkillManager.UpdateRagnarokSkillResources();
	}

	public void ButtonAwake(int mAwakeID)
	{
		OpenAwake(mAwakeID);
	}
	private void OpenAwake(int mAwakeID)
	{
		if (!GameData.AwakeLv.Contains(mAwakeID))
		{
			if (CheckOpenAwake(mAwakeID))
			{
				//GameData.point -= GameData.mAwake[mAwakeID].price;
				GameData.AwakeLv.Add(mAwakeID);
			}
			DataManager.Instance.Save();
		}
		awakeManager.UpdateAwakeButtonView();
		awakeManager.HideInfoWindow();

		bgmButton.GetComponent<Button>().interactable = CalcData.CheckAwakeToMode(GameData.AwakeModeNum.BGM);
		musicPlayerObj.SetActive(CalcData.CheckAwakeToMode(GameData.AwakeModeNum.BGM));
		RemoveGrayScale(shardBiforestObj, CalcData.CheckAwakeToMode(GameData.AwakeModeNum.SHARD_BEFOREST));
		//ragnarokButton.GetComponent<Button>().interactable = CalcData.CheckAwakeToMode(GameData.AwakeModeNum.RAGNAROK);
		RemoveGrayScale(YggdrasilGroup, CalcData.CheckAwakeToMode(GameData.AwakeModeNum.RAGNAROK));
		RemoveGrayScale(NightStellaGroup, GameData.AwakeLv.Contains(2004));
		RemoveGrayScale(ButtonSkin, CalcData.CheckAwakeToMode(GameData.AwakeModeNum.GODDESS));

		ViewInstButton();

		//SE
		SoundManager.Instance.PlaySe(SE_Awake);

		//木を光らせる
		//WoodAnimator.runtimeAnimatorController = null;
		//WoodAnimator.runtimeAnimatorController = WoodAwakeAnim;

		////パーティクル
		//ParticleManager.Instance.PlayParticle_Awake(Wood.transform.position);
	}
	public bool CheckOpenAwake(int mAwakeID)
	{
		bool b = true;

		if (GameData.mAwake[mAwakeID].price <= GameData.point)
		{
			b = true;
		}

		return b;
	}


	public void InstWindowView(int v = 0)
	{
		for (int i = 0; i < InstViewButtons.Count; i++)
		{
			InstViewButtons[i].GetComponent<TabButtonController>().ChangeView(v == i);
		}
	}

	public void OpenSettingWindow(bool isClose = true)
	{
		if (!isClose)
		{
			ButtonRecordGroup.GetComponent<TabButtonController>().ChangeView(false);
			ButtonSettingGroup.GetComponent<TabButtonController>().ChangeView(false);
		}
		else
		{
			ButtonRecordGroup.GetComponent<TabButtonController>().ChangeView(false);
			ButtonSettingGroup.GetComponent<TabButtonController>().ChangeViewOrClose();
			skinSelectManager.Hide(); // 立ち絵、CG切り替え画面非表示
			BookShelfManager.Instance.Close(); // 閉じる
		}
	}
	public void OpenRecordWindow(bool isClose = true)
	{
		if (!isClose)
		{
			ButtonSettingGroup.GetComponent<TabButtonController>().ChangeView(false);
			ButtonRecordGroup.GetComponent<TabButtonController>().ChangeView(false);
		}
		else
		{
			ButtonSettingGroup.GetComponent<TabButtonController>().ChangeView(false);
			ButtonRecordGroup.GetComponent<TabButtonController>().ChangeViewOrClose();
			skinSelectManager.Hide(); // 立ち絵、CG切り替え画面非表示
			BookShelfManager.Instance.Close(); // 閉じる
		}
	}


	public void OnEnterShowFruitInfo()
	{
		FruitInfoWindow.SetActive(true);
	}
	public void OnExitHideFruitInfo()
	{
		FruitInfoWindow.SetActive(false);
	}
	public void OnEnterShowNightStellaInfo()
	{
		NightStellaInfoWindow.SetActive(true);
	}
	public void OnExitHideNightStellaInfo()
	{
		NightStellaInfoWindow.SetActive(false);
	}
	public void OnEnterShowGungnirInfo()
	{
		GungnirInfoWindow.SetActive(true);
	}
	public void OnExitHideGungnirInfo()
	{
		GungnirInfoWindow.SetActive(false);
	}
	public void OnEnterShowWisdomInfo()
	{
		WisdomInfoWindow.SetActive(true);
	}
	public void OnExitHideWisdomInfo()
	{
		WisdomInfoWindow.SetActive(false);
	}
	public void OnEnterShowRagnarokInfo()
	{
		RagnarokInfoWindow.SetActive(true);
	}
	public void OnExitHideRagnarokInfo()
	{
		RagnarokInfoWindow.SetActive(false);
	}


	public void ButtonChangeUseFruit()
	{
		ChangeUseFruit(!_ModeUseFruit);
	}
	private void ChangeUseFruit(bool b)
	{
		_ModeUseFruit = b;
		//ButtonModeFruit.GetComponent<TabButtonController>().ChangeView(_ModeUseFruit);
		ViewInstButton();
	}



	public void ButtonChangeButtonMulti(int n = 1)
	{
		_ButtonLvupMulti = n;
		UpdateViewButtonMulti();
		ViewInstButton();
	}
	private void UpdateViewButtonMulti()
	{
		//一気にレベルアップボタン切り替え
		//めんどいのでべたに書く
		ButtonMultis[0].GetComponent<TabButtonController>().ChangeView(_ButtonLvupMulti == 1);
		ButtonMultis[1].GetComponent<TabButtonController>().ChangeView(_ButtonLvupMulti == 10);
		ButtonMultis[2].GetComponent<TabButtonController>().ChangeView(_ButtonLvupMulti == 100);
	}



	/// <summary>
	/// 聖霊ボタンの生命のレベル部分表示化
	/// </summary>
	private void ShowInstFruitLv(bool b)
	{
		//ButtonModeFruit.SetActive(b);

		foreach (InstButtonManager m in instButtonManager)
		{
			if (m != null)
			{
				m.ShowFruitLv(b);
			}
		}
	}










	///グングニル
	public void ButtonGungnir()
	{
		if (debugMode || GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_27])
		{
			PlayGungnir();

			//パーティクル
			ParticleManager.Instance.PlayParticle_Gungunir(Wood.transform.position);
		}
		else
		{
			MessageManager.Instance.PlayMessage(new List<string>() { LanguageCSV.Instance.GetCSV(GameData.MESSAGE_CLOSE_MENU) });
		}
	}
	private void PlayGungnir()
	{
		if (GameData.GungnirCycleCnt >= CalcData.GetGungnirTime())
		{
			GameData.total_gungnir++;
			GameData.all_gungnir++;
			GameData.GungnirCycleCnt = 0;
			GameData.GungnirPowerTime = CalcData.GetGungnirPowerTime();
		}
	}




	///ミーミルの知恵
	public void ButtonWisdom()
	{
		if (debugMode || GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_38])
		{
			PlayWisdom();

			//パーティクル
			ParticleManager.Instance.PlayParticle_Wisdom(Wood.transform.position);
		}
		else
		{
			MessageManager.Instance.PlayMessage(new List<string>() { LanguageCSV.Instance.GetCSV(GameData.MESSAGE_CLOSE_MENU) });
		}
	}
	private void PlayWisdom()
	{
		if (GameData.WisdomCycleCnt >= CalcData.GetWisdomTime())
		{
			GameData.total_wisdom++;
			GameData.all_wisdom++;
			GameData.WisdomClickCnt = 0;
			GameData.WisdomCycleCnt = 0;
			GameData.WisdomPowerTime = CalcData.GetWisdomPowerTime();
		}
	}




	public void ButtonDefScreenSize()
	{
		if (!Screen.fullScreen)
		{
			Screen.SetResolution(1280, 720, Screen.fullScreen);
		}
		else
		{
			Screen.SetResolution(1920, 1080, Screen.fullScreen);
		}
	}

	public void ButtonFullScreen()
	{
		if (Screen.fullScreen)
		{
			Screen.SetResolution(1280, 720, Screen.fullScreen);
		}
		else
		{
			Screen.SetResolution(1920, 1080, Screen.fullScreen);
		}
		Screen.fullScreen = !Screen.fullScreen;
	}



	public void ButtonSteamAchivementReset()
	{
		settingSystemPopUpWindowController.SetLabel(LanguageCSV.Instance.GetCSV(GameData.MESSAGE_STEAM_RESET));
		settingSystemPopUpWindowController.SetButtonAction(() =>
		{
			settingSystemPopUpWindowController.Hide();
		});
		settingSystemPopUpWindowController.Show();
	}

	public void SendSteamAchivement(int achivementType)
	{
		//Steam実績
		bool achi_open = false;
		string ach_name = "";

		switch (achivementType)
		{
		case GameData.ACHIVEMENT_TYPE_CLICK:

			foreach (int i in GameData.mAchivement_Click)
			{
				AchivementClass aclass = GameData.mAchivementClick[i];
			}

			break;
		case GameData.ACHIVEMENT_TYPE_POINT:

			foreach (int i in GameData.mAchivement_Point)
			{
				AchivementClass aclass = GameData.mAchivementClick[i];
			}

			break;
		case GameData.ACHIVEMENT_TYPE_PPS:

			foreach (int i in GameData.mAchivement_PPS)
			{
				AchivementClass aclass = GameData.mAchivementClick[i];
			}

			break;

		case GameData.ACHIVEMENT_TYPE_INST_LV:

			int _titalLv = (int)CalcData.GetAllInstLv();
			foreach (int i in GameData.mAchivement_InstLv)
			{
				AchivementClass aclass = GameData.mAchivementClick[i];
			}

			break;

		case GameData.ACHIVEMENT_TYPE_ALL_TIME:
		{

			double play_h = Math.Floor(GameData.all_playtime / 3600);
			foreach (int i in GameData.mAchivement_AllTime)
			{
				AchivementClass aclass = GameData.mAchivementClick[i];
			}
		}
		break;

		case GameData.ACHIVEMENT_TYPE_TIME:
		{

			bool play_h_half = GameData.all_playtime >= 1800;
			double play_h = Math.Floor(GameData.playtime / 3600);
			foreach (int i in GameData.mAchivement_Time)
			{
				AchivementClass aclass = GameData.mAchivementClick[i];
			}
		}
		break;
		case GameData.ACHIVEMENT_TYPE_FRAGMENT:

			foreach (int i in GameData.mAchivement_Fragment)
			{
				AchivementClass aclass = GameData.mAchivementClick[i];
			}

			break;
		case GameData.ACHIVEMENT_TYPE_BEFOREST:

			foreach (int i in GameData.mAchivement_Beforest)
			{
				AchivementClass aclass = GameData.mAchivementClick[i];
			}

			break;
		case GameData.ACHIVEMENT_TYPE_FRUIT:

			foreach (int i in GameData.mAchivement_Fruit)
			{
				AchivementClass aclass = GameData.mAchivementClick[i];
			}

			break;
		case GameData.ACHIVEMENT_TYPE_RAGNAROK:

			foreach (int i in GameData.mAchivement_Ragnarok)
			{
				AchivementClass aclass = GameData.mAchivementClick[i];
			}

			break;

		}

		if (achi_open)
		{
			string t = LanguageCSV.Instance.GetCSV(GameData.MESSAGE_STEAM_ACHIVEMENT);
			if (ach_name != "")
			{
				t += $"\n「{ach_name}」";
			}
			popupMessageManager.SetMessage(t, 1);
		}
	}








	public void ButtonGameDataDelete()
	{
		settingSystemPopUpWindowController.SetLabel(LanguageCSV.Instance.GetCSV(GameData.MESSAGE_GAMEDATA_RESET));
		settingSystemPopUpWindowController.SetButtonAction(() =>
		{
			settingSystemPopUpWindowController.Hide();

			StartCoroutine(corDeleteData());

		});
		settingSystemPopUpWindowController.Show();
	}

	IEnumerator corDeleteData()
	{
		LoadingManager.Instance.On();

		//削除
		GameData.GameModePhase = -1;

		DataManager.Instance.Delete();

		yield return new WaitForSeconds(0.2f);

		GameData.ResetSaveData(true);

		yield return new WaitForSeconds(0.2f);

		DataManager.Instance.Read();

		yield return new WaitForSeconds(0.2f);

		DataManager.Instance.Save();

		yield return new WaitForSeconds(0.2f);
		GameData.GameModePhase = GameData.GAME_MODE_SEED;
		isInit = false;
		MainCanvasInit();
		OnInit();

		LoadingManager.Instance.Off();
		popupMessageManager.SetMessage(LanguageCSV.Instance.GetCSV(GameData.MESSAGE_GAMEDATA_RESETED));


		yield break;
	}





	public void ButtonGameExit()
	{
		settingSystemPopUpWindowController.SetLabel(LanguageCSV.Instance.GetCSV(GameData.MESSAGE_GAME_EXIT));
		settingSystemPopUpWindowController.SetButtonAction(() =>
		{
			settingSystemPopUpWindowController.Hide();

#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
            Application.Quit();
#endif

		});
		settingSystemPopUpWindowController.Show();
	}




	public void ButtonMusicPlayer()
	{
		musicPlayerObj.SetActive(!musicPlayerObj.activeSelf);
	}












	private void UpdateEnviroment()
	{
		float h = (float)DateTime.Now.Hour + ((float)DateTime.Now.Minute / 60f) + ((1f / 60f / 60f) * (float)DateTime.Now.Second) + ((1f / 60f / 60f / 1000f) * (float)DateTime.Now.Millisecond);//test_h;
																																																 //h = test_h;
																																																 //Debug.Log($"h:{h}");

		float ax = 1.5f;//楕円
		float ay = -4f;//座標調整

		//太陽
		float p = (h + 6) / 24f * 360f;
		float centerx = 0f;
		float centery = 0f;
		float distance = 7.875f;

		float radian = p * (Mathf.PI / -180);
		float x = centerx + distance * Mathf.Cos(radian) * ax;
		float y = centery + distance * Mathf.Sin(radian) + ay;

		var newpos = new Vector3(x, y, 0f);
		SpriteSun.transform.localPosition = newpos;



		//月
		p = (h - 6) / 24f * 360f;

		radian = p * (Mathf.PI / -180);
		x = centerx + distance * Mathf.Cos(radian) * ax;
		y = centery + distance * Mathf.Sin(radian) + ay;

		newpos = new Vector3(x, y, 0f);
		SpriteMoon.transform.localPosition = newpos;

		//Light
		//ChangeLight(h, GameData.LIGHT_CHANGE_TIME);


		//グレースケール
		switch (GameData.GameModePhase)
		{
		case GameData.GAME_MODE_SEED:
		case GameData.GAME_MODE_RAGNAROK:
			postProcessVolume.weight = 0;
			break;
		case GameData.GAME_MODE_TREE:
			if (GameData.total_point >= GameData.GrayScaleLimit)
			{
				postProcessVolume.weight = 0;
			}
			else
			{
				postProcessVolume.weight = 1f - (float)(GameData.total_point / GameData.GrayScaleLimit);
			}
			break;
		default:
			break;
		}
	}

	//private void ChangeLight(float h=0, float time=0f)
	//{
	//    if (0 <= h && h <= 4)
	//    {
	//        if (directionalLightManager.GetNowIndex() != GameData.LIGHT_MIDNIGHT)
	//        {
	//            directionalLightManager.LightChange(GameData.LIGHT_MIDNIGHT, time);
	//        }
	//    }
	//    else if (5 <= h && h <= 7)
	//    {
	//        if (directionalLightManager.GetNowIndex() != GameData.LIGHT_MORNING)
	//        {
	//            directionalLightManager.LightChange(GameData.LIGHT_MORNING, time);
	//        }
	//    }
	//    else if (8 <= h && h <= 15)
	//    {
	//        if (directionalLightManager.GetNowIndex() != GameData.LIGHT_NOON)
	//        {
	//            directionalLightManager.LightChange(GameData.LIGHT_NOON, time);
	//        }
	//    }
	//    else if (16 <= h && h <= 19)
	//    {
	//        if (directionalLightManager.GetNowIndex() != GameData.LIGHT_EVENING)
	//        {
	//            directionalLightManager.LightChange(GameData.LIGHT_EVENING, time);
	//        }
	//    }
	//    else
	//    {
	//        if (directionalLightManager.GetNowIndex() != GameData.LIGHT_NIGHT)
	//        {
	//            directionalLightManager.LightChange(GameData.LIGHT_NIGHT, time);
	//        }
	//    }
	//}

	//public void ButtonChangeLightTest()
	//{
	//    lightIndex++;
	//    if (directionalLightManager.GetLightNum() <= lightIndex) lightIndex = 0;
	//    directionalLightManager.LightChange(lightIndex, 3f);
	//}










	public void ButtonTestMessage()
	{
		List<string> msg = new List<string>() {
		"ラグナロクを迎えてもよろしいでしょうか？",
		"全てのプレイ進捗を失われ、世界が生まれ変わります。",
		"全てのステラはユグドラシルの種に変換されます。",
		"※実績と一部の覚醒は継承されます。",
		};

		MessageManager.Instance.PlayMessage(msg);
	}
	public void Story(int checkPoint = 0)
	{
		List<string> msg = new List<string>();

		switch (checkPoint)
		{
		case 0:
			if (GameData.story == 0)
			{
				msg = new List<string>() {
						"ここは朽ち果てた世界...",
						"あなたが植えた最後の種からユグドラシルは蘇りました",
						"ユグドラシルをクリックすることで『ステラ』を合成することができます",
					};

				GameData.story++;
			}
			break;
		case 1:
			if (GameData.story == 1)
			{
				msg = new List<string>() {
						"ステラは世界を蘇らせるために不可欠なもの",
						"たくさんのステラを集めるのです",
					};

				GameData.story++;
			}
			break;
		case 2:
			if (GameData.story == 2)
			{
				msg = new List<string>() {
						"あなたのおかげで少し世界に力が戻りました",
						"ですが…まだたくさんのステラが必要です",
						"ユグドラシルのこと、この世界のこと、頼みましたよ…",
					};

				GameData.story++;
			}
			break;
		}

		if (msg.Count > 0)
		{
			MessageManager.Instance.PlayMessage(msg);
			if (DataManager.Instance != null) DataManager.Instance.Save();
		}
	}


	public void RemoveGrayScale(GameObject parent, bool b = true)
	{
		if (debugMode)
		{
			b = true;
		}
		UIEffect[] uiEffects = parent.GetComponentsInChildren<UIEffect>();
		foreach (UIEffect e in uiEffects)
		{
			if (e.effectMode == EffectMode.Grayscale)
			{
				e.effectFactor = b ? 0 : 1;
			}
		}
	}

	/// <summary>
	/// 自動クリックをOnOffする
	/// </summary>
	public void AutoClick()
	{
		isAuto = !isAuto;
		if (!isAuto)
		{
			GameData.WisdomPowerTime = 0;
		}
	}

	/// <summary>
	/// 超速モードの切り替え
	/// </summary>
	public void SuperFast()
	{
		isSuperFast = !isSuperFast;
		GameData.WISDOM_CLICK_FRAME_MAGNIFICATION = !isSuperFast ? 1 : 0.1f;
	}
}
