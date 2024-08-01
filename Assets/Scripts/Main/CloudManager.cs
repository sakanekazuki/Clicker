using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    [SerializeField] private Transform cloudParent;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject spriteObj;
    [SerializeField] private float speed = -0.001f;
    [SerializeField] private int firstCreateMax = 30;
    [SerializeField] private int CreatePar = 1000;

    private string LayerBg = "Bg";
    private string LayerFront = "FrontCloud";

    private void Start()
    {
        //最初にいくつか作る
        int c = UnityEngine.Random.RandomRange(0, firstCreateMax);
        for (int i=0; i<=c; i++)
        {
            CreateCloud(UnityEngine.Random.RandomRange(0f, 1.0f));
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.C))
        {
            CreateCloud(UnityEngine.Random.RandomRange(0f, 1.0f));
        }
#endif

        if (UnityEngine.Random.RandomRange(0, CreatePar) == 0)
        {
            CreateCloud(1f);
        }

    }

    public void Init()
    {
        
    }

    private void CreateCloud(float progress = 0f)
    {
        //Debug.Log($"cloud");

        GameObject obj = Instantiate(spriteObj, cloudParent.transform, false) as GameObject;
        CloudController cc = obj.GetComponent<CloudController>();
        cc.speed = speed * UnityEngine.Random.RandomRange(0.9f, 1.1f);
        cc.init(progress, sprites[UnityEngine.Random.RandomRange(0, sprites.Length)]);


        //たまに手前
        if (UnityEngine.Random.RandomRange(0, 5) == 0)
        {
            obj.GetComponent<SpriteRenderer>().sortingLayerName = LayerFront;
        }

        obj.SetActive(true);
    }

}
