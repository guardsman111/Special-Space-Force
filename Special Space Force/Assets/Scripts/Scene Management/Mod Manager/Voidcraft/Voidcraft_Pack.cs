using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voidcraft_Pack : MonoBehaviour
{
    public string type;
    public string className;
    public int armour;
    public int speed;
    public int capacity;
    public int costPerCraft;

    public List<Void_Weapon_Class> weapons;
    public List<Sprite> containedSprites;

    public Voidcraft_Pack()
    {

    }
}
