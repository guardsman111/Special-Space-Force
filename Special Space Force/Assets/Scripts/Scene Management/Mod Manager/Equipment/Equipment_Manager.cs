using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Equipment_Manager : MonoBehaviour
{
    /// <summary>
    /// This code manages equipment for the slot manager and troopers - performs the change equipment and pattern functions
    /// Also stores the equipments available loaded from files
    /// </summary>
    
    public FileFinder finder;
    public Core_Equipment coreEquipment;
    
    private List<Sprite_Pack> armourPacks;
    private List<Sprite_Pack> fatiguesPacks;
    private List<Sprite_Pack> helmetPacks;
    private List<Sprite_Pack> equipmentPacks;
    private List<Sprite_Pack> weaponPacks;

    //Remember that trims and equipments are the wrong way round in code compared to on screen
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

    public Texture2D templateTexture;
    public Toggle selectFromTemplate;

    //Sets up sprite packs for all the equipments in files (mod and default) also sets up dropdowns to be usable
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

        //For each file in resources with specific naming scheme, takes the file and 
        //turns it into the relevant class then stores it as a Sprite_Pack
        foreach (string s in armourFileLocations)
        {
            Armour_Class temp = Serializer.Deserialize<Armour_Class>(s);
            tempSP = new Sprite_Pack();
            tempSP.packName = temp.armourName;
            tempSP.patternName = temp.patternName;
            tempSP.containedSprites = new List<Sprite>();
            try
            {
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.ArmourOutlinePath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
                tempSP.numberOfColours = 1;
            }
            catch { }
            try
            {
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.ArmourPrimaryPath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
            }
            catch { }
            try
            {
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.ArmourSecondaryPath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
                tempSP.numberOfColours = 2;
            }
            catch { }
            try
            {
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.ArmourTertiaryPath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
                tempSP.numberOfColours = 3;
            }
            catch { }
            try
            {
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.ArmourEquipmentPath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
            }
            catch { }
            try
            {
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.ArmourSpecialPath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
            }
            catch { }

            tempSP.packType = "Armour";
            tempSP.CostPerItem = temp.CostPerItem;
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
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.FatiguesOutlinePath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
                tempSP.numberOfColours = 1;
            }
            catch { }
            try
            {
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.FatiguesPrimaryPath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
            }
            catch { }
            try
            {
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.FatiguesSecondaryPath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
                tempSP.numberOfColours = 2;
            }
            catch { }
            try
            {
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.FatiguesTertiaryPath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
                tempSP.numberOfColours = 3;
            }
            catch { }
            try
            {
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.FatiguesEquipmentPath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
            }
            catch { }
            try
            {
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.FatiguesSpecialPath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
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
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.HelmetOutlinePath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
            }
            catch { }
            try
            {
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.HelmetPrimaryPath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
                tempSP.numberOfColours = 1;
            }
            catch { }
            try
            {
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.HelmetSecondaryPath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
                tempSP.numberOfColours = 2;
            }
            catch { }
            try
            {
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.HelmetTertiaryPath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
                tempSP.numberOfColours = 3;
            }
            catch { }
            try
            {
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.HelmetEquipmentPath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
            }
            catch { }
            try
            {
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.HelmetVisorPath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
            }
            catch { }
            tempSP.packType = "Helmet";
            tempSP.CostPerItem = temp.CostPerItem;
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
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.EquipmentOutlinePath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
            }
            catch { }
            try
            {
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.EquipmentPrimaryPath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
                tempSP.numberOfColours = 1;
            }
            catch { }
            try
            {
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.EquipmentSecondaryPath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
                tempSP.numberOfColours = 2;
            }
            catch { }
            tempSP.packType = "Equipment";
            tempSP.CostPerItem = temp.CostPerItem;
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
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.EquipmentOutlinePath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
            }
            catch { }
            try
            {
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.EquipmentPrimaryPath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
                tempSP.numberOfColours = 1;
            }
            catch { }
            try
            {
                Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
                byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.EquipmentSecondaryPath);
                newTex.LoadImage(bytes);
                newTex.filterMode = FilterMode.Point;
                Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
                tempSP.containedSprites.Add(newSprite);
                tempSP.numberOfColours = 2;
            }
            catch { }
            
            tempSP.packType = temp.EquipmentType;
            tempSP.CostPerItem = temp.CostPerItem;
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
        //Serializer.Serialize(, finder.defaultPath + "/Equipment/Armour/Mk1/Mk1ArmourPrimary1.xml");
    }

    //Gets players default colours
    //Remember that trim and equipment are the wrong way round in code compared to on screen
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

    //Get players chosen starting equipment
    public List<string> GetDefault(string type)
    {
        List<string> strings = new List<string>();

        if (type == "Equipment")
        {
            strings.Add(HelmetDropdownCustom.options[HelmetDropdownCustom.value].text);
            strings.Add(ArmourDropdownCustom.options[ArmourDropdownCustom.value].text);
            strings.Add(FatiguesDropdownCustom.options[FatiguesDropdownCustom.value].text);
            strings.Add(Weapon1DropdownCustom.options[Weapon1DropdownCustom.value].text);
            strings.Add(Weapon2DropdownCustom.options[Weapon2DropdownCustom.value].text);
            strings.Add(EquipmentDropdownCustom.options[EquipmentDropdownCustom.value].text);
        }
        else if (type == "Patterns")
        {
            strings.Add(HelmetPatternCustom.options[HelmetPatternCustom.value].text);
            strings.Add(ArmourPatternCustom.options[ArmourPatternCustom.value].text);
            strings.Add(FatiguesPatternCustom.options[FatiguesPatternCustom.value].text);
        }

        return strings;
    }

    //Sets up the passed dropdown to display all the equipment of its chosen type
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
            dropdown.value = 2;
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
            dropdown.value = 1;
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
            dropdown.value = 1;
        }
    }

    //Sets up pattern dropdowns for the currently selected equipment
    //Note that only the current equipment pattern can be changed
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

    //Changes a troopers equipment according to the dropdown that was changed
    public void ChangeEquipment(Trooper_Script trooper, Dropdown dropdown)
    {
        if (dropdown.name == "Armour Dropdown")
        {
            if (dropdown.options[dropdown.value].text != "None")
            {
                Sprite_Pack pack = new Sprite_Pack();
                trooper.trooperImages[10].gameObject.SetActive(true);
                trooper.trooperImages[13].gameObject.SetActive(true);
                trooper.trooperImages[14].gameObject.SetActive(true);
                trooper.trooperImages[16].gameObject.SetActive(true);

                foreach (Sprite_Pack p in armourPacks)
                {
                    if (p.packName == dropdown.options[dropdown.value].text && p.patternName == trooper.armourPattern)
                    {
                        pack = p;
                        break;
                    }
                }
                if (pack.packName == null)
                {
                    foreach (Sprite_Pack p in armourPacks)
                    {
                        if (p.packName == dropdown.options[dropdown.value].text)
                        {
                            pack = p;
                            ArmourPatternCustom.value = 0;
                            break;
                        }
                    }
                }

                PatternDropdown("Armour", pack.packName, dropdown.gameObject.GetComponent<Sprite_Changer>().dropdownChild);

                if (pack.numberOfColours == 1)
                {
                    trooper.trooperImages[10].sprite = pack.containedSprites[1];
                    trooper.trooperImages[13].sprite = pack.containedSprites[3];
                    trooper.trooperImages[14].sprite = pack.containedSprites[2];
                    trooper.trooperImages[16].sprite = pack.containedSprites[0];
                    trooper.trooperImages[11].gameObject.SetActive(false);
                    trooper.trooperImages[12].gameObject.SetActive(false);
                    trooper.armour = pack.packName;
                }
                if (pack.numberOfColours == 2)
                {
                    trooper.trooperImages[10].sprite = pack.containedSprites[1];
                    trooper.trooperImages[11].gameObject.SetActive(true);
                    trooper.trooperImages[11].sprite = pack.containedSprites[2];
                    trooper.trooperImages[13].sprite = pack.containedSprites[4];
                    trooper.trooperImages[14].sprite = pack.containedSprites[3];
                    trooper.trooperImages[16].sprite = pack.containedSprites[0];
                    trooper.trooperImages[12].gameObject.SetActive(false);
                    trooper.armour = pack.packName;
                }
                if (pack.numberOfColours == 3)
                {
                    trooper.trooperImages[10].sprite = pack.containedSprites[1];
                    trooper.trooperImages[11].gameObject.SetActive(true);
                    trooper.trooperImages[11].sprite = pack.containedSprites[2];
                    trooper.trooperImages[12].gameObject.SetActive(true);
                    trooper.trooperImages[12].sprite = pack.containedSprites[3];
                    trooper.trooperImages[13].sprite = pack.containedSprites[5];
                    trooper.trooperImages[14].sprite = pack.containedSprites[4];
                    trooper.trooperImages[16].sprite = pack.containedSprites[0];
                    trooper.armour = pack.packName;
                }
            } 
            else
            {
                trooper.trooperImages[10].gameObject.SetActive(false);
                trooper.trooperImages[11].gameObject.SetActive(false);
                trooper.trooperImages[12].gameObject.SetActive(false);
                trooper.trooperImages[13].gameObject.SetActive(false);
                trooper.trooperImages[14].gameObject.SetActive(false);
                trooper.trooperImages[15].gameObject.SetActive(false);
                trooper.trooperImages[16].gameObject.SetActive(false);
                trooper.armour = "None";
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
                    break;
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
                trooper.trooperImages[4].sprite = pack.containedSprites[1];
                trooper.trooperImages[7].sprite = pack.containedSprites[3];
                trooper.trooperImages[8].sprite = pack.containedSprites[2];
                trooper.trooperImages[9].sprite = pack.containedSprites[0];
                trooper.trooperImages[5].gameObject.SetActive(false);
                trooper.trooperImages[6].gameObject.SetActive(false);
                trooper.fatigues = pack.packName;
            }
            if (pack.numberOfColours == 2)
            {
                trooper.trooperImages[4].sprite = pack.containedSprites[1];
                trooper.trooperImages[5].gameObject.SetActive(true);
                trooper.trooperImages[5].sprite = pack.containedSprites[2];
                trooper.trooperImages[7].sprite = pack.containedSprites[4];
                trooper.trooperImages[8].sprite = pack.containedSprites[3];
                trooper.trooperImages[9].sprite = pack.containedSprites[0];
                trooper.trooperImages[6].gameObject.SetActive(false);
                trooper.fatigues = pack.packName;
            }
            if (pack.numberOfColours == 3)
            {
                trooper.trooperImages[4].sprite = pack.containedSprites[1];
                trooper.trooperImages[5].gameObject.SetActive(true);
                trooper.trooperImages[5].sprite = pack.containedSprites[2];
                trooper.trooperImages[6].gameObject.SetActive(true);
                trooper.trooperImages[6].sprite = pack.containedSprites[3];
                trooper.trooperImages[7].sprite = pack.containedSprites[5];
                trooper.trooperImages[8].sprite = pack.containedSprites[4];
                trooper.trooperImages[9].sprite = pack.containedSprites[0];
                trooper.fatigues = pack.packName;
            }

        }

        if (dropdown.name == "Helmet Dropdown")
        {
            if (dropdown.options[dropdown.value].text != "None")
            {
                Sprite_Pack pack = new Sprite_Pack();
                trooper.trooperImages[17].gameObject.SetActive(true);
                trooper.trooperImages[20].gameObject.SetActive(true);
                trooper.trooperImages[21].gameObject.SetActive(true);
                trooper.trooperImages[22].gameObject.SetActive(true);
                trooper.trooperImages[2].gameObject.SetActive(false);
                trooper.trooperImages[3].gameObject.SetActive(false);

                foreach (Sprite_Pack p in helmetPacks)
                {
                    if (p.packName == dropdown.options[dropdown.value].text && p.patternName == trooper.helmetPattern)
                    {
                        pack = p;
                        break;
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
                    trooper.trooperImages[17].sprite = pack.containedSprites[1];
                    trooper.trooperImages[20].sprite = pack.containedSprites[2];
                    trooper.trooperImages[21].sprite = pack.containedSprites[3];
                    trooper.trooperImages[22].sprite = pack.containedSprites[0];
                    trooper.trooperImages[18].gameObject.SetActive(false);
                    trooper.trooperImages[19].gameObject.SetActive(false);
                    trooper.helmet = pack.packName;
                }
                if (pack.numberOfColours == 2)
                {
                    trooper.trooperImages[17].sprite = pack.containedSprites[1];
                    trooper.trooperImages[18].gameObject.SetActive(true);
                    trooper.trooperImages[18].sprite = pack.containedSprites[2];
                    trooper.trooperImages[20].sprite = pack.containedSprites[3];
                    trooper.trooperImages[21].sprite = pack.containedSprites[4];
                    trooper.trooperImages[22].sprite = pack.containedSprites[0];
                    trooper.trooperImages[19].gameObject.SetActive(false);
                    trooper.helmet = pack.packName;
                }
                if (pack.numberOfColours == 3)
                {
                    trooper.trooperImages[17].sprite = pack.containedSprites[1];
                    trooper.trooperImages[18].gameObject.SetActive(true);
                    trooper.trooperImages[18].sprite = pack.containedSprites[2];
                    trooper.trooperImages[19].gameObject.SetActive(true);
                    trooper.trooperImages[19].sprite = pack.containedSprites[3];
                    trooper.trooperImages[20].sprite = pack.containedSprites[4];
                    trooper.trooperImages[21].sprite = pack.containedSprites[5];
                    trooper.trooperImages[22].sprite = pack.containedSprites[0];
                    trooper.helmet = pack.packName;
                }
            } 
            else
            {
                trooper.trooperImages[17].gameObject.SetActive(false);
                trooper.trooperImages[18].gameObject.SetActive(false);
                trooper.trooperImages[19].gameObject.SetActive(false);
                trooper.trooperImages[20].gameObject.SetActive(false);
                trooper.trooperImages[21].gameObject.SetActive(false);
                trooper.trooperImages[22].gameObject.SetActive(false);
                trooper.trooperImages[2].gameObject.SetActive(true);
                trooper.trooperImages[3].gameObject.SetActive(true);
                trooper.helmet = "None";
            }
        }

        if (dropdown.name == "Weapons Dropdown")
        {
            if (dropdown.options[dropdown.value].text != "None")
            {
                Sprite_Pack pack = new Sprite_Pack();
                trooper.trooperImages[26].gameObject.SetActive(true);
                trooper.trooperImages[27].gameObject.SetActive(true);
                trooper.trooperImages[28].gameObject.SetActive(true);

                foreach (Sprite_Pack p in weaponPacks)
                {
                    if (p.packName == dropdown.options[dropdown.value].text)
                    {
                        pack = p;
                        break;
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
                    trooper.trooperImages[26].sprite = pack.containedSprites[0];
                    trooper.trooperImages[27].sprite = pack.containedSprites[1];
                    trooper.trooperImages[28].gameObject.SetActive(false);
                    trooper.primaryWeapon = pack.packName;
                }
                if (pack.numberOfColours == 2)
                {
                    trooper.trooperImages[26].sprite = pack.containedSprites[0];
                    trooper.trooperImages[27].sprite = pack.containedSprites[1];
                    trooper.trooperImages[28].sprite = pack.containedSprites[2];
                    trooper.primaryWeapon = pack.packName;
                }
            }
            else
            {
                trooper.trooperImages[26].gameObject.SetActive(false);
                trooper.trooperImages[27].gameObject.SetActive(false);
                trooper.trooperImages[28].gameObject.SetActive(false);
                trooper.primaryWeapon = "None";
            }
        }

        if (dropdown.name == "Weapons 2 Dropdown")
        {
            if (dropdown.options[dropdown.value].text != "None")
            {
                Sprite_Pack pack = new Sprite_Pack();
                trooper.trooperImages[29].gameObject.SetActive(true);
                trooper.trooperImages[30].gameObject.SetActive(true);
                trooper.trooperImages[31].gameObject.SetActive(true);

                foreach (Sprite_Pack p in weaponPacks)
                {
                    if (p.packName == dropdown.options[dropdown.value].text)
                    {
                        pack = p;
                        break;
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
                    trooper.trooperImages[29].sprite = pack.containedSprites[0];
                    trooper.trooperImages[30].sprite = pack.containedSprites[1];
                    trooper.trooperImages[31].gameObject.SetActive(false);
                    trooper.secondaryWeapon = pack.packName;
                }
                if (pack.numberOfColours == 2)
                {
                    trooper.trooperImages[29].sprite = pack.containedSprites[0];
                    trooper.trooperImages[30].sprite = pack.containedSprites[1];
                    trooper.trooperImages[31].sprite = pack.containedSprites[2];
                    trooper.secondaryWeapon = pack.packName;
                }
            }
            else
            {
                trooper.trooperImages[29].gameObject.SetActive(false);
                trooper.trooperImages[30].gameObject.SetActive(false);
                trooper.trooperImages[31].gameObject.SetActive(false);
                trooper.secondaryWeapon = "None";
            }
        }

        if (dropdown.name == "Equipment Dropdown")
        {
            if (dropdown.options[dropdown.value].text != "None")
            {
                Sprite_Pack pack = new Sprite_Pack();
                trooper.trooperImages[23].gameObject.SetActive(true);
                trooper.trooperImages[24].gameObject.SetActive(true);
                trooper.trooperImages[25].gameObject.SetActive(true);

                foreach (Sprite_Pack p in equipmentPacks)
                {
                    if (p.packName == dropdown.options[dropdown.value].text)
                    {
                        pack = p;
                        break;
                    }
                }
                if (pack.packName == null)
                {
                    foreach (Sprite_Pack p in equipmentPacks)
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
                    trooper.trooperImages[23].sprite = pack.containedSprites[0];
                    trooper.trooperImages[24].sprite = pack.containedSprites[1];
                    trooper.trooperImages[25].gameObject.SetActive(false);
                    trooper.equipment = pack.packName;
                }
                if (pack.numberOfColours == 2)
                {
                    trooper.trooperImages[23].sprite = pack.containedSprites[0];
                    trooper.trooperImages[24].sprite = pack.containedSprites[1];
                    trooper.trooperImages[25].sprite = pack.containedSprites[2];
                    trooper.equipment = pack.packName;
                }
            }
            else
            {
                trooper.trooperImages[23].gameObject.SetActive(false);
                trooper.trooperImages[24].gameObject.SetActive(false);
                trooper.trooperImages[25].gameObject.SetActive(false);
                trooper.equipment = "None";
            }
        }
    }

    //Changes trooper equipment pattern according to which dropdown was selected
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
                    break;
                }
            }

            if (pack.numberOfColours == 1)
            {
                trooper.trooperImages[10].sprite = pack.containedSprites[1];
                trooper.trooperImages[13].sprite = pack.containedSprites[3];
                trooper.trooperImages[14].sprite = pack.containedSprites[2];
                trooper.trooperImages[16].sprite = pack.containedSprites[0];
                trooper.trooperImages[11].gameObject.SetActive(false);
                trooper.trooperImages[12].gameObject.SetActive(false);
                trooper.armourPattern = pack.patternName;
            }
            if (pack.numberOfColours == 2)
            {
                trooper.trooperImages[10].sprite = pack.containedSprites[1];
                trooper.trooperImages[11].gameObject.SetActive(true);
                trooper.trooperImages[11].sprite = pack.containedSprites[2];
                trooper.trooperImages[13].sprite = pack.containedSprites[4];
                trooper.trooperImages[14].sprite = pack.containedSprites[3];
                trooper.trooperImages[16].sprite = pack.containedSprites[0];
                trooper.trooperImages[12].gameObject.SetActive(false);
                trooper.armourPattern = pack.patternName;
            }
            if (pack.numberOfColours == 3)
            {
                trooper.trooperImages[10].sprite = pack.containedSprites[1];
                trooper.trooperImages[11].gameObject.SetActive(true);
                trooper.trooperImages[11].sprite = pack.containedSprites[2];
                trooper.trooperImages[12].gameObject.SetActive(true);
                trooper.trooperImages[12].sprite = pack.containedSprites[3];
                trooper.trooperImages[13].sprite = pack.containedSprites[5];
                trooper.trooperImages[14].sprite = pack.containedSprites[4];
                trooper.trooperImages[16].sprite = pack.containedSprites[0];
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
                    break;
                }
            }

            if (pack.numberOfColours == 1)
            {
                trooper.trooperImages[4].sprite = pack.containedSprites[1];
                trooper.trooperImages[7].sprite = pack.containedSprites[3];
                trooper.trooperImages[8].sprite = pack.containedSprites[2];
                trooper.trooperImages[9].sprite = pack.containedSprites[0];
                trooper.trooperImages[5].gameObject.SetActive(false);
                trooper.trooperImages[6].gameObject.SetActive(false);
                trooper.fatiguesPattern = pack.patternName;
            }
            if (pack.numberOfColours == 2)
            {
                trooper.trooperImages[4].sprite = pack.containedSprites[1];
                trooper.trooperImages[5].gameObject.SetActive(true);
                trooper.trooperImages[5].sprite = pack.containedSprites[2];
                trooper.trooperImages[7].sprite = pack.containedSprites[4];
                trooper.trooperImages[8].sprite = pack.containedSprites[3];
                trooper.trooperImages[9].sprite = pack.containedSprites[0];
                trooper.trooperImages[6].gameObject.SetActive(false);
                trooper.fatiguesPattern = pack.patternName;
            }
            if (pack.numberOfColours == 3)
            {
                trooper.trooperImages[4].sprite = pack.containedSprites[1];
                trooper.trooperImages[5].gameObject.SetActive(true);
                trooper.trooperImages[5].sprite = pack.containedSprites[2];
                trooper.trooperImages[6].gameObject.SetActive(true);
                trooper.trooperImages[6].sprite = pack.containedSprites[3];
                trooper.trooperImages[7].sprite = pack.containedSprites[5];
                trooper.trooperImages[8].sprite = pack.containedSprites[4];
                trooper.trooperImages[9].sprite = pack.containedSprites[0];
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
                trooper.trooperImages[17].sprite = pack.containedSprites[1];
                trooper.trooperImages[20].sprite = pack.containedSprites[2];
                trooper.trooperImages[21].sprite = pack.containedSprites[3];
                trooper.trooperImages[22].sprite = pack.containedSprites[0];
                trooper.trooperImages[18].gameObject.SetActive(false);
                trooper.trooperImages[19].gameObject.SetActive(false);
                trooper.helmetPattern = pack.patternName;
            }
            if (pack.numberOfColours == 2)
            {
                trooper.trooperImages[17].sprite = pack.containedSprites[1];
                trooper.trooperImages[18].gameObject.SetActive(true);
                trooper.trooperImages[18].sprite = pack.containedSprites[2];
                trooper.trooperImages[20].sprite = pack.containedSprites[3];
                trooper.trooperImages[21].sprite = pack.containedSprites[4];
                trooper.trooperImages[22].sprite = pack.containedSprites[0];
                trooper.trooperImages[19].gameObject.SetActive(false);
                trooper.helmetPattern = pack.patternName;
            }
            if (pack.numberOfColours == 3)
            {
                trooper.trooperImages[17].sprite = pack.containedSprites[1];
                trooper.trooperImages[18].gameObject.SetActive(true);
                trooper.trooperImages[18].sprite = pack.containedSprites[2];
                trooper.trooperImages[19].gameObject.SetActive(true);
                trooper.trooperImages[19].sprite = pack.containedSprites[3];
                trooper.trooperImages[20].sprite = pack.containedSprites[4];
                trooper.trooperImages[21].sprite = pack.containedSprites[5];
                trooper.trooperImages[22].sprite = pack.containedSprites[0];
                trooper.helmetPattern = pack.patternName;
            }
        }
    }

    //Loads trooper equipment from the save file
    public void LoadEquipment(Trooper_Script trooper, string type, string name)
    {
        if (type == "Armour")
        {
            if (name != "None")
            {
                Sprite_Pack pack = new Sprite_Pack();
                trooper.trooperImages[10].gameObject.SetActive(true);
                trooper.trooperImages[13].gameObject.SetActive(true);
                trooper.trooperImages[14].gameObject.SetActive(true);
                trooper.trooperImages[16].gameObject.SetActive(true);

                foreach (Sprite_Pack p in armourPacks)
                {
                    if (p.packName == name && p.patternName == trooper.armourPattern)
                    {
                        pack = p;
                        break;
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
                    trooper.trooperImages[10].sprite = pack.containedSprites[1];
                    trooper.trooperImages[13].sprite = pack.containedSprites[3];
                    trooper.trooperImages[14].sprite = pack.containedSprites[2];
                    trooper.trooperImages[16].sprite = pack.containedSprites[0];
                    trooper.trooperImages[11].gameObject.SetActive(false);
                    trooper.trooperImages[12].gameObject.SetActive(false);
                    trooper.armour = pack.packName;
                }
                if (pack.numberOfColours == 2)
                {
                    trooper.trooperImages[10].sprite = pack.containedSprites[1];
                    trooper.trooperImages[11].gameObject.SetActive(true);
                    trooper.trooperImages[11].sprite = pack.containedSprites[2];
                    trooper.trooperImages[13].sprite = pack.containedSprites[4];
                    trooper.trooperImages[14].sprite = pack.containedSprites[3];
                    trooper.trooperImages[16].sprite = pack.containedSprites[0];
                    trooper.trooperImages[12].gameObject.SetActive(false);
                    trooper.armour = pack.packName;
                }
                if (pack.numberOfColours == 3)
                {
                    trooper.trooperImages[10].sprite = pack.containedSprites[1];
                    trooper.trooperImages[11].gameObject.SetActive(true);
                    trooper.trooperImages[11].sprite = pack.containedSprites[2];
                    trooper.trooperImages[12].gameObject.SetActive(true);
                    trooper.trooperImages[12].sprite = pack.containedSprites[3];
                    trooper.trooperImages[13].sprite = pack.containedSprites[5];
                    trooper.trooperImages[14].sprite = pack.containedSprites[4];
                    trooper.trooperImages[16].sprite = pack.containedSprites[0];
                    trooper.armour = pack.packName;
                }
            }
            else
            {
                trooper.trooperImages[10].gameObject.SetActive(false);
                trooper.trooperImages[11].gameObject.SetActive(false);
                trooper.trooperImages[12].gameObject.SetActive(false);
                trooper.trooperImages[13].gameObject.SetActive(false);
                trooper.trooperImages[14].gameObject.SetActive(false);
                trooper.trooperImages[15].gameObject.SetActive(false);
                trooper.trooperImages[16].gameObject.SetActive(false);
                trooper.armour = "None";
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
                    break;
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
                trooper.trooperImages[4].sprite = pack.containedSprites[1];
                trooper.trooperImages[7].sprite = pack.containedSprites[3];
                trooper.trooperImages[8].sprite = pack.containedSprites[2];
                trooper.trooperImages[9].sprite = pack.containedSprites[0];
                trooper.trooperImages[5].gameObject.SetActive(false);
                trooper.trooperImages[6].gameObject.SetActive(false);
                trooper.fatigues = pack.packName;
            }
            if (pack.numberOfColours == 2)
            {
                trooper.trooperImages[4].sprite = pack.containedSprites[1];
                trooper.trooperImages[5].gameObject.SetActive(true);
                trooper.trooperImages[5].sprite = pack.containedSprites[2];
                trooper.trooperImages[7].sprite = pack.containedSprites[4];
                trooper.trooperImages[8].sprite = pack.containedSprites[3];
                trooper.trooperImages[9].sprite = pack.containedSprites[0];
                trooper.trooperImages[6].gameObject.SetActive(false);
                trooper.fatigues = pack.packName;
            }
            if (pack.numberOfColours == 3)
            {
                trooper.trooperImages[4].sprite = pack.containedSprites[1];
                trooper.trooperImages[5].gameObject.SetActive(true);
                trooper.trooperImages[5].sprite = pack.containedSprites[2];
                trooper.trooperImages[6].gameObject.SetActive(true);
                trooper.trooperImages[6].sprite = pack.containedSprites[3];
                trooper.trooperImages[7].sprite = pack.containedSprites[5];
                trooper.trooperImages[8].sprite = pack.containedSprites[4];
                trooper.trooperImages[9].sprite = pack.containedSprites[0];
                trooper.fatigues = pack.packName;
            }
        }

        if (type == "Helmet")
        {
            if (name != "None")
            {
                Sprite_Pack pack = new Sprite_Pack();
                trooper.trooperImages[17].gameObject.SetActive(true);
                trooper.trooperImages[20].gameObject.SetActive(true);
                trooper.trooperImages[21].gameObject.SetActive(true);
                trooper.trooperImages[22].gameObject.SetActive(true);
                trooper.trooperImages[2].gameObject.SetActive(false);
                trooper.trooperImages[3].gameObject.SetActive(false);

                foreach (Sprite_Pack p in helmetPacks)
                {
                    if (p.packName == name && p.patternName == trooper.helmetPattern)
                    {
                        pack = p;
                        break;
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
                    trooper.trooperImages[17].sprite = pack.containedSprites[1];
                    trooper.trooperImages[20].sprite = pack.containedSprites[2];
                    trooper.trooperImages[21].sprite = pack.containedSprites[3];
                    trooper.trooperImages[22].sprite = pack.containedSprites[0];
                    trooper.trooperImages[18].gameObject.SetActive(false);
                    trooper.trooperImages[19].gameObject.SetActive(false);
                    trooper.helmet = pack.packName;
                }
                if (pack.numberOfColours == 2)
                {
                    trooper.trooperImages[17].sprite = pack.containedSprites[1];
                    trooper.trooperImages[18].gameObject.SetActive(true);
                    trooper.trooperImages[18].sprite = pack.containedSprites[2];
                    trooper.trooperImages[20].sprite = pack.containedSprites[3];
                    trooper.trooperImages[21].sprite = pack.containedSprites[4];
                    trooper.trooperImages[22].sprite = pack.containedSprites[0];
                    trooper.trooperImages[19].gameObject.SetActive(false);
                    trooper.helmet = pack.packName;
                }
                if (pack.numberOfColours == 3)
                {
                    trooper.trooperImages[17].sprite = pack.containedSprites[1];
                    trooper.trooperImages[18].gameObject.SetActive(true);
                    trooper.trooperImages[18].sprite = pack.containedSprites[2];
                    trooper.trooperImages[19].gameObject.SetActive(true);
                    trooper.trooperImages[19].sprite = pack.containedSprites[3];
                    trooper.trooperImages[20].sprite = pack.containedSprites[4];
                    trooper.trooperImages[21].sprite = pack.containedSprites[5];
                    trooper.trooperImages[22].sprite = pack.containedSprites[0];
                    trooper.helmet = pack.packName;
                }
            }
            else
            {
                trooper.trooperImages[17].gameObject.SetActive(false);
                trooper.trooperImages[18].gameObject.SetActive(false);
                trooper.trooperImages[19].gameObject.SetActive(false);
                trooper.trooperImages[20].gameObject.SetActive(false);
                trooper.trooperImages[21].gameObject.SetActive(false);
                trooper.trooperImages[22].gameObject.SetActive(false);
                trooper.trooperImages[2].gameObject.SetActive(true);
                trooper.trooperImages[3].gameObject.SetActive(true);
                trooper.helmet = "None";
            }
        }

        if (type == "Weapons")
        {
            if (name != "None")
            {
                Sprite_Pack pack = new Sprite_Pack();
                trooper.trooperImages[26].gameObject.SetActive(true);
                trooper.trooperImages[27].gameObject.SetActive(true);
                trooper.trooperImages[28].gameObject.SetActive(true);

                foreach (Sprite_Pack p in weaponPacks)
                {
                    if (p.packName == name)
                    {
                        pack = p;
                        break;
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
                    trooper.trooperImages[26].sprite = pack.containedSprites[0];
                    trooper.trooperImages[27].sprite = pack.containedSprites[1];
                    trooper.trooperImages[28].gameObject.SetActive(false);
                    trooper.primaryWeapon = pack.packName;
                }
                if (pack.numberOfColours == 2)
                {
                    trooper.trooperImages[26].sprite = pack.containedSprites[0];
                    trooper.trooperImages[27].sprite = pack.containedSprites[1];
                    trooper.trooperImages[28].sprite = pack.containedSprites[2];
                    trooper.primaryWeapon = pack.packName;
                }
            }
            else
            {
                trooper.trooperImages[26].gameObject.SetActive(false);
                trooper.trooperImages[27].gameObject.SetActive(false);
                trooper.trooperImages[28].gameObject.SetActive(false);
                trooper.primaryWeapon = "None";
            }
        }

        if (type == "Weapons 2")
        {
            if (name != "None")
            {
                Sprite_Pack pack = new Sprite_Pack();
                trooper.trooperImages[29].gameObject.SetActive(true);
                trooper.trooperImages[30].gameObject.SetActive(true);
                trooper.trooperImages[31].gameObject.SetActive(true);

                foreach (Sprite_Pack p in weaponPacks)
                {
                    if (p.packName == name)
                    {
                        pack = p;
                        break;
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
                    trooper.trooperImages[29].sprite = pack.containedSprites[0];
                    trooper.trooperImages[30].sprite = pack.containedSprites[1];
                    trooper.trooperImages[31].gameObject.SetActive(false);
                    trooper.secondaryWeapon = pack.packName;
                }
                if (pack.numberOfColours == 2)
                {
                    trooper.trooperImages[29].sprite = pack.containedSprites[0];
                    trooper.trooperImages[30].sprite = pack.containedSprites[1];
                    trooper.trooperImages[31].sprite = pack.containedSprites[2];
                    trooper.secondaryWeapon = pack.packName;
                }
            }
            else
            {
                trooper.trooperImages[29].gameObject.SetActive(false);
                trooper.trooperImages[30].gameObject.SetActive(false);
                trooper.trooperImages[31].gameObject.SetActive(false);
                trooper.secondaryWeapon = "None";
            }
        }

        if (type == "Equipment")
        {
            if (name != "None")
            {
                Sprite_Pack pack = new Sprite_Pack();
                trooper.trooperImages[23].gameObject.SetActive(true);
                trooper.trooperImages[24].gameObject.SetActive(true);
                trooper.trooperImages[25].gameObject.SetActive(true);

                foreach (Sprite_Pack p in weaponPacks)
                {
                    if (p.packName == name)
                    {
                        pack = p;
                        break;
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
                    trooper.trooperImages[23].sprite = pack.containedSprites[0];
                    trooper.trooperImages[24].sprite = pack.containedSprites[1];
                    trooper.trooperImages[25].gameObject.SetActive(false);
                    trooper.equipment = pack.packName;
                }
                if (pack.numberOfColours == 2)
                {
                    trooper.trooperImages[23].sprite = pack.containedSprites[0];
                    trooper.trooperImages[24].sprite = pack.containedSprites[1];
                    trooper.trooperImages[25].sprite = pack.containedSprites[2];
                    trooper.equipment = pack.packName;
                }
            }
            else
            {
                trooper.trooperImages[23].gameObject.SetActive(false);
                trooper.trooperImages[24].gameObject.SetActive(false);
                trooper.trooperImages[25].gameObject.SetActive(false);
                trooper.equipment = "None";
            }
        }
    }

    //Sets dropdowns on trooper selection
    public void SetDropdowns(Trooper_Script trooper)
    {
        if (ArmourDropdownTrooper.options[ArmourDropdownTrooper.value].text != trooper.armour)
        {
            if (trooper.armour == "None")
            {
                ArmourDropdownTrooper.value = 0;
            }
            else
            {
                for (int i = 0; i < ArmourDropdownTrooper.options.Count; i++)
                {
                    if (ArmourDropdownTrooper.options[i].text == trooper.armour)
                    {
                        ArmourDropdownTrooper.value = i;
                    }
                }
            }
        }

        if (HelmetDropdownTrooper.options[HelmetDropdownTrooper.value].text != trooper.helmet)
        {
            if (trooper.helmet == "None")
            {
                HelmetDropdownTrooper.value = 0;
            }
            else
            {
                for (int i = 0; i < HelmetDropdownTrooper.options.Count; i++)
                {
                    if (HelmetDropdownTrooper.options[i].text == trooper.helmet)
                    {
                        HelmetDropdownTrooper.value = i;
                    }
                }
            }
        }

        if (FatiguesDropdownTrooper.options[FatiguesDropdownTrooper.value].text != trooper.fatigues)
        {
            if (trooper.fatigues == "None")
            {
                FatiguesDropdownTrooper.value = 0;
            }
            else
            {
                for (int i = 0; i < FatiguesDropdownTrooper.options.Count; i++)
                {
                    if (FatiguesDropdownTrooper.options[i].text == trooper.fatigues)
                    {
                        FatiguesDropdownTrooper.value = i;
                    }
                }
            }
        }

        if (Weapon1DropdownTrooper.options[Weapon1DropdownTrooper.value].text != trooper.primaryWeapon)
        {
            {
                if (trooper.primaryWeapon == "None")
                {
                    Weapon1DropdownTrooper.value = 0;
                }
                else
                {
                    for (int i = 0; i < Weapon1DropdownTrooper.options.Count; i++)
                    {
                        if (Weapon1DropdownTrooper.options[i].text == trooper.primaryWeapon)
                        {
                            Weapon1DropdownTrooper.value = i;
                        }
                    }
                }
            }
        }

        if (Weapon2DropdownTrooper.options[Weapon2DropdownTrooper.value].text != trooper.secondaryWeapon)
        {
            {
                if (trooper.secondaryWeapon == "None")
                {
                    Weapon2DropdownTrooper.value = 0;
                }
                else
                {
                    for (int i = 0; i < Weapon2DropdownTrooper.options.Count; i++)
                    {
                        if (Weapon2DropdownTrooper.options[i].text == trooper.secondaryWeapon)
                        {
                            Weapon2DropdownTrooper.value = i;
                        }
                    }
                }
            }
        }

        if (EquipmentDropdownTrooper.options[EquipmentDropdownTrooper.value].text != trooper.equipment)
        {
            if (trooper.equipment == "None")
            {
                EquipmentDropdownTrooper.value = 0;
            }
            else
            {
                for (int i = 0; i < EquipmentDropdownTrooper.options.Count; i++)
                {
                    if (EquipmentDropdownTrooper.options[i].text == trooper.equipment)
                    {
                        EquipmentDropdownTrooper.value = i;
                    }
                }
            }
        }

        if (ArmourPatternTrooper.options[ArmourPatternTrooper.value].text != trooper.armourPattern)
        {
            for (int i = 0; i < ArmourPatternTrooper.options.Count; i++)
            {
                if (ArmourPatternTrooper.options[i].text == trooper.armourPattern)
                {
                    ArmourPatternTrooper.value = i;
                }
            }
        }

        if (HelmetPatternTrooper.options[HelmetPatternTrooper.value].text != trooper.helmetPattern)
        {
            for (int i = 0; i < HelmetPatternTrooper.options.Count; i++)
            {
                if (HelmetPatternTrooper.options[i].text == trooper.helmetPattern)
                {
                    HelmetPatternTrooper.value = i;
                }
            }
        }

        if (FatiguesPatternTrooper.options[FatiguesPatternTrooper.value].text != trooper.fatiguesPattern)
        {
            for (int i = 0; i < FatiguesPatternTrooper.options.Count; i++)
            {
                if (FatiguesPatternTrooper.options[i].text == trooper.fatiguesPattern)
                {
                    FatiguesPatternTrooper.value = i;
                }
            }
        }
    }
}
