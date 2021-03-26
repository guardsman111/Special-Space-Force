using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Class
{
    /// <summary>
    /// A Class that holds save information to be easily serialized
    /// </summary>
    public string saveName;
    public int height;
    public int width;
    public int turnNumber;
    public float enemyMultiplyer;

    public List<System_Class> systems;
    public Generation_Class generatedProduct;
    public List<Slot_Class> topSlots;
    public List<Fleet_Class> fleets;

    public Save_Class()
    {

    }
}
