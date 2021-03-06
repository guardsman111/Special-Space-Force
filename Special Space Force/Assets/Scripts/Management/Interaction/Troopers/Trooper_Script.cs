using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trooper_Script : MonoBehaviour
{
    /// <summary>
    /// This script contains all the code for the trooper it is attatched too.
    /// Sets the images and positions in the visual layout.
    /// </summary>
    public string trooperName;
    public string trooperRank;
    public int trooperPosition;
    public string slotIdentifier;
    public int identifierLoc;
    public int trooperFace;
    public int trooperHair;
    public int hairColour;
    public Trooper_Class trooperClass;
    public Slot_Script trooperSquad;
    public string armour;
    public string fatigues;
    public string helmet;
    public string primaryWeapon;
    public string secondaryWeapon;
    public string equipment;
    public string armourPattern;
    public string fatiguesPattern;
    public string helmetPattern;

    //Base values
    [SerializeField]
    private int movement;
    [SerializeField]
    private int constitution;
    [SerializeField]
    private int bravery;

    //Skill values
    [SerializeField]
    private int speed;
    [SerializeField]
    private int agility;
    [SerializeField]
    private int strength;
    [SerializeField]
    private int size;
    [SerializeField]
    private int morale;
    [SerializeField]
    private int breakValue;
    [SerializeField]
    private int melee;
    [SerializeField]
    private int ranged;
    [SerializeField]
    private int stealth;
    [SerializeField]
    private int stamina;

    //Traits by integer position in the generated product's list of traits
    public int trait1;
    public int trait2;

    public Slot_Manager manager;
    public Equipment_Manager equipmentManager;
    public GameObject image;
    public TMP_InputField input;

    public Image[] trooperImages; //0 Trooper Outline, 1 T Colour, 2 Hair Outline, 3 Hair Colour, 4 Fatigue Primary, 5 F Secondary, 6 F Tertiary, 7 F Special, 8 F Equipment,
                                  //9 F Outline, 10 Armour Primary, 11 A Secondary, 12 A Tertiary, 13 A Special, 14 A Equipment, 15 A Force Icon, 16 A Outline, 17 Helmet Primary,
                                  //18 H Secondary, 19 H Tertiary, 20 H Equipment, 21 H Visor, 22 H Outline, 23 E Outline, 24 E Primary, 25 E Secondary, 26 W outline, 
                                  //27 W Primary, 28 W Secondary, 29 W outline, 30 W Primary, 31 W Secondary

    public Image_Manager imageManager;

    public Text[] slotLocations;

    //Makes trooper from existing trooper_class
    public void MakeTrooper(Trooper_Class trooper, int positionID, Slot_Manager nManager, Slot_Script Squad)
    {
        manager = nManager;
        equipmentManager = GameObject.FindGameObjectWithTag("EquipmentManager").GetComponent<Equipment_Manager>();
        trooperClass = trooper;
        trooperName = trooper.trooperName;
        trooperRank = trooper.trooperRank;
        trooperPosition = positionID;
        trooperFace = trooper.trooperFace;
        trooperHair = trooper.trooperHair;
        hairColour = trooper.hairColour;
        trooperSquad = Squad;

        if (trooper.movement == 0)
        {
            //Setup Random skills and stats for troopers
            movement = Random.Range(60, 80);
            constitution = Random.Range(40, 90);
            bravery = Random.Range(20, 100);

            stamina = constitution;
            speed = (movement / 2);
            agility = (movement / 2);
            strength = constitution;
            size = 45 + (strength / 10);
            morale = bravery;
            breakValue = 30 - (bravery / 5);
            melee = Random.Range(40, 80);
            ranged = Random.Range(40, 80);
            stealth = Random.Range(10, 90);

            GenerateTraits(manager.modManager.traitManager.GetTraits());

            if (trait1 != -1)
            {
                Trait_Class traitTemp = manager.modManager.traitManager.GetTraits()[trait1];
                AffectTrait(traitTemp);
            }

            if (trait2 != -1)
            {
                Trait_Class traitTemp = manager.modManager.traitManager.GetTraits()[trait2];
                AffectTrait(traitTemp);
            }
        } 
        else
        {
            //Load skills and stats for troopers
            movement = trooper.movement;
            constitution = trooper.constitution;
            bravery = trooper.bravery;

            stamina = trooper.stamina;
            speed = trooper.speed;
            agility = trooper.agility;
            strength = trooper.strength;
            size = trooper.size;
            morale = trooper.morale;
            breakValue = trooper.breakValue;
            melee = trooper.melee;
            ranged = trooper.ranged;
            stealth = trooper.stealth;

            trait1 = trooper.trait1;
            trait2 = trooper.trait2;
        }

        trooperImages[1].sprite = manager.trooperSkinPack.containedSprites[trooperFace];

        if (trooper.gender == 0)
        {
            trooperImages[3].sprite = manager.femaleHairPack.containedSprites[trooperHair];
            trooperImages[2].sprite = manager.femaleHairOutlinePack.containedSprites[trooperHair];
        }
        if (trooper.gender == 1)
        {
            trooperImages[3].sprite = manager.maleHairPack.containedSprites[trooperHair];
            trooperImages[2].sprite = manager.maleHairOutlinePack.containedSprites[trooperHair];
        }

        trooperImages[3].color = manager.hairColours[hairColour];

        input.text = trooperName;


        if (manager.modManager.equipmentManager.selectFromTemplate.isOn)
        {
            if (trooper.armour != null)
            {
                armour = trooper.armour;
                if (trooper.armourP != null)
                {
                    armourPattern = trooper.armourP;
                }
                else
                {
                    armourPattern = manager.manager.save.generatedProduct.defaultPatterns[1];
                    trooper.armourP = armourPattern;
                }
            }
            else
            {
                armour = manager.manager.save.generatedProduct.defaultEquipment[1];
                trooper.armour = armour;
                armourPattern = manager.manager.save.generatedProduct.defaultPatterns[1];
                trooper.armourP = armourPattern;
            }
            if (trooper.helmet != null)
            {
                helmet = trooper.helmet;
                if (trooper.helmetP != null)
                {
                    helmetPattern = trooper.helmetP;
                }
                else
                {
                    helmetPattern = manager.manager.save.generatedProduct.defaultPatterns[0];
                    trooper.helmetP = helmetPattern;
                }
            }
            else
            {
                helmet = manager.manager.save.generatedProduct.defaultEquipment[0];
                trooper.helmet = helmet;
                helmetPattern = manager.manager.save.generatedProduct.defaultPatterns[0];
                trooper.helmetP = helmetPattern;
            }
            if (trooper.fatigues != null)
            {
                fatigues = trooper.fatigues;
                if (trooper.fatiguesP != null)
                {
                    fatiguesPattern = trooper.fatiguesP;
                }
                else
                {
                    fatiguesPattern = manager.manager.save.generatedProduct.defaultPatterns[2];
                    trooper.fatiguesP = fatiguesPattern;
                }
            }
            else
            {
                fatigues = manager.manager.save.generatedProduct.defaultEquipment[2];
                trooper.fatigues = fatigues;
                fatiguesPattern = manager.manager.save.generatedProduct.defaultPatterns[2];
                trooper.fatiguesP = fatiguesPattern;
            }
            if (trooper.weapon1 != null)
            {
                primaryWeapon = trooper.weapon1;
            }
            else
            {
                primaryWeapon = manager.manager.save.generatedProduct.defaultEquipment[3];
                trooper.weapon1 = primaryWeapon;
            }
            if (trooper.weapon2 != null)
            {
                secondaryWeapon = trooper.weapon2;
            }
            else
            {
                secondaryWeapon = manager.manager.save.generatedProduct.defaultEquipment[4];
                trooper.weapon2 = secondaryWeapon;
            }
            if (trooper.equipment != null)
            {
                equipment = trooper.equipment;
            }
            else
            {
                equipment = manager.manager.save.generatedProduct.defaultEquipment[5];
                trooper.equipment = equipment;
            }
        }
        else
        {
            armour = manager.manager.save.generatedProduct.defaultEquipment[1];
            trooper.armour = armour;
            armourPattern = manager.manager.save.generatedProduct.defaultPatterns[1];
            trooper.armourP = armourPattern;
            helmet = manager.manager.save.generatedProduct.defaultEquipment[0];
            trooper.helmet = helmet;
            helmetPattern = manager.manager.save.generatedProduct.defaultPatterns[0];
            trooper.helmetP = helmetPattern;
            fatigues = manager.manager.save.generatedProduct.defaultEquipment[2];
            trooper.fatigues = fatigues;
            fatiguesPattern = manager.manager.save.generatedProduct.defaultPatterns[2]; ;
            trooper.fatiguesP = fatiguesPattern;
            primaryWeapon = manager.manager.save.generatedProduct.defaultEquipment[3];
            trooper.weapon1 = primaryWeapon;
            secondaryWeapon = manager.manager.save.generatedProduct.defaultEquipment[4];
            trooper.weapon2 = secondaryWeapon;
            equipment = manager.manager.save.generatedProduct.defaultEquipment[5];
            trooper.equipment = equipment;
            equipmentManager.LoadEquipment(this, "Armour", armour);
            equipmentManager.LoadEquipment(this, "Helmet", helmet);
            equipmentManager.LoadEquipment(this, "Fatigues", fatigues);
            equipmentManager.LoadEquipment(this, "Weapons", primaryWeapon);
            equipmentManager.LoadEquipment(this, "Weapons 2", secondaryWeapon);
            equipmentManager.LoadEquipment(this, "Equipment", equipment);
        }
        SaveTrooper(trooperClass);
        TrooperColours();
        manager.ChangeTroopers(1);
    }

    //Loads a trooper from existing trooper_class
    public void LoadTrooper(Trooper_Class trooper, Slot_Manager nManager, Slot_Script Squad)
    {
        manager = nManager;
        equipmentManager = GameObject.FindGameObjectWithTag("EquipmentManager").GetComponent<Equipment_Manager>();
        trooperClass = trooper;
        trooperName = trooper.trooperName;
        trooperRank = trooper.trooperRank;
        trooperPosition = trooper.trooperPosition;
        identifierLoc = trooper.indicatorLoc;
        trooperFace = trooper.trooperFace;
        trooperHair = trooper.trooperHair;
        hairColour = trooper.hairColour;
        trooperSquad = Squad;

        //Load skills and stats for troopers
        movement = trooper.movement;
        constitution = trooper.constitution;
        bravery = trooper.bravery;

        stamina = trooper.stamina;
        speed = trooper.speed;
        agility = trooper.agility;
        strength = trooper.strength;
        size = trooper.size;
        morale = trooper.morale;
        breakValue = trooper.breakValue;
        melee = trooper.melee;
        ranged = trooper.ranged;
        stealth = trooper.stealth;

        trait1 = trooper.trait1;
        trait2 = trooper.trait2;

        trooperImages[1].sprite = manager.trooperSkinPack.containedSprites[trooperFace];

        if (trooper.gender == 0)
        {
            trooperImages[3].sprite = manager.femaleHairPack.containedSprites[trooperHair];
            trooperImages[2].sprite = manager.femaleHairOutlinePack.containedSprites[trooperHair];
        }
        if (trooper.gender == 1)
        {
            trooperImages[3].sprite = manager.maleHairPack.containedSprites[trooperHair];
            trooperImages[2].sprite = manager.maleHairOutlinePack.containedSprites[trooperHair];
        }

        trooperImages[3].color = manager.hairColours[hairColour];

        input.text = trooperName;

        armour = trooper.armour;
        armourPattern = trooper.armourP;
        helmet = trooper.helmet;
        helmetPattern = trooper.helmetP;
        fatigues = trooper.fatigues;
        fatiguesPattern = trooper.fatiguesP;
        primaryWeapon = trooper.weapon1;
        secondaryWeapon = trooper.weapon2;
        equipment = trooper.equipment;
        equipmentManager.LoadEquipment(this, "Armour", armour);
        equipmentManager.LoadEquipment(this, "Helmet", helmet);
        equipmentManager.LoadEquipment(this, "Fatigues", fatigues);
        equipmentManager.LoadEquipment(this, "Weapons", primaryWeapon);
        equipmentManager.LoadEquipment(this, "Weapons 2", secondaryWeapon);
        equipmentManager.LoadEquipment(this, "Equipment", equipment);

        TrooperColours();
        manager.ChangeTroopers(1);
        FindSlotIdentifier();
    }

    //Generates up to 2 random traits
    public void GenerateTraits(List<Trait_Class> traits)
    {
        trait1 = -1;
        trait2 = -1;
        int random1 = Random.Range(0, 10);
        int random2 = Random.Range(0, 10);

        if (random1 > 5 && random2 > 5)
        {
            trait1 = Random.Range(1, traits.Count);
            trait2 = Random.Range(1, traits.Count);
            if (trait1 == trait2)
            {
                trait2 = -1;
            }
        }
        else if (random1 > 5 && random2 <= 5 || random1 <= 5 && random2 > 5)
        {
            trait1 = Random.Range(1, traits.Count);
        }
    }


    //Sets the position and scale of the slot according to its slot height relative to the currently viewed slot
    public void SetPosition(Slot_Script parent, Slot_Script viewedSlot)
    {
        RectTransform rTransform = GetComponent<RectTransform>();
        if (parent == viewedSlot) 
        {
            switch (trooperPosition)
            {

                case 1:
                    rTransform.position = parent.squadPositions[0].transform.position;
                    break;

                case 2:
                    rTransform.position = parent.squadPositions[1].transform.position;
                    break;

                case 3:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(300,0);
                    break;

                case 4:
                    rTransform.position = parent.squadPositions[1].transform.position + new Vector3(300, 0);
                    break;

                case 5:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(600, 0);
                    break;

                case 6:
                    rTransform.position = parent.squadPositions[1].transform.position + new Vector3(600, 0);
                    break;

                case 7:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(900, 0);
                    break;

                case 8:
                    rTransform.position = parent.squadPositions[1].transform.position + new Vector3(900, 0);
                    break;

                case 9:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(1200, 0);
                    break;

                case 10:
                    rTransform.position = parent.squadPositions[1].transform.position + new Vector3(1200, 0);
                    break;

                case 11:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(1500, 0);
                    break;

                case 12:
                    rTransform.position = parent.squadPositions[1].transform.position + new Vector3(1500, 0);
                    break;

                case 13:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(1800, 0);
                    break;

                case 14:
                    rTransform.position = parent.squadPositions[1].transform.position + new Vector3(1800, 0);
                    break;

                case 15:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(2100, 0);
                    break;

                case 16:
                    rTransform.position = parent.squadPositions[1].transform.position + new Vector3(2100, 0);
                    break;

                case 17:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(2400, 0);
                    break;

                case 18:
                    rTransform.position = parent.squadPositions[1].transform.position + new Vector3(2400, 0);
                    break;

                case 19:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(2700, 0);
                    break;

                case 20:
                    rTransform.position = parent.squadPositions[1].transform.position + new Vector3(2700, 0);
                    break;

            }
            gameObject.transform.localScale = new Vector3(1, 1);
            input.transform.localScale = new Vector3(1, 1);
            input.textComponent.transform.localScale = new Vector3(1, 1);
            input.textComponent.enableWordWrapping = true;

            gameObject.transform.localScale = new Vector3(1, 1);
            gameObject.SetActive(true);
            input.gameObject.SetActive(true);
            image.SetActive(true);
        }
        else if (viewedSlot.containedSlots.Contains(trooperSquad))
        {
            switch (trooperPosition)
            {

                case 1:
                    rTransform.position = parent.squadPositions[0].transform.position;
                    break;

                case 2:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175),0);
                    break;

                case 3:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 2), 0);
                    break;

                case 4:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 3), 0);
                    break;

                case 5:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 4), 0);
                    break;

                case 6:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(0, -200);
                    break;

                case 7:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175), -200);
                    break;

                case 8:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 2), -200);
                    break;

                case 9:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 3), -200);
                    break;

                case 10:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 4), -200);
                    break;

                case 11:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(0, -400);
                    break;

                case 12:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175), -400);
                    break;

                case 13:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 2), -400);
                    break;

                case 14:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 3), -400);
                    break;

                case 15:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 4), -400);
                    break;

                case 16:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(0, -600);
                    break;

                case 17:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175), -600);
                    break;

                case 18:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 2), -600);
                    break;

                case 19:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 3), -600);
                    break;

                case 20:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 4), -600);
                    break;

            }

            input.gameObject.SetActive(true);
            image.SetActive(true);
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f);
            input.transform.localScale = new Vector3(2, 2);
        }
        else
        {
            input.gameObject.SetActive(false);
            image.SetActive(false);
        }
    }

    //Changes the troopers equipment of the dropdown type
    public void ChangeEquipment(Dropdown dropdown)
    {
        equipmentManager.ChangeEquipment(this, dropdown);
        if (trooperClass != null)
        {
            trooperClass = SaveTrooper(trooperClass);
        }
    }

    //Changes the troopers pattern of the dropdown type
    public void ChangePattern(Dropdown dropdown)
    {
        equipmentManager.ChangePattern(this, dropdown);
        if (trooperClass != null)
        {
            trooperClass = SaveTrooper(trooperClass);
        }
    }

    //Sets the troopers colour scheme
    public void TrooperColours()
    {
        if (trooperSquad.slotClass.useSquadColours)
        {
            trooperImages[4].color = trooperSquad.slotClass.squadColours[9];
            trooperImages[5].color = trooperSquad.slotClass.squadColours[10];
            trooperImages[6].color = trooperSquad.slotClass.squadColours[11];
            trooperImages[7].color = trooperSquad.slotClass.squadColours[12];
            trooperImages[8].color = trooperSquad.slotClass.squadColours[13];
            trooperImages[10].color = trooperSquad.slotClass.squadColours[4];
            trooperImages[11].color = trooperSquad.slotClass.squadColours[5];
            trooperImages[12].color = trooperSquad.slotClass.squadColours[6];
            trooperImages[13].color = trooperSquad.slotClass.squadColours[7];
            trooperImages[14].color = trooperSquad.slotClass.squadColours[8];
            trooperImages[17].color = trooperSquad.slotClass.squadColours[0];
            trooperImages[18].color = trooperSquad.slotClass.squadColours[1];
            trooperImages[19].color = trooperSquad.slotClass.squadColours[2];
            trooperImages[20].color = trooperSquad.slotClass.squadColours[3];
            trooperImages[24].color = trooperSquad.slotClass.squadColours[16];
            trooperImages[25].color = trooperSquad.slotClass.squadColours[17];
            trooperImages[27].color = trooperSquad.slotClass.squadColours[14];
            trooperImages[28].color = trooperSquad.slotClass.squadColours[15];
            trooperImages[30].color = trooperSquad.slotClass.squadColours[14];
            trooperImages[31].color = trooperSquad.slotClass.squadColours[15];
            foreach (Text t in slotLocations)
            {
                t.color = trooperSquad.slotClass.squadColours[18];
            }
        }
        else
        {
            trooperImages[4].color = equipmentManager.playerDefaultColours[5];
            trooperImages[5].color = equipmentManager.playerDefaultColours[6];
            trooperImages[6].color = equipmentManager.playerDefaultColours[7];
            trooperImages[7].color = equipmentManager.playerDefaultColours[9];
            trooperImages[8].color = equipmentManager.playerDefaultColours[8];
            trooperImages[10].color = equipmentManager.playerDefaultColours[0];
            trooperImages[11].color = equipmentManager.playerDefaultColours[1];
            trooperImages[12].color = equipmentManager.playerDefaultColours[2];
            trooperImages[13].color = equipmentManager.playerDefaultColours[4];
            trooperImages[14].color = equipmentManager.playerDefaultColours[3];
            trooperImages[17].color = equipmentManager.playerDefaultColours[10];
            trooperImages[18].color = equipmentManager.playerDefaultColours[11];
            trooperImages[19].color = equipmentManager.playerDefaultColours[12];
            trooperImages[20].color = equipmentManager.playerDefaultColours[13];
            trooperImages[24].color = equipmentManager.playerDefaultColours[16];
            trooperImages[25].color = equipmentManager.playerDefaultColours[17];
            trooperImages[27].color = equipmentManager.playerDefaultColours[14];
            trooperImages[28].color = equipmentManager.playerDefaultColours[15];
            trooperImages[30].color = equipmentManager.playerDefaultColours[14];
            trooperImages[31].color = equipmentManager.playerDefaultColours[15];
            foreach (Text t in slotLocations)
            {
                t.color = equipmentManager.playerDefaultColours[18];
            }
        }
        trooperClass = SaveTrooper(trooperClass);
    }

    //Changes the troopers name according to the input
    public void ChangeName(TMP_InputField input)
    {
        trooperClass.trooperName = input.text;
        trooperName = input.text;
    }

    public void UIPressed(bool setting)
    {
        manager.UIPressed(setting);
    }

    //Saves the trooper to a trooper_class
    public Trooper_Class SaveTrooper(Trooper_Class trooper)
    {
        Trooper_Class newClass = trooper;

        newClass.trooperName = trooperName;
        newClass.trooperRank = trooperRank;
        newClass.trooperFace = trooperFace;
        newClass.trooperHair = trooperHair;
        newClass.hairColour = hairColour;
        newClass.gender = trooperClass.gender;
        newClass.trooperPosition = trooperPosition;

        newClass.armour = armour;
        newClass.armourP = armourPattern;
        newClass.helmet = helmet;
        newClass.helmetP = helmetPattern;
        newClass.fatigues = fatigues;
        newClass.fatiguesP = fatiguesPattern;
        newClass.weapon1 = primaryWeapon;
        newClass.weapon2 = secondaryWeapon;
        newClass.equipment = equipment;

        newClass.movement = movement;
        newClass.constitution = constitution;
        newClass.bravery = bravery;
        newClass.speed = speed;
        newClass.agility = agility;
        newClass.strength = strength;
        newClass.size = size;
        newClass.morale = morale;
        newClass.breakValue = breakValue;
        newClass.melee = melee;
        newClass.ranged = ranged;
        newClass.stealth = stealth;
        newClass.stamina = stamina;

        newClass.trait1 = trait1;
        newClass.trait2 = trait2;

        return newClass;
    }

    //Returns the stat of the given string
    public int GetStat(string name)
    {
        int returner = 0;
        switch (name)
        {
            case "Speed":
                returner = speed;
                break;
            case "Agility":
                returner = agility;
                break;
            case "Strength":
                returner = strength;
                break;
            case "Size":
                returner = size;
                break;
            case "Morale":
                returner = morale;
                break;
            case "Break Value":
                returner = breakValue;
                break;
            case "Melee":
                returner = melee;
                break;
            case "Ranged":
                returner = ranged;
                break;
            case "Stealth":
                returner = stealth;
                break;
            case "Stamina":
                returner = stamina;
                break;
            default:
                return 0;
        }
        return returner;
    }

    //Returns the trait string of the requested trait 
    public string GetTrait(string name)
    {
        string returner = "";
        Trait_Class traitTemp;

        switch (name)
        {
            case "Trait1":
                if (trait1 != -1)
                {
                    traitTemp = manager.modManager.traitManager.GetTraits()[trait1];
                    returner = traitTemp.traitName;
                }
                break;
            case "Trait2":
                if (trait2 != -1)
                {
                    traitTemp = manager.modManager.traitManager.GetTraits()[trait2];
                    returner = traitTemp.traitName;
                }
                break;
        }

        return returner;
    }

    //Adds the effect of the traits to the trooper's stats
    public void AffectTrait(Trait_Class traitTemp)
    {
        if (traitTemp.speed != "")
        {
            string newSpeed = traitTemp.speed;
            if (traitTemp.speed.Contains("+"))
            {
                newSpeed.Replace("+", "");
                speed += int.Parse(newSpeed);
            }
            if (traitTemp.speed.Contains("-"))
            {
                newSpeed.Replace("-", "");
                speed -= int.Parse(newSpeed);
            }
            if (traitTemp.speed.Contains("*"))
            {
                newSpeed.Replace("*", "");
                if (speed > 1)
                {
                    speed = speed * int.Parse(newSpeed);
                }
            }
            if (traitTemp.speed.Contains("/"))
            {
                newSpeed.Replace("/", "");
                if (speed > 1)
                {
                    speed = speed / int.Parse(newSpeed);
                }
            }
        }
        if (traitTemp.agility != "")
        {
            string newAgility = traitTemp.agility;
            if (traitTemp.agility.Contains("+"))
            {
                newAgility.Replace("+", "");
                agility += int.Parse(newAgility);
            }
            if (traitTemp.agility.Contains("-"))
            {
                newAgility.Replace("-", "");
                agility -= int.Parse(newAgility);
            }
            if (traitTemp.agility.Contains("*"))
            {
                newAgility.Replace("*", "");
                if (agility > 1)
                {
                    agility = agility * int.Parse(newAgility);
                }
            }
            if (traitTemp.agility.Contains("/"))
            {
                newAgility.Replace("/", "");
                if (agility > 1)
                {
                    agility = agility / int.Parse(newAgility);
                }
            }
        }
        if (traitTemp.strength != "")
        {
            string newStrength = traitTemp.strength;
            if (traitTemp.strength.Contains("+"))
            {
                newStrength.Replace("+", "");
                strength += int.Parse(newStrength);
            }
            if (traitTemp.strength.Contains("-"))
            {
                newStrength.Replace("-", "");
                strength -= int.Parse(newStrength);
            }
            if (traitTemp.strength.Contains("*"))
            {
                newStrength.Replace("*", "");
                if (strength > 1)
                {
                    strength = strength * int.Parse(newStrength);
                }
            }
            if (traitTemp.strength.Contains("/"))
            {
                newStrength.Replace("/", "");
                if (strength > 1)
                {
                    strength = strength / int.Parse(newStrength);
                }
            }
        }
        if (traitTemp.size != "")
        {
            string newStrength = traitTemp.size;
            if (traitTemp.size.Contains("+"))
            {
                newStrength.Replace("+", "");
                size += int.Parse(newStrength);
            }
            if (traitTemp.size.Contains("-"))
            {
                newStrength.Replace("-", "");
                size -= int.Parse(newStrength);
            }
            if (traitTemp.size.Contains("*"))
            {
                newStrength.Replace("*", "");
                if (size > 1)
                {
                    size = size * int.Parse(newStrength);
                }
            }
            if (traitTemp.size.Contains("/"))
            {
                newStrength.Replace("/", "");
                if (size > 1)
                {
                    size = size / int.Parse(newStrength);
                }
            }
        }
        if (traitTemp.morale != "")
        {
            string newMorale = traitTemp.morale;
            if (traitTemp.morale.Contains("+"))
            {
                newMorale.Replace("+", "");
                morale += int.Parse(newMorale);
            }
            if (traitTemp.morale.Contains("-"))
            {
                newMorale.Replace("-", "");
                morale -= int.Parse(newMorale);
            }
            if (traitTemp.morale.Contains("*"))
            {
                newMorale.Replace("*", "");
                if (morale > 1)
                {
                    morale = morale * int.Parse(newMorale);
                }
            }
            if (traitTemp.morale.Contains("/"))
            {
                newMorale.Replace("/", "");
                if (morale > 1)
                {
                    morale = morale / int.Parse(newMorale);
                }
            }
        }
        if (traitTemp.breakValue != "")
        {
            string newBreakValue = traitTemp.breakValue;
            if (traitTemp.breakValue.Contains("+"))
            {
                newBreakValue = newBreakValue.Replace("+", "");
                breakValue += int.Parse(newBreakValue);
            }
            if (traitTemp.breakValue.Contains("-"))
            {
                newBreakValue = newBreakValue.Replace("-", "");
                breakValue -= int.Parse(newBreakValue);
            }
            if (traitTemp.breakValue.Contains("*"))
            {
                newBreakValue = newBreakValue.Replace("*", "");
                if (breakValue > 1)
                {
                    breakValue = breakValue * int.Parse(newBreakValue);
                }
            }
            if (traitTemp.breakValue.Contains("/"))
            {
                newBreakValue = newBreakValue.Replace("/", "");
                if (breakValue > 1)
                {
                    breakValue = breakValue / int.Parse(newBreakValue);
                }
            }
        }
        if (traitTemp.melee != "")
        {
            string newMelee = traitTemp.melee;
            if (traitTemp.melee.Contains("+"))
            {
                newMelee.Replace("+", "");
                melee += int.Parse(newMelee);
            }
            if (traitTemp.melee.Contains("-"))
            {
                newMelee.Replace("-", "");
                melee -= int.Parse(newMelee);
            }
            if (traitTemp.melee.Contains("*"))
            {
                newMelee.Replace("*", "");
                if (melee > 1)
                {
                    melee = melee * int.Parse(newMelee);
                }
            }
            if (traitTemp.melee.Contains("/"))
            {
                newMelee.Replace("/", "");
                if (melee > 1)
                {
                    melee = melee / int.Parse(newMelee);
                }
            }
        }
        if (traitTemp.ranged != "")
        {
            string newRanged = traitTemp.ranged;
            if (traitTemp.ranged.Contains("+"))
            {
                newRanged.Replace("+", "");
                ranged += int.Parse(newRanged);
            }
            if (traitTemp.ranged.Contains("-"))
            {
                newRanged.Replace("-", "");
                ranged -= int.Parse(newRanged);
            }
            if (traitTemp.ranged.Contains("*"))
            {
                newRanged.Replace("*", "");
                if (ranged > 1)
                {
                    ranged = ranged * int.Parse(newRanged);
                }
            }
            if (traitTemp.ranged.Contains("/"))
            {
                newRanged.Replace("/", "");
                if (ranged > 1)
                {
                    ranged = ranged / int.Parse(newRanged);
                }
            }
        }
        if (traitTemp.stealth != "")
        {
            string newStealth = traitTemp.stealth;
            if (traitTemp.stealth.Contains("+"))
            {
                newStealth.Replace("+", "");
                stealth += int.Parse(newStealth);
            }
            if (traitTemp.stealth.Contains("-"))
            {
                newStealth.Replace("-", "");
                stealth -= int.Parse(newStealth);
            }
            if (traitTemp.stealth.Contains("*"))
            {
                newStealth.Replace("*", "");
                if (stealth > 1)
                {
                    stealth = stealth * int.Parse(newStealth);
                }
            }
            if (traitTemp.stealth.Contains("/"))
            {
                newStealth.Replace("/", "");
                if (stealth > 1)
                {
                    stealth = stealth / int.Parse(newStealth);
                }
            }
        }
        if (traitTemp.stamina != "")
        {
            string newStamina = traitTemp.stamina;
            if (traitTemp.stamina.Contains("+"))
            {
                newStamina.Replace("+", "");
                stamina += int.Parse(newStamina);
            }
            if (traitTemp.stamina.Contains("-"))
            {
                newStamina.Replace("-", "");
                stamina -= int.Parse(newStamina);
            }
            if (traitTemp.stamina.Contains("*"))
            {
                newStamina.Replace("*", "");
                if (stamina > 1)
                {
                    stamina = stamina * int.Parse(newStamina);
                }
            }
            if (traitTemp.stamina.Contains("/"))
            {
                newStamina.Replace("/", "");
                if (stamina > 1)
                {
                    stamina = stamina / int.Parse(newStamina);
                }
            }
        }
    }

    //UI reference to change slot identifier location
    public void ToggleSlotLocation(int modifier)
    {
        for(int i = 0; i < slotLocations.Length; i++)
        {
            if(slotLocations[i].gameObject.activeSelf == true)
            {
                if(modifier == +1 && i == slotLocations.Length - 1)
                {
                    slotLocations[0].gameObject.SetActive(true);
                    slotLocations[i].gameObject.SetActive(false);
                }
                else if (modifier == -1 && i == 0)
                {
                    slotLocations[slotLocations.Length - 1].gameObject.SetActive(true);
                    slotLocations[i].gameObject.SetActive(false);
                }
                else
                {
                    slotLocations[i + modifier].gameObject.SetActive(true);
                    slotLocations[i].gameObject.SetActive(false);
                }
                break;
            }
        }
    }

    public void FindSlotIdentifier()
    {
        identifierLoc = manager.manager.save.generatedProduct.identifierLoc;
        if (trooperSquad != null && trooperSquad.slotParent != null && trooperSquad.slotParent.slotParent != null)
        {
            string temp1 = trooperSquad.slotParent.slotParent.slotName[0].ToString();
            string temp2 = trooperSquad.slotParent.slotName[0].ToString();
            string temp3 = trooperSquad.slotName[0].ToString();
            foreach (Text t in slotLocations)
            {
                t.text = temp1 + temp2 + temp3;
            }
            slotIdentifier = slotLocations[identifierLoc].text;
            trooperClass.indicatorLoc = identifierLoc;
            trooperClass.indicatorStr = slotLocations[identifierLoc].text;
        }
        else
        {
            slotLocations[identifierLoc].text = trooperClass.indicatorStr;
        }

        foreach (Text t in slotLocations)
        {
            t.gameObject.SetActive(false);
        }

        slotLocations[trooperClass.indicatorLoc].gameObject.SetActive(true);
    }
}
