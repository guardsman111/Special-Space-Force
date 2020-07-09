using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudRotation : MonoBehaviour
{
    public float newSpeed;

    private void Start()
    {
        float random = Random.Range(0, 360);
        transform.rotation = new Quaternion(0, random, 0, 0);
        newSpeed = Random.Range(0.005f, 0.035f);
    }

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
