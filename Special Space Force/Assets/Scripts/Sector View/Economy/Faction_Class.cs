using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction_Class
{
    public string factionName;
    public int factionResourcePile;
    public int factionIncome;
    public List<System_Script> controlledSystems;
    public List<Planet_Script> controlledPlanets;

    public Faction_Class()
    {

    }
}
