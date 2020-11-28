using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Class
{
    /// <summary>
    /// This script holds information about a system including all of the planets in a list
    /// </summary>
   
    public string systemName;
    public string colour;
    public int allegiance;
    public int posX;
    public int posZ;
    public int nPlanets;
    public List<Planet_Class> Array;

    public System_Class()
    {
    }
}
