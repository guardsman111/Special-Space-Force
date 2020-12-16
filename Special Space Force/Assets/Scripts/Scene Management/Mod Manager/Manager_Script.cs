﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Script : MonoBehaviour
{
    /// <summary>
    /// Holds references for the individual mod managers
    /// </summary>
    public Biome_Manager biomeManager;
    public Race_Manager raceManager;
    public Equipment_Manager equipmentManager;
    public Trait_Manager traitManager;
    public Faction_Manager factionManager;
    public Turn_Manager turnManager;

    private Generation_Class generatedProduct;

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
