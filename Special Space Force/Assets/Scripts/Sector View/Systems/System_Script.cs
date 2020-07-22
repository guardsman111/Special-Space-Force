using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Script : MonoBehaviour
{
    public System_Generator systemGenerator;
    private System_Class star;
    public int totalPlanets;
    public int totalinhabitable;
    public int totaluninhabitable;


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
        star = new System_Class();
        star.systemName = name;
        star.colour = colour;
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
            temp.planetName = name + " " + i;

            //Generate population and biome
            float random;
            bool habitable;
            habitable = true;
            random = Random.Range(0, 100);
            int biomeID;

            if (random > systemGenerator.generatedProduct.habitableChance) { habitable = false; }
            if (habitable)
            {
                random = Random.Range(0, 1000000000);
                temp.population = (int)random;
                int rand = Random.Range(0, systemGenerator.BiomeManager.CheckCount());
                temp.biome = systemGenerator.BiomeManager.Biomes[rand].biomeName;
                biomeID = rand;
                totalPlanets += 1;
                totalinhabitable += 1;
            } 
            else
            {
                random = Random.Range(0, 10000000);
                temp.population = (int)random;
                int rand = Random.Range(0, systemGenerator.BiomeManager.CheckCount());
                temp.biome = systemGenerator.BiomeManager.Biomes[rand].biomeName;
                biomeID = rand;
                totalPlanets += 1;
                totaluninhabitable += 1;
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
}
