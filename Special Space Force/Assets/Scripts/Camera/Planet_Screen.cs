using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet_Screen : MonoBehaviour
{
    /// <summary>
    /// This is a small class that is just meant to hold information for the planetary camera so the player can view planetary stats
    /// These stats are changed every time you view a planet
    /// </summary>
    public Text planetName;
    public Text planetBiome;
    public Text planetType;
    public Text planetSize;
    public Text planetPopulation;
    public Text planetBaseMetals;
    public Text planetPreciousMetals;
    public Text planetFood;
    public GameObject planetEconomyWindow;
    public Text planetUsableSpace;
    public Text planetIndustry;
    public Text planetPopCons;
    public Text planetPlanetOutput;
    public Text planetOrbitalOutput;
    public Text planetTotalOutput;
    public GameObject planetMilitaryWindow;
    public Text planetGarrisonLevel;
    public Text planetGarrisonDesc;
    public Text planetGarrisonFleetLevel;
    public Text planetGarrisonFleetDesc;
    public Fleet_Manager fManager;
    public Voidcraft_Manager vManager;
    public GameObject genericOrbiter;
    public Quickview_Voidcraft_Manager QVManager;
    public System_Screen systemScreen;
}
