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
            if (!planetCamera.enabled)
            {
                planetCamera.GetComponent<Camera_Targeted>().target = transform;
                systemCamera.enabled = false;
                star.enabled = false;
                planetCamera.enabled = true;
                planetScreenCamera.enabled = true;
                ToggleVisiblePlanets.TogglePlanetsOn(false);
                this.gameObject.SetActive(true);

                Planet_Script sPlanet = gameObject.GetComponent<Planet_Script>();

                sPlanet.planetSkin.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

                //Sets moons as visible
                foreach(GameObject go in sPlanet.moons)
                {
                    go.SetActive(true);
                }

                int random = Random.Range(0, 100);

                //if (random < 75) 
                //{
                //    sPlanet.storms.SetActive(true);
                //    sPlanet.storms.GetComponent<Weather_Visuals>().Toggle();
                //}
                //else
                //{
                //    sPlanet.storms.SetActive(false);
                //}

                //Changes the planetScreen text 
                planetScreen.planetName.text = sPlanet.planet.planetName;
                float earthRelativePlanetSize = sPlanet.planet.size + 25;
                planetScreen.planetBiome.text = "Prevalent Biome: " + sPlanet.planet.biome;
                planetScreen.planetSize.text = "Earth Size Ratio: " + earthRelativePlanetSize / 100.0f + " Earth(s)";
                if (sPlanet.planet.population > 0 && sPlanet.parentSystem.Star.allegiance == 0)
                {
                    planetScreen.planetPopulation.text = "Population: " + sPlanet.planet.population.ToString("00,0") + ",000";
                    planetScreen.planetType.text = "Main Export: " + sPlanet.Stats.catagory;
                    planetScreen.planetMilitaryWindow.SetActive(true);
                    planetScreen.planetEconomyWindow.SetActive(true);
                    planetScreen.planetUsableSpace.enabled = true;
                    planetScreen.planetIndustry.enabled = true;
                    planetScreen.planetPopCons.enabled = true;

                    planetScreen.planetPlanetOutput.enabled = true;
                    planetScreen.planetOrbitalOutput.enabled = true;
                    planetScreen.planetTotalOutput.enabled = true;
                    planetScreen.planetUsableSpace.text = "Usable Surface Landmass: " + (sPlanet.planet.usableSpace * 100).ToString("0.0") + "%";
                    planetScreen.planetIndustry.text = "Surface Used by Industry: " + sPlanet.planet.builtIndustry.ToString("0.0") + "%";
                    planetScreen.planetPopCons.text = "Output Consumed by Population: " + sPlanet.Stats.popConsumption.ToString("00,0") + " Kilo-Tonnes";

                    planetScreen.planetPlanetOutput.text = "Resources Output: " + sPlanet.Stats.resourceOutput.ToString("00,0") + " Kilo-Tonnes";
                    planetScreen.planetOrbitalOutput.text = "Population Output: " + sPlanet.Stats.popOutput.ToString("00,0") + " Kilo-Tonnes";
                    planetScreen.planetTotalOutput.text = "Total Monthly Output: " + sPlanet.Stats.output.ToString("00,0") + " Kilo-Tonnes";

                    planetScreen.planetGarrisonLevel.text = "Grade " + sPlanet.Stats.garrisonSize.ToString();
                    planetScreen.planetGarrisonDesc.text = sPlanet.Stats.garrisonDesc;
                    planetScreen.planetGarrisonFleetLevel.text = "Grade " + sPlanet.Stats.garrisonFleetSize.ToString();
                    planetScreen.planetGarrisonFleetDesc.text = sPlanet.Stats.garrisonFleetDesc;

                }
                else if(sPlanet.parentSystem.Star.allegiance != 0)
                {
                    if(sPlanet.planet.population > 0)
                    {
                        planetScreen.planetPopulation.text = "Population: " + sPlanet.planet.population.ToString("00,0") + ",000";
                        planetScreen.planetType.text = "Main Export: Unknown";
                        planetScreen.planetUsableSpace.enabled = true;
                        planetScreen.planetIndustry.enabled = true;
                        planetScreen.planetPopCons.enabled = false;

                        planetScreen.planetPlanetOutput.enabled = false;
                        planetScreen.planetOrbitalOutput.enabled = false;
                        planetScreen.planetTotalOutput.enabled = false;
                        planetScreen.planetUsableSpace.text = "Usable Surface Landmass: " + (sPlanet.planet.usableSpace * 100).ToString("0.0") + "%";
                        planetScreen.planetIndustry.text = "Surface Used by Industry: " + sPlanet.planet.builtIndustry.ToString("0.0") + "%";
                        planetScreen.planetMilitaryWindow.SetActive(false);
                        planetScreen.planetEconomyWindow.SetActive(false);
                    }
                    else
                    {
                        planetScreen.planetPopulation.text = "Uninhabited";
                        planetScreen.planetType.text = "Main Export: Unknown";
                        planetScreen.planetUsableSpace.enabled = false;
                        planetScreen.planetIndustry.enabled = false;
                        planetScreen.planetPopCons.enabled = false;

                        planetScreen.planetPlanetOutput.enabled = false;
                        planetScreen.planetOrbitalOutput.enabled = false;
                        planetScreen.planetTotalOutput.enabled = false;

                        planetScreen.planetMilitaryWindow.SetActive(false);
                        planetScreen.planetEconomyWindow.SetActive(false);
                    }
                }
                else
                {
                    planetScreen.planetPopulation.text = "Uninhabited";
                    planetScreen.planetType.text = "Main Export: Unknown";
                    planetScreen.planetUsableSpace.enabled = false;
                    planetScreen.planetIndustry.enabled = false;
                    planetScreen.planetPopCons.enabled = false;

                    planetScreen.planetPlanetOutput.enabled = false;
                    planetScreen.planetOrbitalOutput.enabled = false;
                    planetScreen.planetTotalOutput.enabled = false;

                    planetScreen.planetMilitaryWindow.SetActive(false);
                    planetScreen.planetEconomyWindow.SetActive(false);
                }
                if (sPlanet.Stats.Biome.surfacePop)
                {
                    planetScreen.planetBaseMetals.text = "Base Metals Abundancy: " + sPlanet.planet.baseMetalsAmount.ToString("0.0") + "%";
                    planetScreen.planetPreciousMetals.text = "Precious Metals Abundancy: " + sPlanet.planet.preciousMetalsAmount.ToString("0.0") + "%";
                    planetScreen.planetFood.text = "Agricultural Land: " + sPlanet.planet.foodAvailability.ToString("0.0") + "%";
                }
                else
                {
                    planetScreen.planetBaseMetals.text = "Unable to extract Base Metal Scan information";
                    planetScreen.planetPreciousMetals.text = "Unable to extract Precious Metal Scan information";
                    planetScreen.planetFood.text = "Surface not suitable for Life";
                }
                previous = true;
                systemCamera.GetComponent<Camera_Container_Script>().systemHelper.HideHelper();
                systemCamera.GetComponent<Camera_Container_Script>().planetHelper.ShowHelper();
            }
        }
    }

    //On escape returns to the system camera
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (GameObject go in gameObject.GetComponent<Planet_Script>().moons)
            {
                go.SetActive(false);
            }
            systemCamera.enabled = true;
            star.enabled = true;
            planetCamera.enabled = false;
            planetScreenCamera.enabled = false;
            previous = false;
            ToggleVisiblePlanets.TogglePlanetsOn(true);
            systemCamera.GetComponent<Camera_Container_Script>().systemHelper.ShowHelper();
            systemCamera.GetComponent<Camera_Container_Script>().planetHelper.HideHelper();
        }
    }
}
