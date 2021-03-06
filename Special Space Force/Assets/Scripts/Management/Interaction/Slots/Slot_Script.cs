using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot_Script : MonoBehaviour
{
    /// <summary>
    /// This Script holds the information for a single slot, but also contains Lists of slots and troopers stored within it.
    /// </summary>
    
    public List<Slot_Script> containedSlots;
    public List<Trooper_Script> containedTroopers;
    public string slotName;
    public Slot_Class slotClass;
    public Slot_Script slotParent;
    public Transform firstPosition;
    public RectTransform background;
    public int ID;
    public int uID; // Unique ID given on creation
    public TMP_InputField input;
    public TextMeshProUGUI nTroops;
    public Text location;
    public Slot_Manager manager;
    public bool squad;
    public Squad_Role_Class squadRole;
    private int craftID;
    private int systemID;
    private int planetN;

    public GameObject[] positions4;
    public GameObject[] positions6;
    public GameObject[] positions9;
    public GameObject[] squadPositions;

    public Image addImage;

    public int slotHeight;
    public bool cColours = false;

    //Creates a slot from blank with just a name and height
    public void MakeSlot(string newSlotName, int newSlotHeight, Slot_Manager nManager)
    {
        manager = nManager;
        slotClass = new Slot_Class();
        slotClass.slotName = newSlotName;
        slotClass.slotHeight = newSlotHeight;
        slotName = newSlotName;
        slotHeight = newSlotHeight;
        containedSlots = new List<Slot_Script>();
        containedTroopers = new List<Trooper_Script>();
        squad = false;
        squadRole = manager.modManager.rankManager.squadRoles[0];
        input.text = slotName;
        if (uID == 0)
        {
            uID = Random.Range(1, 10000000);
        }
    }

    //Creates a slot from a slot class
    public void MakeSlot(Slot_Class slot, int positionID, Slot_Manager nManager)
    {
        manager = nManager;
        slotClass = slot;
        slotHeight = slot.slotHeight;
        containedSlots = new List<Slot_Script>();
        squad = slot.squad;
        squadRole = manager.modManager.rankManager.squadRoles[0];
        ID = positionID;
        slotClass.positionID = ID;
        if (slot.slotName != null)
        {
            slotName = slot.slotName;
            slotClass.slotName = slotName;
        }
        else
        {
            slotName = manager.manager.localisationManager.CreateName("SlotNames", this);
            slotClass.slotName = slotName;
        }

        input.text = slotName;

        while (manager.slotIDs.Contains(uID))
        {
            uID = Random.Range(1, 10000000);
        }

        manager.slotIDs.Add(uID);
        slotClass.uID = uID;

    }

    //Creates a slot from a slot script and inserts a new parent
    public void MakeSlot(Slot_Script slot, Slot_Script parent, Slot_Manager nManager)
    {
        manager = nManager;
        slotClass = slot.slotClass;
        slotHeight = slot.slotHeight;
        ID = slot.ID;
        slotParent = parent;
        background = slot.background;
        containedSlots = slot.containedSlots;
        containedTroopers = slot.containedTroopers;

        squad = slot.squad;
        squadRole = slot.squadRole;
        slotName = manager.manager.localisationManager.CreateName("SlotNames", this);
        slot.slotName = slotName;
        slotClass.slotName = slotName;
        input.text = slotName;

        while (manager.slotIDs.Contains(uID))
        {
            uID = Random.Range(1, 10000000);
        }

        slotClass.squadColours = new List<Color32>();

        foreach (Color c in manager.modManager.equipmentManager.playerDefaultColours)
        {
            slotClass.squadColours.Add(c);
        }

        manager.slotIDs.Add(uID);
        slotClass.uID = uID;
    }

    //Creates a slot from a slot class
    public void LoadSlot(Slot_Class slot, int positionID, Slot_Manager nManager)
    {
        manager = nManager;
        slotClass = slot;
        slotHeight = slot.slotHeight;
        containedSlots = new List<Slot_Script>();
        squad = slot.squad;
        squadRole = manager.modManager.rankManager.squadRoles[slot.squadRole];
        ID = slotClass.positionID;
        slotName = slot.slotName;
        cColours = slot.useSquadColours;

        input.text = slotName;
        uID = slotClass.uID;

        slotClass.uID = uID;

        FindLocation();
    }

    //Creates a slot from a slot class
    public void LoadSlotSimple(Slot_Class slot, int positionID, Slot_Manager nManager)
    {
        slotClass = slot;
        slotHeight = slot.slotHeight;
        squad = slot.squad;
        squadRole = nManager.modManager.rankManager.squadRoles[slot.squadRole];
        ID = slotClass.positionID;
        slotName = slot.slotName;

        uID = slotClass.uID;
    }

    //Creates a new unique ID
    public void RegenerateUID()
    {
        uID = Random.Range(1, 10000000);
    }

    //Replaces contained slots with a passed slot_script list
    public void AddContainedSlots(List<Slot_Script> slotList)
    {
        containedSlots = slotList;
    }

    //Replaces contained troopers with a passed trooper_script list
    public void AddContainedTroopers(List<Trooper_Script> trooperList)
    {
        containedTroopers = trooperList;
        slotClass.numberOfTroopers = trooperList.Count;
    }

    //Sets the position of the slot according to its slot height relative to the currently viewed slot
    public void SetPosition(Slot_Script parent, int height, Slot_Script viewedSlot)
    {
        RectTransform rTransform = GetComponent<RectTransform>();

        //Set to default showing
        gameObject.transform.localScale = new Vector3(1, 1);
        input.transform.localScale = new Vector3(1, 1);
        input.textComponent.transform.localScale = new Vector3(1, 1);
        input.gameObject.SetActive(true);
        gameObject.GetComponent<Image>().enabled = true;
        background.gameObject.SetActive(true);
        input.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0f, 400f);

        //Set Scale and position depending on its size compared too parent (anything else should not appear)
        switch (height)
        {
            //height 0 is the slot layer just below the selected slot
            case 0:
                switch (ID)
                {

                    case 1:
                        rTransform.localPosition = new Vector3(0, 0);
                        break;

                    case 2:
                        rTransform.localPosition = new Vector3(950, 0);
                        break;

                    case 3:
                        rTransform.localPosition = new Vector3(1900, 0);
                        break;

                    case 4:
                        rTransform.localPosition = new Vector3(2850, 0);
                        break;

                    case 5:
                        rTransform.localPosition = new Vector3(3800, 0);
                        break;

                    case 6:
                        rTransform.localPosition = new Vector3(4750, 0);
                        break;

                    case 7:
                        rTransform.localPosition = new Vector3(5700, 0);
                        break;

                    case 8:
                        rTransform.localPosition = new Vector3(6650, 0);
                        break;

                    case 9:
                        rTransform.localPosition = new Vector3(7600, 0);
                        break;

                    case 10:
                        rTransform.localPosition = new Vector3(8550, 0);
                        break;

                    case 11:
                        rTransform.localPosition = new Vector3(9500, 0);
                        break;

                    case 12:
                        rTransform.localPosition = new Vector3(10450, 0);
                        break;

                }
                nTroops.enabled = false;
                gameObject.transform.localScale = new Vector3(1, 1);
                input.transform.localScale = new Vector3(1, 1);
                input.textComponent.transform.localScale = new Vector3(1, 1);
                input.textComponent.fontSize = 36;
                input.textComponent.enableWordWrapping = true;
                input.GetComponent<RectTransform>().transform.localPosition = new Vector3(0, 400);
                input.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 60);

                gameObject.transform.localScale = new Vector3(1, 1);
                gameObject.GetComponent<Image>().color = new Color32(191, 191, 191, 100);
                break;
            //If the height is 1 (meaning it is a middle size slot)
            case 1:
                input.textComponent.enableWordWrapping = true;
                location.enabled = false;
                if (parent.containedSlots.Count < 5)
                {
                    switch (ID)
                    {

                        case 1:
                            rTransform.position = parent.positions4[0].transform.position;
                            break;

                        case 2:
                            rTransform.position = parent.positions4[0].transform.position + new Vector3(450, 0);
                            break;

                        case 3:
                            rTransform.position = parent.positions4[0].transform.position + new Vector3(0, -400);
                            break;

                        case 4:
                            rTransform.position = parent.positions4[0].transform.position + new Vector3(450, -400);
                            break;

                    }

                    transform.localScale += new Vector3(-((transform.localScale.x / 100) * 60), -((transform.localScale.y / 100) * 60));
                    input.transform.localScale = new Vector3(1.4f, 1.4f);
                    input.textComponent.transform.localScale = new Vector3(1.4f, 1.4f);
                    input.textComponent.fontSize = 36;
                    input.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 60);
                }
                if (parent.containedSlots.Count >= 5 && parent.containedSlots.Count < 7)
                {
                    switch (ID)
                    {
                        case 1:
                            rTransform.position = parent.positions6[0].transform.position;
                            break;

                        case 2:
                            rTransform.position = parent.positions6[0].transform.position + new Vector3(300, 0);
                            break;

                        case 3:
                            rTransform.position = parent.positions6[0].transform.position + new Vector3(600, 0);
                            break;

                        case 4:
                            rTransform.position = parent.positions6[0].transform.position + new Vector3(0, -400);
                            break;

                        case 5:
                            rTransform.position = parent.positions6[0].transform.position + new Vector3(300, -400);
                            break;

                        case 6:
                            rTransform.position = parent.positions6[0].transform.position + new Vector3(600, -400);
                            break;

                    }

                    transform.localScale += new Vector3(-((transform.localScale.x / 100) * 70), -((transform.localScale.y / 100) * 70));
                    input.transform.localScale = new Vector3(1.5f, 1.5f);
                    input.textComponent.transform.localScale = new Vector3(1.5f, 1.5f);
                    input.textComponent.fontSize = 36;
                    input.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 60);
                }
                if (parent.containedSlots.Count >= 7)
                {
                    switch (ID)
                    {
                        case 1:
                            rTransform.position = parent.positions9[0].transform.position;
                            break;

                        case 2:
                            rTransform.position = parent.positions9[0].transform.position + new Vector3(300, 0);
                            break;

                        case 3:
                            rTransform.position = parent.positions9[0].transform.position + new Vector3(600, 0);
                            break;

                        case 4:
                            rTransform.position = parent.positions9[0].transform.position + new Vector3(0, -265);
                            break;

                        case 5:
                            rTransform.position = parent.positions9[0].transform.position + new Vector3(300, -265);
                            break;

                        case 6:
                            rTransform.position = parent.positions9[0].transform.position + new Vector3(600, -265);
                            break;

                        case 7:
                            rTransform.position = parent.positions9[0].transform.position + new Vector3(0, -530);
                            break;

                        case 8:
                            rTransform.position = parent.positions9[0].transform.position + new Vector3(300, -530);
                            break;

                        case 9:
                            rTransform.position = parent.positions9[0].transform.position + new Vector3(600, -530);
                            break;

                        default:
                            input.gameObject.SetActive(false);
                            gameObject.GetComponent<Image>().enabled = false;
                            background.gameObject.SetActive(false);
                            break;
                    }

                    transform.localScale += new Vector3(-((transform.localScale.x / 100) * 71), -((transform.localScale.y / 100) * 71));
                    input.transform.localScale = new Vector3(1.6f, 1.6f);
                    input.textComponent.transform.localScale = new Vector3(1.6f, 1.6f);
                    input.textComponent.fontSize = 36;
                    input.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 60);
                }
                if (containedTroopers.Count != 0)
                {
                    nTroops.enabled = true;
                    nTroops.fontSize = 200;
                    nTroops.text = containedTroopers.Count.ToString();
                }
                else
                {
                    nTroops.enabled = false;
                }
                input.enabled = true;
                gameObject.GetComponent<Image>().color = new Color32(169, 169, 169,100);
                break;
            //If the height difference is -3 (meaning the slot is the smallest visible slot)
            case 2:
                input.textComponent.enableWordWrapping = true;
                location.enabled = false;
                if (parent.containedSlots.Count < 5)
                {
                    switch (ID)
                    {

                        case 1:
                            rTransform.localPosition = new Vector3(-225, 175);
                            break;

                        case 2:
                            rTransform.localPosition = new Vector3(225, 175);
                            break;

                        case 3:
                            rTransform.localPosition = new Vector3(-225, -225);
                            break;

                        case 4:
                            rTransform.localPosition = new Vector3(225, -225);
                            break;

                    }

                    transform.localScale += new Vector3(-((transform.localScale.x / 100) * 60), -((transform.localScale.y / 100) * 60));
                    input.transform.localScale = new Vector3(2.4f, 2.4f);
                    input.textComponent.transform.localScale = new Vector3(1, 1);
                    input.textComponent.fontSize = 72;
                    input.GetComponent<RectTransform>().transform.localPosition = new Vector3(0, 0);
                    input.GetComponent<RectTransform>().sizeDelta = new Vector2(375, 300);
                }
                if (parent.containedSlots.Count  >= 5 && parent.containedSlots.Count < 7)
                {
                    switch (ID)
                    {
                        case 1:
                            rTransform.localPosition = new Vector3(-300, 200);
                            break;

                        case 2:
                            rTransform.localPosition = new Vector3(0, 200);
                            break;

                        case 3:
                            rTransform.localPosition = new Vector3(300, 200);
                            break;

                        case 4:
                            rTransform.localPosition = new Vector3(-300, -200);
                            break;

                        case 5:
                            rTransform.localPosition = new Vector3(0, -200);
                            break;

                        case 6:
                            rTransform.localPosition = new Vector3(300, -200);
                            break;

                    }

                    transform.localScale += new Vector3(-((transform.localScale.x / 100) * 70), -((transform.localScale.y / 100) * 70));
                    input.transform.localScale = new Vector3(2.3f, 2.3f);
                    input.textComponent.transform.localScale = new Vector3(1, 1);
                    input.textComponent.fontSize = 72;
                    input.GetComponent<RectTransform>().transform.localPosition = new Vector3(0, 0);
                    input.GetComponent<RectTransform>().sizeDelta = new Vector2(375, 300);
                }
                if (parent.containedSlots.Count >= 7)
                {
                    switch (ID)
                    {
                        case 1:
                            rTransform.localPosition = new Vector3(-300, 240);
                            break;

                        case 2:
                            rTransform.localPosition = new Vector3(0, 240);
                            break;

                        case 3:
                            rTransform.localPosition = new Vector3(300, 240);
                            break;

                        case 4:
                            rTransform.localPosition = new Vector3(-300, -25);
                            break;

                        case 5:
                            rTransform.localPosition = new Vector3(0, -25);
                            break;

                        case 6:
                            rTransform.localPosition = new Vector3(300, -25);
                            break;

                        case 7:
                            rTransform.localPosition = new Vector3(-300, -290);
                            break;

                        case 8:
                            rTransform.localPosition = new Vector3(0, -290);
                            break;

                        case 9:
                            rTransform.localPosition = new Vector3(300, -290);
                            break;

                    }

                    transform.localScale += new Vector3(-((transform.localScale.x / 100) * 71), -((transform.localScale.y / 100) * 71));
                    input.transform.localScale = new Vector3(2.2f, 2.2f);
                    input.textComponent.transform.localScale = new Vector3(1, 1);
                    input.textComponent.fontSize = 72;
                    input.GetComponent<RectTransform>().transform.localPosition = new Vector3(0, 0);
                    input.GetComponent<RectTransform>().sizeDelta = new Vector2(375, 300);
                }
                if (containedTroopers.Count != 0)
                {
                    nTroops.enabled = true;
                    nTroops.fontSize = 200;
                    nTroops.text = containedTroopers.Count.ToString();
                } 
                else
                {
                    nTroops.enabled = false;
                }
                input.enabled = true;
                gameObject.GetComponent<Image>().color = new Color32(180, 180, 180, 100);
                break;
        }


        //If the slot should be visible (is child of the viewed slot or one of its children, or is a child of a child of the viewed slot(Mad eh?))
        //then it is set to visible here

        if (squad)
        {
            if (slotHeight == viewedSlot.slotHeight && slotParent == viewedSlot.slotParent)
            {
                foreach (Trooper_Script ts in containedTroopers)
                {
                    ts.SetPosition(manager.slotN1.GetComponent<Slot_Script>(), viewedSlot);
                    gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (Trooper_Script ts in containedTroopers)
                {
                    ts.SetPosition(this, viewedSlot);
                    gameObject.SetActive(true);
                }
            }
            addImage.enabled = false;
            gameObject.GetComponent<Image>().color = new Color32(120, 233, 136, 255);
        }

        if (uID == 0)
        {
            input.gameObject.SetActive(false);
            gameObject.GetComponent<Image>().enabled = false;
            background.gameObject.SetActive(false);
        }
    }

    //Sets the position and scale of the slot according to its slot height relative to the currently viewed slot
    public void SetPosition(Slot_Script parent, Slot_Script viewedSlot)
    {
        RectTransform rTransform = GetComponent<RectTransform>();

        switch (ID)
        {

            case 1:
                rTransform.localPosition = new Vector3(-4000, 24);
                break;

            case 2:
                rTransform.localPosition = new Vector3(-3050, 24);
                break;

            case 3:
                rTransform.localPosition = new Vector3(-2100, 24);
                break;

            case 4:
                rTransform.localPosition = new Vector3(-1150, 24);
                break;

            case 5:
                rTransform.localPosition = new Vector3(-200, 24);
                break;

            case 6:
                rTransform.localPosition = new Vector3(750, 24);
                break;

            case 7:
                rTransform.localPosition = new Vector3(1700, 24);
                break;

            case 8:
                rTransform.localPosition = new Vector3(2650, 24);
                break;

            case 9:
                rTransform.localPosition = new Vector3(3600, 24);
                break;

            case 10:
                rTransform.localPosition = new Vector3(4550, 24);
                break;

            case 11:
                rTransform.localPosition = new Vector3(5500, 0);
                break;

            case 12:
                rTransform.localPosition = new Vector3(6450, 0);
                break;

        }


        if (uID == 0)
        {
            input.gameObject.SetActive(false);
            gameObject.GetComponent<Image>().enabled = false;
            background.gameObject.SetActive(false);
        }
        nTroops.enabled = false;
        gameObject.transform.localScale = new Vector3(1, 1);
        input.transform.localScale = new Vector3(1, 1);
        input.textComponent.transform.localScale = new Vector3(1, 1);
        input.textComponent.fontSize = 36;
        input.textComponent.enableWordWrapping = true;
        input.GetComponent<RectTransform>().transform.localPosition = new Vector3(0, 400);
        input.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 60);

        gameObject.transform.localScale = new Vector3(1, 1);
        gameObject.GetComponent<Image>().color = new Color32(191, 191, 191,100);


        //If its parent is the viewed slot it is set visible here
        if (slotParent == viewedSlot)
        {
            input.gameObject.SetActive(true);
            gameObject.GetComponent<Image>().enabled = true;
            background.gameObject.SetActive(true);
        }
        else
        {
            input.gameObject.SetActive(false);
            gameObject.GetComponent<Image>().enabled = false;
            background.gameObject.SetActive(false);
        }

        if (squad)
        {
            foreach (Trooper_Script ts in containedTroopers)
            {
                ts.SetPosition(this, viewedSlot);
                gameObject.SetActive(true);
            }
            gameObject.GetComponent<Image>().color = new Color32(124, 171, 128, 255);
        } 
        else
        {
            //Sets children's positions with the first method
            foreach (Slot_Script ss in containedSlots)
            {
                ss.gameObject.transform.localScale = new Vector3(1, 1);
                ss.SetPosition(this, 0 + 1, viewedSlot);
            }
        }
    }
    //Sets the position and scale of the slot according to its slot height relative to the currently viewed slot
    public void SetPosition(Slot_Script parent, Slot_Script viewedSlot, int i)
    {
        RectTransform rTransform = GetComponent<RectTransform>();

        if (i == 0)
        {
            rTransform.localPosition = new Vector3(-4000, 24);
            input.gameObject.SetActive(false);
            gameObject.GetComponent<Image>().enabled = false;
            background.gameObject.SetActive(false);
        }
        else
        {

            switch (ID)
            {

                case 1:
                    rTransform.localPosition = new Vector3(-4000, 24);
                    break;

                case 2:
                    rTransform.localPosition = new Vector3(-3050, 24);
                    break;

                case 3:
                    rTransform.localPosition = new Vector3(-2100, 24);
                    break;

                case 4:
                    rTransform.localPosition = new Vector3(-1150, 24);
                    break;

                case 5:
                    rTransform.localPosition = new Vector3(-200, 24);
                    break;

                case 6:
                    rTransform.localPosition = new Vector3(750, 24);
                    break;

                case 7:
                    rTransform.localPosition = new Vector3(1700, 24);
                    break;

                case 8:
                    rTransform.localPosition = new Vector3(2650, 24);
                    break;

                case 9:
                    rTransform.localPosition = new Vector3(3600, 24);
                    break;

                case 10:
                    rTransform.localPosition = new Vector3(4550, 24);
                    break;

                case 11:
                    rTransform.localPosition = new Vector3(5500, 0);
                    break;

                case 12:
                    rTransform.localPosition = new Vector3(6450, 0);
                    break;

            }
        }

        if(uID == 0)
        {
            input.gameObject.SetActive(false);
            gameObject.GetComponent<Image>().enabled = false;
            background.gameObject.SetActive(false);
        }
        nTroops.enabled = false;
        gameObject.transform.localScale = new Vector3(1, 1);
        input.transform.localScale = new Vector3(1, 1);
        input.textComponent.transform.localScale = new Vector3(1, 1);
        input.textComponent.fontSize = 36;
        input.textComponent.enableWordWrapping = true;
        input.GetComponent<RectTransform>().transform.localPosition = new Vector3(0, 400);
        input.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 60);

        gameObject.transform.localScale = new Vector3(1, 1);
        gameObject.GetComponent<Image>().color = new Color32(191, 191, 191, 100);


        if (squad)
        {
            foreach (Trooper_Script ts in containedTroopers)
            {
                ts.SetPosition(this, viewedSlot);
                gameObject.SetActive(true);
            }
            gameObject.GetComponent<Image>().color = new Color32(124, 171, 128, 255);
        }
        else
        {
            //Sets children's positions with the first method
            foreach (Slot_Script ss in containedSlots)
            {
                ss.gameObject.transform.localScale = new Vector3(1, 1);
                ss.SetPosition(this, 0 + 1, viewedSlot);
            }
        }
    }

    //Master Script save code that only runs on the top script. Forces the saving of all other scripts below it.
    public Slot_Class MasterSaveClass()
    {
        slotClass = new Slot_Class();
        slotClass.slotHeight = slotHeight;
        slotClass.containedSlots = new List<Slot_Class>();
        slotClass.squad = squad;

        foreach (Slot_Script ss in containedSlots)
        {
            slotClass.containedSlots.Add(ss.SaveClass());
        }

        return slotClass;
    }

    //Saves the Script as a Class
    public Slot_Class SaveClass()
    {
        slotClass = new Slot_Class();
        slotClass.slotName = slotName;
        slotClass.slotHeight = slotHeight;
        slotClass.positionID = ID;
        slotClass.uID = uID;
        slotClass.squad = squad;
        slotClass.useSquadColours = cColours;
        craftID = slotClass.craftID;
        systemID = slotClass.systemID;
        planetN = slotClass.planetN;
        slotClass.craftID = craftID;
        slotClass.systemID = systemID;
        slotClass.planetN = planetN;
        if (slotClass.squad == true)
        {
            slotClass.numberOfTroopers = slotClass.containedTroopers.Count;
        }



        return slotClass;
    }

    //
    public void Add()
    {
        if (!squad)
        {
            manager.NewSlot(this);
        }
    }

    //
    public void Delete()
    {
        manager.speakerScript2.PlaySound();
        manager.DeleteSlot(this);
    }

    //Sets the squad name according to the input
    public void SetName(TMP_Text nName)
    {
        slotName = nName.text;
        input.text = nName.text;
        slotClass.slotName = nName.text;
    }

    //Sets the squad name according to the input
    public void SetName(string nName)
    {
        slotName = nName;
        input.text = nName;
        slotClass.slotName = nName;
    }

    //Sets the manager UI pressed value, so that the user can interact easier with the name
    public void NameSelected()
    {
        manager.menu = true;
    }

    //De-sets the manager UI pressed value, so that the user can interact easier with the name
    public void NameDeselected()
    {
        manager.menu = false;
    }

    //Changes the slot height 
    public void ChangeHeight(int Height)
    {
        slotHeight = Height + 1;
        foreach(Slot_Script ss in containedSlots)
        {
            ss.ChangeHeight(slotHeight);
        }
        slotClass.slotHeight = Height + 1;
    }

    public int GetLocationIDCraft()
    {
        int returner = -1;

        if(slotClass.craftID != 0)
        {
            returner = slotClass.craftID;
        }

        return returner;
    }

    public int GetLocationIDSystem()
    {
        int returner = -1;

        if (slotClass.systemID != 0)
        {
            returner = slotClass.systemID;
        }

        return returner;
    }

    public int GetLocationPlanetN()
    {
        int returner = -1;

        if (slotClass.planetN != 0)
        {
            returner = slotClass.planetN;
        }

        return returner;
    }

    public string FindLocation()
    {
        string tempS = "Location: ";
        if (slotClass.systemID != 0)
        {
            foreach (System_Script id in manager.modManager.sectorManager.systems)
            {
                if (id.Star.uID == slotClass.systemID)
                {
                    tempS += id.Star.systemName;
                    if (slotClass.planetN > 0)
                    {
                        tempS += " " + (id.SystemPlanets[slotClass.planetN - 1].planetName.Replace(id.Star.systemName, ""));
                    }
                    location.text = tempS;
                    break;
                }
            }
        }
        else if(slotClass.craftID != 0)
        {
            foreach (Voidcraft_Class id in manager.modManager.fManager.Craft)
            {
                if (id.ID == slotClass.craftID)
                {
                    tempS += id.craftName;
                    location.text = tempS;
                    break;
                }
            }
        }
        else
        {
            tempS += "Mixed";
            location.text = tempS;
        }
        return tempS;
    }

    public void GetDefaultSquadColours()
    {
        if (squad)
        {
            slotClass.squadColours = new List<Color32>(new Color32[19]);

            slotClass.squadColours[0] = manager.modManager.equipmentManager.playerDefaultColours[10];
            slotClass.squadColours[1] = manager.modManager.equipmentManager.playerDefaultColours[11];
            slotClass.squadColours[2] = manager.modManager.equipmentManager.playerDefaultColours[12];
            slotClass.squadColours[3] = manager.modManager.equipmentManager.playerDefaultColours[13];
            slotClass.squadColours[4] = manager.modManager.equipmentManager.playerDefaultColours[0];
            slotClass.squadColours[5] = manager.modManager.equipmentManager.playerDefaultColours[1];
            slotClass.squadColours[6] = manager.modManager.equipmentManager.playerDefaultColours[2];
            slotClass.squadColours[7] = manager.modManager.equipmentManager.playerDefaultColours[4];
            slotClass.squadColours[8] = manager.modManager.equipmentManager.playerDefaultColours[3];
            slotClass.squadColours[9] = manager.modManager.equipmentManager.playerDefaultColours[5];
            slotClass.squadColours[10] = manager.modManager.equipmentManager.playerDefaultColours[6];
            slotClass.squadColours[11] = manager.modManager.equipmentManager.playerDefaultColours[7];
            slotClass.squadColours[12] = manager.modManager.equipmentManager.playerDefaultColours[9];
            slotClass.squadColours[13] = manager.modManager.equipmentManager.playerDefaultColours[8];
            slotClass.squadColours[14] = manager.modManager.equipmentManager.playerDefaultColours[14];
            slotClass.squadColours[15] = manager.modManager.equipmentManager.playerDefaultColours[15];
            slotClass.squadColours[16] = manager.modManager.equipmentManager.playerDefaultColours[16];
            slotClass.squadColours[17] = manager.modManager.equipmentManager.playerDefaultColours[17];
            slotClass.squadColours[18] = manager.modManager.equipmentManager.playerDefaultColours[18];
        }
    }

}
