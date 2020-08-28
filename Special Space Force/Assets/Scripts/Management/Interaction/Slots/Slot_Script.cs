using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot_Script : MonoBehaviour
{
    public List<Slot_Script> containedSlots;
    public List<Trooper_Script> containedTroopers;
    public string slotName;
    public Slot_Class slotClass;
    public Slot_Script slotParent;
    public Transform firstPosition;
    public RectTransform background;
    public int ID;
    public InputField input;
    public Slot_Manager manager;

    public GameObject[] positions4;
    public GameObject[] positions6;
    public GameObject[] positions9;

    public int slotHeight;

    public void MakeSlot(string newSlotName, int newSlotHeight, Slot_Manager nManager)
    {
        slotClass = new Slot_Class();
        slotClass.slotName = newSlotName;
        slotClass.slotHeight = newSlotHeight;
        slotName = newSlotName;
        slotHeight = newSlotHeight;
        containedSlots = new List<Slot_Script>();
        containedTroopers = new List<Trooper_Script>();
        input.text = slotName;
        manager = nManager;
    }

    public void MakeSlot(Slot_Class slot, int positionID, Slot_Manager nManager)
    {
        slotClass = slot;
        slotName = slot.slotName;
        slotHeight = slot.slotHeight;

        ID = positionID;
        input.text = slotName;
        manager = nManager;
    }

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
    }

    public void AddContainedSlots(List<Slot_Script> slotList)
    {
        containedSlots = slotList;
    }

    public void AddContainedTroopers(List<Trooper_Script> trooperList)
    {
        containedTroopers = trooperList;
    }

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

        //Set Scale depending on its size compared too parent (anything else should not appear)
        switch (viewedSlot.slotHeight - slotHeight)
        {
            case -2:
                if (nSlots < 5)
                {
                    transform.localScale += new Vector3(-((transform.localScale.x / 100) * 60), -((transform.localScale.y / 100) * 60));
                    input.transform.localScale = new Vector3(1.4f, 1.4f);
                    input.textComponent.transform.localScale = new Vector3(1.4f, 1.4f);

                    switch (ID)
                    {

                        case 1:
                            rTransform.position = parent.positions4[0].transform.position;
                            break;

                        case 2:
                            rTransform.position = parent.positions4[1].transform.position;
                            break;

                        case 3:
                            rTransform.position = parent.positions4[2].transform.position;
                            break;

                    }
                }
                if (nSlots >= 5 && nSlots < 7)
                {
                    transform.localScale += new Vector3(-((transform.localScale.x / 100) * 70), -((transform.localScale.y / 100) * 70));
                    input.transform.localScale = new Vector3(1.5f, 1.5f);
                    input.textComponent.transform.localScale = new Vector3(1.5f, 1.5f);

                    switch (ID)
                    {
                        case 1:
                            rTransform.position = parent.positions6[0].transform.position;
                            break;

                        case 2:
                            rTransform.position = parent.positions6[1].transform.position;
                            break;

                        case 3:
                            rTransform.position = parent.positions6[2].transform.position;
                            break;

                        case 4:
                            rTransform.position = parent.positions6[3].transform.position;
                            break;

                        case 5:
                            rTransform.position = parent.positions6[4].transform.position;
                            break;

                        case 6:
                            rTransform.position = parent.positions6[5].transform.position;
                            break;

                    }
                }
                if (nSlots >= 7)
                {
                    transform.localScale += new Vector3(-((transform.localScale.x / 100) * 71), -((transform.localScale.y / 100) * 71));
                    input.transform.localScale = new Vector3(1.6f, 1.6f);
                    input.textComponent.transform.localScale = new Vector3(1.6f, 1.6f);

                    switch (ID)
                    {
                        case 1:
                            rTransform.position = parent.positions9[0].transform.position;
                            break;

                        case 2:
                            rTransform.position = parent.positions9[1].transform.position;
                            break;

                        case 3:
                            rTransform.position = parent.positions9[2].transform.position;
                            break;

                        case 4:
                            rTransform.position = parent.positions9[3].transform.position;
                            break;

                        case 5:
                            rTransform.position = parent.positions9[4].transform.position;
                            break;

                        case 6:
                            rTransform.position = parent.positions9[5].transform.position;
                            break;

                        case 7:
                            rTransform.position = parent.positions9[6].transform.position;
                            break;

                        case 8:
                            rTransform.position = parent.positions9[7].transform.position;
                            break;

                        case 9:
                            rTransform.position = parent.positions9[8].transform.position;
                            break;

                    }
                }
                input.enabled = true;
                gameObject.GetComponent<Image>().enabled = true;
                background.gameObject.SetActive(true);
                break;
            case -3:
                if (nSlots < 5)
                {
                    transform.localScale += new Vector3(-((transform.localScale.x / 100) * 60), -((transform.localScale.y / 100) * 60));
                    //input.transform.localScale = new Vector3(2.4f, 2.4f);
                    //input.textComponent.transform.localScale = new Vector3(2.4f, 2.4f);
                    input.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0f, 300f);

                    switch (ID)
                    {

                        case 1:
                            rTransform.position = parent.positions4[0].transform.position;
                            break;

                        case 2:
                            rTransform.position = parent.positions4[1].transform.position;
                            break;

                        case 3:
                            rTransform.position = parent.positions4[2].transform.position;
                            break;

                    }
                }
                if (nSlots >= 5 && nSlots < 7)
                {
                    transform.localScale += new Vector3(-((transform.localScale.x / 100) * 70), -((transform.localScale.y / 100) * 70));
                    //input.transform.localScale = new Vector3(2.3f, 2.3f);
                    //input.textComponent.transform.localScale = new Vector3(2.3f, 2.3f);
                    input.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0f, 300f);

                    switch (ID)
                    {
                        case 1:
                            rTransform.position = parent.positions6[0].transform.position;
                            break;

                        case 2:
                            rTransform.position = parent.positions6[1].transform.position;
                            break;

                        case 3:
                            rTransform.position = parent.positions6[2].transform.position;
                            break;

                        case 4:
                            rTransform.position = parent.positions6[3].transform.position;
                            break;

                        case 5:
                            rTransform.position = parent.positions6[4].transform.position;
                            break;

                        case 6:
                            rTransform.position = parent.positions6[5].transform.position;
                            break;

                    }
                }
                if (nSlots >= 7)
                {
                    transform.localScale += new Vector3(-((transform.localScale.x / 100) * 71), -((transform.localScale.y / 100) * 71));
                    //input.transform.localScale = new Vector3(2.2f, 2.2f);
                    //input.textComponent.transform.localScale = new Vector3(2.2f, 2.2f);
                    input.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0f, 300f);

                    switch (ID)
                    {
                        case 1:
                            rTransform.position = parent.positions9[0].transform.position;
                            break;

                        case 2:
                            rTransform.position = parent.positions9[1].transform.position;
                            break;

                        case 3:
                            rTransform.position = parent.positions9[2].transform.position;
                            break;

                        case 4:
                            rTransform.position = parent.positions9[3].transform.position;
                            break;

                        case 5:
                            rTransform.position = parent.positions9[4].transform.position;
                            break;

                        case 6:
                            rTransform.position = parent.positions9[5].transform.position;
                            break;

                        case 7:
                            rTransform.position = parent.positions9[6].transform.position;
                            break;

                        case 8:
                            rTransform.position = parent.positions9[7].transform.position;
                            break;

                        case 9:
                            rTransform.position = parent.positions9[8].transform.position;
                            break;

                    }
                }
                input.enabled = true;
                gameObject.GetComponent<Image>().enabled = true;
                background.gameObject.SetActive(true);
                break;
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

        if (slotHeight == viewedSlot.slotHeight)
        {
            foreach (Slot_Script ss in containedSlots)
            {
                ss.SetPosition(manager.slotN1.GetComponent<Slot_Script>(), viewedSlot);
                gameObject.SetActive(true);
            }
            rTransform.localPosition = new Vector3(0, -30);
            gameObject.transform.localScale = new Vector3(1, 1);
            input.gameObject.SetActive(false);
            gameObject.GetComponent<Image>().enabled = false;
            background.gameObject.SetActive(false);
        }
        else if(slotHeight == 0 && viewedSlot.slotHeight == -1)
        {
            SetPosition(manager.slotN1.GetComponent<Slot_Script>(), viewedSlot);
        }
        else
        {
            foreach (Slot_Script ss in containedSlots)
            {
                ss.SetPosition(ss.slotParent, ss.slotParent.containedSlots.Count, viewedSlot);
            }
        }

        if (slotParent == viewedSlot || slotParent.slotParent == viewedSlot || slotParent.slotParent.slotParent == viewedSlot)
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
    }

    public void SetPosition(Slot_Script parent, Slot_Script viewedSlot)
    {
        RectTransform rTransform = GetComponent<RectTransform>();
        switch (ID)
        {

            case 1:
                rTransform.position = parent.positions6[0].transform.position + new Vector3(0,0);
                break;

            case 2:
                rTransform.position = parent.positions6[0].transform.position + new Vector3(rTransform.rect.width + 25, 0);
                break;

            case 3:
                rTransform.position = parent.positions6[0].transform.position + new Vector3((rTransform.rect.width * 2) + 25, 0);
                break;

            case 4:
                rTransform.position = parent.positions6[0].transform.position + new Vector3((rTransform.rect.width * 3) + 25, 0);
                break;

            case 5:
                rTransform.position = parent.positions6[0].transform.position + new Vector3((rTransform.rect.width * 4) + 25, 0);
                break;

            case 6:
                rTransform.position = parent.positions6[0].transform.position + new Vector3((rTransform.rect.width * 5) + 25, 0);
                break;

        }
        gameObject.transform.localScale = new Vector3(1, 1);
        input.transform.localScale = new Vector3(1, 1);
        input.textComponent.transform.localScale = new Vector3(1, 1);

        gameObject.transform.localScale = new Vector3(1, 1);

        foreach (Slot_Script ss in containedSlots)
        {
            ss.gameObject.transform.localScale = new Vector3(1, 1);
            ss.SetPosition(this, containedSlots.Count, viewedSlot);
        }

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
    }

    public Slot_Class MasterSaveClass()
    {
        slotClass = new Slot_Class();
        slotClass.slotHeight = slotHeight;
        slotClass.containedSlots = new List<Slot_Class>();

        foreach (Slot_Script ss in containedSlots)
        {
            slotClass.containedSlots.Add(ss.SaveClass());
        }

        return slotClass;
    }

    public Slot_Class SaveClass()
    {
        slotClass = new Slot_Class();
        slotClass.slotName = slotName;
        slotClass.slotHeight = slotHeight;
        slotClass.positionID = ID;

        slotClass.containedSlots = new List<Slot_Class>();

        foreach (Slot_Script ss in containedSlots)
        {
            slotClass.containedSlots.Add(ss.SaveClass());
        }

        return slotClass;
    }
}
