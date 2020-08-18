using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleVisiblePlanets : MonoBehaviour
{
    /// <summary>
    /// Toggles planets on and off, so that planets are not visible in the sector camera and are visible in the system camera
    /// </summary>
    
    public static GameObject[] Planets;
    public static bool hidden = false;


    public void Run()
    {
        FindPlanets();
        //Debug.Log(Planets[0].name);
        TogglePlanetsOn(false);
    }

    //Turns the planets array on or off
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

    //Sets all planets to the parameter
    public static void TogglePlanetsOn(bool status)
    {
        foreach (GameObject g in Planets)
        {
            g.SetActive(status);
        }
        hidden = status;
    }

    //Finds all the planets first game objects
    private void FindPlanets()
    {
        Planets = GameObject.FindGameObjectsWithTag("PlanetCore");
    }
}
