using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleVisiblePlanets : MonoBehaviour
{
    public static GameObject[] Planets;
    public static bool hidden = false;


    private void Start()
    {
        FindPlanets();
        //Debug.Log(Planets[0].name);
        TogglePlanetsOn(false);
    }

    public static void TogglePlanetsOn()
    {
        if (hidden) {
            foreach (GameObject g in Planets)
            {
                g.SetActive(true);
            }
            hidden = false;
        }
        if (!hidden)
        {
            foreach (GameObject g in Planets)
            {
                g.SetActive(false);
            }
            hidden = true;
        }
    }
    public static void TogglePlanetsOn(bool status)
    {
        foreach (GameObject g in Planets)
        {
            g.SetActive(status);
        }
        hidden = status;
    }

    private void FindPlanets()
    {
        Planets = GameObject.FindGameObjectsWithTag("PlanetCore");
    }
}
