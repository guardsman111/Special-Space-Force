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
        int playerStrength = sysGen.generatedProduct.playerStrength;
        star = new System_Class();
        star.systemName = name;
        this.gameObject.GetComponentInChildren<TextMeshPro>().text = name;
        star.colour = colour;

        //Set System Allegiance
        float playerRand = Random.Range(0, 100);
        star.allegiance = 0;
        allegiance = "Player";

        //If rand + player faction strength more than 90, keep as player
        if (playerRand + (playerStrength * 10) >= 90)
        {

        }
        else //For each ai toggled on, generate if star allegiance
        {

            for (int i = 0; i < sysGen.generatedProduct.toggledAI.Count; i++)
            {
                bool created = false;
                float aiRand = Random.Range(0, 100);

                //If star has already got an allegiance
                if (created) 
                {
                    //If the star's current owner has a lower start strength than the current AI (i)
                    if (sysGen.generatedProduct.toggledAI[i].startThreat > sysGen.generatedProduct.toggledAI[star.allegiance - 1].startThreat)
                    {
                        if (aiRand + (sysGen.generatedProduct.toggledAI[i].startThreat * 10) >= 90)
                        {
                            created = true;
                            star.allegiance = i + 1;
                            allegiance = sysGen.generatedProduct.toggledAI[i].race.empireName;
                            gameObject.GetComponentInChildren<TextMeshPro>().color = sysGen.generatedProduct.toggledAI[i].colour;
                        }
                    }

                    //If Star's current owner has same start strength as the current AI (i)
                    if (sysGen.generatedProduct.toggledAI[i].startThreat == sysGen.generatedProduct.toggledAI[star.allegiance - 1].startThreat)
                    {
                        int chanceRand = Random.Range(0, 100);

                        //if chanceRand - start threat is greater than current allegiance start threat, set allegiance to this
                        if (chanceRand >= 10 + sysGen.generatedProduct.toggledAI[star.allegiance - 1].startThreat)
                        {
                            if (aiRand + (sysGen.generatedProduct.toggledAI[i].startThreat * 10) >= 90)
                            {
                                created = true;
                                star.allegiance = i + 1;
                                allegiance = sysGen.generatedProduct.toggledAI[i].race.empireName;
                                gameObject.GetComponentInChildren<TextMeshPro>().color = sysGen.generatedProduct.toggledAI[i].colour;
                            }
                        }
                    }
                } 
                //Else, if the random number + start threat is greater than 90, set as this allegiance
                else
                {
                    if (aiRand + (sysGen.generatedProduct.toggledAI[i].startThreat * 10) >= 90)
                    {
                        created = true;
                        star.allegiance = i + 1;
                        allegiance = sysGen.generatedProduct.toggledAI[i].race.empireName;
                        gameObject.GetComponentInChildren<TextMeshPro>().color = sysGen.generatedProduct.toggledAI[i].colour;
                    }
                }
            }
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

                //Turns off SFX clouds on the planets
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

            //Precious Metals
            if (sysGen.BiomeManager.Biomes[temp.biomeID].SurfacePop)
            {
                temp.preciousMetalsAmount = Weighting(avgResource);
            }
            else
            {
                temp.preciousMetalsAmount = 0;
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

            //Generate Random Usable space between biome min and max space
            temp.usableSpace = Random.Range(systemGenerator.BiomeManager.Biomes[biomeID].minSpace, systemGenerator.BiomeManager.Biomes[biomeID].maxSpace);
            
            //Generate Planet_Script using the temp Planet_Script and add to Star Array
            planetT.GetComponent<Planet_Script>().PlanetGen(temp);
            star.Array.Add(temp);
        }
    }

    //Generates the planetary stats from a save (Loading)
    public void SystemGen(System_Class system, GameObject prefab, System_Generator sysGen)
    {
        //Simply copies required information from the save to the live map
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
