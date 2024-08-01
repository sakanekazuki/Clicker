using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    public float speed = 0.001f;
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 4f;
    [SerializeField] private SpriteRenderer sprite;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = transform.localPosition;
        p.x += speed;
        transform.localPosition = p;

        float prog = (p.x - minX) / (maxX - minX);
        float a = 1f;
        if (prog <= 0.1f) a = prog * 10f;
        else if (prog >= 0.9f) a = (prog - 1f) * -10f;
        if (a >= 0.9f) a = 0.9f;//多少見える?
        //Debug.Log($"prog:{prog} a:{a} {(255f * a)}");

        var color = sprite.color;
        color.a = a;
        sprite.color = color;


        if (p.x <= minX || maxX <= p.x)
        {
            Destroy(gameObject);
        }


    }

    public void init(float progress = 0f, Sprite sp = null)
    {
        sprite.sprite = sp;

        Vector3 p = transform.localPosition;
        p.x = minX + (maxX - minX) * progress;

        p.y = UnityEngine.Random.RandomRange(minY, maxY);

        transform.localPosition = p;


        float r = UnityEngine.Random.RandomRange(0.75f, 2f);
        transform.localScale = new Vector3(r, r, 1);

        Update();
    }
}
