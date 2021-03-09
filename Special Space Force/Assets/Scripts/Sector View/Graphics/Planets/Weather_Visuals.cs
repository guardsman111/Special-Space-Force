using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather_Visuals : MonoBehaviour
{
    public GameObject[] stormPoints;

    private void Start()
    {
    }

    public void Toggle()
    {
        foreach(GameObject go in stormPoints)
        {
            float random = Random.Range(0, 100);
            if(random >= 50)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }
    }
}
