using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Voidcraft_Indepth_Manager : MonoBehaviour
{
    public Manager_Script manager;
    public System_Voidcraft_Script sManager;
    public Fleet_Manager fManager;
    public List<Advanced_System_Craft> craft;
    public System_Class currentSystem;

    public GameObject content;
    public GameObject prefabCraft;

    public GameObject craftSpace1;

    List<string> nOptions;

    public void OpenSystem(System_Class system)
    {
        currentSystem = system;
        foreach (Voidcraft_Class vc in fManager.Craft)
        {
            if (vc.starID == system.uID)
            {
                GameObject temp = Instantiate(prefabCraft, content.transform);
                Advanced_System_Craft tempS = temp.GetComponent<Advanced_System_Craft>();
                tempS.Create(vc, sManager, this);
                temp.transform.position = craftSpace1.transform.position + new Vector3(0, -200 * craft.Count, 0);
                craft.Add(tempS);
            }
        }
    }

    public void SetupLoaderDropdowns(Dropdown Load, Dropdown Unload, Advanced_System_Craft voidcraft)
    {
        if (currentSystem.allegiance == 0)
        {
            Load.ClearOptions();
            nOptions = new List<string>();

            nOptions.Add("None");

            foreach (Slot_Class sc in manager.sManager.slotN1.GetComponent<Slot_Script>().slotClass.containedSlots)
            {
                if (sc.systemID != 0)
                {
                    if (sc.systemID == currentSystem.uID)
                    {
                        if (sc.numberOfTroopers < voidcraft.linkedCraft.capacity)
                        {
                            nOptions.Add("-" + sc.slotName);
                            voidcraft.availableSlots.Add(sc);
                        }
                        CheckOption(sc, voidcraft, "");
                    }
                    else
                    {
                        CheckOption(sc, voidcraft, "");
                    }
                }
                else
                {
                    CheckOption(sc, voidcraft, "");
                }
            }

            if (nOptions.Count > 0)
            {
                Load.AddOptions(nOptions);
            }


            Unload.ClearOptions();
            nOptions = new List<string>();

            nOptions.Add("None");

            foreach (Slot_Class sc in voidcraft.linkedScript.carriedSlots)
            {
                nOptions.Add("-" + sc.slotName);
            }

            if (nOptions.Count > 0)
            {
                Unload.AddOptions(nOptions);
            }
        }
        else
        {
            Load.ClearOptions();
            nOptions = new List<string>();

            nOptions.Add("Hostile Territory!");
            Load.AddOptions(nOptions);

            Unload.ClearOptions();
            nOptions = new List<string>();

            nOptions.Add("Coming in Version 0.4!");
            Unload.AddOptions(nOptions);
        }

    }

    public void ReloadDropdowns(Dropdown Load, Dropdown Unload, Advanced_System_Craft voidcraft)
    {
        voidcraft.availableSlots.Clear();
        Load.ClearOptions();
        nOptions = new List<string>();

        nOptions.Add("None");

        foreach (Slot_Class sc in manager.sManager.slotN1.GetComponent<Slot_Script>().slotClass.containedSlots)
        {
            if (sc.systemID != 0)
            {
                if (sc.systemID == currentSystem.uID)
                {
                    if (sc.numberOfTroopers < voidcraft.linkedCraft.capacity - voidcraft.linkedCraft.capacityF)
                    {
                        nOptions.Add("-" + sc.slotName);
                        voidcraft.availableSlots.Add(sc);
                    }
                    CheckOption(sc, voidcraft, "");
                }
                else
                {
                    CheckOption(sc, voidcraft, "");
                    //ChangeParent(sc);
                }
            }
            else
            {
                CheckOption(sc, voidcraft, "");
            }
        }

        if (nOptions.Count > 0)
        {
            Load.AddOptions(nOptions);
        }


        Unload.ClearOptions();
        nOptions = new List<string>();

        nOptions.Add("None");

        foreach (Slot_Class sc in voidcraft.linkedScript.carriedSlots)
        {
            nOptions.Add("-" + sc.slotName);
        }

        if (nOptions.Count > 0)
        {
            Unload.AddOptions(nOptions);
        }

    }

    public void CheckOption(Slot_Class slot, Advanced_System_Craft voidcraft, string additive)
    {
        foreach (Slot_Class sc in slot.containedSlots)
        {
            if (sc.systemID != 0)
            {
                if (sc.systemID == currentSystem.uID)
                {
                    if (sc.numberOfTroopers < voidcraft.linkedCraft.capacity)
                    {
                        nOptions.Add((additive + "-" + sc.slotName));
                        voidcraft.availableSlots.Add(sc);
                    }
                    CheckOption(sc, voidcraft, additive + sc.slotName[0]);
                }
                else //Probably shouldn't do this right here, should change it with a pass through once the child slot locations have been confirmed
                {
                    CheckOption(sc, voidcraft, additive + sc.slotName[0]);
                    //slot.systemID = 0;
                }
            }
            else
            {
                CheckOption(sc, voidcraft, additive + sc.slotName[0]);
            }
        }
    }

    //Changes parent as first child - if the other slots contained all match, this will stay as is. If not, it will be changed to 0
    public void ChangeParent(Slot_Class slot)
    {
        bool found = false;
        bool different = false;

        foreach (Slot_Class sc in manager.sManager.slotN1.GetComponent<Slot_Script>().slotClass.containedSlots)
        {
            if (sc.containedSlots.Contains(slot)) //If sc is parent of slot
            {
                found = true;
                different = CheckSiblings(slot, sc);
                if (different == false)
                {
                    sc.systemID = slot.systemID;
                    sc.planetN = slot.planetN;
                    sc.craftID = slot.craftID;
                }
                else
                {
                    sc.systemID = 0;
                    sc.planetN = 0;
                    sc.craftID = 0;
                }
            }
            else
            {
                found = ChangeParent(slot, sc);
                if (found)
                {
                    different = CheckSiblings(slot, sc);
                    if (different == false)
                    {
                        sc.systemID = slot.systemID;
                        sc.planetN = slot.planetN;
                        sc.craftID = slot.craftID;
                    }
                    else
                    {
                        sc.systemID = 0;
                        sc.planetN = 0;
                        sc.craftID = 0;
                    }
                }
            }

        }

    }

    public bool ChangeParent(Slot_Class slot, Slot_Class checkingSlot)
    {
        bool found = false;
        bool different = false;

        foreach (Slot_Class sc in checkingSlot.containedSlots)
        {
            if (sc.containedSlots.Contains(slot)) //If sc is parent of slot
            {
                Debug.Log("Child is slot");
                found = true;
                different = CheckSiblings(slot, sc);
                if (different == false)
                {
                    sc.systemID = slot.systemID;
                    sc.planetN = slot.planetN;
                    checkingSlot.systemID = slot.systemID;
                    checkingSlot.planetN = slot.planetN;
                    sc.craftID = slot.craftID;
                    checkingSlot.craftID = slot.craftID;
                }
                else
                {
                    sc.systemID = 0;
                    sc.planetN = 0;
                    sc.craftID = 0;
                    checkingSlot.systemID = 0;
                    checkingSlot.planetN = 0;
                    checkingSlot.craftID = 0;
                }
                break;
            }
            else //If not check children of sc if they are parent of slot - returns true if finds the parent in one of it's children
            {
                found = ChangeParent(slot, sc);
                if (found)
                {
                    different = CheckSiblings(slot, sc);
                    if (different == false)
                    {
                        sc.systemID = slot.systemID;
                        sc.planetN = slot.planetN;
                        checkingSlot.systemID = slot.systemID;
                        checkingSlot.planetN = slot.planetN;
                        sc.craftID = slot.craftID;
                        checkingSlot.craftID = slot.craftID;
                    }
                    else
                    {
                        sc.systemID = 0;
                        sc.planetN = 0;
                        sc.craftID = 0;
                        checkingSlot.systemID = 0;
                        checkingSlot.planetN = 0;
                        checkingSlot.craftID = 0;
                    }
                    break;
                }
            }
        }

        return found;
    }

    public bool CheckSiblings(Slot_Class slot, Slot_Class checkingSlot)
    {
        bool different = false;

        foreach (Slot_Class sc in checkingSlot.containedSlots)
        { 
            if(sc.systemID != slot.systemID)
            {
                different = true;
                break;
            }
            else if (sc.craftID != slot.craftID)
            {
                different = true;
                break;
            }
        }

        return different;
    }


    public void CloseManager()
    {
        if (craft.Count > 0)
        {
            while (craft.Count > 0)
            {
                Destroy(craft[0].gameObject);
                craft.RemoveAt(0);
            }
        }
    }

    public void LoadSlot(Slot_Class slot, Advanced_System_Craft voidcraft, int value)
    {
        manager.sManager.MoveSlotLocation(slot, "Craft", voidcraft.linkedCraft.ID, 0);
        voidcraft.linkedCraft.uIDTransported.Add(slot.uID);
        voidcraft.linkedScript.carriedSlots.Add(slot);

        RemoveFromDropdown("Load", value, voidcraft);

        foreach (Slot_Class sc in slot.containedSlots)
        {
            LoadChild(sc, voidcraft, value);
        }

        int carried = 0;
        foreach (Slot_Class sc in voidcraft.linkedScript.carriedSlots)
        {
            if (sc.squad)
            {
                carried += sc.numberOfTroopers;
            }
        }
        voidcraft.linkedCraft.capacityF = carried;

        ChangeParent(slot);

        UpdateCraft();
    }

    public void LoadChild(Slot_Class slot, Advanced_System_Craft craft, int value)
    {
        manager.sManager.MoveSlotLocation(slot, "Craft", craft.linkedCraft.ID, 0);
        craft.linkedCraft.uIDTransported.Add(slot.uID);
        craft.linkedScript.carriedSlots.Add(slot); //This is linked to the craft's contained slots

        RemoveFromDropdown("Load", value, craft);
    }

    public void UnloadSlot(Slot_Class slot, Advanced_System_Craft voidcraft, int value)
    {
        manager.sManager.MoveSlotLocation(slot, "Planet", voidcraft.linkedCraft.starID, voidcraft.linkedCraft.planetN);
        voidcraft.linkedCraft.uIDTransported.RemoveAt(value - 1);
        voidcraft.linkedScript.carriedSlots.RemoveAt(value - 1);

        RemoveFromDropdown("Unload", value, voidcraft);

        foreach (Slot_Class sc in slot.containedSlots)
        {
            UnloadChild(sc, voidcraft, value);
        }

        int carried = 0;
        foreach (Slot_Class sc in voidcraft.linkedScript.carriedSlots)
        {
            if (sc.squad)
            {
                carried += sc.numberOfTroopers;
            }
        }
        voidcraft.linkedCraft.capacityF = carried;

        ChangeParent(slot);

        UpdateCraft();
    }

    public void UnloadChild(Slot_Class slot, Advanced_System_Craft craft, int value)
    {
        manager.sManager.MoveSlotLocation(slot, "Planet", craft.linkedCraft.starID, craft.linkedCraft.planetN);
        craft.linkedCraft.uIDTransported.RemoveAt(value - 1);
        craft.linkedScript.carriedSlots.RemoveAt(value - 1);

        RemoveFromDropdown("Unload", value, craft);

        foreach (Slot_Class sc in slot.containedSlots)
        {
            UnloadChild(sc, craft, value);
        }
    }


    public void RemoveFromDropdown(string type, int value, Advanced_System_Craft sCraft)
    {
        if(type == "Load")
        {
        }
        if (type == "Unload")
        {
            sCraft.unload.options.RemoveAt(value);
        }
    }

    public void UpdateCraft()
    {
        foreach(Advanced_System_Craft avc in craft)
        {
            avc.DoUpdate();
        }
    }
}
