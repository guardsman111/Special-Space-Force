using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Equipment_Manager : MonoBehaviour
{
    public FileFinder finder;


    public Sprite[] Mk1Primary1;
    public Sprite[] Mk1Secondary1;

    private List<Sprite_Pack> armourPacks;
    private List<Sprite_Pack> fatiguesPacks;
    private List<Sprite_Pack> helmetPacks;
    private List<Sprite_Pack> equipmentPacks;
    private List<Sprite_Pack> weaponPacks;

    public Dropdown HelmetDropdownCustom;
    public Dropdown FatiguesDropdownCustom;
    public Dropdown ArmourDropdownCustom;
    public Dropdown EquipmentDropdownCustom;
    public Dropdown WeaponDropdownCustom;
    public Dropdown HelmetPatternCustom;
    public Dropdown FatiguesPatternCustom;
    public Dropdown ArmourPatternCustom;
    public Dropdown EquipmentPatternCustom;
    public Dropdown WeaponPatternCustom;

    public Dropdown HelmetDropdownSquad;
    public Dropdown FatiguesDropdownSquad;
    public Dropdown ArmourDropdownSquad;
    public Dropdown EquipmentDropdownSquad;
    public Dropdown WeaponDropdownSquad;

    public Dropdown HelmetDropdownTrooper;
    public Dropdown FatiguesDropdownTrooper;
    public Dropdown ArmourDropdownTrooper;
    public Dropdown EquipmentDropdownTrooper;
    public Dropdown WeaponDropdownTrooper;

    public void Begin()
    {
        armourPacks = new List<Sprite_Pack>();
        fatiguesPacks = new List<Sprite_Pack>();
        helmetPacks = new List<Sprite_Pack>();
        equipmentPacks = new List<Sprite_Pack>();
        weaponPacks = new List<Sprite_Pack>();

        Sprite_Pack tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Armour";
        tempSP.patternName = "Primary1";
        tempSP.packType = "Armour";
        tempSP.numberOfColours = 1;
        foreach (Sprite s in Mk1Primary1)
        {
            tempSP.containedSprites.Add(s);
        }

        tempSP = new Sprite_Pack();
        tempSP.containedSprites = new List<Sprite>();
        tempSP.packName = "Mk1 Armour";
        tempSP.patternName = "Secondary1";
        tempSP.packType = "Armour";
        tempSP.numberOfColours = 2;
        foreach (Sprite s in Mk1Secondary1)
        {
            tempSP.containedSprites.Add(s);
        }

        armourPacks.Add(tempSP);

        List<string> armourFileLocations = finder.Retrieve("Armour", ".meta", ".png", ".jpg");
        List<string> fatigueFileLocations = finder.Retrieve("Fatigues", ".meta", ".png", ".jpg");
        List<string> helmetFileLocations = finder.Retrieve("Helmet", ".meta", ".png", ".jpg");
        List<string> equipmentFileLocations = finder.Retrieve("Equipment", ".meta", ".png", ".jpg");
        List<string> weaponFileLocations = finder.Retrieve("Weapon", ".meta", ".png", ".jpg");

        foreach (string s in armourFileLocations)
        {
            Armour_Class temp = Serializer.Deserialize<Armour_Class>(s);
            tempSP = new Sprite_Pack();
            tempSP.packName = temp.armourName;
            tempSP.patternName = temp.patternName;
            tempSP.containedSprites = new List<Sprite>();
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.ArmourOutlinePath);
                tempTex.filterMode = FilterMode.Point;
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
            }
            catch { }
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.ArmourPrimaryPath);
                tempTex.filterMode = FilterMode.Point;
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
                tempSP.numberOfColours = 1;
            }
            catch { }
            try {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.ArmourSecondaryPath);
                tempTex.filterMode = FilterMode.Point;
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
                tempSP.numberOfColours = 2;
            }
            catch { }
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.ArmourTertiaryPath);
                tempTex.filterMode = FilterMode.Point;
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
                tempSP.numberOfColours = 3;
            }
            catch { }
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.ArmourEquipmentPath);
                tempTex.filterMode = FilterMode.Point;
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
            }
            catch { }
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.ArmourSpecialPath);
                tempTex.filterMode = FilterMode.Point;
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
            }
            catch { }
                tempSP.packType = "Armour";
            armourPacks.Add(tempSP);
        }

        foreach (string s in fatigueFileLocations)
        {
            Fatigues_Class temp = Serializer.Deserialize<Fatigues_Class>(s);
            tempSP = new Sprite_Pack();
            tempSP.packName = temp.fatiguesName;
            tempSP.patternName = temp.patternName;
            tempSP.containedSprites = new List<Sprite>();
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.FatiguesOutlinePath);
                tempTex.filterMode = FilterMode.Point;
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
            }
            catch { }
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.FatiguesPrimaryPath);
                tempTex.filterMode = FilterMode.Point;
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
                tempSP.numberOfColours = 1;
            }
            catch { }
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.FatiguesSecondaryPath);
                tempTex.filterMode = FilterMode.Point;
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
                tempSP.numberOfColours = 2;
            }
            catch { }
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.FatiguesTertiaryPath);
                tempTex.filterMode = FilterMode.Point;
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
                tempSP.numberOfColours = 3;
            }
            catch { }
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.FatiguesEquipmentPath);
                tempTex.filterMode = FilterMode.Point;
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
            }
            catch { }
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.FatiguesSpecialPath);
                tempTex.filterMode = FilterMode.Point;
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
            }
            catch { }
            tempSP.packType = "Fatigues";
            fatiguesPacks.Add(tempSP);
        }

        foreach (string s in helmetFileLocations)
        {
            Helmet_Class temp = Serializer.Deserialize<Helmet_Class>(s);
            tempSP = new Sprite_Pack();
            tempSP.packName = temp.helmetName;
            tempSP.patternName = temp.patternName;
            tempSP.containedSprites = new List<Sprite>();
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.HelmetOutlinePath);
                tempTex.filterMode = FilterMode.Point;
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
            }
            catch { }
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.HelmetPrimaryPath);
                tempTex.filterMode = FilterMode.Point;
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
                tempSP.numberOfColours = 1;
            }
            catch { }
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.HelmetSecondaryPath);
                tempTex.filterMode = FilterMode.Point;
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
                tempSP.numberOfColours = 2;
            }
            catch { }
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.HelmetTertiaryPath);
                tempTex.filterMode = FilterMode.Point;
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
                tempSP.numberOfColours = 3;
            }
            catch { }
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.HelmetEquipmentPath);
                tempTex.filterMode = FilterMode.Point;
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
            }
            catch { }
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.HelmetVisorPath);
                tempTex.filterMode = FilterMode.Point;
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
            }
            catch { }
            tempSP.packType = "Helmet";
            helmetPacks.Add(tempSP);
        }

        foreach (string s in equipmentFileLocations)
        {
            Equipment_Class temp = Serializer.Deserialize<Equipment_Class>(s);
            tempSP = new Sprite_Pack();
            tempSP.packName = temp.equipmentName;
            tempSP.containedSprites = new List<Sprite>();
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.EquipmentOutlinePath);
                tempTex.filterMode = FilterMode.Point;
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
            }
            catch { }
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.EquipmentPrimaryPath);
                tempTex.filterMode = FilterMode.Point;
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
                tempSP.numberOfColours = 1;
            }
            catch { }
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.EquipmentSecondaryPath);
                tempTex.filterMode = FilterMode.Point;
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
                tempSP.numberOfColours = 2;
            }
            catch { }
            tempSP.packType = "Equipment";
            equipmentPacks.Add(tempSP);
        }


        foreach (string s in weaponFileLocations)
        {
            Equipment_Class temp = Serializer.Deserialize<Equipment_Class>(s);
            tempSP = new Sprite_Pack();
            tempSP.packName = temp.equipmentName;
            tempSP.containedSprites = new List<Sprite>();
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.EquipmentOutlinePath);
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
            }
            catch { }
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.EquipmentPrimaryPath);
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
                tempSP.numberOfColours = 1;
            }
            catch { }
            try
            {
                Texture2D tempTex = Resources.Load<Texture2D>(temp.EquipmentSecondaryPath);
                tempSP.containedSprites.Add(Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f)));
                tempSP.numberOfColours = 2;
            }
            catch { }
            tempSP.packType = "Weapon";
            weaponPacks.Add(tempSP);
        }

        SetupDropdown(HelmetDropdownCustom);
        SetupDropdown(ArmourDropdownCustom);
        SetupDropdown(FatiguesDropdownCustom);
    }

    public void SaveExamples()
    {
        //var file = File.Create(finder.defaultPath + "/Equipment/Armour/Mk1/Mk1ArmourPrimary1.xml");
        //file.Close();
        //Serializer.Serialize(Mk1Primary, finder.defaultPath + "/Equipment/Armour/Mk1/Mk1ArmourPrimary1.xml");
    }

    public void SetupDropdown(Dropdown dropdown)
    {
        if (dropdown.name == "Helmet Dropdown")
        {
            List<string> names = new List<string>();
            foreach (Sprite_Pack p in helmetPacks)
            {
                if (!names.Contains(p.packName))
                {
                    names.Add(p.packName);
                }
            }
            dropdown.ClearOptions();
            dropdown.AddOptions(names);

            Dropdown child = this.GetComponentInChildren<Dropdown>();
            PatternDropdown("Helmet", "Mk1 Helmet", HelmetPatternCustom);
        }
        if (dropdown.name == "Armour Dropdown")
        {
            List<string> names = new List<string>();
            foreach (Sprite_Pack p in armourPacks)
            {
                if (!names.Contains(p.packName))
                {
                    names.Add(p.packName);
                }
            }
            dropdown.ClearOptions();
            dropdown.AddOptions(names);

            Dropdown child = this.GetComponentInChildren<Dropdown>();
            PatternDropdown("Armour", "Mk1 Armour", ArmourPatternCustom);
        }
        if (dropdown.name == "Fatigues Dropdown")
        {
            List<string> names = new List<string>();
            foreach (Sprite_Pack p in fatiguesPacks)
            {
                if (!names.Contains(p.packName))
                {
                    names.Add(p.packName);
                }
            }
            dropdown.ClearOptions();
            dropdown.AddOptions(names);

            Dropdown child = this.GetComponentInChildren<Dropdown>();
            PatternDropdown("Fatigues", "BDU Casual", FatiguesPatternCustom);
        }
    }

    public void PatternDropdown(string type, string pName, Dropdown dropdown)
    {
        if (type == "Helmet")
        {
            List<string> names = new List<string>();
            foreach (Sprite_Pack p in helmetPacks)
            {
                if (p.packName == pName && !names.Contains(p.patternName))
                {
                    names.Insert(0,p.patternName);
                }
            }
            dropdown.ClearOptions();
            dropdown.AddOptions(names);
        }
        if (type == "Armour")
        {
            List<string> names = new List<string>();
            foreach (Sprite_Pack p in armourPacks)
            {
                if (p.packName == pName && !names.Contains(p.patternName))
                {
                    names.Insert(0, p.patternName);
                }
            }
            dropdown.ClearOptions();
            dropdown.AddOptions(names);
        }
        if (type == "Fatigues")
        {
            List<string> names = new List<string>();
            foreach (Sprite_Pack p in fatiguesPacks)
            {
                if (p.packName == pName && !names.Contains(p.patternName))
                {
                    names.Insert(0, p.patternName);
                }
            }
            dropdown.ClearOptions();
            dropdown.AddOptions(names);
        }
    }

    public void ChangeEquipments(List<Trooper_Script> trooper, Dropdown dropdown)
    {

    }

    public void ChangePatterns(List<Trooper_Script> trooper, Dropdown dropdown)
    {

    }

    public void ChangeEquipment(Trooper_Script trooper, Dropdown dropdown)
    {
        if (dropdown.name == "Armour Dropdown")
        {
            Sprite_Pack pack = new Sprite_Pack();

            foreach (Sprite_Pack p in armourPacks)
            {
                if (p.packName == dropdown.options[dropdown.value].text && p.patternName == trooper.armourPattern)
                {
                    pack = p;
                }
            }
            if (pack.numberOfColours == 1)
            {
                trooper.trooperImages[9].sprite = pack.containedSprites[1];
                trooper.trooperImages[12].sprite = pack.containedSprites[3];
                trooper.trooperImages[13].sprite = pack.containedSprites[2];
                trooper.trooperImages[15].sprite = pack.containedSprites[0];
                trooper.trooperImages[10].enabled = false;
                trooper.trooperImages[11].enabled = false;
                trooper.armour = pack.packName;
            }
            if (pack.numberOfColours == 2)
            {
                trooper.trooperImages[9].sprite = pack.containedSprites[1];
                trooper.trooperImages[10].enabled = true;
                trooper.trooperImages[10].sprite = pack.containedSprites[2];
                trooper.trooperImages[12].sprite = pack.containedSprites[4];
                trooper.trooperImages[13].sprite = pack.containedSprites[3];
                trooper.trooperImages[15].sprite = pack.containedSprites[0];
                trooper.trooperImages[11].enabled = false;
                trooper.armour = pack.packName;
            }
        }
    }

    public void ChangePattern(Trooper_Script trooper, Dropdown dropdown)
    {
        if (dropdown == ArmourPatternCustom)
        {
            Sprite_Pack pack = new Sprite_Pack();

            foreach (Sprite_Pack p in armourPacks)
            {
                if (p.packName == trooper.armour && p.patternName == dropdown.options[dropdown.value].text)
                {
                    pack = p;
                }
            }

            if (pack.numberOfColours == 1)
            {
                trooper.trooperImages[9].sprite = pack.containedSprites[1];
                trooper.trooperImages[12].sprite = pack.containedSprites[3];
                trooper.trooperImages[13].sprite = pack.containedSprites[2];
                trooper.trooperImages[15].sprite = pack.containedSprites[0];
                trooper.trooperImages[10].gameObject.SetActive(false);
                trooper.trooperImages[11].gameObject.SetActive(false);
                trooper.armourPattern = pack.patternName;
            }
            if (pack.numberOfColours == 2)
            {
                trooper.trooperImages[9].sprite = pack.containedSprites[1];
                trooper.trooperImages[10].gameObject.SetActive(true);
                trooper.trooperImages[10].sprite = pack.containedSprites[2];
                trooper.trooperImages[12].sprite = pack.containedSprites[4];
                trooper.trooperImages[13].sprite = pack.containedSprites[3];
                trooper.trooperImages[15].sprite = pack.containedSprites[0];
                trooper.trooperImages[11].gameObject.SetActive(false);
                trooper.armourPattern = pack.patternName;
            }
        }
    }
}
