using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class System_Script : MonoBehaviour
{
    /// <summary>
    /// This Script is generated and uses inputted information to create a star system, including planets and their stats
    /// </summary>
    public System_Generator systemGenerator;
    private System_Class star;
    public string allegiance;

    private List<Planet_Script> systemPlanets;

    public List<Planet_Script> SystemPlanets
    {
        get { return systemPlanets; }

        set
        {
            if
              (value != systemPlanets)
            {
                systemPlanets = value;
            }
        }
    }

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
        systemGenerator = sysGen;
        int avgSize = sysGen.AvgPlanetSize;
        float avgResource = sysGen.generatedProduct.resourceAbundancy;
        float industrialism = sysGen.generatedProduct.industrialism;
        int playerStrength = sysGen.generatedProduct.playerStrength;
        star = new System_Class();
        systemPlanets = new List<Planet_Script>();
        star.systemName = name;
        this.gameObject.GetComponentInChildren<TextMeshPro>().text = name;
        star.colour = colour;

        //Set System Allegiance
        float playerRand = Random.Range(0, 100);
        star.allegiance = -1;

        //If rand + player faction strength more than 90, keep as player
        if (playerRand + (playerStrength * 10) <= 90)
        {
            //For each ai toggled on, generate if star allegiance
            for (int i = 0; i < sysGen.generatedProduct.toggledAI.Count; i++)
            {
                float aiRand = Random.Range(0, 100);

                //If star has already got an allegiance
                if (star.allegiance == 0) 
                {
                    //If the star's current owner has a lower start strength than the current AI (i)
                    if (sysGen.generatedProduct.toggledAI[i].startThreat > sysGen.generatedProduct.toggledAI[star.allegiance - 1].startThreat)
                    {
                        if (aiRand + ((sysGen.generatedProduct.toggledAI[i].startThreat - sysGen.generatedProduct.toggledAI[star.allegiance - 1].startThreat / 2) * 10) >= 90 && sysGen.generatedProduct.toggledAI[i].nPlanets > 0)
                        {
                            star.allegiance = i + 1;
                            allegiance = sysGen.generatedProduct.toggledAI[i].race.empireName;
                            gameObject.GetComponentInChildren<TextMeshPro>().color = sysGen.generatedProduct.toggledAI[i].colour;
                            sysGen.generatedProduct.toggledAI[i].nPlanets += 1;
                        }
                    }

                    //If Star's current owner has same start strength as the current AI (i)
                    if (sysGen.generatedProduct.toggledAI[i].startThreat == sysGen.generatedProduct.toggledAI[star.allegiance - 1].startThreat)
                    {
                        int chanceRand = Random.Range(0, 2);

                        //if chanceRand - start threat is greater than current allegiance start threat, set allegiance to this
                        if (chanceRand == 0)
                        {
                            if (aiRand + (sysGen.generatedProduct.toggledAI[i].startThreat * 10) >= 90 && sysGen.generatedProduct.toggledAI[i].nPlanets > 0)
                            {
                                star.allegiance = i + 1;
                                allegiance = sysGen.generatedProduct.toggledAI[i].race.empireName;
                                gameObject.GetComponentInChildren<TextMeshPro>().color = sysGen.generatedProduct.toggledAI[i].colour;
                                sysGen.generatedProduct.toggledAI[i].nPlanets += 1;
                            }
                        }
                    }
                    
                    //If current AI has 0 systems and the current owner has more than 1 system, change the system anyway
                    if (sysGen.generatedProduct.toggledAI[i].nPlanets == 0 && sysGen.generatedProduct.toggledAI[star.allegiance - 1].nPlanets > 1)
                    {
                        star.allegiance = i + 1;
                        allegiance = sysGen.generatedProduct.toggledAI[i].race.empireName;
                        gameObject.GetComponentInChildren<TextMeshPro>().color = sysGen.generatedProduct.toggledAI[i].colour;
                        sysGen.generatedProduct.toggledAI[i].nPlanets += 1;
                    }
                } 
                //Else, if the random number + start threat is greater than 90, set as this allegiance
                else
                {
                    if (aiRand + (sysGen.generatedProduct.toggledAI[i].startThreat * 10) >= 90)
                    {
                        star.allegiance = i + 1;
                        allegiance = sysGen.generatedProduct.toggledAI[i].race.empireName;
                        gameObject.GetComponentInChildren<TextMeshPro>().color = sysGen.generatedProduct.toggledAI[i].colour;
                        sysGen.generatedProduct.toggledAI[i].nPlanets += 1;
                    }

                    //If the AI has 0 systems, make this system for that AI
                    if (sysGen.generatedProduct.toggledAI[i].nPlanets == 0)
                    {
                        star.allegiance = i + 1;
                        allegiance = sysGen.generatedProduct.toggledAI[i].race.empireName;
                        gameObject.GetComponentInChildren<TextMeshPro>().color = sysGen.generatedProduct.toggledAI[i].colour;
                        sysGen.generatedProduct.toggledAI[i].nPlanets += 1;
                    }
                }
            }
        }
        else
        {
            star.allegiance = 0;
            allegiance = "Player";
        }
        if (star.allegiance == -1)
        {
            star.allegiance = 0;
            allegiance = "Player";
        }

        switch (star.allegiance)
        {
            case 0:
                sysGen.factionManager.Factions[0].controlledSystems.Add(this);
                break;

            case 1:
                sysGen.factionManager.Factions[1].controlledSystems.Add(this);
                break;

            case 2:
                sysGen.factionManager.Factions[2].controlledSystems.Add(this);
                break;

            case 3:
                sysGen.factionManager.Factions[3].controlledSystems.Add(this);
                break;

            case 4:
                sysGen.factionManager.Factions[4].controlledSystems.Add(this);
                break;

            case 5:
                sysGen.factionManager.Factions[5].controlledSystems.Add(this);
                break;

            case 6:
                sysGen.factionManager.Factions[6].controlledSystems.Add(this);
                break;
        }

        //Set Star_Class position
        star.posX = x;
        star.posZ = z;
        star.nPlanets = planets;
        star.Array = new List<Planet_Class>();

        //For each planet 
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

            //Generate habitable and inhabited chances
            if (random > systemGenerator.generatedProduct.habitableChance) { habitable = false; }
            if (random2 > systemGenerator.generatedProduct.inhabitedChance && i != 0) { inhabited = false; }

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

            if (habitable)
            {
                //Set population

                if (inhabited) { random = Random.Range(0, 1000000000); temp.population = (int)random; }
                else { temp.population = 0; }

                //Make sure planet biome is a habitable one (to avoid bugs of people living on gas giants)
                int rand = Random.Range(0, systemGenerator.BiomeManager.CheckCount());
                while (!systemGenerator.BiomeManager.Biomes[rand].Atmo)
                {
                    rand = Random.Range(0, systemGenerator.BiomeManager.CheckCount());
                }
                temp.biome = systemGenerator.BiomeManager.Biomes[rand].biomeName;
                biomeID = rand;

                temp.popProduction = (industrialism * industrialism) / 10;
            } 
            else
            {
                //Set population
                if (inhabited) { random = Random.Range(0, 10000000); temp.population = (int)random; } 
                else { temp.population = 0; }

                //Make sure planet biome is an uninhabitable one
                int rand = Random.Range(0, systemGenerator.BiomeManager.CheckCount());
                while (systemGenerator.BiomeManager.Biomes[rand].Atmo)
                {
                    rand = Random.Range(0, systemGenerator.BiomeManager.CheckCount());
                }
                temp.biome = systemGenerator.BiomeManager.Biomes[rand].biomeName;
                temp.biomeID = rand;
                biomeID = rand;

                temp.popProduction = (industrialism * industrialism) / 10;
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

            if(temp.baseMetalsAmount > 100)
            {
                temp.baseMetalsAmount = 100;
            }

            //Precious Metals
            if (sysGen.BiomeManager.Biomes[temp.biomeID].SurfacePop)
            {
                temp.preciousMetalsAmount = Weighting(avgResource);
            }
            else
            {
                temp.preciousMetalsAmount = 0;
            }

            if (temp.preciousMetalsAmount > 100)
            {
                temp.preciousMetalsAmount = 100;
            }

            //Food Availability
            if (habitable)
            {
                temp.foodAvailability = Weighting(avgResource);
            }
            else
            {
                temp.foodAvailability = 0;
            }

            if (temp.foodAvailability > 100)
            {
                temp.foodAvailability = 100;
            }

            //Generate Random Usable space between biome min and max space
            temp.usableSpace = Random.Range(systemGenerator.BiomeManager.Biomes[biomeID].minSpace, systemGenerator.BiomeManager.Biomes[biomeID].maxSpace);

            //Generate Industrial Level
            if (inhabited)
            {
                temp.builtIndustry = (int)Weighting(industrialism * 20);
                if (temp.builtIndustry == 0)
                {
                    temp.builtIndustry = temp.population / 10000000;
                }
                if (temp.builtIndustry > temp.usableSpace * 100)
                {
                    temp.builtIndustry = (int)(temp.usableSpace * 100);
                }

            }
            else
            {
                temp.builtIndustry = 0;
            }

            temp.inhabited = inhabited;
            
            //Generate Planet_Script using the temp Planet_Script and add to Star Array
            planetT.GetComponent<Planet_Script>().PlanetGen(temp, sysGen.factionManager);
            systemPlanets.Add(planetT.GetComponent<Planet_Script>());
            star.Array.Add(temp);
            planetT.GetComponent<Planet_Script>().inhabited = inhabited;
        }
    }

    //Generates the planetary stats from a save (Loading)
    public void SystemGen(System_Class system, GameObject prefab, System_Generator sysGen, Save_Class save)
    {
        //Simply copies required information from the save to the live map
        star = system;

        star.allegiance = system.allegiance;
        if (star.allegiance > 0)
        {
            allegiance = save.generatedProduct.toggledAI[star.allegiance - 1].race.empireName;
            gameObject.GetComponentInChildren<TextMeshPro>().color = save.generatedProduct.toggledAI[star.allegiance - 1].colour;
        }

        this.gameObject.GetComponentInChildren<TextMeshPro>().text = star.systemName;

        for (int i = 0; i < system.Array.Count; i++)
        {
            var planetT = Instantiate(prefab, this.transform);
            planetT.transform.position = transform.position;
            planetT.transform.position += new Vector3(400 + (i * 150), 0, 0);

            Planet_Class temp = new Planet_Class();
            temp = system.Array[i];
            planetT.GetComponent<Planet_Script>().PlanetGen(temp, sysGen.factionManager);
        }
    }

    //Generate a random number with a weighting dictated by a float. Cannot return a nagative number (minimum 0)
    public float Weighting(float abundancy)
    {
        float weight;
        float weighting;
        weighting = Random.Range(0, 100);
        weight = 1f;

        //Create weighting according to size of abundancy
        if (weighting > 90)
        {
            weight = 100f;
        }
        if (weighting < 90)
        {
            weight = 70f;
        }
        if (weighting < 70)
        {
            weight = 50f;
        }
        if (weighting < 50)
        {
            weight = 30f;
        }
        if (weighting < 30)
        {
            weight = 10f;
        }
        if (weighting < 10)
        {
            weight = 5f;
        }

        float modifier;
        bool negative = false;

        //Generate a 50/50 chance of being a negative modifier
        float randomNegative = Random.Range(0, 100);

        if (randomNegative >= 50)
        {
            negative = true;
        }

        //Generate a modifier between 0 and weight, if negative make it negative
        modifier = Random.Range(0, weight);

        if(negative)
        {
            modifier = -modifier;
        }

        //Debug.Log(modifier);
        //Debug.Log(weight);

        //Add modifier to abundancy, if less than 0 make it 0, then return
        modifier = modifier + abundancy;

        if (modifier < 0)
        {
            modifier = 0;
        }

        return modifier;
    }
}
