using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Stats
{
    public string PName;
    public Planet_Biome Biome;
    public int Population;
    public float popHappiness = 0.8f;

    //Constructor for Planet_Stats - takes input from planet_script and creates the Planet_Stats
    public Planet_Stats(Planet_Class pClass, MeshRenderer skin)
    {
        PName = pClass.planetName;
        Biome = new Planet_Biome(pClass.biome, skin);
        Population = pClass.population;
        BiomeEffect();
    }

    //Refreshes biome stats to default and recalculates
    public void BiomeRefreshEffect()
    {
            popHappiness = 0.8f;
            popHappiness += Biome.GetHappinessModifier();
            CheckHappinessTotal();
    }

    //Calculates biome stats
    public void BiomeEffect()
    {
        popHappiness += Biome.GetHappinessModifier();
        CheckHappinessTotal();
    }


    //Checks population happiness isnt above 1
    public void CheckHappinessTotal() { if (popHappiness > 1) popHappiness = 1; }


}
