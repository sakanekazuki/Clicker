using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageTranslation : MonoBehaviour
{
    public enum LanguageType
    {
        ja,
        en,
        tw,
        kr,
        cn
    }
    public enum TranslationType { 
        KeyValue,
        Params
    }
    [SerializeField] private Text targetText;
    [SerializeField] private ButtonLabelCloseController buttonLabelCloseController;
    [SerializeField] public string TranslationKey = "";
    [SerializeField] public TranslationType translationType;
    [Header("Paramsの時に必要")] [SerializeField] public LanguageParamsClass.Field paramsField;

    private int select_lang = 0;

    private void Reset()
    {
        targetText = GetComponent<Text>();
        if(targetText != null) TranslationKey = targetText.text;
    }
    private void Start()
    {
        if(TranslationKey == "") TranslationKey = targetText.text;
        UpdateTranslation();
    }

    public void ResetData()
    {
        select_lang = -1;
    }

    private void OnEnable()
    {
        Update();
    }

    private void Update()
    {
        if (GameData.language == select_lang) return;

        UpdateTranslation();
        select_lang = GameData.language;
        if (buttonLabelCloseController != null)
        {
            buttonLabelCloseController.labelText = targetText.text;
        }

    }

    private void UpdateTranslation()
    {
        switch (translationType)
        {
            default:
            case TranslationType.KeyValue:
                targetText.text = LanguageCSV.Instance.GetCSV(TranslationKey);
                break;
            case TranslationType.Params:
                targetText.text = LanguageCSVparams.Instance.GetCSV(TranslationKey, paramsField);
                break;
        }
    }

}
