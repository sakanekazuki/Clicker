using System.Collections;
using System.Collections.Generic;
using naichilab.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class ResetGameManager : MonoBehaviour
{
    [SerializeField] private Text TextUserSeed;
    [SerializeField] private Text TextGetSeed;
    [SerializeField] private Text TextAttent;
    [SerializeField] private MainCanvas mainCanvas;

    [SerializeField] GameObject GameModeRagnarokCanvas;
    [SerializeField] RagnarokSkillManager ragnarokSkillManager;

    public void Show()
    {
        //止めてみる？
        //mainCanvas.isMainGameStop();
        ragnarokSkillManager.ViewMode(false);

        ViewUI();

        gameObject.SetActive(true);
    }

    private void Update()
    {
        //言語切り替えのためあえてUpdateでも走らせる
        ViewUI();
    }

    private void ViewUI()
    {
        TextUserSeed.text = $"{LanguageCSV.Instance.GetCSV(GameData.SEED_USER_TEMPLATE)}{GameData.SeedYggdrasil.ToReadableString()}";
        TextGetSeed.text = $"+{CalcData.GetSeedNum(out float _).ToReadableString()}";

        TextAttent.text = $"{LanguageCSV.Instance.GetCSV(GameData.SEED_CONFIRM).Replace("○○", CalcData.GetSeedNum(out float _).ToReadableString())}";
    }

    public void Close()
    {
        //mainCanvas.isMainGameStopResume();
        Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ButtonRagnarokView()
    {
        GameModeRagnarokCanvas.SetActive(true);
        ragnarokSkillManager.ViewMode(true);
    }
    public void ButtonRagnarokViewClose()
    {
        GameModeRagnarokCanvas.SetActive(false);
        ragnarokSkillManager.ViewMode(false);
    }
}
