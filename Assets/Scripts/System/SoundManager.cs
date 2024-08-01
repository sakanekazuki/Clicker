using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public AudioSource mAudioBgm; //BGM用AudioSource
    public AudioSource mSubAudioBgm; //BGM用SuBAudioSource[クロスフェード対応]
    public AudioSource mAudioSe;  //効果音用AudioSource
    public AudioSource mAudioVoice;  //ボイス用AudioSource
    public AudioSource mAudioVoiceB;  //ボイス用AudioSource
    public static SoundManager Instance = null;

    private bool introFlg = false;
    AudioClip bgm_clip_intro = null;
    AudioClip bgm_clip_loop = null;

    private List<AudioClip> clips = new List<AudioClip>();

    public static float MasterVolume = 1f;
    public static float BgmVolume = 1f;
    public static float SeVolume = 1f;
    public static float VoiceVolume = 1f;

    private bool half = false;

    private bool mute = false;

    //シングルトンの処理
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        //後々updateじゃないようにしたい

        float tmpBgmVol = BgmVolume;
        float tmpSeVol = SeVolume;
        if (half)
        {
            tmpBgmVol = tmpBgmVol * 0.3f;
        }
        if (mute)
        {
            tmpBgmVol = 0f;
            tmpSeVol = 0f;
        }

        //イントロ対応
        if (introFlg && bgm_clip_intro != null && bgm_clip_loop != null)
        {
            if (!mAudioBgm.isPlaying)
            {
                //ループ再開
                mAudioBgm.loop = true;
                PlayBgm(bgm_clip_loop);
                bgm_clip_intro = null;
                introFlg = false;
            }
        }

        //DebugTool.Log("bgm vol "+ vol);
        mAudioBgm.volume = tmpBgmVol * MasterVolume;
        mSubAudioBgm.volume = tmpBgmVol * MasterVolume;
        mAudioSe.volume = tmpSeVol * MasterVolume;
        mAudioVoice.volume = VoiceVolume * MasterVolume;
        mAudioVoiceB.volume = VoiceVolume * MasterVolume;

        //同じ音を一緒に鳴らさないようにする
        if (clips.Count > 0)
        {
            foreach (AudioClip a in clips)
            {
                mAudioSe.PlayOneShot(a, mAudioSe.volume);
            }
            clips = new List<AudioClip>();
        }

    }


    // BGMの再生
    public void PlayBgm(AudioClip clip)
    {

        bgm_clip_intro = null;
        bgm_clip_loop = null;

        //保険
        //if (clip == null)
        //{
        //    clip = (AudioClip)Resources.Load("MyProject/Sound/BGM/bgm_home_1");
        //    //if (obj != null)
        //    //{
        //    //    obj.GetComponent<Text>().text += " Hoken_" + clip.name;
        //    //}
        //}

        // 再生中かつ同じファイルなら再生しない
        if (mAudioBgm.isPlaying && mAudioBgm.clip == clip) return;

        // 音声セット
        mAudioBgm.clip = clip;

        mAudioBgm.loop = true;

        // 再生
        mAudioBgm.Play();
    }

    // クロスフェード[実験用]
    public void CrossFadeBGM(AudioSource oldSourceVoice, AudioSource newSourceVoice)
    {
        mSubAudioBgm = oldSourceVoice;
    }

    // BGM introセット
    public void SetBgmIntro(AudioClip clip)
    {
        bgm_clip_intro = clip;
    }
    public void SetBgmLoop(AudioClip clip)
    {
        bgm_clip_loop = clip;
    }
    // セットしたイントロ付きBGMの再生
    public void PlayBgmIntroToLoop()
    {
        AudioClip clip = bgm_clip_intro;

        // 再生中かつ同じファイルなら再生しない
        if (mAudioBgm.isPlaying && mAudioBgm.clip == clip) return;
        if (mAudioBgm.isPlaying && mAudioBgm.clip == bgm_clip_intro) return;
        if (mAudioBgm.isPlaying && mAudioBgm.clip == bgm_clip_loop) return;

        // 音声セット
        mAudioBgm.clip = clip;

        //Introはループしない
        mAudioBgm.loop = false;

        // 再生
        mAudioBgm.Play();

        introFlg = true;
    }


    // BGMの停止
    public void StopBgm()
    {
        mAudioBgm.Stop();

        bgm_clip_intro = null;
        bgm_clip_loop = null;
    }
    // BGMの一時停止
    public void PauseBgm()
    {
        mAudioBgm.Pause();
    }

    // SEの再生
    public void PlaySe(AudioClip clip)
    {
        //同じ音を一緒に鳴らさないようにする
        bool check = false;
        foreach (AudioClip a in clips)
        {
            if(a.name == clip.name)
            {
                check = true;
                break;
            }
        }
        if (!check)
        {
            clips.Add(clip);
        }
    }

    // SEの停止
    public void StopSe()
    {
        mAudioSe.Stop();
    }

    // 全部停止
    public void AllStop()
    {
        StopBgm();
        StopSe();
        StopVoice();
        StopVoiceB();
    }

    // BGMの音量半減
    public void setVolumeHalfBgm(bool b)
    {
        half = b;
    }
    // BGMの音量ミュート
    public void setVolumeMuteBgm(bool b)
    {
        mute = b;
    }
    // BGMの音量ミュート
    public bool getVolumeMuteBgm()
    {
        return mute;
    }

    // ボイスの再生
    public void PlayVoice(AudioClip clip)
    {
        float vol = VoiceVolume;
        mAudioVoice.volume = vol;

        //1つしか再生しない
        mAudioVoice.clip = clip;
        mAudioVoice.Play();
    }

    // ボイス再生チェック
    public bool PlayVoiceCheck()
    {
        return mAudioVoice.isPlaying;
    }

    // ボイスストップ
    public void StopVoice()
    {
        mAudioVoice.Stop();
    }


    // もうひとつのボイスの再生
    public void PlayVoiceB(AudioClip clip)
    {
        float vol = VoiceVolume;
        mAudioVoiceB.volume = vol;

        //1つしか再生しない
        mAudioVoiceB.clip = clip;
        mAudioVoiceB.Play();
    }

    // ボイス再生チェック
    public bool PlayVoiceCheckB()
    {
        return mAudioVoiceB.isPlaying;
    }

    // ボイスストップ
    public void StopVoiceB()
    {
        mAudioVoiceB.Stop();
    }

    //BGM再生チェック
    public bool PlayBGMCheck()
    {
        return mAudioBgm.isPlaying;
    }

    //ループ再生しない
    public void ChangeBgmLoop(bool b = true)
    {
        mAudioBgm.loop = b;
    }
}
