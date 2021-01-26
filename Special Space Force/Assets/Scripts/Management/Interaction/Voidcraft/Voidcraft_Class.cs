using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voidcraft_Class
{
    public string className;
    public string craftName;
    public string type;
    public int ID;
    public int positionID;
    public int starID;
    public int planetN;

    public List<Void_Weapon_Class> weapons;
    public List<int> uIDTransported;

    public string CraftOutlinePath;
    public string CraftPrimaryPath;
    public string CraftSecondaryPath;
    public string CraftTertiaryPath;
    public string CraftTrimPath;
    public string CraftSpecialPath;

    public int filterMode;

    public int costPerCraft;

    public int speed;
    public int armour;

    public Voidcraft_Class()
    {

    }
}
