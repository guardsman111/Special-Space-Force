using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction_Script
{
    public Faction_Class faction;
    public List<System_Script> controlledSystems;
    public List<Planet_Script> controlledPlanets;

    public float currentlyBuilding;
    public float maxBuilding;
}
