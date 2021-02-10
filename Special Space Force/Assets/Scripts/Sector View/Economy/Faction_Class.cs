﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction_Class
{
    public string factionName;
    public int factionID;
    public int factionResourcePile;
    public int factionIncome;
    public AI_Class AIRace;
    public List<System_Class> controlledSystems;
    public List<Planet_Class> controlledPlanets;

    public List<Travelling_Voidcraft_Class> travellingCraft;

    public float xenophobia;
    public float militarism;
    public float expansionism;
    public float industrialism;
    public int lowerBuffer;
    public int upperBuffer;

    public Faction_Class()
    {

    }
}
