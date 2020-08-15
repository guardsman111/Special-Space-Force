﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class System_Script : MonoBehaviour
{
    public System_Generator systemGenerator;
    private System_Class star;
    public int totalPlanets;
    public int totalinhabitable;
    public int totaluninhabitable;
    public string allegiance;



    public System_Class Star
    {
        get { return star; }

        set
        {
            if
              (value != star)
            {
                star = value;
            }
        }
    }

    //Generates the system stats and generates planets
    public void SystemGen(string name, string colour, int x, int z, int planets, GameObject prefab, System_Generator sysGen)
    {
        totalPlanets = 0;
        totalinhabitable = 0;
        totaluninhabitable = 0;
        systemGenerator = sysGen;
        int avgSize = sysGen.AvgPlanetSize;
        float avgResource = sysGen.generatedProduct.resourceAbundancy;
        int playerStrength = sysGen.generatedProduct.playerStrength;
        star = new System_Class();
        star.systemName = name;
        this.gameObject.GetComponentInChildren<TextMeshPro>().text = name;
        star.colour = colour;
        float playerRand = Random.Range(0, 120);
        int aiRand = Random.Range(0, sysGen.generatedProduct.toggledAI.Count);
        star.allegiance = 0;
        allegiance = "Player";
        Debug.Log("Player Random " + playerRand);
        Debug.Log("Player Required " + (playerStrength + 1) * 20);
        if ((playerStrength + 1) * 20 >= playerRand)
        {
        }
        else
        {
            star.allegiance = aiRand + 1;
            allegiance = sysGen.generatedProduct.toggledAI[aiRand].race.empireName;
            gameObject.GetComponentInChildren<TextMeshPro>().color = sysGen.generatedProduct.toggledAI[aiRand].colour;
        }
        star.posX = x;
        star.posZ = z;
        star.nPlanets = planets;
        star.Array = new List<Planet_Class>();
        for (int i = 0; i < planets; i++)
        {
            //Generate Planet and position
            var planetT = Instantiate(prefab, this.transform);
            planetT.transform.position = transform.position;
            planetT.transform.position += new Vector3(400 + (i * 150), 0, 0);

            //Generate planet Class to fill with planet details
            Planet_Class temp = new Planet_Class();
            string number = "I";
            if(i == 0)
            {
                number = "I";
            }
            if (i == 1)
            {
                number = "II";
            }
            if (i == 2)
            {
                number = "III";
            }
            if (i == 3)
            {
                number = "IV";
            }
            if (i == 4)
            {
                number = "V";
            }
            if (i == 5)
            {
                number = "VI";
            }
            if (i == 6)
            {
                number = "VII";
            }
            temp.planetName = name + " " + number;

            //Generate population and biome
            float random;
            float random2;
            bool habitable;
            habitable = true;
            bool inhabited;
            inhabited = true;
            random = Random.Range(0, 100);
            random2 = Random.Range(0, 100);
            int biomeID;

            if (random > systemGenerator.generatedProduct.habitableChance) { habitable = false; }
            if (random2 > systemGenerator.generatedProduct.inhabitedChance && i != 0) { inhabited = false; }

            if (habitable)
            {
                if (inhabited) { random = Random.Range(0, 1000000000); temp.population = (int)random; }
                else { temp.population = 0; }

                int rand = Random.Range(0, systemGenerator.BiomeManager.CheckCount());
                while (!systemGenerator.BiomeManager.Biomes[rand].Atmo)
                {
                    rand = Random.Range(0, systemGenerator.BiomeManager.CheckCount());
                }
                temp.biome = systemGenerator.BiomeManager.Biomes[rand].biomeName;
                biomeID = rand;
                totalPlanets += 1;
                totalinhabitable += 1;
            } 
            else
            {
                if (inhabited) { random = Random.Range(0, 10000000); temp.population = (int)random; } 
                else { temp.population = 0; }

                int rand = Random.Range(0, systemGenerator.BiomeManager.CheckCount());
                while (systemGenerator.BiomeManager.Biomes[rand].Atmo)
                {
                    rand = Random.Range(0, systemGenerator.BiomeManager.CheckCount());
                }
                temp.biome = systemGenerator.BiomeManager.Biomes[rand].biomeName;
                temp.biomeID = rand;
                biomeID = rand;
                totalPlanets += 1;
                totaluninhabitable += 1;
                foreach(CloudRotation cr in planetT.gameObject.GetComponentsInChildren<CloudRotation>())
                {
                    if (cr.gameObject.name == "Clouds_Stormy")
                    {
                        cr.gameObject.SetActive(false);
                    }
                }
            }

            //Generate size and usable space
            for (int j = 0; j < 2; j++)
            {
                temp.size = Random.Range(50, 100);
                int sizeR = Random.Range(0, 50);
                float sizeCalc = Mathf.Abs(avgSize - temp.size);
                if (sizeCalc < sizeR)
                {
                    break;
                } 
            }

            //Generate Resources

            //Base Metals
            if (sysGen.BiomeManager.Biomes[temp.biomeID].SurfacePop)
            {
                temp.baseMetalsAmount = Weighting(avgResource);
            }
            else
            {
                temp.baseMetalsAmount = 0;
            }

            //Debug.Log("Base Metals - " + temp.baseMetalsAmount);

            //Precious Metals
            if (sysGen.BiomeManager.Biomes[temp.biomeID].SurfacePop)
            {
                temp.preciousMetalsAmount = Weighting(avgResource);
            }
            else
            {
                temp.preciousMetalsAmount = 0;
            }

            //Debug.Log(temp.preciousMetalsAmount);

            //Food Availability
            if (habitable)
            {
                temp.foodAvailability = Weighting(avgResource);
            }
            else
            {
                temp.foodAvailability = 0;
            }

            //Debug.Log(temp.foodAvailability);

            temp.usableSpace = Random.Range(systemGenerator.BiomeManager.Biomes[biomeID].minSpace, systemGenerator.BiomeManager.Biomes[biomeID].maxSpace);
            planetT.GetComponent<Planet_Script>().PlanetGen(temp);
            star.Array.Add(temp);
        }
    }

    //Generates the planetary stats
    public void SystemGen(System_Class system, GameObject prefab, System_Generator sysGen)
    {
        star = system;
        for (int i = 0; i < system.Array.Count; i++)
        {
            var planetT = Instantiate(prefab, this.transform);
            planetT.transform.position = transform.position;
            planetT.transform.position += new Vector3(400 + (i * 150), 0, 0);


            Planet_Class temp = new Planet_Class();
            temp.planetName = system.Array[i].planetName;
            temp.biome = system.Array[i].biome;
            temp.population = system.Array[i].population;
            temp.size = system.Array[i].size;
            temp.usableSpace = system.Array[i].usableSpace;
            planetT.GetComponent<Planet_Script>().PlanetGen(temp);
        }
    }

    public float Weighting(float abundancy)
    {
        float weight;
        float weighting;
        weighting = Random.Range(0, 100);
        weight = 1f;
        if (weighting > 90)
        {
            weight = 100f;
        }
        if (weighting < 90)
        {
            weight = 50f;
        }
        if (weighting < 70)
        {
            weight = 20f;
        }
        if (weighting < 50)
        {
            weight = 10f;
        }
        if (weighting < 30)
        {
            weight = 5f;
        }
        if (weighting < 10)
        {
            weight = 1f;
        }

        float modifier;
        bool negative = false;

        float randomNegative = Random.Range(0, 100);

        if (randomNegative >= 50)
        {
            negative = true;
        }

        modifier = Random.Range(0, weight);

        if(negative)
        {
            modifier = -modifier;
        }

        //Debug.Log(modifier);
        //Debug.Log(weight);

        modifier = modifier + abundancy;

        if (modifier < 0)
        {
            modifier = 0;
        }

        return modifier;
    }
}
