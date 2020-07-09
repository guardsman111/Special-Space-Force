using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonController : MonoBehaviour
{
    public Transform target;
    public float xRotSpd;
    public float yRotSpd;
    public float zRotSpd;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(target.position, Vector3.left, yRotSpd);
        transform.RotateAround(target.position, Vector3.down, zRotSpd);
    }
}
