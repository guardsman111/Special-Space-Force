using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core_Equipment : MonoBehaviour
{
    /// <summary>
    /// Defines default equipment that is not in the resources folder to protect any assets used
    /// </summary>

    public Equipment_Manager manager;

    public Sprite[] Mk1ArmourP;
    public Sprite[] Mk1Armour2Camo;
    public Sprite[] Mk1Armour3Camo;
    public Sprite[] Mk1Armour3CamoDigi;
    public int Mk1ArmourCost;
    public Sprite[] Mk1HelmetP;
    public Sprite[] Mk1HelmetS1;
    public Sprite[] Mk1HelmetS2;
    public Sprite[] Mk1Helmet2Camo;
    public Sprite[] Mk1Helmet3Camo;
    public Sprite[] Mk1Helmet3CamoDigi;
    public int Mk1HelmetCost;
    public Sprite[] Mk1Mod1HelmetP;
    public Sprite[] Mk1Mod1HelmetS1;
    public Sprite[] Mk1Mod1HelmetS2;
    public Sprite[] Mk1Mod1Helmet2Camo;
    public Sprite[] Mk1Mod1Helmet3Camo;
    public Sprite[] Mk1Mod1Helmet3CamoDigi;
    public int Mk1Mod1HelmetCost;
    public Sprite[] FatiguesCasualP;
    public Sprite[] FatiguesCasual2Camo;
    public Sprite[] FatiguesCasual3Camo;
    public Sprite[] FatiguesCasual3CamoDigi;
    public Sprite[] FatiguesUBACSP;
    public Sprite[] FatiguesUBACS2Camo;
    public Sprite[] FatiguesUBACS3Camo;
    public Sprite[] FatiguesUBACS3CamoDigi;
    public Sprite[] Backpack1;
    public int Backpack1Cost;
    public Sprite[] Pistol1;
    public int Pistol1Cost;
    public Sprite[] PistolS1;
    public int PistolS1Cost;
    public Sprite[] Rifle1;
    public int Rifle1Cost;
    public Sprite[] Sniper1;
    public int Sniper1Cost;
    public Sprite[] Sword1;
    public int Sword1Cost;
    public Sprite[] SwordS1;
    public int SwordS1Cost;

    //Core armour packs
    public List<Sprite_Pack> CoreArmour()
    {
        List<Sprite_Pack> armourPacks = new List<Sprite_Pack>();

        Sprite_Pack tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Armour";
        tempSP.patternName = "Primary1";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk1ArmourCost;
        tempSP.numberOfColours = 1;
        foreach (Sprite s in Mk1ArmourP)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Armour";
        tempSP.patternName = "Camo 2C";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk1ArmourCost;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk1Armour2Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Armour";
        tempSP.patternName = "Camo 3C";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk1ArmourCost;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk1Armour3Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Armour";
        tempSP.patternName = "Digital Camo 3C";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk1ArmourCost;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk1Armour3CamoDigi)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        return armourPacks;
    }

    //Core helmet packs
    public List<Sprite_Pack> CoreHelmet()
    {
        List<Sprite_Pack> helmetPacks = new List<Sprite_Pack>();

        Sprite_Pack tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Helmet";
        tempSP.patternName = "Primary1";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk1HelmetCost;
        tempSP.numberOfColours = 1;
        foreach (Sprite s in Mk1HelmetP)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Helmet";
        tempSP.patternName = "Secondary1";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk1HelmetCost;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk1HelmetS1)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Helmet";
        tempSP.patternName = "Secondary2";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk1HelmetCost;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk1HelmetS2)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Helmet";
        tempSP.patternName = "Camo 2C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk1HelmetCost;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk1Helmet2Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Helmet";
        tempSP.patternName = "Camo 3C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk1HelmetCost;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk1Helmet3Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Helmet";
        tempSP.patternName = "Digital Camo 3C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk1HelmetCost;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk1Helmet3CamoDigi)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Helmet Comms";
        tempSP.patternName = "Primary1";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk1Mod1HelmetCost;
        tempSP.numberOfColours = 1;
        foreach (Sprite s in Mk1Mod1HelmetP)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Helmet Comms";
        tempSP.patternName = "Secondary1";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk1Mod1HelmetCost;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk1Mod1HelmetS1)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Helmet Comms";
        tempSP.patternName = "Secondary2";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk1Mod1HelmetCost;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk1Mod1HelmetS2)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Helmet Comms";
        tempSP.patternName = "Camo 2C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk1Mod1HelmetCost;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk1Mod1Helmet2Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Helmet Comms";
        tempSP.patternName = "Camo 3C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk1Mod1HelmetCost;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk1Mod1Helmet3Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Helmet Comms";
        tempSP.patternName = "Digital Camo 3C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk1Mod1HelmetCost;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk1Mod1Helmet3CamoDigi)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        return helmetPacks;
    }

    //Core fatigues packs
    public List<Sprite_Pack> CoreFatigues()
    {
        List<Sprite_Pack> fatiguesPacks = new List<Sprite_Pack>();

        Sprite_Pack tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "BDU Casual";
        tempSP.patternName = "Primary1";
        tempSP.packType = "Fatigues";
        tempSP.numberOfColours = 1;
        foreach (Sprite s in FatiguesCasualP)
        {
            tempSP.containedSprites.Add(s);
        }

        fatiguesPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "BDU Casual";
        tempSP.patternName = "Camo 2C";
        tempSP.packType = "Fatigues";
        tempSP.numberOfColours = 2;
        foreach (Sprite s in FatiguesCasual2Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        fatiguesPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "BDU Casual";
        tempSP.patternName = "Camo 3C";
        tempSP.packType = "Fatigues";
        tempSP.numberOfColours = 3;
        foreach (Sprite s in FatiguesCasual3Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        fatiguesPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "BDU Casual";
        tempSP.patternName = "Digital Camo 3C";
        tempSP.packType = "Fatigues";
        tempSP.numberOfColours = 3;
        foreach (Sprite s in FatiguesCasual3CamoDigi)
        {
            tempSP.containedSprites.Add(s);
        }

        fatiguesPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "BDU UBACS";
        tempSP.patternName = "Primary1";
        tempSP.packType = "Fatigues";
        tempSP.numberOfColours = 1;
        foreach (Sprite s in FatiguesUBACSP)
        {
            tempSP.containedSprites.Add(s);
        }

        fatiguesPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "BDU UBACS";
        tempSP.patternName = "Camo 2C";
        tempSP.packType = "Fatigues";
        tempSP.numberOfColours = 2;
        foreach (Sprite s in FatiguesUBACS2Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        fatiguesPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "BDU UBACS";
        tempSP.patternName = "Camo 3C";
        tempSP.packType = "Fatigues";
        tempSP.numberOfColours = 3;
        foreach (Sprite s in FatiguesUBACS3Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        fatiguesPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "BDU UBACS";
        tempSP.patternName = "Digital Camo 3C";
        tempSP.packType = "Fatigues";
        tempSP.numberOfColours = 3;
        foreach (Sprite s in FatiguesUBACS3CamoDigi)
        {
            tempSP.containedSprites.Add(s);
        }

        fatiguesPacks.Add(tempSP);

        return fatiguesPacks;
    }

    //Core equipment packs
    public List<Sprite_Pack> CoreEquipment()
    {
        List<Sprite_Pack> equipPacks = new List<Sprite_Pack>();

        Sprite_Pack tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Backpack";
        tempSP.patternName = "Primary1";
        tempSP.packType = "Equipment";
        tempSP.CostPerItem = Backpack1Cost;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Backpack1)
        {
            tempSP.containedSprites.Add(s);
        }

        equipPacks.Add(tempSP);

        return equipPacks;
    }

    //Core weapons packs (both primary and secondary)
    public List<Sprite_Pack> CoreWeapons()
    {
        List<Sprite_Pack> weaponPacks = new List<Sprite_Pack>();

        Sprite_Pack tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Pistol1";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponP";
        tempSP.CostPerItem = Pistol1Cost;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Pistol1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "PistolSec1";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponS";
        tempSP.CostPerItem = PistolS1Cost;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in PistolS1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Rifle1";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponP";
        tempSP.CostPerItem = Rifle1Cost;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Rifle1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Sniper";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponP";
        tempSP.CostPerItem = Sniper1Cost;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Sniper1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Sword1";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponP";
        tempSP.CostPerItem = Sword1Cost;
        tempSP.numberOfColours = 1;
        foreach (Sprite s in Sword1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "SwordSec1";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponS";
        tempSP.CostPerItem = SwordS1Cost;
        tempSP.numberOfColours = 1;
        foreach (Sprite s in SwordS1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        return weaponPacks;
    }
}
