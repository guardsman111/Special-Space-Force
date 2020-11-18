using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trooper_Script : MonoBehaviour
{
    public string trooperName;
    public string trooperRank;
    public int trooperPosition;
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

    public int trait1;
    public int trait2;

    public Slot_Manager manager;
    public Equipment_Manager equipmentManager;
    public GameObject image;
    public TMP_InputField input;

    public Image[] trooperImages; //Trooper Outline, T Colour, Hair Colour, Fatigue Primary, F Secondary, F Tertiary, F Special, F Outline,
                                  //Armour Primary, A Secondary, A Tertiary, A Special, A Equipment, A Force Icon, A Outline, Helmet Primary,
                                  //H Secondary, H Tertiary, H Equipment, H Visor, H Outline, E Outline, E Primary, E Primary, W outline, 
                                  //W Primary, W Secondary

    public Image_Manager imageManager;

    public void MakeTrooper(Trooper_Class trooper, int positionID, Slot_Manager nManager)
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
            trooperImages[2].sprite = manager.femaleHairPack.containedSprites[trooperHair];
        }
        if (trooper.gender == 1)
        {
            trooperImages[2].sprite = manager.maleHairPack.containedSprites[trooperHair];
        }

        trooperImages[2].color = manager.hairColours[hairColour];

        input.text = trooperName;


        if (trooper.armour != null)
        {
            armour = trooper.armour;
            armourPattern = "Primary1";
            helmet = trooper.helmet;
            armourPattern = "Primary1";
            fatigues = trooper.fatigues;
            armourPattern = "Primary1";
            primaryWeapon = trooper.weapon1;
            secondaryWeapon = trooper.weapon2;
            equipment = trooper.equipment;
            equipmentManager.LoadEquipment(this, "Armour", armour);
            equipmentManager.LoadEquipment(this, "Helmet", helmet);
            equipmentManager.LoadEquipment(this, "Fatigues", fatigues);
            equipmentManager.LoadEquipment(this, "Weapons", primaryWeapon);
            equipmentManager.LoadEquipment(this, "Weapons 2", secondaryWeapon);
            equipmentManager.LoadEquipment(this, "Equipment", equipment);
        }
        else
        {
            armour = "Mk1 Armour";
            trooper.armour = armour;
            armourPattern = "Primary1";
            trooper.armourP = armourPattern;
            helmet = "Mk1 Helmet";
            trooper.helmet = helmet;
            helmetPattern = "Primary1";
            trooper.helmetP = helmetPattern;
            fatigues = "BDU UBACS";
            trooper.fatigues = fatigues;
            fatiguesPattern = "Primary1";
            trooper.fatiguesP = fatiguesPattern;
            primaryWeapon = "Pistol1";
            trooper.weapon1 = primaryWeapon;
            secondaryWeapon = "SwordSec1";
            trooper.weapon2 = secondaryWeapon;
            equipment = "Backpack";
            trooper.equipment = equipment;
            equipmentManager.LoadEquipment(this, "Armour", armour);
            equipmentManager.LoadEquipment(this, "Helmet", helmet);
            equipmentManager.LoadEquipment(this, "Fatigues", fatigues);
            equipmentManager.LoadEquipment(this, "Weapons", primaryWeapon);
            equipmentManager.LoadEquipment(this, "Weapons 2", secondaryWeapon);
            equipmentManager.LoadEquipment(this, "Equipment", equipment);
        }
        TrooperColours();
        manager.ChangeTroopers(1);
    }

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
        if (trooperSquad == viewedSlot) 
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

    public void ChangeEquipment(Dropdown dropdown)
    {
        equipmentManager.ChangeEquipment(this, dropdown);
    }

    public void ChangePattern(Dropdown dropdown)
    {
        equipmentManager.ChangePattern(this, dropdown);
    }

    public void TrooperColours()
    {
        trooperImages[3].color = equipmentManager.playerDefaultColours[5];
        trooperImages[4].color = equipmentManager.playerDefaultColours[6];
        trooperImages[5].color = equipmentManager.playerDefaultColours[7];
        trooperImages[6].color = equipmentManager.playerDefaultColours[9];
        trooperImages[7].color = equipmentManager.playerDefaultColours[8];
        trooperImages[9].color = equipmentManager.playerDefaultColours[0];
        trooperImages[10].color = equipmentManager.playerDefaultColours[1];
        trooperImages[11].color = equipmentManager.playerDefaultColours[2];
        trooperImages[12].color = equipmentManager.playerDefaultColours[4];
        trooperImages[13].color = equipmentManager.playerDefaultColours[3];
        trooperImages[16].color = equipmentManager.playerDefaultColours[10];
        trooperImages[17].color = equipmentManager.playerDefaultColours[11];
        trooperImages[18].color = equipmentManager.playerDefaultColours[12];
        trooperImages[19].color = equipmentManager.playerDefaultColours[13];
        trooperImages[23].color = equipmentManager.playerDefaultColours[16];
        trooperImages[24].color = equipmentManager.playerDefaultColours[17];
        trooperImages[26].color = equipmentManager.playerDefaultColours[14];
        trooperImages[27].color = equipmentManager.playerDefaultColours[15];
        trooperImages[29].color = equipmentManager.playerDefaultColours[14];
        trooperImages[30].color = equipmentManager.playerDefaultColours[15];
    }

    public void ChangeName(TMP_InputField input)
    {
        trooperClass.trooperName = input.text;
        trooperName = input.text;
    }

    public void UIPressed(bool setting)
    {
        manager.UIPressed(setting);
    }

    public Trooper_Class SaveTrooper()
    {
        Trooper_Class newClass = new Trooper_Class();

        newClass.trooperName = trooperName;
        newClass.trooperRank = trooperRank;
        newClass.trooperFace = trooperFace;
        newClass.trooperHair = trooperHair;
        newClass.hairColour = hairColour;
        newClass.gender = trooperClass.gender;
        newClass.trooperPosition = trooperPosition;

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
}
