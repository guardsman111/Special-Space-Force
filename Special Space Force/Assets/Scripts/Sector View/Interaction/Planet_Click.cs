using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet_Click : MonoBehaviour
{
    /// <summary>
    /// This script manages the cameras on clicking a planet
    /// </summary>
    
    [SerializeField]
    private Camera systemCamera;
    [SerializeField]
    private Camera planetCamera;
    [SerializeField]
    private Camera planetScreenCamera;
    [SerializeField]
    private Planet_Screen planetScreen;
    [SerializeField]
    private System_Click star;

    private Text[] planetScreenTexts;

    private bool previous;

    //Finds all the cameras
    void Start()
    {
        planetCamera = GameObject.Find("PlanetCamera").GetComponent<Camera>();
        planetScreenCamera = GameObject.Find("Planet Screen Camera").GetComponent<Camera>();
        systemCamera = GameObject.Find("SystemCamera").GetComponent<Camera>();
        star = gameObject.GetComponentInParent<System_Click>();
        planetScreen = planetCamera.GetComponentInChildren<Planet_Screen>();
    }

    //Toggles the Cameras depending on the current enabled camera
    private void OnMouseDown()
    {
        if (!previous)
        {
            planetCamera.GetComponent<Camera_Targeted>().target = transform;
            systemCamera.enabled = false;
            star.enabled = false;
            planetCamera.enabled = true;
            planetScreenCamera.enabled = true;
            ToggleVisiblePlanets.TogglePlanetsOn(false);
            this.gameObject.SetActive(true);

            //Changes the planetScreen text 
            planetScreen.planetName.text = gameObject.GetComponent<Planet_Script>().planet.planetName;
            float earthRelativePlanetSize = gameObject.GetComponent<Planet_Script>().planet.size + 25;
            planetScreen.planetSize.text = "Earth Size Ratio: " + earthRelativePlanetSize / 100.0f + " Earth(s)";
            if (gameObject.GetComponent<Planet_Script>().planet.population > 0)
            {
                planetScreen.planetPopulation.text = "Population: " + gameObject.GetComponent<Planet_Script>().planet.population.ToString("00,0");
            }
            else
            {
                planetScreen.planetPopulation.text = "Uninhabited";
            }
            planetScreen.planetBaseMetals.text = "Base Metals Abundancy: " + gameObject.GetComponent<Planet_Script>().planet.baseMetalsAmount.ToString("0.0") + "%";
            planetScreen.planetPreciousMetals.text = "Precious Metals Abundancy: " + gameObject.GetComponent<Planet_Script>().planet.preciousMetalsAmount.ToString("0.0") + "%";
            planetScreen.planetFood.text = "Arable Land: " + gameObject.GetComponent<Planet_Script>().planet.foodAvailability.ToString("0.0") + "%";
            previous = true;
        }
    }

    //On escape returns to the system camera
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            systemCamera.enabled = true;
            star.enabled = true;
            planetCamera.enabled = false;
            planetScreenCamera.enabled = false;
            previous = false;
            ToggleVisiblePlanets.TogglePlanetsOn(true);
        }
    }
}
