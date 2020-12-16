using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite_Pack
{
    /// <summary>
    /// Holds a list of sprites and a few identifier values
    /// </summary>
    public string packName;
    public string patternName;
    public int numberOfColours;
    public int CostPerItem;
    public string packType; //Hair, Skin, Armour, Fatigues, Helmet, Equipment
    public List<Sprite> containedSprites;

    public Sprite_Pack()
    {

    }
}
