using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMessageManager : MonoBehaviour
{
    [SerializeField] GameObject MessageContent;
    private List<string> messagePool = new List<string>();
    private List<int> sePool = new List<int>();
    private Text MessageContentText;
    private TypefaceAnimator typefaceAnimator;
    [SerializeField] private AudioClip SE_AchievementGet;

    public void SetMessage(string m="",int se=0)
    {
        messagePool.Add(m);
        sePool.Add(se);
    }

    private void Update() { 

        if (MessageContentText == null) MessageContentText = MessageContent.GetComponent<Text>();
        if (typefaceAnimator == null) typefaceAnimator = MessageContent.GetComponent<TypefaceAnimator>();

        //Debug.Log($"typefaceAnimator {typefaceAnimator.progress} {messagePool.Count}");
        if ((typefaceAnimator.progress == 0f || typefaceAnimator.progress == 1f) && messagePool.Count > 0)
        {
            MessageContentText.text = messagePool[0];
            messagePool.RemoveAt(0);
            if (sePool[0] == 1)
            {
                //SE
                SoundManager.Instance.PlaySe(SE_AchievementGet);
            }
            sePool.RemoveAt(0);
            typefaceAnimator.progress = 0f;
            MessageContent.GetComponent<TypefaceAnimator>().Play();
        }
        
    }

}
