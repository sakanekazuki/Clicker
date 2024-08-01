using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using NAudio.Wave;
using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class MusicPlayerManager : MonoBehaviour
{
    [SerializeField] List<AudioClip> GameInPlayList;
    [SerializeField] List<GameObject> PlayListButtons;
    [SerializeField] GameObject ButtonMute;
    [SerializeField] GameObject ButtonBack;
    [SerializeField] GameObject ButtonPlay;
    [SerializeField] GameObject ButtonNext;
    [SerializeField] GameObject ButtonShuffle;
    [SerializeField] GameObject IconPlay;
    [SerializeField] GameObject IconPause;
    [SerializeField] GameObject IconAll;
    [SerializeField] GameObject IconLoop;
    [SerializeField] GameObject IconShuffle;
    [SerializeField] GameObject ButtonBGMSelect;
    [SerializeField] GameObject BGMSelectContent;
    [SerializeField] GameObject ButtonCloseOpen;
    [SerializeField] GameObject ButtonCloseClose;
    [SerializeField] GameObject parent;
    [SerializeField] TabButtonController ButtonCategoryGame;
    [SerializeField] TabButtonController ButtonCategoryCustom;
    [SerializeField] AudioClip clip_Ragnarok;
    [SerializeField] AudioClip clip_Boost;

    private int tmpSelect = 0;
    private int playIndex = 0;
    private bool isPlay = false;
    private int playMode = 0;
    private bool isInit = false;
    private bool isPlayLoading = false;
    private bool isOpen = true;
    private bool isGameBGM = true;
    private int GameBGMNum = 0;
    private int CustomBGMNum = 0;

    private string CustomSoudsRoot = Application.streamingAssetsPath;
    public string DirName = "CustomSounds";

    private bool isClick;

    private void Awake()
    {
        
        //LoadGameSounds();
        _ = taskLoadCustomSounds();

        CloseView(isOpen);
    }

    //public IEnumerator corLoadCustomSounds(Action onComplete=null, Action<int> onStart = null, Action<int> onProgress = null)
    //{
    //    // 使用前に呼び出す必要がある
    //    BetterStreamingAssets.Initialize();

    //    // StreamingAssets 内のすべてのファイルを列挙する
    //    //var files = BetterStreamingAssets.GetFiles("\\", "*", SearchOption.AllDirectories);
    //    var files = BetterStreamingAssets.GetFiles("\\" + DirName + "\\", "*", SearchOption.AllDirectories);

    //    onStart?.Invoke(files.Length);

    //    int _cnt = 0;
    //    foreach (var file in files)
    //    {
    //        Debug.Log($"file:{file}");
    //        _cnt++;

    //        GameObject obj = Instantiate(ButtonBGMSelect, BGMSelectContent.transform, false) as GameObject;
    //        obj.name = file;
    //        obj.SetActive(true);
    //        PlayListButtons.Add(obj);

    //        BgmButtonManager bgmButtonManager = obj.GetComponent<BgmButtonManager>();
    //        string _name = file.Replace($"{DirName}/", "").Replace(".mp3", "");
    //        bgmButtonManager.Name = _name;
    //        bgmButtonManager.textName.text = _name;
    //        bgmButtonManager.streamingPass = file;
    //        bgmButtonManager.audioClip = null;
    //        bgmButtonManager.index = PlayListButtons.Count;
    //        bgmButtonManager.isCustom = true;
    //        bgmButtonManager.SetButton((_index) => { ButtonSelect(_index); });

    //        if (!CheckConvertedMP3toWAV(file))
    //        {
    //            onProgress?.Invoke(_cnt);
    //            yield return null;
    //        }

    //        string wavPath = "";
    //        ConvertMP3toWAV(file, out wavPath);

    //    }

    //    onProgress?.Invoke(files.Length);
    //    onComplete?.Invoke();

    //    yield break;
    //}

    public void ChangeSelectList(bool gameBGM = true)
    {
        isGameBGM = gameBGM;

        UpdatePlayListView();
        BottomButtonView();
    }

    // 内臓BGM
    private void LoadGameSounds()
    {
        foreach (var clip in GameInPlayList)
        {
            string file = clip.name;
            SetBgmSelectButton(file, (AudioClip)clip, false, false);
            GameBGMNum++;
        }
    }


    // メソッドにasync修飾子を付ける、戻り値はTaskに
    public async UniTask taskLoadCustomSounds(Action onComplete = null, Action<int> onStart = null, Action<int> onProgress = null)
    {

        // 使用前に呼び出す必要がある
        BetterStreamingAssets.Initialize();



        // StreamingAssets 内のすべてのファイルを列挙する
        //var files = BetterStreamingAssets.GetFiles("\\", "*", SearchOption.AllDirectories);
        var files = BetterStreamingAssets.GetFiles("\\" + DirName + "\\", "*", SearchOption.AllDirectories);

        CustomBGMNum = 0;
        foreach (var file in files)
        {
            if (!file.Contains(".mp3")) continue;
            CustomBGMNum++;
        }
        string mymusicPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "\\" + DirName;
        if (Directory.Exists(mymusicPath))
        {
            DirectoryInfo dir = new DirectoryInfo(mymusicPath);
            FileInfo[] info = dir.GetFiles("*.mp3");
            foreach (FileInfo f in info)
            {
                CustomBGMNum++;
            }
        }
        onStart?.Invoke(CustomBGMNum);


        int _cnt = 0;
        foreach (var file in files)
        {
            if (!file.Contains(".mp3")) continue;

            _cnt++;

            SetBgmSelectButton(file, null, true, false);

            if (!CheckConvertedMP3toWAV(file))
            {
                onProgress?.Invoke(_cnt);

                //非同期でロード
                _ = taskConvertMp3ToWav(GetFilePath(file), GetWavPath(file));
            }
        }

        if (Directory.Exists(mymusicPath))
        {
            DirectoryInfo dir = new DirectoryInfo(mymusicPath);
            FileInfo[] info = dir.GetFiles("*.mp3");
            foreach (FileInfo f in info)
            {
                string file = f.Name;

                _cnt++;

                SetBgmSelectButton(file,null,false,true);

                if (!CheckConvertedMP3toWAV(file))
                {
                    onProgress?.Invoke(_cnt);

                    //非同期でロード
                    _ = taskConvertMp3ToWav(GetFilePathMyMusic(file), GetWavPath(file));
                }
            }
        }


        onProgress?.Invoke(files.Length);



        onComplete?.Invoke();
    }


    private void SetBgmSelectButton(string file, AudioClip _clip,　bool _isCustom, bool _isMyMusic)
    {
        GameObject obj = Instantiate(ButtonBGMSelect, BGMSelectContent.transform, false) as GameObject;
        obj.name = file;
        obj.SetActive(true);
        PlayListButtons.Add(obj);

        BgmButtonManager bgmButtonManager = obj.GetComponent<BgmButtonManager>();
        string _name = file.Replace($"{DirName}/", "").Replace(".mp3", "");
        bgmButtonManager.Name = _name;
        bgmButtonManager.textName.text = _name;
        bgmButtonManager.streamingPass = file;
        bgmButtonManager.audioClip = _clip;
        bgmButtonManager.index = PlayListButtons.Count;
        bgmButtonManager.isCustom = _isCustom;
        bgmButtonManager.isMyMusic = _isMyMusic;
        bgmButtonManager.SetButton((_index) => { ButtonSelect(_index, false); });
    }

    public void Init(int _Index = 0, int _playMode = 0)
    {
        playMode = _playMode;
        playIndex = _Index;
        isGameBGM = playIndex <= GameBGMNum;
        ChangeSelectList(isGameBGM);
        ButtonSelect(playIndex);
        Play();
        BottomButtonView();

        isInit = true;
    }

    private void Update()
    {

        if (isInit && isPlay && !isPlayLoading)
        {
            if (!SoundManager.Instance.PlayBGMCheck())
            {
                //Debug.Log($"playMode:{playMode}");
                switch (playMode)
                {
                    case 0:
                        tmpSelect = playIndex;
                        Play();
                        break;
                    case 1:
                        tmpSelect = playIndex;
                        ButtonClickNext(1);
                        break;
                    case 2:
                        tmpSelect = GetShuffleIndex();
                        Play();
                        break;
                }
            }
        }
    }


    private void UpdatePlayListView()
    {
        for (int i=0; i < PlayListButtons.Count; i++)
        {
            BgmButtonManager bgmButtonManager = PlayListButtons[i].GetComponent<BgmButtonManager>();
            if (isGameBGM)
            {
                PlayListButtons[i].SetActive((!bgmButtonManager.isCustom && !bgmButtonManager.isMyMusic));
            }
            else
            {
                PlayListButtons[i].SetActive((bgmButtonManager.isCustom || bgmButtonManager.isMyMusic));
            }

            if (!isPlay)
            {
                PlayListButtons[i].GetComponent<BgmButtonManager>().Stop();
                continue;
            }


            if ((playIndex - 1) == i)
            {
                PlayListButtons[i].GetComponent<BgmButtonManager>().Play();
            }
            else
            {
                PlayListButtons[i].GetComponent<BgmButtonManager>().Stop();
            }
        }
    }


    public void PlayOrPause()
    {
        if (IconPlay.activeSelf) Play();
        else Pause();

    }

    public void Play()
    {
        if (isPlayLoading) return;

        if (PlayListButtons.Count < tmpSelect) tmpSelect = PlayListButtons.Count;
        else if (tmpSelect <= 0) tmpSelect = 1;

        isPlay = true;

        bool restartFlg = (playIndex == tmpSelect);
        playIndex = tmpSelect;
        GameData.BgmSelect = playIndex;

        //Debug.Log($"Play:{playIndex}");
        if (restartFlg)
        {
            SoundManager.Instance.StopBgm();
        }
        //if (PlayListButtons[playIndex-1].GetComponent<BgmButtonManager>().isCustom)
        //{
        //    //カスサン
        //    string mp3Path = PlayListButtons[(playIndex - 1)].GetComponent<BgmButtonManager>().streamingPass;
        //    PlayStreaming(mp3Path, GetFilePath(mp3Path));
        //}
        //else if (PlayListButtons[playIndex - 1].GetComponent<BgmButtonManager>().isMyMusic)
        //{
        //    //MyMusic版カスサン
        //    string mp3Path = PlayListButtons[(playIndex - 1)].GetComponent<BgmButtonManager>().streamingPass;
        //    PlayStreaming(mp3Path, GetFilePathMyMusic(mp3Path));
        //}
        //else
        //{
        //    //内臓
        SoundManager.Instance.PlayBgm(GameInPlayList[0]);
        //}

        if (!restartFlg) ButtonSelect(tmpSelect);
        UpdatePlayListView();
        BottomButtonView();
        DataManager.Instance.Save();
    }

    public void Pause()
    {
        if (isPlayLoading) return;
        isPlay = false;

        SoundManager.Instance.StopBgm();

        UpdatePlayListView();
        BottomButtonView();
        DataManager.Instance.Save();
    }


    //各ボタン見た目
    private void BottomButtonView()
    {
        SoundManager.Instance.ChangeBgmLoop(playMode == 0);

        if (playIndex == tmpSelect)
        {
            IconPlay.SetActive(!isPlay);
            IconPause.SetActive(isPlay);
        }
        else
        {
            IconPlay.SetActive(true);
            IconPause.SetActive(false);
        }

        IconLoop.SetActive(playMode == 0);
        IconAll.SetActive(playMode == 1);
        IconShuffle.SetActive(playMode == 2);

        ButtonCategoryGame.ChangeView(isGameBGM);
        ButtonCategoryCustom.ChangeView(!isGameBGM);
    }


    public void ButtonSelect(int num = 0, bool isSingleClickFlg=true)
    {
        if (isSingleClickFlg)
        {
            tmpSelect = num;

            //Debug.Log($"PlayListButtons {PlayListButtons.Count}");
            for (int i = 0; i < PlayListButtons.Count; i++)
            {
                PlayListButtons[i].GetComponent<BgmButtonManager>().Select((tmpSelect - 1) == i);
            }

            BottomButtonView();
        }
        else
        {
            OnClick(
                () => {
                    tmpSelect = num;

                    Debug.Log($"PlayListButtons {PlayListButtons.Count}");
                    for (int i = 0; i < PlayListButtons.Count; i++)
                    {
                        PlayListButtons[i].GetComponent<BgmButtonManager>().Select((tmpSelect - 1) == i);
                    }

                    BottomButtonView();
                },
                () => {
                    PlayOrPause();
                }
            );
        }


    }


    public void ButtonClickNext(int add = 0)
    {
        if (isPlayLoading) return;


        if (playMode == 0)
        {
            tmpSelect = playIndex;
        }
        //一旦頭のひとつ前に合わせる
        if (isGameBGM || CustomBGMNum == 0)
        {
            if (GameBGMNum < tmpSelect) tmpSelect = 0;
        }
        else
        {
            if (tmpSelect <= GameBGMNum) tmpSelect = GameBGMNum;
        }

        if (playMode == 0)
        {
            Play();
            return;
        }


        tmpSelect += add;


        if (isGameBGM || CustomBGMNum == 0)
        {
            if (GameBGMNum < tmpSelect) tmpSelect = 1;
            else if (tmpSelect <= 0) tmpSelect = GameBGMNum;
        }
        else
        {
            if (PlayListButtons.Count < tmpSelect) tmpSelect = GameBGMNum + 1;
            else if (tmpSelect <= GameBGMNum) tmpSelect = PlayListButtons.Count;
        }

        if (playMode == 2)
        {
            //shuffle
            tmpSelect = GetShuffleIndex();
        }


        Play();
    }

    public int GetShuffleIndex()
    {
        if (isGameBGM || CustomBGMNum == 0)
        {
            return UnityEngine.Random.RandomRange(0, GameBGMNum) + 1;
        }
        else
        {
            return UnityEngine.Random.RandomRange(GameBGMNum, PlayListButtons.Count) + 1;
        }
    }

    public void ButtonClickShuffle()
    {
        playMode++;
        if (playMode > 2)
        {
            playMode = 0;
        }
        GameData.AudioPlayMode = playMode;
        DataManager.Instance.Save();
        BottomButtonView();
    }










    // Use this for initialization
    private void PlayStreaming(string mp3Path, string _filepath)
    {
        Debug.Log($"PlayStreaming mp3Path:{mp3Path}");


        if (File.Exists(_filepath) && mp3Path.Contains(".mp3"))
        {
            //wav 変換した一時ファイルを保存するパス
            //var wavPath = Application.temporaryCachePath + "/converted.wav";

            string wavPath = Convert(mp3Path, _filepath);
            StartCoroutine(PlayWav(wavPath));
        }
        else
        {
            Debug.Log($"File not found : {_filepath}");
        }
    }


    private bool CheckConvertedMP3toWAV(string mp3Path)
    {
        bool converted = false;
       
        string wavPath = GetWavPath(mp3Path);
        if (File.Exists(wavPath))
        {
            converted = true;
        }

        return converted;
    }
    private string Convert(string mp3Path, string _filepath)
    {
        string wavPath = "";
        if (File.Exists(_filepath))
        {
            wavPath = GetWavPath(mp3Path);
            if (!CheckConvertedMP3toWAV(mp3Path))
            {
                ConvertMp3ToWav(_filepath, wavPath);
            }
        }

        return wavPath;
    }
    private string GetFilePath(string mp3Path)
    {
        var _filepath = $"{CustomSoudsRoot}/{mp3Path}";
        return _filepath;
    }
    private string GetFilePathMyMusic(string mp3Path)
    {
        var _filepath = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "/" + DirName}/{mp3Path}";
        return _filepath;
    }
    private string GetWavPath(string mp3Path)
    {
        string fileName = mp3Path.Replace($"{DirName}/", "").Replace(".mp3", "");
        string wavPath = Application.temporaryCachePath + "/" + fileName + ".wav";
        return wavPath;
    }

    //mp3 → wav 変換して、保存したパスを返す
    public void ConvertMp3ToWav(string mp3Path, string wavPath)
    {
        //ファイルを byte 配列で読み込み
        var bytes = File.ReadAllBytes(mp3Path);

        //wav を一時ファイルとして保存
        using (var stream = new MemoryStream())
        {
            stream.Write(bytes, 0, bytes.Length);
            stream.Position = 0;

            using (var reader = new Mp3FileReader(stream))
            {
                WaveFileWriter.CreateWaveFile(wavPath, reader);  //wav で書き出し
                Debug.Log($"Convert to wav successfully : {wavPath}");
            }
        }
    }


    public async Task taskConvertMp3ToWav(string mp3Path, string wavPath)
    {
        await Task.Run(()=> { 
            //ファイルを byte 配列で読み込み
            var bytes = File.ReadAllBytes(mp3Path);

            //wav を一時ファイルとして保存
            using (var stream = new MemoryStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Position = 0;

                using (var reader = new Mp3FileReader(stream))
                {
                    WaveFileWriter.CreateWaveFile(wavPath, reader);  //wav で書き出し
                    Debug.Log($"Convert to wav successfully : {wavPath}");
                }
            }
        });
    }



    //ストリーミング再生する
    IEnumerator PlayWav(string path)
    {
        if (isPlayLoading) yield break;
        isPlayLoading = true;

        //Debug.Log($"PlayWav:{path}");
        using (var www = new WWW("file://" + path))
        {
            while (!www.isDone)
                yield return null;

            if (string.IsNullOrEmpty(www.error))
            {
                var clip = www.GetAudioClip(false, true);
                //audioSource.clip = clip;
                //audioSource.Play();
                SoundManager.Instance.PlayBgm(clip);
                Debug.Log($"Play wav : {path}");
            }
            else
            {
                Debug.Log(www.error);
            }
        }
        isPlayLoading = false;
    }



    public void ButtonCloseView(bool b = true)
    {
        CloseView(b);
    }
    private void CloseView(bool b = true)
    {
        //ButtonCloseOpen.SetActive(b);
        //ButtonCloseClose.SetActive(!b);
        //parent.SetActive(b);
    }




    public void PlayBGMRagnarok()
    {
        Pause();
        SoundManager.Instance.PlayBgm(clip_Ragnarok);
    }

    public void PlayBGMBoost(bool _play = true)
    {
        if (_play)
        {
            if (SoundManager.Instance.mAudioBgm.clip == clip_Boost) return;

            SoundManager.Instance.PlayBgm(clip_Boost);
        }
        else
        {
            if (SoundManager.Instance.mAudioBgm.clip == clip_Boost)
            {
                if (isPlay) Play();
                else Pause();
            }
        }
    }





    /// <summary>
    /// ダブルクリック用
    /// </summary>
    /// <param name="OnDoubleClick"></param>
    public void OnClick(Action OnSingleClick = null,Action OnDoubleClick = null)
    {
        if (!isClick)
        {
            isClick = true;
            Debug.Log($"OnSingleClick");
            OnSingleClick?.Invoke();
            StartCoroutine(MeasureTime());
        }
        else
        {
            //DoubleClick();
            Debug.Log($"OnDoubleClick");
            isClick = false;
            OnDoubleClick?.Invoke();
        }

    }

    IEnumerator MeasureTime()
    {
        var times = 0f;
        while (isClick)
        {
            times += Time.deltaTime;
            if (times < 0.3f)
            {
                yield return null;
            }
            else
            {
                isClick = false;
                //SingleClick();
                yield break;
            }
        }
    }


}
