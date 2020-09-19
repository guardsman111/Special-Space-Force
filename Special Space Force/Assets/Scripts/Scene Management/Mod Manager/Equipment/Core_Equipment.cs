using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core_Equipment : MonoBehaviour
{
    public Equipment_Manager manager;

    public Sprite[] Mk1ArmourP1;
    public Sprite[] Mk1ArmourPS1;
    public Sprite[] Mk1HelmetP1;
    public Sprite[] Mk1HelmetPS1;
    public Sprite[] BDUCasual;
    public Sprite[] BDUUBACSP1;
    public Sprite[] BDUUBACSPS1;
    public Sprite[] BDUUBACS3C1;

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
}
