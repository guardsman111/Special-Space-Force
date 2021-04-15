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
    public int Mk1ArmourAV;
    public int Mk1ArmourCost;
    public Sprite[] Mk2ArmourTP;
    public Sprite[] Mk2ArmourT2Camo;
    public Sprite[] Mk2ArmourT3Camo;
    public Sprite[] Mk2ArmourT3CamoDigi;
    public int Mk2ArmourTAV;
    public int Mk2ArmourTCost;
    public Sprite[] Mk2ArmourRP;
    public Sprite[] Mk2ArmourR2Camo;
    public Sprite[] Mk2ArmourR3Camo;
    public Sprite[] Mk2ArmourR3CamoDigi;
    public int Mk2ArmourRAV;
    public int Mk2ArmourRCost;
    public Sprite[] Mk2ArmourBP;
    public Sprite[] Mk2ArmourB2Camo;
    public Sprite[] Mk2ArmourB3Camo;
    public Sprite[] Mk2ArmourB3CamoDigi;
    public int Mk2ArmourBAV;
    public int Mk2ArmourBCost;
    public Sprite[] Mk3ArmourTP;
    public Sprite[] Mk3ArmourT2Camo;
    public Sprite[] Mk3ArmourT3Camo;
    public Sprite[] Mk3ArmourT3CamoDigi;
    public Sprite[] Mk3ArmourTS1;
    public Sprite[] Mk3ArmourTS2;
    public int Mk3ArmourTAV;
    public int Mk3ArmourTCost;
    public Sprite[] Mk3ArmourHP;
    public Sprite[] Mk3ArmourH2Camo;
    public Sprite[] Mk3ArmourH3Camo;
    public Sprite[] Mk3ArmourH3CamoDigi;
    public Sprite[] Mk3ArmourHS1;
    public Sprite[] Mk3ArmourHS2;
    public int Mk3ArmourHAV;
    public int Mk3ArmourHCost;
    public Sprite[] Mk1HelmetP;
    public Sprite[] Mk1HelmetS1;
    public Sprite[] Mk1HelmetS2;
    public Sprite[] Mk1Helmet2Camo;
    public Sprite[] Mk1Helmet3Camo;
    public Sprite[] Mk1Helmet3CamoDigi;
    public int Mk1HelmetAV;
    public int Mk1HelmetCost;
    public Sprite[] Mk1Mod1HelmetP;
    public Sprite[] Mk1Mod1HelmetS1;
    public Sprite[] Mk1Mod1HelmetS2;
    public Sprite[] Mk1Mod1Helmet2Camo;
    public Sprite[] Mk1Mod1Helmet3Camo;
    public Sprite[] Mk1Mod1Helmet3CamoDigi;
    public int Mk1Mod1HelmetAV;
    public int Mk1Mod1HelmetCost;
    public Sprite[] Mk2HelmetTP;
    public Sprite[] Mk2HelmetTS1;
    public Sprite[] Mk2HelmetTS2;
    public Sprite[] Mk2HelmetT2Camo;
    public Sprite[] Mk2HelmetT3Camo;
    public Sprite[] Mk2HelmetT3CamoDigi;
    public int Mk2HelmetTAV;
    public int Mk2HelmetTCost;
    public Sprite[] Mk2HelmetRP;
    public Sprite[] Mk2HelmetRS1;
    public Sprite[] Mk2HelmetRS2;
    public Sprite[] Mk2HelmetR2Camo;
    public Sprite[] Mk2HelmetR3Camo;
    public Sprite[] Mk2HelmetR3CamoDigi;
    public int Mk2HelmetRAV;
    public int Mk2HelmetRCost;
    public Sprite[] Mk2HelmetBP;
    public Sprite[] Mk2HelmetBS1;
    public Sprite[] Mk2HelmetBS2;
    public Sprite[] Mk2HelmetB2Camo;
    public Sprite[] Mk2HelmetB3Camo;
    public Sprite[] Mk2HelmetB3CamoDigi;
    public int Mk2HelmetBAV;
    public int Mk2HelmetBCost;
    public Sprite[] Mk3HelmetTP;
    public Sprite[] Mk3HelmetTS1;
    public Sprite[] Mk3HelmetTS2;
    public Sprite[] Mk3HelmetT2Camo;
    public Sprite[] Mk3HelmetT3Camo;
    public Sprite[] Mk3HelmetT3CamoDigi;
    public int Mk3HelmetTAV;
    public int Mk3HelmetTCost;
    public Sprite[] Mk3HelmetHP;
    public Sprite[] Mk3HelmetHS1;
    public Sprite[] Mk3HelmetHS2;
    public Sprite[] Mk3HelmetH2Camo;
    public Sprite[] Mk3HelmetH3Camo;
    public Sprite[] Mk3HelmetH3CamoDigi;
    public int Mk3HelmetHAV;
    public int Mk3HelmetHCost;
    public Sprite[] FatiguesCasualP;
    public Sprite[] FatiguesCasual2Camo;
    public Sprite[] FatiguesCasual3Camo;
    public Sprite[] FatiguesCasual3CamoDigi;
    public Sprite[] FatiguesUBACSP;
    public Sprite[] FatiguesUBACS2Camo;
    public Sprite[] FatiguesUBACS3Camo;
    public Sprite[] FatiguesUBACS3CamoDigi;
    public Sprite[] FatiguesBSuitP;
    public Sprite[] FatiguesBSuit2Camo;
    public Sprite[] FatiguesBSuit3Camo;
    public Sprite[] FatiguesBSuit3CamoDigi;
    public Sprite[] Backpack1;
    public int Backpack1Cost;
    public Sprite[] Grenade1;
    public int Grenade1R;
    public int Grenade1D;
    public int Grenade1Cost;
    public Sprite[] SGrenade1;
    public int SGrenade1R;
    public int SGrenade1D;
    public int SGrenade1Cost;
    public Sprite[] Cloak1;
    public int Cloak1Cost;
    public Sprite[] Cloak2;
    public int Cloak2Cost;
    public Sprite[] JumpPack1;
    public int JumpPack1Cost;
    public Sprite[] Pistol1;
    public int Pistol1R;
    public int Pistol1D;
    public int Pistol1Cost;
    public Sprite[] PistolS1;
    public int PistolS1Cost;
    public Sprite[] Rifle1;
    public int Rifle1R;
    public int Rifle1D;
    public int Rifle1Cost;
    public Sprite[] Revolver1;
    public int Revolver1R;
    public int Revolver1D;
    public int Revolver1Cost;
    public Sprite[] RevolverS1;
    public int RevolverS1Cost;
    public Sprite[] Shotgun1;
    public int Shotgun1R;
    public int Shotgun1D;
    public int Shotgun1Cost;
    public Sprite[] ShotgunS1;
    public int ShotgunS1Cost;
    public Sprite[] Sniper1;
    public int Sniper1R;
    public int Sniper1D;
    public int Sniper1Cost;
    public Sprite[] Sword1;
    public int Sword1R;
    public int Sword1D;
    public int Sword1Cost;
    public Sprite[] SwordS1;
    public int SwordS1Cost;
    public Sprite[] ShockPistol1;
    public Sprite[] ShockPistol1S;
    public int ShockPistol1R;
    public int ShockPistol1D;
    public int ShockPistol1Cost;
    public Sprite[] ShockRifle1;
    public int ShockRifle1R;
    public int ShockRifle1D;
    public int ShockRifle1Cost;
    public Sprite[] AdvancedMG1;
    public int AdvancedMG1R;
    public int AdvancedMG1D;
    public int AdvancedMG1Cost;
    public Sprite[] RocketLauncher1;
    public int RocketLauncher1R;
    public int RocketLauncher1D;
    public int RocketLauncher1Cost;
    public Sprite[] LaserPistol1;
    public Sprite[] LaserPistol1S;
    public int LaserPistol1R;
    public int LaserPistol1D;
    public int LaserPistol1Cost;
    public Sprite[] LaserRifle1;
    public int LaserRifle1R;
    public int LaserRifle1D;
    public int LaserRifle1Cost;
    public Sprite[] PlasmaRifle1;
    public int PlasmaRifle1R;
    public int PlasmaRifle1D;
    public int PlasmaRifle1Cost;
    public Sprite[] MagmaLauncher1;
    public int MagmaLauncher1R;
    public int MagmaLauncher1D;
    public int MagmaLauncher1Cost;

    //Core armour packs
    public List<Sprite_Pack> CoreArmour()
    {
        List<Sprite_Pack> armourPacks = new List<Sprite_Pack>();

        Sprite_Pack tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Armour";
        tempSP.patternName = "Primary";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk1ArmourCost;
        tempSP.armourV = Mk1ArmourAV;
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
        tempSP.armourV = Mk1ArmourAV;
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
        tempSP.armourV = Mk1ArmourAV;
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
        tempSP.armourV = Mk1ArmourAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk1Armour3CamoDigi)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        //Mk2 Armour

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Trooper Armour";
        tempSP.patternName = "Primary";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk2ArmourTCost;
        tempSP.armourV = Mk2ArmourTAV;
        tempSP.numberOfColours = 1;
        foreach (Sprite s in Mk2ArmourTP)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Trooper Armour";
        tempSP.patternName = "Camo 2C";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk2ArmourTCost;
        tempSP.armourV = Mk2ArmourTAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk2ArmourT2Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Trooper Armour";
        tempSP.patternName = "Camo 3C";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk2ArmourTCost;
        tempSP.armourV = Mk2ArmourTAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk2ArmourT3Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Trooper Armour";
        tempSP.patternName = "Digital Camo 3C";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk2ArmourTCost;
        tempSP.armourV = Mk2ArmourTAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk2ArmourT3CamoDigi)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Recon Armour";
        tempSP.patternName = "Primary";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk2ArmourRCost;
        tempSP.armourV = Mk2ArmourRAV;
        tempSP.numberOfColours = 1;
        foreach (Sprite s in Mk2ArmourRP)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Recon Armour";
        tempSP.patternName = "Camo 2C";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk2ArmourRCost;
        tempSP.armourV = Mk2ArmourRAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk2ArmourR2Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Recon Armour";
        tempSP.patternName = "Camo 3C";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk2ArmourRCost;
        tempSP.armourV = Mk2ArmourRAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk2ArmourR3Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Recon Armour";
        tempSP.patternName = "Digital Camo 3C";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk2ArmourRCost;
        tempSP.armourV = Mk2ArmourRAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk2ArmourR3CamoDigi)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Breacher Armour";
        tempSP.patternName = "Primary";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk2ArmourBCost;
        tempSP.armourV = Mk2ArmourBAV;
        tempSP.numberOfColours = 1;
        foreach (Sprite s in Mk2ArmourBP)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Breacher Armour";
        tempSP.patternName = "Camo 2C";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk2ArmourBCost;
        tempSP.armourV = Mk2ArmourBAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk2ArmourB2Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Breacher Armour";
        tempSP.patternName = "Camo 3C";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk2ArmourBCost;
        tempSP.armourV = Mk2ArmourBAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk2ArmourB3Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Breacher Armour";
        tempSP.patternName = "Digital Camo 3C";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk2ArmourBCost;
        tempSP.armourV = Mk2ArmourBAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk2ArmourB3CamoDigi)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        //Mk3 Armour

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Trooper Armour";
        tempSP.patternName = "Primary";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk3ArmourTCost;
        tempSP.armourV = Mk3ArmourTAV;
        tempSP.numberOfColours = 1;
        foreach (Sprite s in Mk3ArmourTP)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Trooper Armour";
        tempSP.patternName = "Secondary Halves";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk3ArmourTCost;
        tempSP.armourV = Mk3ArmourTAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk3ArmourTS1)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Trooper Armour";
        tempSP.patternName = "Secondary Parts";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk3ArmourTCost;
        tempSP.armourV = Mk3ArmourTAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk3ArmourTS2)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Trooper Armour";
        tempSP.patternName = "Camo 2C";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk3ArmourTCost;
        tempSP.armourV = Mk3ArmourTAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk3ArmourT2Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Trooper Armour";
        tempSP.patternName = "Camo 3C";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk3ArmourTCost;
        tempSP.armourV = Mk3ArmourTAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk3ArmourT3Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Trooper Armour";
        tempSP.patternName = "Digital Camo 3C";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk3ArmourTCost;
        tempSP.armourV = Mk3ArmourTAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk3ArmourT3CamoDigi)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Heavy Armour";
        tempSP.patternName = "Primary";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk3ArmourHCost;
        tempSP.armourV = Mk3ArmourHAV;
        tempSP.numberOfColours = 1;
        foreach (Sprite s in Mk3ArmourHP)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Heavy Armour";
        tempSP.patternName = "Secondary Halves";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk3ArmourHCost;
        tempSP.armourV = Mk3ArmourHAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk3ArmourHS1)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Heavy Armour";
        tempSP.patternName = "Secondary Parts";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk3ArmourHCost;
        tempSP.armourV = Mk3ArmourHAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk3ArmourHS2)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Heavy Armour";
        tempSP.patternName = "Camo 2C";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk3ArmourHCost;
        tempSP.armourV = Mk3ArmourHAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk3ArmourH2Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Heavy Armour";
        tempSP.patternName = "Camo 3C";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk3ArmourHCost;
        tempSP.armourV = Mk3ArmourHAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk3ArmourH3Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Heavy Armour";
        tempSP.patternName = "Digital Camo 3C";
        tempSP.packType = "Armour";
        tempSP.CostPerItem = Mk3ArmourHCost;
        tempSP.armourV = Mk3ArmourHAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk3ArmourH3CamoDigi)
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
        tempSP.patternName = "Primary";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk1HelmetCost;
        tempSP.armourV = Mk1HelmetAV;
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
        tempSP.armourV = Mk1HelmetAV;
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
        tempSP.armourV = Mk1HelmetAV;
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
        tempSP.armourV = Mk1HelmetAV;
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
        tempSP.armourV = Mk1HelmetAV;
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
        tempSP.armourV = Mk1HelmetAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk1Helmet3CamoDigi)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Comms Helmet";
        tempSP.patternName = "Primary";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk1Mod1HelmetCost;
        tempSP.armourV = Mk1Mod1HelmetAV;
        tempSP.numberOfColours = 1;
        foreach (Sprite s in Mk1Mod1HelmetP)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Comms Helmet";
        tempSP.patternName = "Secondary1";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk1Mod1HelmetCost;
        tempSP.armourV = Mk1Mod1HelmetAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk1Mod1HelmetS1)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Comms Helmet";
        tempSP.patternName = "Secondary2";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk1Mod1HelmetCost;
        tempSP.armourV = Mk1Mod1HelmetAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk1Mod1HelmetS2)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Comms Helmet";
        tempSP.patternName = "Camo 2C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk1Mod1HelmetCost;
        tempSP.armourV = Mk1Mod1HelmetAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk1Mod1Helmet2Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Comms Helmet";
        tempSP.patternName = "Camo 3C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk1Mod1HelmetCost;
        tempSP.armourV = Mk1Mod1HelmetAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk1Mod1Helmet3Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Comms Helmet";
        tempSP.patternName = "Digital Camo 3C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk1Mod1HelmetCost;
        tempSP.armourV = Mk1Mod1HelmetAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk1Mod1Helmet3CamoDigi)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        //Mk2 Helmets

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Trooper Helmet";
        tempSP.patternName = "Primary";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk2HelmetTCost;
        tempSP.armourV = Mk2HelmetTAV;
        tempSP.numberOfColours = 1;
        foreach (Sprite s in Mk2HelmetTP)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Trooper Helmet";
        tempSP.patternName = "Secondary Halves";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk2HelmetTCost;
        tempSP.armourV = Mk2HelmetTAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk2HelmetTS1)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Trooper Helmet";
        tempSP.patternName = "Secondary Parts";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk2HelmetTCost;
        tempSP.armourV = Mk2HelmetTAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk2HelmetTS2)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Trooper Helmet";
        tempSP.patternName = "Camo 2C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk2HelmetTCost;
        tempSP.armourV = Mk2HelmetTAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk2HelmetT2Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Trooper Helmet";
        tempSP.patternName = "Camo 3C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk2HelmetTCost;
        tempSP.armourV = Mk2HelmetTAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk2HelmetT3Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Trooper Helmet";
        tempSP.patternName = "Digital Camo 3C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk2HelmetTCost;
        tempSP.armourV = Mk2HelmetTAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk2HelmetT3CamoDigi)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Recon Helmet";
        tempSP.patternName = "Primary";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk2HelmetRCost;
        tempSP.armourV = Mk2HelmetRAV;
        tempSP.numberOfColours = 1;
        foreach (Sprite s in Mk2HelmetRP)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Recon Helmet";
        tempSP.patternName = "Secondary Halves";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk2HelmetRCost;
        tempSP.armourV = Mk2HelmetRAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk2HelmetRS1)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Recon Helmet";
        tempSP.patternName = "Secondary Parts";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk2HelmetRCost;
        tempSP.armourV = Mk2HelmetRAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk2HelmetRS2)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Recon Helmet";
        tempSP.patternName = "Camo 2C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk2HelmetRCost;
        tempSP.armourV = Mk2HelmetRAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk2HelmetR2Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Recon Helmet";
        tempSP.patternName = "Camo 3C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk2HelmetRCost;
        tempSP.armourV = Mk2HelmetRAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk2HelmetR3Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Recon Helmet";
        tempSP.patternName = "Digital Camo 3C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk2HelmetRCost;
        tempSP.armourV = Mk2HelmetRAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk2HelmetR3CamoDigi)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);
        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Breacher Helmet";
        tempSP.patternName = "Primary";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk2HelmetBCost;
        tempSP.armourV = Mk2HelmetBAV;
        tempSP.numberOfColours = 1;
        foreach (Sprite s in Mk2HelmetBP)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Breacher Helmet";
        tempSP.patternName = "Secondary Halves";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk2HelmetBCost;
        tempSP.armourV = Mk2HelmetBAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk2HelmetBS1)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Breacher Helmet";
        tempSP.patternName = "Secondary Parts";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk2HelmetBCost;
        tempSP.armourV = Mk2HelmetBAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk2HelmetBS2)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Breacher Helmet";
        tempSP.patternName = "Camo 2C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk2HelmetBCost;
        tempSP.armourV = Mk2HelmetBAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk2HelmetB2Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Breacher Helmet";
        tempSP.patternName = "Camo 3C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk2HelmetBCost;
        tempSP.armourV = Mk2HelmetBAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk2HelmetB3Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk2 Breacher Helmet";
        tempSP.patternName = "Digital Camo 3C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk2HelmetBCost;
        tempSP.armourV = Mk2HelmetBAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk2HelmetB3CamoDigi)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        //Mk3 Helmets

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Trooper Helmet";
        tempSP.patternName = "Primary";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk3HelmetTCost;
        tempSP.armourV = Mk3HelmetTAV;
        tempSP.numberOfColours = 1;
        foreach (Sprite s in Mk3HelmetTP)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Trooper Helmet";
        tempSP.patternName = "Secondary Halves";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk3HelmetTCost;
        tempSP.armourV = Mk3HelmetTAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk3HelmetTS1)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Trooper Helmet";
        tempSP.patternName = "Secondary Parts";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk3HelmetTCost;
        tempSP.armourV = Mk3HelmetTAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk3HelmetTS2)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Trooper Helmet";
        tempSP.patternName = "Camo 2C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk3HelmetTCost;
        tempSP.armourV = Mk3HelmetTAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk3HelmetT2Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Trooper Helmet";
        tempSP.patternName = "Camo 3C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk3HelmetTCost;
        tempSP.armourV = Mk3HelmetTAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk3HelmetT3Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Trooper Helmet";
        tempSP.patternName = "Digital Camo 3C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk3HelmetTCost;
        tempSP.armourV = Mk3HelmetTAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk3HelmetT3CamoDigi)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Heavy Helmet";
        tempSP.patternName = "Primary";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk3HelmetHCost;
        tempSP.armourV = Mk3HelmetHAV;
        tempSP.numberOfColours = 1;
        foreach (Sprite s in Mk3HelmetHP)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Heavy Helmet";
        tempSP.patternName = "Secondary Halves";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk3HelmetHCost;
        tempSP.armourV = Mk3HelmetHAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk3HelmetHS1)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Heavy Helmet";
        tempSP.patternName = "Secondary Parts";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk3HelmetHCost;
        tempSP.armourV = Mk3HelmetHAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk3HelmetHS2)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Heavy Helmet";
        tempSP.patternName = "Camo 2C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk3HelmetHCost;
        tempSP.armourV = Mk3HelmetHAV;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk3HelmetH2Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Heavy Helmet";
        tempSP.patternName = "Camo 3C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk3HelmetHCost;
        tempSP.armourV = Mk3HelmetHAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk3HelmetH3Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        helmetPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk3 Heavy Helmet";
        tempSP.patternName = "Digital Camo 3C";
        tempSP.packType = "Helmet";
        tempSP.CostPerItem = Mk3HelmetHCost;
        tempSP.armourV = Mk3HelmetHAV;
        tempSP.numberOfColours = 3;
        foreach (Sprite s in Mk3HelmetH3CamoDigi)
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
        tempSP.patternName = "Primary";
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
        tempSP.patternName = "Primary";
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

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Link Suit";
        tempSP.patternName = "Primary";
        tempSP.packType = "Fatigues";
        tempSP.numberOfColours = 1;
        foreach (Sprite s in FatiguesBSuitP)
        {
            tempSP.containedSprites.Add(s);
        }

        fatiguesPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Link Suit";
        tempSP.patternName = "Camo 2C";
        tempSP.packType = "Fatigues";
        tempSP.numberOfColours = 2;
        foreach (Sprite s in FatiguesBSuit2Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        fatiguesPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Link Suit";
        tempSP.patternName = "Camo 3C";
        tempSP.packType = "Fatigues";
        tempSP.numberOfColours = 3;
        foreach (Sprite s in FatiguesBSuit3Camo)
        {
            tempSP.containedSprites.Add(s);
        }

        fatiguesPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Link Suit";
        tempSP.patternName = "Digital Camo 3C";
        tempSP.packType = "Fatigues";
        tempSP.numberOfColours = 3;
        foreach (Sprite s in FatiguesBSuit3CamoDigi)
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

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Frag Grenades";
        tempSP.patternName = "Primary1";
        tempSP.packType = "Equipment";
        tempSP.CostPerItem = Grenade1Cost;
        tempSP.rangeV = Grenade1R;
        tempSP.damageV = Grenade1D;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Grenade1)
        {
            tempSP.containedSprites.Add(s);
        }

        equipPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Sticky Grenades";
        tempSP.patternName = "Primary1";
        tempSP.packType = "Equipment";
        tempSP.CostPerItem = SGrenade1Cost;
        tempSP.rangeV = SGrenade1R;
        tempSP.damageV = SGrenade1D;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in SGrenade1)
        {
            tempSP.containedSprites.Add(s);
        }

        equipPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Cloak";
        tempSP.patternName = "Primary1";
        tempSP.packType = "Equipment";
        tempSP.CostPerItem = Cloak1Cost;
        tempSP.numberOfColours = 1;
        foreach (Sprite s in Cloak1)
        {
            tempSP.containedSprites.Add(s);
        }

        equipPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Camo Cloak";
        tempSP.patternName = "Primary1";
        tempSP.packType = "Equipment";
        tempSP.CostPerItem = Cloak2Cost;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Cloak2)
        {
            tempSP.containedSprites.Add(s);
        }

        equipPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Jump Pack";
        tempSP.patternName = "Primary1";
        tempSP.packType = "Equipment";
        tempSP.CostPerItem = JumpPack1Cost;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in JumpPack1)
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
        tempSP.packName = "Pistol";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponP";
        tempSP.CostPerItem = Pistol1Cost;
        tempSP.rangeV = Pistol1R;
        tempSP.damageV = Pistol1D;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Pistol1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Pistol Sec";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponS";
        tempSP.CostPerItem = PistolS1Cost;
        tempSP.rangeV = Pistol1R;
        tempSP.damageV = Pistol1D;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in PistolS1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Battle Rifle";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponP";
        tempSP.CostPerItem = Rifle1Cost;
        tempSP.rangeV = Rifle1R;
        tempSP.damageV = Rifle1D;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Rifle1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Revolver";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponP";
        tempSP.CostPerItem = Revolver1Cost;
        tempSP.rangeV = Revolver1R;
        tempSP.damageV = Revolver1D;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Revolver1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Revolver Sec";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponS";
        tempSP.CostPerItem = RevolverS1Cost;
        tempSP.rangeV = Revolver1R;
        tempSP.damageV = Revolver1D;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in RevolverS1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Shotgun";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponP";
        tempSP.CostPerItem = Shotgun1Cost;
        tempSP.rangeV = Shotgun1R;
        tempSP.damageV = Shotgun1D;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Shotgun1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Shotgun Sec";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponS";
        tempSP.CostPerItem = ShotgunS1Cost;
        tempSP.rangeV = Shotgun1R;
        tempSP.damageV = Shotgun1D;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in ShotgunS1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Anti Material Sniper Rifle";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponP";
        tempSP.CostPerItem = Sniper1Cost;
        tempSP.rangeV = Sniper1R;
        tempSP.damageV = Sniper1D;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Sniper1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Sword";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponP";
        tempSP.CostPerItem = Sword1Cost;
        tempSP.rangeV = Sword1R;
        tempSP.damageV = Sword1D;
        tempSP.numberOfColours = 1;
        foreach (Sprite s in Sword1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Sword Sec";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponS";
        tempSP.CostPerItem = SwordS1Cost;
        tempSP.rangeV = Sword1R;
        tempSP.damageV = Sword1D;
        tempSP.numberOfColours = 1;
        foreach (Sprite s in SwordS1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Shock Pistol";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponP";
        tempSP.CostPerItem = ShockPistol1Cost;
        tempSP.rangeV = ShockPistol1R;
        tempSP.damageV = ShockPistol1D;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in ShockPistol1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Shock Pistol Sec";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponS";
        tempSP.CostPerItem = ShockPistol1Cost;
        tempSP.rangeV = ShockPistol1R;
        tempSP.damageV = ShockPistol1D;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in ShockPistol1S)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Shock Blaster";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponP";
        tempSP.CostPerItem = ShockRifle1Cost;
        tempSP.rangeV = ShockRifle1R;
        tempSP.damageV = ShockRifle1D;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in ShockRifle1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Advanced Support Rifle";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponP";
        tempSP.CostPerItem = AdvancedMG1Cost;
        tempSP.rangeV = AdvancedMG1R;
        tempSP.damageV = AdvancedMG1D;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in AdvancedMG1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Missile Launcher";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponP";
        tempSP.CostPerItem = RocketLauncher1Cost;
        tempSP.rangeV = RocketLauncher1R;
        tempSP.damageV = RocketLauncher1D;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in RocketLauncher1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Laser Pistol";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponP";
        tempSP.CostPerItem = LaserPistol1Cost;
        tempSP.rangeV = LaserPistol1R;
        tempSP.damageV = LaserPistol1D;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in LaserPistol1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Laser Pistol Sec";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponS";
        tempSP.CostPerItem = LaserPistol1Cost;
        tempSP.rangeV = LaserPistol1R;
        tempSP.damageV = LaserPistol1D;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in LaserPistol1S)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Laser Rifle";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponP";
        tempSP.CostPerItem = LaserRifle1Cost;
        tempSP.rangeV = LaserRifle1R;
        tempSP.damageV = LaserRifle1D;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in LaserRifle1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Plasma Rifle";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponP";
        tempSP.CostPerItem = PlasmaRifle1Cost;
        tempSP.rangeV = PlasmaRifle1R;
        tempSP.damageV = PlasmaRifle1D;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in PlasmaRifle1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Magma Launcher";
        tempSP.patternName = "Primary1";
        tempSP.packType = "WeaponP";
        tempSP.CostPerItem = MagmaLauncher1Cost;
        tempSP.rangeV = MagmaLauncher1R;
        tempSP.damageV = MagmaLauncher1D;
        tempSP.numberOfColours = 2;
        foreach (Sprite s in MagmaLauncher1)
        {
            tempSP.containedSprites.Add(s);
        }

        weaponPacks.Add(tempSP);

        return weaponPacks;
    }
}
