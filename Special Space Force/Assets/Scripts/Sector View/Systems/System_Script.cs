using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Script : MonoBehaviour
{
    public System_Generator systemGenerator;
    private System_Class star;


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
        star = new System_Class();
        star.systemName = name;
        star.colour = colour;
        star.posX = x;
        star.posZ = z;
        star.nPlanets = planets;
        star.Array = new List<Planet_Class>();
        for (int i = 0; i < planets; i++)
        {
            var planetT = Instantiate(prefab, this.transform);
            planetT.transform.position = transform.position;
            planetT.transform.position += new Vector3(400 + (i * 150), 0, 0);
            Planet_Class temp = new Planet_Class();
            temp.planetName = name + " " + i;
            float random = Random.Range(0, 1000000000);
            int rand = Random.Range(0, systemGenerator.BiomeManager.CheckCount());
            temp.biome = systemGenerator.BiomeManager.Biomes[rand].biomeName;
            temp.population = (int)random;
            temp.size = Random.Range(50,100); //replace with reference to min/max planet size
            temp.usableSpace = Random.Range(systemGenerator.BiomeManager.Biomes[rand].minSpace, systemGenerator.BiomeManager.Biomes[rand].maxSpace);
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
