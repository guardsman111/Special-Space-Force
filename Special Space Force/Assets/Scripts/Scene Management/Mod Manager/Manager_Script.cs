using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Script : MonoBehaviour
{
    /// <summary>
    /// Holds references for the individual mod managers
    /// </summary>
    public Biome_Manager biomeManager;
    public Sector_Manager sectorManager;
    public Race_Manager raceManager;
    public Equipment_Manager equipmentManager;
    public Trait_Manager traitManager;
    public Faction_Manager factionManager;
    public Turn_Manager turnManager;
    public Rank_Manager rankManager;
    public Voidcraft_Manager voidcraftManager;
    public Slot_Manager sManager;
    public Fleet_Manager fManager;
    public Force_Manager forceManager;
    public Master_Threats_Manager threatManager;
    public Combat_Setup_Manager combatSetupManager;

    private Generation_Class generatedProduct;

    public Quickview_Voidcraft_Manager craftSystem;
    public Quickview_Voidcraft_Manager craftPlanet;

    public System_Voidcraft_Script craftMover;

    public Generation_Class GeneratedProduct
    {
        get { return generatedProduct; }

        set
        {
            if(value != generatedProduct)
            {
                generatedProduct = value;
            }
        }
    }
}
