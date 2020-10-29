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
    public Slot_Manager manager;
    public bool squad;
    public int squadRole;

    public GameObject[] positions4;
    public GameObject[] positions6;
    public GameObject[] positions9;
    public GameObject[] squadPositions;

    public Image addImage;

    public int slotHeight;

    //Creates a slot from blank with just a name and height
    public void MakeSlot(string newSlotName, int newSlotHeight, Slot_Manager nManager)
    {
        slotClass = new Slot_Class();
        slotClass.slotName = newSlotName;
        slotClass.slotHeight = newSlotHeight;
        slotName = newSlotName;
        slotHeight = newSlotHeight;
        containedSlots = new List<Slot_Script>();
        containedTroopers = new List<Trooper_Script>();
        squad = false;
        squadRole = 0;
        input.text = slotName;
        manager = nManager;
        if (uID == 0)
        {
            uID = Random.Range(1, 10000000);
        }
    }

    //Creates a slot from a slot class
    public void MakeSlot(Slot_Class slot, int positionID, Slot_Manager nManager)
    {
        slotClass = slot;
        slotName = slot.slotName;
        slotHeight = slot.slotHeight;
        containedSlots = new List<Slot_Script>();
        squad = slot.squad;
        squadRole = slot.squadRole;

        ID = positionID;
        input.text = slotName;
        manager = nManager;
        if (uID == 0)
        {
            uID = Random.Range(1, 10000000);
        }
    }

    //Creates a slot from a slot script and inserts a new parent
    public void MakeSlot(Slot_Script slot, Slot_Script parent, Slot_Manager nManager)
    {
        slotClass = slot.slotClass;
        slotName = slot.slotName;
        slotHeight = slot.slotHeight;
        ID = slot.ID;
        slotParent = parent;
        input.text = slotName;
        manager = nManager;
        background = slot.background;
        containedSlots = slot.containedSlots;
        containedTroopers = slot.containedTroopers;
        if (uID == 0)
        {
            uID = Random.Range(1, 10000000);
        }
        squad = slot.squad;
        squadRole = slot.squadRole;
    }

    //Creates a new unique ID
    public void RegenerateUID()
    {
        uID = Random.Range(1, 10000000);
    }

    public void AddContainedSlots(List<Slot_Script> slotList)
    {
        containedSlots = slotList;
    }

    public void AddContainedTroopers(List<Trooper_Script> trooperList)
    {
        containedTroopers = trooperList;
    }

    //Sets the position of the slot according to its slot height relative to the currently viewed slot
    public void SetPosition(Slot_Script parent, int nSlots, Slot_Script viewedSlot)
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
        switch (viewedSlot.slotHeight - slotHeight)
        {
            //If the height difference is -2 (meaning it is a middle size slot)
            case -2:
                input.textComponent.enableWordWrapping = true;
                if (nSlots < 5)
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
                if (nSlots >= 5 && nSlots < 7)
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
                if (nSlots >= 7)
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
            case -3:
                input.textComponent.enableWordWrapping = true;
                if (nSlots < 5)
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
                if (nSlots >= 5 && nSlots < 7)
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
                if (nSlots >= 7)
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
                } else
                {
                    nTroops.enabled = false;
                }
                input.enabled = true;
                gameObject.GetComponent<Image>().color = new Color32(180, 180, 180, 100);
                break;
            //By default, hide the slot and return positions to neutral
            default:
                if (viewedSlot.slotHeight - slotHeight > 0)
                {
                    input.gameObject.SetActive(false);
                    gameObject.GetComponent<Image>().enabled = false;
                    background.gameObject.SetActive(false);
                    rTransform.localPosition = new Vector3(0, 0);
                }
                else
                {
                    input.gameObject.SetActive(false);
                    gameObject.GetComponent<Image>().enabled = false;
                    background.gameObject.SetActive(false);
                    rTransform.localPosition = new Vector3(0, 0);
                }
                break;
        }

        //If height is the same, set its children using the second method
        if (slotHeight == viewedSlot.slotHeight)
        {
            foreach (Slot_Script ss in containedSlots)
            {
                ss.SetPosition(manager.slotN1.GetComponent<Slot_Script>(), viewedSlot);
                gameObject.SetActive(true);
            } 
            rTransform.localPosition = new Vector3(0, 0);
            gameObject.transform.localScale = new Vector3(1, 1);
            nTroops.gameObject.SetActive(false);
            input.gameObject.SetActive(false);
            gameObject.GetComponent<Image>().enabled = false;
            background.gameObject.SetActive(false);
        }
        //If viewing the top slot, sets the positions of 0 height slots using the second method (They aren't caught by above code individually
        else if(slotHeight == 0 && viewedSlot.slotHeight == -1)
        {
            SetPosition(manager.slotN1.GetComponent<Slot_Script>(), viewedSlot);
        }
        else //else set its children with this method
        {
            foreach (Slot_Script ss in containedSlots)
            {
                ss.SetPosition(ss.slotParent, ss.slotParent.containedSlots.Count, viewedSlot);
            }
        }

        //If the slot should be visible (is child of the viewed slot or one of its children, or is a child of a child of the viewed slot(Mad eh?))
        //then it is set to visible here
        if (slotParent == viewedSlot || slotParent.slotParent == viewedSlot || slotParent.slotParent.slotParent == viewedSlot)
        {
            input.gameObject.SetActive(true);
            gameObject.GetComponent<Image>().enabled = true;
            background.gameObject.SetActive(true);
        } 
        else
        {
            nTroops.enabled = false;
            input.gameObject.SetActive(false);
            gameObject.GetComponent<Image>().enabled = false;
            background.gameObject.SetActive(false);
        }

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
        }
    }

    //Sets the position and scale of the slot according to its slot height relative to the currently viewed slot
    public void SetPosition(Slot_Script parent, Slot_Script viewedSlot)
    {
        RectTransform rTransform = GetComponent<RectTransform>();
        switch (ID)
        {

            case 1:
                rTransform.localPosition = new Vector3(-420,0);
                break;

            case 2:
                rTransform.localPosition = new Vector3(530, 0);
                break;

            case 3:
                rTransform.localPosition = new Vector3(1480, 0);
                break;

            case 4:
                rTransform.localPosition = new Vector3(2430, 0);
                break;

            case 5:
                rTransform.localPosition = new Vector3(3380, 0);
                break;

            case 6:
                rTransform.localPosition = new Vector3(4330, 0);
                break;

            case 7:
                rTransform.localPosition = new Vector3(5280, 0);
                break;

            case 8:
                rTransform.localPosition = new Vector3(6230, 0);
                break;

            case 9:
                rTransform.localPosition = new Vector3(7180, 0);
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
        } else
        {
            //Sets children's positions with the first method
            foreach (Slot_Script ss in containedSlots)
            {
                ss.gameObject.transform.localScale = new Vector3(1, 1);
                ss.SetPosition(this, containedSlots.Count, viewedSlot);
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
        slotClass.squad = squad;

        slotClass.containedSlots = new List<Slot_Class>();

        foreach (Slot_Script ss in containedSlots)
        {
            slotClass.containedSlots.Add(ss.SaveClass());
        }

        return slotClass;
    }

    public void Add()
    {
        if (!squad)
        {
            manager.NewSlot(this);
        }
    }

    public void Delete()
    {
        manager.DeleteSlot(this);
    }

    public void SetName(TMP_Text nName)
    {
        slotName = nName.text;
        input.text = nName.text;
        manager.SetDropdown();
    }

    public void SetName(string nName)
    {
        slotName = nName;
        input.text = nName;
    }

    public void NameSelected()
    {
        manager.menu = true;
    }

    public void NameDeselected()
    {
        manager.menu = false;
    }

    public void ChangeHeight(int Height)
    {
        slotHeight = Height + 1;
        foreach(Slot_Script ss in containedSlots)
        {
            ss.ChangeHeight(slotHeight);
        }
    }
}
