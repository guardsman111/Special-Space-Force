using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonController : MonoBehaviour
{
    /// <summary>
    /// Currently unused script to rotate moons around planets
    /// </summary>

    public GameObject[] items;

    public Transform target;
    public float xRotSpd;
    public float yRotSpd;
    public float zRotSpd;

    void Start()
    {
        yRotSpd = Random.Range(-10f, 10f);
        zRotSpd = Random.Range(-10f, 10f);
    }

    //Rotates the object around another
    void Update()
    {
        transform.Rotate(0, yRotSpd * Time.deltaTime, zRotSpd * Time.deltaTime); //rotates 50 degrees per second around z axis
    }
}
