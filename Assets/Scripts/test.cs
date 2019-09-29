using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Quaternion a = new Quaternion(0, 0.4f, 0.8f, 0);
    private Quaternion rot1 = Quaternion.identity;
    public Quaternion rot2 = new Quaternion(0, 0, 1, 0);
    public Vector3 r;
    //public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 1;
        Vector3  f= a.eulerAngles;
        Debug.Log(f);
        //transform.rotation = Quaternion.Slerp(transform.rotation, a, Time.deltaTime * speed);
        transform.localRotation = rot1 * a * rot2;
    }
    private static Quaternion ConvertRotation(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
