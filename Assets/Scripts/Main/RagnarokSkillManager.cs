using System.Collections;
using System.Collections.Generic;
using naichilab.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class RagnarokSkillManager : MonoBehaviour
{
    [SerializeField] private MainCanvas mainCanvas;
    [SerializeField] private GameObject ScrollContent;
    [SerializeField] private Text TextBonus;
    [SerializeField] private Text TextSeed;
    [SerializeField] private PopUpWindowController popUpWindowController;

    [SerializeField] private GameObject ViewRagnarok;
    [SerializeField] private GameObject ViewViewMode;
    public bool ModeView = false;

    private void OnEnable()
    {
        ScrollRect s = ScrollContent.transform.parent.parent.gameObject.GetComponent<ScrollRect>();
        s.verticalNormalizedPosition = 0.75f;
        s.horizontalNormalizedPosition = 0.5f;
        ScrollContent.transform.localScale = Vector3.one;

        UpdateRagnarokSkillResources();

        popUpWindowController.Hide();
    }

    public void UpdateRagnarokSkillResources()
    {
        TextBonus.text = "0";

        //神々の威厳 = ウロボロスの効果
        if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_1])
            TextBonus.text = CalcData.GetOuroborosPow().ToString();

        TextSeed.text = $"{GameData.SeedYggdrasil.ToReadableString()}";
    }

    public void CloseRagnarokSkillWindow()
    {
        mainCanvas.CloseRagnarokPhase();
    }

    public void ViewMode(bool b = true)
    {
        ViewRagnarok.SetActive(!b);
        ViewViewMode.SetActive(b);
        ModeView = b;
    }

    public void ButtonZoom(float add = 0f)
    {
        Vector3 s = ScrollContent.transform.localScale;
        s.x += add;
        if (s.x <= 0.4f) s.x = 0.4f;
        if (s.x >= 1f) s.x = 1f;
        s.y = s.x;
        s.z = s.x;
        ScrollContent.transform.localScale = s;
    }
}
