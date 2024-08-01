using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconLoopManager : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] List<GameObject> prefabs;
    [SerializeField] List<SpriteRenderer> spriteRenderer;
    [SerializeField] CircleDeployer circleDeployer;
    [SerializeField] private RuntimeAnimatorController ClickWhite;
    private Transform transform;

    private float rotateX = 0f;
    private float rotateY = 0f;
    private float rotateZ = 5f;

    private Coroutine _corUpdate;
    private Coroutine _cor;
    private bool isInit = false;

    private void Awake()
    {
        transform = parent;
    }

    public void Init()
    {
        UpdateIcon();

        SetIcon(true);

        isInit = true;
    }

    private void UpdateIcon()
    {
        if (_corUpdate != null)
        {
            StopCoroutine(_corUpdate);
        }
        _corUpdate = StartCoroutine(corUpdateIcon());
    }

    void Update()
    {
        gameObject.transform.Rotate(new Vector3(rotateX, rotateY, rotateZ) * Time.deltaTime);
    }

    public void SetIcon(bool fst=false)
    {
        if (_cor != null)
        {
            StopCoroutine(_cor);
        }
        _cor = StartCoroutine(corOpenIcon(fst));
    }

    private IEnumerator corOpenIcon(bool fst = true)
    {
        int _cnt = 60;
        if (fst) _cnt = 1;
        while (_cnt > 0)
        {
            foreach (SpriteRenderer sr in spriteRenderer)
            {
                Color c = sr.color;
                c.a -= (0.01667f);
                if (_cnt == 1) c.a = 0;
                sr.color = c;
            }
            _cnt--;

            yield return null;
        }

        UpdateIcon();
        foreach (SpriteRenderer sr in spriteRenderer)
        {
            Color c = sr.color;
            c.a = 0;
            sr.color = c;
        }

        yield return new WaitForSeconds(0.1f);

        _cnt = 60;
        while (_cnt > 0)
        {
            foreach (SpriteRenderer sr in spriteRenderer)
            {
                Color c = sr.color;
                c.a += (0.01667f);
                if (_cnt == 1) c.a = 1;
                sr.color = c;
            }
            _cnt--;

            yield return null;
        }

        yield break;
    }


    private IEnumerator corUpdateIcon()
    {

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        spriteRenderer = new List<SpriteRenderer>();

        for (int instCase = 1; instCase < (GameData.INST_COST_BASE.Count); instCase++)
        {
            if (GameData.InstLv[instCase] > 0)
            {
                GameObject obj = Instantiate(prefabs[instCase - 1], parent, false) as GameObject;
                obj.name = instCase.ToString();
                obj.SetActive(true);

                spriteRenderer.Add(obj.GetComponent<SpriteRenderer>());
            }
        }

        yield return new WaitForEndOfFrame();

        circleDeployer.PositionDeploy();

        yield break;
    }

    public bool CheckGetPos(int InstCase = 0)
    {
        InstCase--;
        if (spriteRenderer.Count > InstCase)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public Vector3 GetPos(int InstCase=0)
    {
        InstCase--;
        if (spriteRenderer.Count > InstCase)
        {
            return spriteRenderer[InstCase].gameObject.transform.position;
        }
        else
        {
            return new Vector3(1000,1000,1000);
        }
    }

    public void Flash(int InstCase = 0)
    {
        InstCase--;
        if (spriteRenderer.Count > InstCase)
        {
            spriteRenderer[InstCase].gameObject.GetComponent<Animator>().runtimeAnimatorController = null;
            spriteRenderer[InstCase].gameObject.GetComponent<Animator>().runtimeAnimatorController = ClickWhite;
        }
    }
}
