using System.Collections;
using System.Collections.Generic;
using Coffee.UIEffects;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    public static MessageManager Instance { get; private set; }

    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject arrow;
    [SerializeField] private TypefaceAnimator typefaceAnimator;
    [SerializeField] private Text MessageText;
    [SerializeField] private Button MessageClickButton;
    [SerializeField] private CanvasGroup canvasGroup;
    private List<string> messageList;

    private bool FlgButtonClick = false;
    private float alphaSpeed = 0.033f;
    private UITransitionEffect uiTransitionEffect;

    private void Awake()
    {
        if (Instance != null) Destroy(Instance);
        Instance = this;

        parent.SetActive(false);
        MessageText.text = "";
        uiTransitionEffect = arrow.GetComponent<UITransitionEffect>();
    }


    public void SetMessage(List<string> s)
    {
        messageList = s;
    }

    public void PlayMessage(List<string> s = null)
    {
        //Debug.Log($"PlayMessage");

        //if (s != null)
        //{
        //    messageList = s;
        //}

        //MessageText.text = "";
        //MessageClickButton.gameObject.SetActive(false);
        //canvasGroup.alpha = 0;
        //arrow.SetActive(false);
        //parent.SetActive(true);

        //StartCoroutine(CorMessage());
    }

    IEnumerator CorMessage()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += alphaSpeed;

            //Debug.Log($"canvasGroup.alpha:{canvasGroup.alpha}");
            yield return null;
        }
        MessageClickButton.gameObject.SetActive(true);

        while (messageList.Count > 0)
        {
            yield return new WaitForSeconds(0.1f);

            bool finishMsg = false;
            arrow.SetActive(false);
            MessageText.text = LanguageCSV.Instance.GetCSV(messageList[0]);
            typefaceAnimator.progress = 0;
            typefaceAnimator.Play();
            while (true)
            {
                if (typefaceAnimator.progress >= 1)
                {
                    if (!finishMsg)
                    {
                        //次への矢印表示
                        arrow.SetActive(true);
                    }
                    finishMsg = true;
                }

                if (FlgButtonClick)
                {
                    FlgButtonClick = false;

                    if (typefaceAnimator.progress < 1)
                    {
                        //メッセージ表示を一括
                        typefaceAnimator.progress = 1;
                    }
                    else
                    {
                        //次へ
                        arrow.SetActive(false);
                        MessageText.text = "";
                        messageList.RemoveAt(0);
                        break;
                    }
                }
                yield return null;
            }
            yield return null;
        }

        MessageClickButton.gameObject.SetActive(false);
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= alphaSpeed;
            //Debug.Log($"canvasGroup.alpha:{canvasGroup.alpha}");
            yield return null;
        }
        parent.SetActive(false);

        yield break;
    }

    public void ButtonClick()
    {
        //Debug.Log($"ButtonClick");
        FlgButtonClick = true;
    }

}
