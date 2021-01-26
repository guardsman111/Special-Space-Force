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
        tempVC.weapons = new List<Void_Weapon_Class>();
        Void_Weapon_Class tempW = new Void_Weapon_Class();
        tempW.name = "Twin 120mm SABOT Turret";
        tempW.number = 5;
        tempW.range = 3;
        tempW.damage = 80;
        tempVC.weapons.Add(tempW);
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
        tempW = new Void_Weapon_Class();
        tempW.name = "Twin 120mm SABOT Turret";
        tempW.number = 3;
        tempW.range = 3;
        tempW.damage = 80;
        tempVC.weapons.Add(tempW);
        tempW = new Void_Weapon_Class();
        tempW.name = "Twin 180mm Railgun Turret";
        tempW.number = 1;
        tempW.range = 5;
        tempW.damage = 250;
        tempVC.weapons.Add(tempW);
        tempW = new Void_Weapon_Class();
        tempW.name = "Galloway Cannon";
        tempW.number = 1;
        tempW.range = 7;
        tempW.damage = 180;
        tempVC.weapons.Add(tempW);
        foreach (Sprite s in Hifrin)
        {
            tempVC.containedSprites.Add(s);
        }

        voidcraft.Add(tempVC);

        return voidcraft;
    }
}
