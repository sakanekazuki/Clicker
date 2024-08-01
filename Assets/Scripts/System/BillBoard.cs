using UnityEngine;

public class BillBoard : MonoBehaviour
{
    void Update()
    {
        //var c = Camera.main.transform.position;
        //var p = transform.position;
        //c.x = p.x;
        //transform.LookAt(2 * p - c);

        transform.eulerAngles = new Vector3(0f,0f,0f);
    }
}