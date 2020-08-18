using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudRotation : MonoBehaviour
{
    /// <summary>
    /// This script rotates the object it is attatched too
    /// </summary>
    
    public float newSpeed;
    
    //Set new random rotation and a new speed of rotation
    private void Start()
    {
        float random = Random.Range(0, 360);
        transform.rotation = new Quaternion(0, random, 0, 0);
        newSpeed = Random.Range(0.005f, 0.035f);
    }

    //Rotate the object
    private void FixedUpdate()
    {
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles + new Vector3(0, newSpeed, 0));
    }

    private void ChangeSpeed()
    {
    }

    void OnPlanetSelect()
    {

    }

}
