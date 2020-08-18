using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonController : MonoBehaviour
{
    /// <summary>
    /// Currently unused script to rotate moons around planets
    /// </summary>
    
    public Transform target;
    public float xRotSpd;
    public float yRotSpd;
    public float zRotSpd;


    //Rotates the object around another
    void FixedUpdate()
    {
        transform.RotateAround(target.position, Vector3.left, yRotSpd);
        transform.RotateAround(target.position, Vector3.down, zRotSpd);
    }
}
