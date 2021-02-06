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
    public TextMeshPro sName;
    public TextMeshPro faction;
    public GameObject craftIcon;
    private System_Class star;
    public float combinedOutput;
    public string allegiance;
    public bool colonising;
    public int id;

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
    public void SystemGen(string name, string colour, int x, int z, int planets, int ID, GameObject prefab, System_Generator sysGen)
    {
        systemGenerator = sysGen;
        int avgSize = sysGen.AvgPlanetSize;
        float avgResource = sysGen.generatedProduct.resourceAbundancy;
        float industrialism = sysGen.generatedProduct.industrialism;
        int playerStrength = sysGen.generatedProduct.playerStrength;
        star = new System_Class();
        systemPlanets = new List<Planet_Script>();
        star.systemName = name;
        sName.text = name;
        star.colour = colour;
        id = ID;
        star.id = id;

        //Set Star_Class position
        star.posX = x;
        star.posZ = z;
        star.nPlanets = planets;
        star.Array = new List<Planet_Class>();

        if (star.allegiance > 0)
        {
            allegiance = sysGen.generatedProduct.factions[star.allegiance].AIRace.race.empireName;
            sName.color = sysGen.generatedProduct.factions[star.allegiance].AIRace.colour;
            faction.color = sysGen.generatedProduct.factions[star.allegiance].AIRace.colour;
        }

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

                if(temp.population > 500000)
                {
                    random = Random.Range(0f, 10000000f);
                    random = Mathf.Round(random);
                    temp.population = random;
                }

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
                if (inhabited)
                {
                    random = Random.Range(0, 10000); 
                    random = Mathf.Round(random); 
                    temp.population = (int)random;
                }

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

            //Generate Random Usable space between biome min and max space
            temp.usableSpace = Random.Range(systemGenerator.BiomeManager.Biomes[biomeID].minSpace, systemGenerator.BiomeManager.Biomes[biomeID].maxSpace);

            if (inhabited && temp.population == 0)
            {
                random = Random.Range(0, 1000000 * temp.usableSpace);
                random = Mathf.Round(random);
                temp.population = random;
            }
            else if(!inhabited) { temp.population = 0; }


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


            //Generate Industrial Level
            if (inhabited)
            {
                temp.builtIndustry = (int)Weighting(industrialism * 20);
                if (temp.builtIndustry <= 5 && sysGen.BiomeManager.Biomes[temp.biomeID].SurfacePop)
                {
                    temp.builtIndustry = Random.Range(5,30);
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
            planetT.GetComponent<Planet_Script>().PlanetGen(this, temp, sysGen.factionManager);
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
        id = star.id;
        systemPlanets = new List<Planet_Script>();

        star.allegiance = system.allegiance;
        if (star.allegiance > 0)
        {
            allegiance = save.generatedProduct.factions[star.allegiance].AIRace.race.empireName;
            sName.color = save.generatedProduct.factions[star.allegiance].AIRace.colour;
            faction.color = save.generatedProduct.factions[star.allegiance].AIRace.colour;
        }

        this.gameObject.GetComponentInChildren<TextMeshPro>().text = star.systemName;

        for (int i = 0; i < system.Array.Count; i++)
        {
            var planetT = Instantiate(prefab, this.transform);
            planetT.transform.position = transform.position;
            planetT.transform.position += new Vector3(400 + (i * 150), 0, 0);

            Planet_Class temp = new Planet_Class();
            temp = system.Array[i];
            planetT.GetComponent<Planet_Script>().PlanetGen(this, temp, sysGen.factionManager);
            systemPlanets.Add(planetT.GetComponent<Planet_Script>());
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

    public void ToggleCraft(bool active)
    {
        craftIcon.SetActive(active);
    }
}
