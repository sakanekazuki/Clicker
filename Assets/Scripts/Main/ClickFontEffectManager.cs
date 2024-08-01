using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickFontEffectManager : MonoBehaviour
{
    public static ClickFontEffectManager Instance { get; private set; }

    [SerializeField] private int CreateChild = 100;
    [SerializeField] private GameObject ChildPrefabs;
    private int FontSizeDef = 20;
    private int FontSizeBig = 60;

    private List<GameObject> ChildPool = new List<GameObject>();
    private int UseChildIndex = 0;

    void Start()
    {
        Instance = this;

        for (int i=0; i< CreateChild; i++)
        {
            GameObject obj = Instantiate(ChildPrefabs, transform, false) as GameObject;
            obj.name = i.ToString();
            obj.SetActive(false);
            obj.GetComponentInChildren<TypefaceAnimator>().onComplete.AddListener(obj.GetComponent<ClickFontEffectChildController>().OnFinish);
            ChildPool.Add(obj);
        }
    }

    public void View(string t, float x, float y, bool big=false, int adjPos=0)
    {
        //Debug.Log($"{x} {y} adjPos:{adjPos}");

        GameObject obj = ChildPool[UseChildIndex];
        obj.transform.localPosition = new Vector3(x, y, 0);
        Text _t = obj.GetComponentInChildren<Text>();
        _t.text = t;
        _t.fontSize = big ? FontSizeBig : FontSizeDef;
        TypefaceAnimator ta = obj.GetComponentInChildren<TypefaceAnimator>();
        ta.progress = 0f;
        ta.delay = 0.15f * (float)adjPos;
        //ta.Play();

        obj.SetActive(true);

        UseChildIndex++;
        if (UseChildIndex >= CreateChild) UseChildIndex = 0;
    }

    public void AllReset()
    {
        foreach (GameObject v in ChildPool)
        {
            v.SetActive(false);
        }
    }

}
