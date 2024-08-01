using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Coffee.UIEffects;
using UnityEngine.UI;

public class TransitionRuleController : MonoBehaviour
{
    public static TransitionRuleController Instance { get; private set; }

    public Texture[] rule_list;

    public UITransitionEffect targetUITransitionEffect;

    [SerializeField] private Image img;

    private Coroutine _tmp_col = null;

    private const float defTransitionRuleTime = 1f;

    private float TransitionRuleTime = 0;
    private int TransitionDirection = -1;

    private Action OnComplete = null;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        TransitionRuleTime = defTransitionRuleTime;


        if (targetUITransitionEffect == null)
        {
            targetUITransitionEffect = gameObject.GetComponent<UITransitionEffect>();
        }
        targetUITransitionEffect.effectFactor = 0f;

        if (targetUITransitionEffect.transitionTexture == null)
        {
            targetUITransitionEffect.transitionTexture = rule_list[0];
        }

        img.raycastTarget = false;
    }

    public void On(Action _onComplete = null, int i = 0, float time = defTransitionRuleTime)
    {
        ChangeTexture(i);
        TransitionRuleTime = time;

        OnComplete = _onComplete;

        TransitionDirection = 1;

        RulePlay();
    }
    public void Off(Action _onComplete = null, int i = 0, float time = defTransitionRuleTime)
    {
        //DebugLogger.Log("Off " + time);
        ChangeTexture(i);
        TransitionRuleTime = time;

        OnComplete = _onComplete;

        TransitionDirection = 0;

        RulePlay();
    }
    public void CheckOff(Action _onComplete = null, int i = 0, float time = defTransitionRuleTime)
    {
        if (targetUITransitionEffect.effectFactor == 0f)
        {
            time = 0f;
        }
        Off(_onComplete, i, time);
    }

    private void ChangeTexture(int i = 0)
    {
        targetUITransitionEffect.transitionTexture = rule_list[i];
    }

    private void RulePlay()
    {
        gameObject.SetActive(true);
        img.raycastTarget = true;

        if (_tmp_col != null) StopCoroutine(_tmp_col);
        _tmp_col = StartCoroutine(RulePlayCoroutine());
    }

    IEnumerator RulePlayCoroutine()
    {
        float nowFacter = targetUITransitionEffect.effectFactor;
        float toValue = TransitionDirection;

        //DebugLogger.Log("TransitionRuleTime " + TransitionRuleTime);
        if (TransitionRuleTime > 0f)
        {
            iTween.Stop(gameObject);
            iTween.ValueTo(gameObject, iTween.Hash(
                "from", nowFacter,
                "to", toValue,
                "time", TransitionRuleTime,
                "onupdate", "SetValueRule",
                "onupdatetarget", gameObject,
                "oncomplete", "CompleteRule",
                "oncompletetarget", gameObject
                ));
        }
        else
        {
            SetValueRule(TransitionDirection);
            CompleteRule();
        }


        yield break;
    }

    void SetValueRule(float num)
    {
        targetUITransitionEffect.effectFactor = num;
    }
    void CompleteRule()
    {

        //DebugLogger.Log("TransitionDirection " + TransitionDirection);
        if (TransitionDirection == 0)
        {
            gameObject.SetActive(false);
            img.raycastTarget = false;
        }

        //DebugLogger.Log("CompleteRule ");
        OnComplete?.Invoke();
    }

}
