using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgmButtonGroupManager : MonoBehaviour
{
    public static BgmButtonGroupManager Instance { get; set; }

    [SerializeField] List<BgmButtonManager> bgmButtonManagers;
    [SerializeField] private Text bgmNameText;

    private void Awake()
    {
        Instance = this;
        PlayBGM(0);

	}

    public void ButtonPlayBGM(int _index)
    {
        if (bgmButtonManagers.Count <= _index) return;

        GameData.BgmSelect = (GameData.BgmSelect == _index) ? -1 : _index;

        PlayBGM(GameData.BgmSelect);
    }

    public void PlayBGM(int _index)
    {
        if (bgmButtonManagers.Count <= _index) return;

        GameData.BgmSelect = _index;
        if (_index < 0)
        {
            SoundManager.Instance.StopBgm();
            bgmNameText.gameObject.SetActive(false);
            bgmNameText.text = "---";
        }
        else
        {
            SoundManager.Instance.PlayBgm(bgmButtonManagers[_index].audioClip);
            bgmNameText.gameObject.SetActive(true);
            bgmNameText.text = bgmButtonManagers[_index].Name;
        }
        ViewButton();
        DataManager.Instance.Save();

    }

    private void ViewButton()
    {
        int _index = GameData.BgmSelect;
        for (int index = 0; index < bgmButtonManagers.Count; index++)
        {
            if (_index == index)
            {
                bgmButtonManagers[index].Play();
            }
            else
            {
                bgmButtonManagers[index].Stop();
            }
        }
    }

    public void ButtonOpen()
    {
        ViewButton();
        gameObject.SetActive(true);
    }
    public void ButtonClose()
    {
        gameObject.SetActive(false);
    }
}
