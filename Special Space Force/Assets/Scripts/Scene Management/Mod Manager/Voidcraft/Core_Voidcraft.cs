using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core_Voidcraft : MonoBehaviour
{

    public Sprite[] Farsky;
    public int FarskyCost;
    public Sprite[] Hifrin;
    public int HifrinCost;

    public List<Voidcraft_Pack> Core()
    {
        List<Voidcraft_Pack> voidcraft = new List<Voidcraft_Pack>();

        Voidcraft_Pack tempVC = new Voidcraft_Pack();
        tempVC.weapons = new List<Void_Weapon_Class>();
        tempVC.containedSprites = new List<Sprite>();
        tempVC.className = "Farsky Heavy Destroyer";
        tempVC.type = "Destroyer";
        tempVC.costPerCraft = FarskyCost;
        foreach (Sprite s in Farsky)
        {
            tempVC.containedSprites.Add(s);
        }

        voidcraft.Add(tempVC);

        tempVC = new Voidcraft_Pack();
        tempVC.weapons = new List<Void_Weapon_Class>();
        tempVC.containedSprites = new List<Sprite>();
        tempVC.className = "Hifrin Cruiser";
        tempVC.type = "Cruiser";
        tempVC.costPerCraft = HifrinCost;
        foreach (Sprite s in Hifrin)
        {
            tempVC.containedSprites.Add(s);
        }

        voidcraft.Add(tempVC);

        return voidcraft;
    }
}
