using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core_Equipment : MonoBehaviour
{
    /// <summary>
    /// Defines default equipment that is not in the resources folder to protect any assets used
    /// </summary>

    public Equipment_Manager manager;

    public Sprite[] Mk1ArmourP1;
    public Sprite[] Mk1ArmourPS1;
    public Sprite[] Mk1HelmetP1;
    public Sprite[] Mk1HelmetPS1;
    public Sprite[] BDUCasual;
    public Sprite[] BDUUBACSP1;
    public Sprite[] BDUUBACSPS1;
    public Sprite[] BDUUBACS3C1;
    public Sprite[] Backpack1;
    public Sprite[] Pistol1;
    public Sprite[] PistolS1;
    public Sprite[] Rifle1;
    public Sprite[] Sniper1;
    public Sprite[] Sword1;
    public Sprite[] SwordS1;

    //Core armour packs
    public List<Sprite_Pack> CoreArmour()
    {
        List<Sprite_Pack> armourPacks = new List<Sprite_Pack>();

        Sprite_Pack tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Armour";
        tempSP.patternName = "Primary1";
        tempSP.packType = "Armour";
        tempSP.numberOfColours = 1;
        foreach (Sprite s in Mk1ArmourP1)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Armour";
        tempSP.patternName = "Secondary1";
        tempSP.packType = "Armour";
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk1ArmourPS1)
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
        tempSP.numberOfColours = 1;
        foreach (Sprite s in Mk1HelmetP1)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Helmet";
        tempSP.patternName = "Secondary1";
        tempSP.packType = "Helmet";
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk1HelmetPS1)
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
        foreach (Sprite s in BDUCasual)
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
        foreach (Sprite s in BDUUBACSP1)
        {
            tempSP.containedSprites.Add(s);
        }

        fatiguesPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "BDU UBACS";
        tempSP.patternName = "Secondary1";
        tempSP.packType = "Fatigues";
        tempSP.numberOfColours = 2;
        foreach (Sprite s in BDUUBACSPS1)
        {
            tempSP.containedSprites.Add(s);
        }

        fatiguesPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "BDU UBACS";
        tempSP.patternName = "3Camo1";
        tempSP.packType = "Fatigues";
        tempSP.numberOfColours = 3;
        foreach (Sprite s in BDUUBACS3C1)
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
        tempSP.numberOfColours = 1;
        foreach (Sprite s in SwordS1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        return weaponPacks;
    }
}
