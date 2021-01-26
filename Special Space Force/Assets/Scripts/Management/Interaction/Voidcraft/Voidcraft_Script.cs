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

    public Image_Manager imageManager;

    public void MakeCraft(Voidcraft_Class craft, Fleet_Manager fm, int ID, Fleet_Script fleet)
    {
        manager = fm;
        craftClass = craft;
        craft.positionID = ID;
        uID = Random.Range(0, 10000);
        while (manager.CraftIDs.Contains(uID))
        {
            uID = Random.Range(0, 10000);
        }
        craft.ID = uID;
        manager.CraftIDs.Add(uID);
        craftPosition = ID;
        craftFleet = fleet;
        modManager.voidcraftManager.LoadCraft(this, craft.className);

        craftName = craft.craftName;
        input.text = craftName;
        CraftColours();
    }

    public void MakeCraft(Voidcraft_Class craft, Manager_Script m, Fleet_Manager fm, int ID, Fleet_Script fleet)
    {
        modManager = m;
        craftClass = craft;
        manager = fm;
        craft.positionID = ID;
        uID = Random.Range(0, 10000);
        while (manager.CraftIDs.Contains(uID))
        {
            uID = Random.Range(0, 10000);
        }
        craft.ID = uID;
        manager.CraftIDs.Add(uID);
        craftPosition = ID;
        craftFleet = fleet;
        modManager.voidcraftManager.LoadCraft(this, craft.className);

        craftName = craft.craftName;
        input.text = craftName;
        CraftColours();
    }

    public void LoadCraft(Voidcraft_Class craft, Manager_Script m, Fleet_Manager fm, Fleet_Script fleet)
    {
        modManager = m;
        craftClass = craft;
        manager = fm;
        uID = craft.ID;
        manager.CraftIDs.Add(uID);
        craftPosition = craft.positionID;
        craftFleet = fleet;
        modManager.voidcraftManager.LoadCraft(this, craft.className);

        craftName = craft.craftName;
        input.text = craftName;
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
            switch (craftPosition)
            {

                case 1:
                    rTransform.position = craftFleet.craftPositions[0].transform.position;
                    break;

                case 2:
                    rTransform.position = craftFleet.craftPositions[1].transform.position;
                    break;

                case 3:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(300, 0);
                    break;

                case 4:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(300, 0);
                    break;

                case 5:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(600, 0);
                    break;

                case 6:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(600, 0);
                    break;

                case 7:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(900, 0);
                    break;

                case 8:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(900, 0);
                    break;

                case 9:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(1200, 0);
                    break;

                case 10:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(1200, 0);
                    break;

                case 11:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(1500, 0);
                    break;

                case 12:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(1500, 0);
                    break;

                case 13:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(1800, 0);
                    break;

                case 14:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(1800, 0);
                    break;

                case 15:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(2100, 0);
                    break;

                case 16:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(2100, 0);
                    break;

                case 17:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(2400, 0);
                    break;

                case 18:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(2400, 0);
                    break;

                case 19:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(2700, 0);
                    break;

                case 20:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(2700, 0);
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
        else if (viewedFleet == null)
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
}
