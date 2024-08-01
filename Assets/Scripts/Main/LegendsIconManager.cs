using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegendsIconManager : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] List<GameObject> prefabs;
    [SerializeField] List<SpriteRenderer> spriteRenderer;
    [SerializeField] CircleDeployer circleDeployer;
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

    public void SetIcon(bool fst = false)
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



        if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_2])
        {
            GameObject obj = Instantiate(prefabs[0], parent, false) as GameObject;
            obj.name = "2";
            obj.SetActive(true);
            spriteRenderer.Add(obj.GetComponent<SpriteRenderer>());
        }
        if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_23])
        {
            GameObject obj = Instantiate(prefabs[1], parent, false) as GameObject;
            obj.name = "23";
            obj.SetActive(true);
            spriteRenderer.Add(obj.GetComponent<SpriteRenderer>());
        }
        if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_24])
        {
            GameObject obj = Instantiate(prefabs[2], parent, false) as GameObject;
            obj.name = "24";
            obj.SetActive(true);
            spriteRenderer.Add(obj.GetComponent<SpriteRenderer>());
        }
        if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_49])
        {
            GameObject obj = Instantiate(prefabs[3], parent, false) as GameObject;
            obj.name = "49";
            obj.SetActive(true);
            spriteRenderer.Add(obj.GetComponent<SpriteRenderer>());
        }
        if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_51])
        {
            GameObject obj = Instantiate(prefabs[4], parent, false) as GameObject;
            obj.name = "51";
            obj.SetActive(true);
            spriteRenderer.Add(obj.GetComponent<SpriteRenderer>());
        }
        if (GameData.RagnarokSkill[(int)GameData.RagnarokSkillIndex.SKILL_52])
        {
            GameObject obj = Instantiate(prefabs[5], parent, false) as GameObject;
            obj.name = "52";
            obj.SetActive(true);
            spriteRenderer.Add(obj.GetComponent<SpriteRenderer>());
        }





        yield return new WaitForEndOfFrame();

        circleDeployer.PositionDeploy();

        yield break;
    }
}
