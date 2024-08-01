using System.Collections;
using System.Collections.Generic;
using naichilab.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRagnarokSkillController : MonoBehaviour
{
    [SerializeField] MainCanvas mainCanvas;
    [SerializeField] PopUpWindowController PopWindow;
    [SerializeField] string NameBase = "ButtonRagnarokSKill";
    [SerializeField] int SkillIndex;
    [SerializeField] Text ButtonLabel;
    [SerializeField] Button button;
    [SerializeField] GameObject[] openedImages;
    [SerializeField] List<Button> TargatChain;
    [SerializeField] SourceImageManager sourceImageManager;
    [SerializeField] RagnarokSkillManager ragnarokSkillManager;

    private void Reset()
    {
        ButtonLabel = GetComponentInChildren<Text>();
        button = GetComponent<Button>();

        //SkillIndex = transform.parent.childCount;
        //ButtonLabel.text = SkillIndex.ToString();
        //gameObject.name = $"{NameBase}_{SkillIndex}";
    }

    private void Start()
    {

        ButtonLabel.text = SkillIndex.ToString();
        //sourceImageManager.SetSprite(GameData.mYggdrasilSkill[SkillIndex].icon);
        sourceImageManager.SetSprite(SkillIndex - 1);

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => {
            bool checkPrise = GameData.mYggdrasilSkill[SkillIndex].price <= GameData.SeedYggdrasil;
            bool opened = GameData.RagnarokSkill[SkillIndex];

            PopWindow.GetComponent<PopUpWindowController>().Label.text = LanguageCSVparams.Instance.GetCSV(GameData.mYggdrasilSkill[SkillIndex].name, LanguageParamsClass.Field.name); 
            PopWindow.GetComponent<PopUpWindowController>().LabelDis.text = LanguageCSVparams.Instance.GetCSV(GameData.mYggdrasilSkill[SkillIndex].name, LanguageParamsClass.Field.dis);


            LanguageTranslation _labelNameTrans = PopWindow.GetComponent<PopUpWindowController>().Label.GetComponent<LanguageTranslation>();
            _labelNameTrans.TranslationKey = GameData.mYggdrasilSkill[SkillIndex].name;
            _labelNameTrans.translationType = LanguageTranslation.TranslationType.Params;
            _labelNameTrans.paramsField = LanguageParamsClass.Field.name;

            LanguageTranslation _labelDisTrans = PopWindow.GetComponent<PopUpWindowController>().LabelDis.GetComponent<LanguageTranslation>();
            _labelDisTrans.TranslationKey = GameData.mYggdrasilSkill[SkillIndex].name;
            _labelDisTrans.translationType = LanguageTranslation.TranslationType.Params;
            _labelDisTrans.paramsField = LanguageParamsClass.Field.dis;

            PopWindow.GetComponent<PopUpWindowController>().LabelPrice.text = $"{GameData.mYggdrasilSkill[SkillIndex].price.ToReadableString()}";
            PopWindow.GetComponent<PopUpWindowController>().LabelPrice.color = checkPrise ? GameData.COLOR_TRUE : GameData.COLOR_FALSE;
            PopWindow.GetComponent<PopUpWindowController>().LabelPrice.enabled = !opened;
            PopWindow.GetComponent<PopUpWindowController>().LabelPriceUI.enabled = !opened;
            PopWindow.GetComponent<PopUpWindowController>().MainButtonObj.SetActive(!opened);
            if (ragnarokSkillManager.ModeView)
            {
                PopWindow.GetComponent<PopUpWindowController>().MainButtonObj.SetActive(false);
            }
            PopWindow.GetComponent<PopUpWindowController>().MainButtonObj.GetComponent<Button>().interactable = checkPrise;
            PopWindow.GetComponent<PopUpWindowController>().buttonAction = () => {
                mainCanvas.OpenRagnarokSkill(SkillIndex);
                PopWindow.Hide();
            };
            PopWindow.Show();
        });
    }

    private void Update()
    {
        bool b = true;
        foreach (Button btn in TargatChain)
        {
            if (!GameData.RagnarokSkill[btn.GetComponent<ButtonRagnarokSkillController>().SkillIndex])
            {
                b = false;
                break;
            }
        }
        foreach (GameObject obj in openedImages)
        {
            obj.SetActive(GameData.RagnarokSkill[SkillIndex]);
        }
        button.interactable = b;
    }

    public void ButtonClick()
    {
        PopWindow.Show();
    }

}
