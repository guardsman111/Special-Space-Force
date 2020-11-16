using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Equipment_Manager : MonoBehaviour
{
    public FileFinder finder;
    public Core_Equipment coreEquipment;
    
    private List<Sprite_Pack> armourPacks;
    private List<Sprite_Pack> fatiguesPacks;
    private List<Sprite_Pack> helmetPacks;
    private List<Sprite_Pack> equipmentPacks;
    private List<Sprite_Pack> weaponPacks;

    public List<Color32> playerDefaultColours;

    public Dropdown HelmetDropdownCustom;
    public Dropdown FatiguesDropdownCustom;
    public Dropdown ArmourDropdownCustom;
    public Dropdown EquipmentDropdownCustom;
    public Dropdown Weapon1DropdownCustom;
    public Dropdown Weapon2DropdownCustom;
    public Dropdown HelmetPatternCustom;
    public Dropdown FatiguesPatternCustom;
    public Dropdown ArmourPatternCustom;

    public Dropdown HelmetDropdownTrooper;
    public Dropdown FatiguesDropdownTrooper;
    public Dropdown ArmourDropdownTrooper;
    public Dropdown EquipmentDropdownTrooper;
    public Dropdown Weapon1DropdownTrooper;
    public Dropdown Weapon2DropdownTrooper;
    public Dropdown HelmetPatternTrooper;
    public Dropdown FatiguesPatternTrooper;
    public Dropdown ArmourPatternTrooper;

    public void Begin()
    {
        armourPacks = coreEquipment.CoreArmour();
        fatiguesPacks = coreEquipment.CoreFatigues();
        helmetPacks = coreEquipment.CoreHelmet();
        equipmentPacks = coreEquipment.CoreEquipment();
        weaponPacks = coreEquipment.CoreWeapons();

        Sprite_Pack tempSP;

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
            tempSP.packName = temp.EquipmentName;
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
            tempSP.packName = temp.EquipmentName.Replace("_", " ");
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
            tempSP.packType = temp.EquipmentType;
            weaponPacks.Add(tempSP);
        }

        SetupDropdown(HelmetDropdownCustom);
        SetupDropdown(ArmourDropdownCustom);
        SetupDropdown(FatiguesDropdownCustom);
        SetupDropdown(Weapon1DropdownCustom);
        SetupDropdown(Weapon2DropdownCustom);
        SetupDropdown(EquipmentDropdownCustom);

        SetupDropdown(HelmetDropdownTrooper);
        SetupDropdown(ArmourDropdownTrooper);
        SetupDropdown(FatiguesDropdownTrooper);
        SetupDropdown(Weapon1DropdownTrooper);
        SetupDropdown(Weapon2DropdownTrooper);
        SetupDropdown(EquipmentDropdownTrooper);

        HelmetDropdownCustom.value = 1;
        ArmourDropdownCustom.value = 1;
        HelmetDropdownTrooper.value = 1;
        ArmourDropdownTrooper.value = 1;
    }

    public void SaveExamples()
    {
        //var file = File.Create(finder.defaultPath + "/Equipment/Armour/Mk1/Mk1ArmourPrimary1.xml");
        //file.Close();
        //Serializer.Serialize(Mk1Primary, finder.defaultPath + "/Equipment/Armour/Mk1/Mk1ArmourPrimary1.xml");
    }

    public List<Color32> GetColours(Image[] images)
    {
        List<Color32> colours = new List<Color32>();

        foreach(Image i in images)
        {
            colours.Add(i.color);
        }

        playerDefaultColours = colours;
        return colours;
    }

    public void SetupDropdown(Dropdown dropdown)
    {
        if (dropdown.name == "Helmet Dropdown")
        {
            List<string> names = new List<string>();
            names.Add("None");
            foreach (Sprite_Pack p in helmetPacks)
            {
                if (!names.Contains(p.packName))
                {
                    names.Add(p.packName);
                }
            }
            dropdown.ClearOptions();
            dropdown.AddOptions(names);

            PatternDropdown("Helmet", "Mk1 Helmet", HelmetPatternCustom);
        }
        if (dropdown.name == "Armour Dropdown")
        {
            List<string> names = new List<string>();
            names.Add("None");
            foreach (Sprite_Pack p in armourPacks)
            {
                if (!names.Contains(p.packName))
                {
                    names.Add(p.packName);
                }
            }
            dropdown.ClearOptions();
            dropdown.AddOptions(names);

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

            PatternDropdown("Fatigues", "BDU Casual", FatiguesPatternCustom);
        }

        if (dropdown.name == "Weapons Dropdown")
        {
            List<string> names = new List<string>();
            names.Add("None");
            foreach (Sprite_Pack p in weaponPacks)
            {
                if (!names.Contains(p.packName))
                {
                    if (p.packType == "WeaponP")
                    {
                        names.Add(p.packName);
                    }
                }
            }
            dropdown.ClearOptions();
            dropdown.AddOptions(names);
        }

        if (dropdown.name == "Weapons 2 Dropdown")
        {
            List<string> names = new List<string>();
            names.Add("None");
            foreach (Sprite_Pack p in weaponPacks)
            {
                if (!names.Contains(p.packName))
                {
                    if (p.packType == "WeaponS")
                    {
                        names.Add(p.packName);
                    }
                }
            }
            dropdown.ClearOptions();
            dropdown.AddOptions(names);
        }

        if (dropdown.name == "Equipment Dropdown")
        {
            List<string> names = new List<string>();
            names.Add("None");
            foreach (Sprite_Pack p in equipmentPacks)
            {
                if (!names.Contains(p.packName))
                {
                    names.Add(p.packName);
                }
            }
            dropdown.ClearOptions();
            dropdown.AddOptions(names);
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
                    names.Add(p.patternName);
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
                    names.Add(p.patternName);
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
                    names.Add(p.patternName);
                }
            }
            dropdown.ClearOptions();
            dropdown.AddOptions(names);
        }
    }

    public void ChangeEquipment(Trooper_Script trooper, Dropdown dropdown)
    {
        if (dropdown.name == "Armour Dropdown")
        {
            if (dropdown.options[dropdown.value].text != "None")
            {
                Sprite_Pack pack = new Sprite_Pack();
                trooper.trooperImages[9].gameObject.SetActive(true);
                trooper.trooperImages[12].gameObject.SetActive(true);
                trooper.trooperImages[13].gameObject.SetActive(true);
                trooper.trooperImages[15].gameObject.SetActive(true);

                foreach (Sprite_Pack p in armourPacks)
                {
                    if (p.packName == dropdown.options[dropdown.value].text && p.patternName == trooper.armourPattern)
                    {
                        pack = p;
                    }
                }
                if (pack.packName == null)
                {
                    foreach (Sprite_Pack p in fatiguesPacks)
                    {
                        if (p.packName == dropdown.options[dropdown.value].text)
                        {
                            pack = p;
                            break;
                        }
                    }
                }

                PatternDropdown("Armour", pack.packName, dropdown.gameObject.GetComponent<Sprite_Changer>().dropdownChild);

                if (pack.numberOfColours == 1)
                {
                    trooper.trooperImages[9].sprite = pack.containedSprites[1];
                    trooper.trooperImages[12].sprite = pack.containedSprites[3];
                    trooper.trooperImages[13].sprite = pack.containedSprites[2];
                    trooper.trooperImages[15].sprite = pack.containedSprites[0];
                    trooper.trooperImages[10].gameObject.SetActive(false);
                    trooper.trooperImages[11].gameObject.SetActive(false);
                    trooper.armour = pack.packName;
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
                    trooper.armour = pack.packName;
                }
                if (pack.numberOfColours == 3)
                {
                    trooper.trooperImages[9].sprite = pack.containedSprites[1];
                    trooper.trooperImages[10].gameObject.SetActive(true);
                    trooper.trooperImages[10].sprite = pack.containedSprites[2];
                    trooper.trooperImages[11].gameObject.SetActive(true);
                    trooper.trooperImages[11].sprite = pack.containedSprites[3];
                    trooper.trooperImages[12].sprite = pack.containedSprites[5];
                    trooper.trooperImages[13].sprite = pack.containedSprites[4];
                    trooper.trooperImages[15].sprite = pack.containedSprites[0];
                    trooper.armour = pack.packName;
                }
            } 
            else
            {
                trooper.trooperImages[9].gameObject.SetActive(false);
                trooper.trooperImages[10].gameObject.SetActive(false);
                trooper.trooperImages[11].gameObject.SetActive(false);
                trooper.trooperImages[12].gameObject.SetActive(false);
                trooper.trooperImages[13].gameObject.SetActive(false);
                trooper.trooperImages[14].gameObject.SetActive(false);
                trooper.trooperImages[15].gameObject.SetActive(false);
            }
        }

        if (dropdown.name == "Fatigues Dropdown")
        {
            Sprite_Pack pack = new Sprite_Pack();

            foreach (Sprite_Pack p in fatiguesPacks)
            {
                if (p.packName == dropdown.options[dropdown.value].text && p.patternName == trooper.fatiguesPattern)
                {
                    pack = p;
                }
            }
            if(pack.packName == null)
            {
                foreach (Sprite_Pack p in fatiguesPacks)
                {
                    if (p.packName == dropdown.options[dropdown.value].text)
                    {
                        pack = p;
                        break;
                    }
                }
            }

            PatternDropdown("Fatigues", pack.packName, dropdown.gameObject.GetComponent<Sprite_Changer>().dropdownChild);

            if (pack.numberOfColours == 1)
            {
                trooper.trooperImages[3].sprite = pack.containedSprites[1];
                trooper.trooperImages[6].sprite = pack.containedSprites[3];
                trooper.trooperImages[7].sprite = pack.containedSprites[2];
                trooper.trooperImages[8].sprite = pack.containedSprites[0];
                trooper.trooperImages[4].gameObject.SetActive(false);
                trooper.trooperImages[5].gameObject.SetActive(false);
                trooper.fatigues = pack.packName;
            }
            if (pack.numberOfColours == 2)
            {
                trooper.trooperImages[3].sprite = pack.containedSprites[1];
                trooper.trooperImages[4].gameObject.SetActive(true);
                trooper.trooperImages[4].sprite = pack.containedSprites[2];
                trooper.trooperImages[6].sprite = pack.containedSprites[4];
                trooper.trooperImages[7].sprite = pack.containedSprites[3];
                trooper.trooperImages[8].sprite = pack.containedSprites[0];
                trooper.trooperImages[5].gameObject.SetActive(false);
                trooper.fatigues = pack.packName;
            }
            if (pack.numberOfColours == 3)
            {
                trooper.trooperImages[3].sprite = pack.containedSprites[1];
                trooper.trooperImages[4].gameObject.SetActive(true);
                trooper.trooperImages[4].sprite = pack.containedSprites[2];
                trooper.trooperImages[5].gameObject.SetActive(true);
                trooper.trooperImages[5].sprite = pack.containedSprites[3];
                trooper.trooperImages[6].sprite = pack.containedSprites[5];
                trooper.trooperImages[7].sprite = pack.containedSprites[4];
                trooper.trooperImages[8].sprite = pack.containedSprites[0];
                trooper.fatigues = pack.packName;
            }
        }

        if (dropdown.name == "Helmet Dropdown")
        {
            if (dropdown.options[dropdown.value].text != "None")
            {
                Sprite_Pack pack = new Sprite_Pack();
                trooper.trooperImages[16].gameObject.SetActive(true);
                trooper.trooperImages[19].gameObject.SetActive(true);
                trooper.trooperImages[20].gameObject.SetActive(true);
                trooper.trooperImages[21].gameObject.SetActive(true);
                trooper.trooperImages[2].gameObject.SetActive(false);

                foreach (Sprite_Pack p in helmetPacks)
                {
                    if (p.packName == dropdown.options[dropdown.value].text && p.patternName == trooper.helmetPattern)
                    {
                        pack = p;
                    }
                }
                if (pack.packName == null)
                {
                    foreach (Sprite_Pack p in helmetPacks)
                    {
                        if (p.packName == dropdown.options[dropdown.value].text)
                        {
                            pack = p;
                            break;
                        }
                    }
                }

                PatternDropdown("Helmet", pack.packName, dropdown.gameObject.GetComponent<Sprite_Changer>().dropdownChild);

                if (pack.numberOfColours == 1)
                {
                    trooper.trooperImages[16].sprite = pack.containedSprites[1];
                    trooper.trooperImages[19].sprite = pack.containedSprites[2];
                    trooper.trooperImages[21].sprite = pack.containedSprites[0];
                    trooper.trooperImages[17].gameObject.SetActive(false);
                    trooper.trooperImages[18].gameObject.SetActive(false);
                    trooper.helmet = pack.packName;
                }
                if (pack.numberOfColours == 2)
                {
                    trooper.trooperImages[16].sprite = pack.containedSprites[1];
                    trooper.trooperImages[17].gameObject.SetActive(true);
                    trooper.trooperImages[17].sprite = pack.containedSprites[2];
                    trooper.trooperImages[19].sprite = pack.containedSprites[3];
                    trooper.trooperImages[21].sprite = pack.containedSprites[0];
                    trooper.trooperImages[18].gameObject.SetActive(false);
                    trooper.helmet = pack.packName;
                }
                if (pack.numberOfColours == 3)
                {
                    trooper.trooperImages[16].sprite = pack.containedSprites[1];
                    trooper.trooperImages[17].gameObject.SetActive(true);
                    trooper.trooperImages[17].sprite = pack.containedSprites[2];
                    trooper.trooperImages[18].gameObject.SetActive(true);
                    trooper.trooperImages[18].sprite = pack.containedSprites[3];
                    trooper.trooperImages[19].sprite = pack.containedSprites[4];
                    trooper.trooperImages[21].sprite = pack.containedSprites[0];
                    trooper.helmet = pack.packName;
                }
            } 
            else
            {
                trooper.trooperImages[16].gameObject.SetActive(false);
                trooper.trooperImages[17].gameObject.SetActive(false);
                trooper.trooperImages[18].gameObject.SetActive(false);
                trooper.trooperImages[19].gameObject.SetActive(false);
                trooper.trooperImages[20].gameObject.SetActive(false);
                trooper.trooperImages[21].gameObject.SetActive(false);
                trooper.trooperImages[2].gameObject.SetActive(true);
            }
        }

        if (dropdown.name == "Weapons Dropdown")
        {
            if (dropdown.options[dropdown.value].text != "None")
            {
                Sprite_Pack pack = new Sprite_Pack();
                trooper.trooperImages[25].gameObject.SetActive(true);
                trooper.trooperImages[26].gameObject.SetActive(true);
                trooper.trooperImages[27].gameObject.SetActive(true);

                foreach (Sprite_Pack p in weaponPacks)
                {
                    if (p.packName == dropdown.options[dropdown.value].text)
                    {
                        pack = p;
                    }
                }
                if (pack.packName == null)
                {
                    foreach (Sprite_Pack p in weaponPacks)
                    {
                        if (p.packName == dropdown.options[dropdown.value].text)
                        {
                            pack = p;
                            break;
                        }
                    }
                }
                
                if (pack.numberOfColours == 1)
                {
                    trooper.trooperImages[25].sprite = pack.containedSprites[0];
                    trooper.trooperImages[26].sprite = pack.containedSprites[1];
                    trooper.trooperImages[27].gameObject.SetActive(false);
                    trooper.helmet = pack.packName;
                }
                if (pack.numberOfColours == 2)
                {
                    trooper.trooperImages[25].sprite = pack.containedSprites[0];
                    trooper.trooperImages[26].sprite = pack.containedSprites[1];
                    trooper.trooperImages[27].sprite = pack.containedSprites[2];
                    trooper.helmet = pack.packName;
                }
            }
            else
            {
                trooper.trooperImages[25].gameObject.SetActive(false);
                trooper.trooperImages[26].gameObject.SetActive(false);
                trooper.trooperImages[27].gameObject.SetActive(false);
            }
        }

        if (dropdown.name == "Weapons 2 Dropdown")
        {
            if (dropdown.options[dropdown.value].text != "None")
            {
                Sprite_Pack pack = new Sprite_Pack();
                trooper.trooperImages[28].gameObject.SetActive(true);
                trooper.trooperImages[29].gameObject.SetActive(true);
                trooper.trooperImages[30].gameObject.SetActive(true);

                foreach (Sprite_Pack p in weaponPacks)
                {
                    if (p.packName == dropdown.options[dropdown.value].text)
                    {
                        pack = p;
                    }
                }
                if (pack.packName == null)
                {
                    foreach (Sprite_Pack p in weaponPacks)
                    {
                        if (p.packName == dropdown.options[dropdown.value].text)
                        {
                            pack = p;
                            break;
                        }
                    }
                }

                if (pack.numberOfColours == 1)
                {
                    trooper.trooperImages[28].sprite = pack.containedSprites[0];
                    trooper.trooperImages[29].sprite = pack.containedSprites[1];
                    trooper.trooperImages[30].gameObject.SetActive(false);
                    trooper.helmet = pack.packName;
                }
                if (pack.numberOfColours == 2)
                {
                    trooper.trooperImages[28].sprite = pack.containedSprites[0];
                    trooper.trooperImages[29].sprite = pack.containedSprites[1];
                    trooper.trooperImages[30].sprite = pack.containedSprites[2];
                    trooper.helmet = pack.packName;
                }
            }
            else
            {
                trooper.trooperImages[28].gameObject.SetActive(false);
                trooper.trooperImages[29].gameObject.SetActive(false);
                trooper.trooperImages[30].gameObject.SetActive(false);
            }
        }

        if (dropdown.name == "Equipment Dropdown")
        {
            if (dropdown.options[dropdown.value].text != "None")
            {
                Sprite_Pack pack = new Sprite_Pack();
                trooper.trooperImages[22].gameObject.SetActive(true);
                trooper.trooperImages[23].gameObject.SetActive(true);
                trooper.trooperImages[24].gameObject.SetActive(true);

                foreach (Sprite_Pack p in weaponPacks)
                {
                    if (p.packName == dropdown.options[dropdown.value].text)
                    {
                        pack = p;
                    }
                }
                if (pack.packName == null)
                {
                    foreach (Sprite_Pack p in weaponPacks)
                    {
                        if (p.packName == dropdown.options[dropdown.value].text)
                        {
                            pack = p;
                            break;
                        }
                    }
                }

                if (pack.numberOfColours == 1)
                {
                    trooper.trooperImages[22].sprite = pack.containedSprites[0];
                    trooper.trooperImages[23].sprite = pack.containedSprites[1];
                    trooper.trooperImages[24].gameObject.SetActive(false);
                    trooper.helmet = pack.packName;
                }
                if (pack.numberOfColours == 2)
                {
                    trooper.trooperImages[22].sprite = pack.containedSprites[0];
                    trooper.trooperImages[23].sprite = pack.containedSprites[1];
                    trooper.trooperImages[24].sprite = pack.containedSprites[2];
                    trooper.helmet = pack.packName;
                }
            }
            else
            {
                trooper.trooperImages[22].gameObject.SetActive(false);
                trooper.trooperImages[23].gameObject.SetActive(false);
                trooper.trooperImages[24].gameObject.SetActive(false);
            }
        }
    }

    public void ChangePattern(Trooper_Script trooper, Dropdown dropdown)
    {
        if (dropdown.name == "Armor")
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
            if (pack.numberOfColours == 3)
            {
                trooper.trooperImages[9].sprite = pack.containedSprites[1];
                trooper.trooperImages[10].gameObject.SetActive(true);
                trooper.trooperImages[10].sprite = pack.containedSprites[2];
                trooper.trooperImages[11].gameObject.SetActive(true);
                trooper.trooperImages[11].sprite = pack.containedSprites[3];
                trooper.trooperImages[12].sprite = pack.containedSprites[4];
                trooper.trooperImages[13].sprite = pack.containedSprites[3];
                trooper.trooperImages[15].sprite = pack.containedSprites[0];
                trooper.armourPattern = pack.patternName;
            }
        }

        if (dropdown.name == "Fatigues")
        {
            Sprite_Pack pack = new Sprite_Pack();

            foreach (Sprite_Pack p in fatiguesPacks)
            {
                if (p.packName == trooper.fatigues && p.patternName == dropdown.options[dropdown.value].text)
                {
                    pack = p;
                }
            }

            if (pack.numberOfColours == 1)
            {
                trooper.trooperImages[3].sprite = pack.containedSprites[1];
                trooper.trooperImages[6].sprite = pack.containedSprites[3];
                trooper.trooperImages[7].sprite = pack.containedSprites[2];
                trooper.trooperImages[8].sprite = pack.containedSprites[0];
                trooper.trooperImages[4].gameObject.SetActive(false);
                trooper.trooperImages[5].gameObject.SetActive(false);
                trooper.fatiguesPattern = pack.patternName;
            }
            if (pack.numberOfColours == 2)
            {
                trooper.trooperImages[3].sprite = pack.containedSprites[1];
                trooper.trooperImages[4].gameObject.SetActive(true);
                trooper.trooperImages[4].sprite = pack.containedSprites[2];
                trooper.trooperImages[6].sprite = pack.containedSprites[4];
                trooper.trooperImages[7].sprite = pack.containedSprites[3];
                trooper.trooperImages[8].sprite = pack.containedSprites[0];
                trooper.trooperImages[5].gameObject.SetActive(false);
                trooper.fatiguesPattern = pack.patternName;
            }
            if (pack.numberOfColours == 3)
            {
                trooper.trooperImages[3].sprite = pack.containedSprites[1];
                trooper.trooperImages[4].gameObject.SetActive(true);
                trooper.trooperImages[4].sprite = pack.containedSprites[2];
                trooper.trooperImages[5].gameObject.SetActive(true);
                trooper.trooperImages[5].sprite = pack.containedSprites[3];
                trooper.trooperImages[6].sprite = pack.containedSprites[5];
                trooper.trooperImages[7].sprite = pack.containedSprites[4];
                trooper.trooperImages[8].sprite = pack.containedSprites[0];
                trooper.fatiguesPattern = pack.patternName;
            }
        }

        if (dropdown.name == "Helmet")
        {
            Sprite_Pack pack = new Sprite_Pack();

            foreach (Sprite_Pack p in helmetPacks)
            {
                if (p.packName == trooper.helmet && p.patternName == dropdown.options[dropdown.value].text)
                {
                    pack = p;
                    //Debug.Log("Success! Matched Pack - ");
                    //Debug.Log("Sprite pack pattern |" + p.patternName + "| and dropdown text |" + dropdown.options[dropdown.value].text);
                    //Debug.Log("Pack name |" + p.packName + "| and trooper gear name |" + trooper.helmet);
                    break;
                }
                else
                {
                    //Debug.Log("Missing Pack");
                    //Debug.Log("Sprite pack pattern |" + p.patternName + "| and dropdown text |" + dropdown.options[dropdown.value].text);
                    //Debug.Log("Pack name |" + p.packName + "| and trooper gear name |" + trooper.helmet);
                }
            }

            if (pack.numberOfColours == 1)
            {
                trooper.trooperImages[16].sprite = pack.containedSprites[1];
                trooper.trooperImages[19].sprite = pack.containedSprites[2];
                trooper.trooperImages[21].sprite = pack.containedSprites[0];
                trooper.trooperImages[17].gameObject.SetActive(false);
                trooper.trooperImages[18].gameObject.SetActive(false);
                trooper.helmetPattern = pack.patternName;
            }
            if (pack.numberOfColours == 2)
            {
                trooper.trooperImages[16].sprite = pack.containedSprites[1];
                trooper.trooperImages[17].gameObject.SetActive(true);
                trooper.trooperImages[17].sprite = pack.containedSprites[2];
                trooper.trooperImages[19].sprite = pack.containedSprites[3];
                trooper.trooperImages[21].sprite = pack.containedSprites[0];
                trooper.trooperImages[18].gameObject.SetActive(false);
                trooper.helmetPattern = pack.patternName;
            }
            if (pack.numberOfColours == 3)
            {
                trooper.trooperImages[16].sprite = pack.containedSprites[1];
                trooper.trooperImages[17].gameObject.SetActive(true);
                trooper.trooperImages[17].sprite = pack.containedSprites[2];
                trooper.trooperImages[18].gameObject.SetActive(true);
                trooper.trooperImages[18].sprite = pack.containedSprites[3];
                trooper.trooperImages[19].sprite = pack.containedSprites[4];
                trooper.trooperImages[21].sprite = pack.containedSprites[0];
                trooper.helmetPattern = pack.patternName;
            }
        }
    }


    public void LoadEquipment(Trooper_Script trooper, string type, string name)
    {
        if (type == "Armour")
        {
            if (name != "None")
            {
                Sprite_Pack pack = new Sprite_Pack();
                trooper.trooperImages[9].gameObject.SetActive(true);
                trooper.trooperImages[12].gameObject.SetActive(true);
                trooper.trooperImages[13].gameObject.SetActive(true);
                trooper.trooperImages[15].gameObject.SetActive(true);

                foreach (Sprite_Pack p in armourPacks)
                {
                    if (p.packName == name && p.patternName == trooper.armourPattern)
                    {
                        pack = p;
                    }
                }
                if (pack.packName == null)
                {
                    foreach (Sprite_Pack p in fatiguesPacks)
                    {
                        if (p.packName == name)
                        {
                            pack = p;
                            break;
                        }
                    }
                }

                PatternDropdown("Armour", pack.packName, ArmourDropdownTrooper.gameObject.GetComponent<Sprite_Changer>().dropdownChild);

                if (pack.numberOfColours == 1)
                {
                    trooper.trooperImages[9].sprite = pack.containedSprites[1];
                    trooper.trooperImages[12].sprite = pack.containedSprites[3];
                    trooper.trooperImages[13].sprite = pack.containedSprites[2];
                    trooper.trooperImages[15].sprite = pack.containedSprites[0];
                    trooper.trooperImages[10].gameObject.SetActive(false);
                    trooper.trooperImages[11].gameObject.SetActive(false);
                    trooper.armour = pack.packName;
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
                    trooper.armour = pack.packName;
                }
                if (pack.numberOfColours == 3)
                {
                    trooper.trooperImages[9].sprite = pack.containedSprites[1];
                    trooper.trooperImages[10].gameObject.SetActive(true);
                    trooper.trooperImages[10].sprite = pack.containedSprites[2];
                    trooper.trooperImages[11].gameObject.SetActive(true);
                    trooper.trooperImages[11].sprite = pack.containedSprites[3];
                    trooper.trooperImages[12].sprite = pack.containedSprites[5];
                    trooper.trooperImages[13].sprite = pack.containedSprites[4];
                    trooper.trooperImages[15].sprite = pack.containedSprites[0];
                    trooper.armour = pack.packName;
                }
            }
            else
            {
                trooper.trooperImages[9].gameObject.SetActive(false);
                trooper.trooperImages[10].gameObject.SetActive(false);
                trooper.trooperImages[11].gameObject.SetActive(false);
                trooper.trooperImages[12].gameObject.SetActive(false);
                trooper.trooperImages[13].gameObject.SetActive(false);
                trooper.trooperImages[14].gameObject.SetActive(false);
                trooper.trooperImages[15].gameObject.SetActive(false);
            }
        }

        if (type == "Fatigues")
        {
            Sprite_Pack pack = new Sprite_Pack();

            foreach (Sprite_Pack p in fatiguesPacks)
            {
                if (p.packName == name && p.patternName == trooper.fatiguesPattern)
                {
                    pack = p;
                }
            }
            if (pack.packName == null)
            {
                foreach (Sprite_Pack p in fatiguesPacks)
                {
                    if (p.packName == name)
                    {
                        pack = p;
                        break;
                    }
                }
            }

            PatternDropdown("Fatigues", pack.packName, FatiguesDropdownTrooper.gameObject.GetComponent<Sprite_Changer>().dropdownChild);

            if (pack.numberOfColours == 1)
            {
                trooper.trooperImages[3].sprite = pack.containedSprites[1];
                trooper.trooperImages[6].sprite = pack.containedSprites[3];
                trooper.trooperImages[7].sprite = pack.containedSprites[2];
                trooper.trooperImages[8].sprite = pack.containedSprites[0];
                trooper.trooperImages[4].gameObject.SetActive(false);
                trooper.trooperImages[5].gameObject.SetActive(false);
                trooper.fatigues = pack.packName;
            }
            if (pack.numberOfColours == 2)
            {
                trooper.trooperImages[3].sprite = pack.containedSprites[1];
                trooper.trooperImages[4].gameObject.SetActive(true);
                trooper.trooperImages[4].sprite = pack.containedSprites[2];
                trooper.trooperImages[6].sprite = pack.containedSprites[4];
                trooper.trooperImages[7].sprite = pack.containedSprites[3];
                trooper.trooperImages[8].sprite = pack.containedSprites[0];
                trooper.trooperImages[5].gameObject.SetActive(false);
                trooper.fatigues = pack.packName;
            }
            if (pack.numberOfColours == 3)
            {
                trooper.trooperImages[3].sprite = pack.containedSprites[1];
                trooper.trooperImages[4].gameObject.SetActive(true);
                trooper.trooperImages[4].sprite = pack.containedSprites[2];
                trooper.trooperImages[5].gameObject.SetActive(true);
                trooper.trooperImages[5].sprite = pack.containedSprites[3];
                trooper.trooperImages[6].sprite = pack.containedSprites[5];
                trooper.trooperImages[7].sprite = pack.containedSprites[4];
                trooper.trooperImages[8].sprite = pack.containedSprites[0];
                trooper.fatigues = pack.packName;
            }
        }

        if (type == "Helmet")
        {
            if (name != "None")
            {
                Sprite_Pack pack = new Sprite_Pack();
                trooper.trooperImages[16].gameObject.SetActive(true);
                trooper.trooperImages[19].gameObject.SetActive(true);
                trooper.trooperImages[20].gameObject.SetActive(true);
                trooper.trooperImages[21].gameObject.SetActive(true);
                trooper.trooperImages[2].gameObject.SetActive(false);

                foreach (Sprite_Pack p in helmetPacks)
                {
                    if (p.packName == name && p.patternName == trooper.helmetPattern)
                    {
                        pack = p;
                    }
                }
                if (pack.packName == null)
                {
                    foreach (Sprite_Pack p in helmetPacks)
                    {
                        if (p.packName == name)
                        {
                            pack = p;
                            break;
                        }
                    }
                }

                PatternDropdown("Helmet", pack.packName, HelmetDropdownTrooper.gameObject.GetComponent<Sprite_Changer>().dropdownChild);

                if (pack.numberOfColours == 1)
                {
                    trooper.trooperImages[16].sprite = pack.containedSprites[1];
                    trooper.trooperImages[19].sprite = pack.containedSprites[2];
                    trooper.trooperImages[21].sprite = pack.containedSprites[0];
                    trooper.trooperImages[17].gameObject.SetActive(false);
                    trooper.trooperImages[18].gameObject.SetActive(false);
                    trooper.helmet = pack.packName;
                }
                if (pack.numberOfColours == 2)
                {
                    trooper.trooperImages[16].sprite = pack.containedSprites[1];
                    trooper.trooperImages[17].gameObject.SetActive(true);
                    trooper.trooperImages[17].sprite = pack.containedSprites[2];
                    trooper.trooperImages[19].sprite = pack.containedSprites[3];
                    trooper.trooperImages[21].sprite = pack.containedSprites[0];
                    trooper.trooperImages[18].gameObject.SetActive(false);
                    trooper.helmet = pack.packName;
                }
                if (pack.numberOfColours == 3)
                {
                    trooper.trooperImages[16].sprite = pack.containedSprites[1];
                    trooper.trooperImages[17].gameObject.SetActive(true);
                    trooper.trooperImages[17].sprite = pack.containedSprites[2];
                    trooper.trooperImages[18].gameObject.SetActive(true);
                    trooper.trooperImages[18].sprite = pack.containedSprites[3];
                    trooper.trooperImages[19].sprite = pack.containedSprites[4];
                    trooper.trooperImages[21].sprite = pack.containedSprites[0];
                    trooper.helmet = pack.packName;
                }
            }
            else
            {
                trooper.trooperImages[16].gameObject.SetActive(false);
                trooper.trooperImages[17].gameObject.SetActive(false);
                trooper.trooperImages[18].gameObject.SetActive(false);
                trooper.trooperImages[19].gameObject.SetActive(false);
                trooper.trooperImages[20].gameObject.SetActive(false);
                trooper.trooperImages[21].gameObject.SetActive(false);
                trooper.trooperImages[2].gameObject.SetActive(true);
            }
        }

        if (type == "Weapons")
        {
            if (name != "None")
            {
                Sprite_Pack pack = new Sprite_Pack();
                trooper.trooperImages[25].gameObject.SetActive(true);
                trooper.trooperImages[26].gameObject.SetActive(true);
                trooper.trooperImages[27].gameObject.SetActive(true);

                foreach (Sprite_Pack p in weaponPacks)
                {
                    if (p.packName == name)
                    {
                        pack = p;
                    }
                }
                if (pack.packName == null)
                {
                    foreach (Sprite_Pack p in weaponPacks)
                    {
                        if (p.packName == name)
                        {
                            pack = p;
                            break;
                        }
                    }
                }

                if (pack.numberOfColours == 1)
                {
                    trooper.trooperImages[25].sprite = pack.containedSprites[0];
                    trooper.trooperImages[26].sprite = pack.containedSprites[1];
                    trooper.trooperImages[27].gameObject.SetActive(false);
                    trooper.helmet = pack.packName;
                }
                if (pack.numberOfColours == 2)
                {
                    trooper.trooperImages[25].sprite = pack.containedSprites[0];
                    trooper.trooperImages[26].sprite = pack.containedSprites[1];
                    trooper.trooperImages[27].sprite = pack.containedSprites[2];
                    trooper.helmet = pack.packName;
                }
            }
            else
            {
                trooper.trooperImages[25].gameObject.SetActive(false);
                trooper.trooperImages[26].gameObject.SetActive(false);
                trooper.trooperImages[27].gameObject.SetActive(false);
            }
        }

        if (type == "Weapons 2")
        {
            if (name != "None")
            {
                Sprite_Pack pack = new Sprite_Pack();
                trooper.trooperImages[28].gameObject.SetActive(true);
                trooper.trooperImages[29].gameObject.SetActive(true);
                trooper.trooperImages[30].gameObject.SetActive(true);

                foreach (Sprite_Pack p in weaponPacks)
                {
                    if (p.packName == name)
                    {
                        pack = p;
                    }
                }
                if (pack.packName == null)
                {
                    foreach (Sprite_Pack p in weaponPacks)
                    {
                        if (p.packName == name)
                        {
                            pack = p;
                            break;
                        }
                    }
                }

                if (pack.numberOfColours == 1)
                {
                    trooper.trooperImages[28].sprite = pack.containedSprites[0];
                    trooper.trooperImages[29].sprite = pack.containedSprites[1];
                    trooper.trooperImages[30].gameObject.SetActive(false);
                    trooper.helmet = pack.packName;
                }
                if (pack.numberOfColours == 2)
                {
                    trooper.trooperImages[28].sprite = pack.containedSprites[0];
                    trooper.trooperImages[29].sprite = pack.containedSprites[1];
                    trooper.trooperImages[30].sprite = pack.containedSprites[2];
                    trooper.helmet = pack.packName;
                }
            }
            else
            {
                trooper.trooperImages[28].gameObject.SetActive(false);
                trooper.trooperImages[29].gameObject.SetActive(false);
                trooper.trooperImages[30].gameObject.SetActive(false);
            }
        }

        if (type == "Equipment")
        {
            if (name != "None")
            {
                Sprite_Pack pack = new Sprite_Pack();
                trooper.trooperImages[21].gameObject.SetActive(true);
                trooper.trooperImages[22].gameObject.SetActive(true);
                trooper.trooperImages[23].gameObject.SetActive(true);

                foreach (Sprite_Pack p in weaponPacks)
                {
                    if (p.packName == name)
                    {
                        pack = p;
                    }
                }
                if (pack.packName == null)
                {
                    foreach (Sprite_Pack p in weaponPacks)
                    {
                        if (p.packName == name)
                        {
                            pack = p;
                            break;
                        }
                    }
                }

                if (pack.numberOfColours == 1)
                {
                    trooper.trooperImages[21].sprite = pack.containedSprites[0];
                    trooper.trooperImages[22].sprite = pack.containedSprites[1];
                    trooper.trooperImages[23].gameObject.SetActive(false);
                    trooper.helmet = pack.packName;
                }
                if (pack.numberOfColours == 2)
                {
                    trooper.trooperImages[21].sprite = pack.containedSprites[0];
                    trooper.trooperImages[22].sprite = pack.containedSprites[1];
                    trooper.trooperImages[23].sprite = pack.containedSprites[2];
                    trooper.helmet = pack.packName;
                }
            }
            else
            {
                trooper.trooperImages[21].gameObject.SetActive(false);
                trooper.trooperImages[22].gameObject.SetActive(false);
                trooper.trooperImages[23].gameObject.SetActive(false);
            }
        }
    }
}
