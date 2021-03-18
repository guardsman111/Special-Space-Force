using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Voidcraft_Script : MonoBehaviour
{
    public Manager_Script modManager;
    public Fleet_Manager manager;

    public string craftName;
    public int uID;
    public Voidcraft_Class craftClass;
    public Fleet_Script craftFleet;
    public int craftPosition;

    //Base values
    [SerializeField]
    private int movementSpeed;
    [SerializeField]
    private int constructionPoints;
    [SerializeField]
    private int carryingCapacity;

    private List<Void_Weapon_Class> weapons;

    public Image[] craftImages; //0 Outline, 1 Primary, 2 Secondary, 3 Tertiary, 4 Trim, 5 Special

    public GameObject image;
    public TMP_InputField input;
    public TMP_InputField planetLocation;

    public Image_Manager imageManager;
    private List<Slot_Class> carriedSlots;

    public List<Slot_Class> CarriedSlots
    {
        get { return carriedSlots; }

        set
        {
            if(value != carriedSlots)
            {
                carriedSlots = value;
            }
        }
    }

    public void MakeCraft(Voidcraft_Class craft, Fleet_Manager fm, int ID, Fleet_Script fleet)
    {
        manager = fm;
        craftClass = new Voidcraft_Class();
        craftClass.className = craft.className;
        craftClass.weapons = craft.weapons;
        craftClass.speed = craft.speed;
        craftClass.costPerCraft = craft.costPerCraft;
        craftClass.armour = craft.armour;
        craftClass.capacity = craft.capacity;
        craftClass.positionID = ID;
        uID = Random.Range(0, 10000);
        while (manager.CraftIDs.Contains(uID))
        {
            uID = Random.Range(0, 10000);
        }
        craftClass.ID = uID;
        manager.CraftIDs.Add(uID);
        craftPosition = ID;
        craftFleet = fleet;
        craftClass.FleetID = fleet.fleetClass.uID;
        modManager.voidcraftManager.LoadCraft(this, craft.className);

        craftName = manager.manager.localisationManager.CreateName("CraftNames", this);
        craftClass.craftName = craftName;
        craftClass.uIDTransported = new List<int>();
        carriedSlots = new List<Slot_Class>();
        input.text = craftName;
        CraftColours();
    }

    public void MakeCraft(Voidcraft_Class craft, Manager_Script m, Fleet_Manager fm, int ID, Fleet_Script fleet)
    {
        modManager = m;
        manager = fm;
        craftClass = new Voidcraft_Class();
        craftClass.positionID = ID;
        craftClass.className = craft.className;
        craftClass.weapons = craft.weapons;
        craftClass.speed = craft.speed;
        craftClass.costPerCraft = craft.costPerCraft;
        craftClass.armour = craft.armour;
        craftClass.capacity = craft.capacity;
        uID = Random.Range(0, 10000);
        while (manager.CraftIDs.Contains(uID))
        {
            uID = Random.Range(0, 10000);
        }
        craftClass.ID = uID;
        manager.CraftIDs.Add(uID);
        craftPosition = ID;
        craftFleet = fleet;
        craftClass.FleetID = fleet.fleetClass.uID;
        modManager.voidcraftManager.LoadCraft(this, craft.className);

        craftName = manager.manager.localisationManager.CreateName("CraftNames", this);
        craftClass.craftName = craftName;
        craft.craftName = craftName;
        craft.FleetID = fleet.fleetClass.uID;
        craft.ID = uID;
        craft.positionID = ID;
        craftClass.uIDTransported = new List<int>();
        carriedSlots = new List<Slot_Class>();
        input.text = craftName;
        CraftColours();
    }

    public void LoadCraft(Voidcraft_Class craft, Manager_Script m, Fleet_Manager fm)
    {
        modManager = m;
        craftClass = craft;
        manager = fm;
        uID = craft.ID;
        manager.CraftIDs.Add(uID);
        craftPosition = craft.positionID;
        foreach(Fleet_Script fs in manager.FleetsS)
        {
            if(fs.fleetClass.uID == craft.FleetID)
            {
                craftFleet = fs;
            }
        }
        modManager.voidcraftManager.LoadCraft(this, craft.className);

        craftClass.uIDTransported = craft.uIDTransported;
        carriedSlots = new List<Slot_Class>();

        foreach(int i in craftClass.uIDTransported)
        {
            foreach(Slot_Class sc in modManager.sManager.Slots)
            {
                if (craft.uIDTransported.Contains(sc.uID))
                {
                    carriedSlots.Add(sc);
                }
                else
                {
                    foreach (Slot_Class sc2 in sc.containedSlots)
                    {
                        CheckChildSlotsForTransported(sc2, craft);
                    }
                }
            }
        }

        craftClass.starID = craft.starID;
        craftClass.planetN = craft.planetN;
        craftName = craft.craftName;
        input.text = craftName;
        CraftColours();
    }

    public void CheckChildSlotsForTransported(Slot_Class checkSlot, Voidcraft_Class craft)
    {
        if (craft.uIDTransported.Contains(checkSlot.uID))
        {
            carriedSlots.Add(checkSlot);
            foreach (Slot_Class sc in checkSlot.containedSlots)
            {
                if (!carriedSlots.Contains(sc))
                {
                    CheckChildSlotsForTransported(sc, craft);
                }
            }
        }
        else
        {
            foreach (Slot_Class sc in checkSlot.containedSlots)
            {
                if (!carriedSlots.Contains(sc))
                {
                    CheckChildSlotsForTransported(sc, craft);
                }
            }
        }
    }

    public void LoadQuickView(Voidcraft_Class craft, Manager_Script m, Fleet_Manager fm)
    {
        modManager = m;
        craftClass = craft;
        manager = fm;
        uID = craft.ID;
        craftPosition = craft.positionID;
        foreach (Fleet_Script fs in manager.FleetsS)
        {
            if (fs.fleetClass.uID == craft.FleetID)
            {
                craftFleet = fs;
            }
        }
        modManager.voidcraftManager.LoadCraft(this, craft.className);

        craftName = craft.craftName;
        input.text = craftName;
        planetLocation.text = GetStat("Location");
        CraftColours();
    }

    public void LoadAdvancedView(Voidcraft_Class craft, Manager_Script m, Fleet_Manager fm)
    {
        modManager = m;
        craftClass = craft;
        manager = fm;
        uID = craft.ID;
        craftPosition = craft.positionID;
        foreach (Fleet_Script fs in manager.FleetsS)
        {
            if (fs.fleetClass.uID == craft.FleetID)
            {
                craftFleet = fs;
            }
        }
        modManager.voidcraftManager.LoadCraft(this, craft.className);

        craftName = craft.craftName;
        input.text = craftName;
        planetLocation.text = GetStat("Location");
        CraftColours();
    }

    //Sets the crafts colour scheme
    public void CraftColours()
    {
        craftImages[1].color = modManager.voidcraftManager.playerFleetColours[0];
        craftImages[2].color = modManager.voidcraftManager.playerFleetColours[1];
        craftImages[3].color = modManager.voidcraftManager.playerFleetColours[2];
        craftImages[4].color = modManager.voidcraftManager.playerFleetColours[3];
        craftImages[5].color = modManager.voidcraftManager.playerFleetColours[4];
    }

    public void ChangeCraft(Dropdown dropdown)
    {
        modManager.voidcraftManager.ChangeCraft(dropdown, this);
    }

    //Sets the position and scale of the slot according to its slot height relative to the currently viewed slot
    public void SetPosition(Fleet_Script viewedFleet)
    {
        RectTransform rTransform = GetComponent<RectTransform>();
        if (craftFleet == viewedFleet)
        {
            this.gameObject.SetActive(true);
            switch (craftPosition)
            {

                case 1:
                    rTransform.position = craftFleet.craftPositions[0].transform.position;
                    break;

                case 2:
                    rTransform.position = craftFleet.craftPositions[1].transform.position;
                    break;

                case 3:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(512, 0);
                    break;

                case 4:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(512, 0);
                    break;

                case 5:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(1024, 0);
                    break;

                case 6:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(1024, 0);
                    break;

                case 7:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(1536, 0);
                    break;

                case 8:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(1536, 0);
                    break;

                case 9:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(2048, 0);
                    break;

                case 10:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(2048, 0);
                    break;

                case 11:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(2560, 0);
                    break;

                case 12:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(2560, 0);
                    break;

                case 13:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(3072, 0);
                    break;

                case 14:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(3072, 0);
                    break;

                case 15:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(3584, 0);
                    break;

                case 16:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(3584, 0);
                    break;

                case 17:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(4096, 0);
                    break;

                case 18:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(4096, 0);
                    break;

                case 19:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(4608, 0);
                    break;

                case 20:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(4608, 0);
                    break;

                case 21:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(5120, 0);
                    break;

                case 22:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(5120, 0);
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
        else if (viewedFleet == manager.mainFleetView || viewedFleet == null)
        {
            if (craftFleet.containedCraft.Count < 7)
            {
                switch (craftPosition)
                {

                    case 1:
                        rTransform.position = craftFleet.craftPositions4[0].transform.position;
                        break;

                    case 2:
                        rTransform.position = craftFleet.craftPositions4[1].transform.position;
                        break;

                    case 3:
                        rTransform.position = craftFleet.craftPositions4[0].transform.position + new Vector3(0, (-175 * 2));
                        break;

                    case 4:
                        rTransform.position = craftFleet.craftPositions4[1].transform.position + new Vector3(0, (-175 * 2));
                        break;

                    case 5:
                        this.gameObject.SetActive(false);
                        break;

                    case 6:
                        this.gameObject.SetActive(false);
                        break;

                    case 7:
                        this.gameObject.SetActive(false);
                        break;

                    case 8:
                        this.gameObject.SetActive(false);
                        break;


                }
            }
            else 
            {
                switch (craftPosition)
                {

                    case 1:
                        rTransform.position = craftFleet.craftPositions9[0].transform.position;
                        break;

                    case 2:
                        rTransform.position = craftFleet.craftPositions9[1].transform.position;
                        break;

                    case 3:
                        rTransform.position = craftFleet.craftPositions9[2].transform.position;
                        break;

                    case 4:
                        rTransform.position = craftFleet.craftPositions9[0].transform.position - new Vector3(0, 250);
                        break;

                    case 5:
                        rTransform.position = craftFleet.craftPositions9[1].transform.position - new Vector3(0, 250);
                        break;

                    case 6:
                        rTransform.position = craftFleet.craftPositions9[2].transform.position - new Vector3(0, 250);
                        break;

                    case 7:
                        rTransform.position = craftFleet.craftPositions9[0].transform.position - new Vector3(0, 250 * 2);
                        break;

                    case 8:
                        rTransform.position = craftFleet.craftPositions9[1].transform.position - new Vector3(0, 250 * 2);
                        break;

                    case 9:
                        rTransform.position = craftFleet.craftPositions9[2].transform.position - new Vector3(0, 250 * 2);
                        break;

                    case 10:
                        this.gameObject.SetActive(false);
                        break;

                    case 11:
                        this.gameObject.SetActive(false);
                        break;

                    case 12:
                        this.gameObject.SetActive(false);
                        break;

                    case 13:
                        this.gameObject.SetActive(false);
                        break;

                    case 14:
                        this.gameObject.SetActive(false);
                        break;

                    case 15:
                        this.gameObject.SetActive(false);
                        break;

                    case 16:
                        this.gameObject.SetActive(false);
                        break;

                    case 17:
                        this.gameObject.SetActive(false);
                        break;

                    case 18:
                        this.gameObject.SetActive(false);
                        break;

                    case 19:
                        this.gameObject.SetActive(false);
                        break;

                    case 20:
                        this.gameObject.SetActive(false);
                        break;

                }
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

    public void UIPressed(bool setting)
    {
        manager.UIPressed(setting);
    }

    //Sets the craft colour scheme
    public void TrooperColours()
    {
        craftImages[1].color = modManager.voidcraftManager.playerFleetColours[0];
        craftImages[2].color = modManager.voidcraftManager.playerFleetColours[1];
        craftImages[3].color = modManager.voidcraftManager.playerFleetColours[2];
        craftImages[4].color = modManager.voidcraftManager.playerFleetColours[3];
        craftImages[5].color = modManager.voidcraftManager.playerFleetColours[4];
    }

    //Returns the stat of the given string
    public string GetStat(string name)
    {
        string returner = "";
        switch (name)
        {
            case "Class":
                returner = craftClass.className;
                break;
            case "Speed":
                returner = craftClass.speed.ToString();
                break;
            case "Armour":
                returner = craftClass.armour.ToString();
                break;
            case "Location":
                string tempS;
                foreach(System_Script id in manager.modManager.sectorManager.systems)
                {
                    if(id.Star.uID == craftClass.starID)
                    {
                        tempS = id.Star.systemName;
                        if (craftClass.planetN > 0)
                        {
                            tempS += " " + (id.SystemPlanets[craftClass.planetN - 1].planetName.Replace(tempS, ""));
                        }
                        returner = tempS;
                        break;
                    }
                }
                //returner = craftClass.armour.ToString();
                break;
            default:
                return "0";
        }
        return returner;
    }

    public List<int> GetCarriedSlots()
    {
        List<int> returner = new List<int>();

        returner = craftClass.uIDTransported;

        return returner;
    }

    public void MoveShip(Planet_Script newPlanet)
    {
        craftClass.planetN = newPlanet.parentSystem.SystemPlanets.IndexOf(newPlanet) + 1;
        planetLocation.text = GetStat("Location");
    }
}
