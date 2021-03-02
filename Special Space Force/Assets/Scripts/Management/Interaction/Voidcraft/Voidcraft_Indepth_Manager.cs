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

    public Dropdown loadMenu;
    public Dropdown unloadMenu;

    private Voidcraft_Script selectedCraft;
    private Advanced_System_Craft advancedCraft;

    List<string> nOptions;

    public Sound_Script speakerManager;

    public void OpenSystem(System_Class system)
    {
        currentSystem = system;
        foreach (Fleet_Script fs in fManager.FleetsS)
        {
            if (fs.fleetClass.containedCraft != null)
            {
                foreach (Voidcraft_Class vc in fs.fleetClass.containedCraft)
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
                        if (sc.numberOfTroopers < (voidcraft.linkedCraft.capacity - voidcraft.linkedCraft.capacityF))
                        {
                            nOptions.Add("- " + sc.slotName);
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

            foreach (Slot_Class sc in voidcraft.linkedScript.CarriedSlots)
            {
                nOptions.Add("- " + sc.slotName);
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
        if (currentSystem.allegiance == 0)
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
                        if (sc.numberOfTroopers < (voidcraft.linkedCraft.capacity - voidcraft.linkedCraft.capacityF))
                        {
                            nOptions.Add("- " + sc.slotName);
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
            List<int> removing = new List<int>();

            nOptions.Add("None");

            for (int i = 0; i < voidcraft.linkedScript.CarriedSlots.Count; i++)
            {
                if (voidcraft.linkedScript.CarriedSlots[i].craftID == voidcraft.linkedCraft.ID)
                {
                    nOptions.Add("- " + voidcraft.linkedScript.CarriedSlots[i].slotName);
                }
                else
                {
                    removing.Add(i);
                }
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

    public void MenuCraftDropdowns(Voidcraft_Script voidcraft)
    {
        if (advancedCraft == null)
        {
            advancedCraft = new Advanced_System_Craft();

            advancedCraft.availableSlots = new List<Slot_Class>();
            advancedCraft.advManager = this;
        }
        advancedCraft.linkedScript = voidcraft;
        advancedCraft.linkedCraft = voidcraft.craftClass;
        advancedCraft.containedSlots = voidcraft.CarriedSlots;

        selectedCraft = voidcraft;

        foreach(System_Script sc in manager.sectorManager.systems)
        {
            if(sc.Star.uID == voidcraft.craftClass.starID)
            {
                currentSystem = sc.Star;
            }
        }

        if (currentSystem.allegiance == 0)
        {
            advancedCraft.availableSlots.Clear();
            loadMenu.ClearOptions();
            nOptions = new List<string>();

            nOptions.Add("None");

            foreach (Slot_Class sc in manager.sManager.slotN1.GetComponent<Slot_Script>().slotClass.containedSlots)
            {
                if (sc.systemID != 0)
                {
                    if (sc.systemID == currentSystem.uID)
                    {
                        if (sc.numberOfTroopers < voidcraft.craftClass.capacity - voidcraft.craftClass.capacityF)
                        {
                            nOptions.Add("- " + sc.slotName);
                            advancedCraft.availableSlots.Add(sc);
                        }
                        CheckOption(sc, advancedCraft, "");
                    }
                    else
                    {
                        CheckOption(sc, advancedCraft, "");
                        //ChangeParent(sc);
                    }
                }
                else
                {
                    CheckOption(sc, advancedCraft, "");
                }
            }

            if (nOptions.Count > 0)
            {
                loadMenu.AddOptions(nOptions);
            }


            unloadMenu.ClearOptions();
            nOptions = new List<string>();
            List<int> removing = new List<int>();

            nOptions.Add("None");

            for (int i = 0; i < voidcraft.CarriedSlots.Count; i++)
            {
                if (voidcraft.CarriedSlots[i].craftID == voidcraft.craftClass.ID)
                {
                    nOptions.Add("- " + voidcraft.CarriedSlots[i].slotName);
                }
                else
                {
                    removing.Add(i);
                }
            }

            if (nOptions.Count > 0)
            {
                unloadMenu.AddOptions(nOptions);
            }
        }
        else
        {
            loadMenu.ClearOptions();
            nOptions = new List<string>();

            nOptions.Add("Hostile Territory!");
            loadMenu.AddOptions(nOptions);

            loadMenu.ClearOptions();
            nOptions = new List<string>();

            nOptions.Add("Coming in Version 0.4!");
            loadMenu.AddOptions(nOptions);
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
                    if (sc.numberOfTroopers < (voidcraft.linkedCraft.capacity - voidcraft.linkedCraft.capacityF))
                    {
                        nOptions.Add((additive + "- " + sc.slotName));
                        voidcraft.availableSlots.Add(sc);
                    }
                    CheckOption(sc, voidcraft, additive + sc.slotName[0] + sc.slotName[1] + sc.slotName[2]);
                }
                else
                {
                    CheckOption(sc, voidcraft, additive + sc.slotName[0] + sc.slotName[1] + sc.slotName[2]);
                    //slot.systemID = 0;
                }
            }
            else
            {
                CheckOption(sc, voidcraft, additive + sc.slotName[0] + sc.slotName[1] + sc.slotName[2]);
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
        voidcraft.linkedScript.CarriedSlots.Add(slot);


        foreach (Slot_Class sc in slot.containedSlots)
        {
            LoadChild(sc, voidcraft, value);
        }

        int carried = 0;
        foreach (Slot_Class sc in voidcraft.linkedScript.CarriedSlots)
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

    public void LoadChild(Slot_Class slot, Advanced_System_Craft voidcraft, int value)
    {
        manager.sManager.MoveSlotLocation(slot, "Craft", voidcraft.linkedCraft.ID, 0);
        voidcraft.linkedCraft.uIDTransported.Add(slot.uID);
        voidcraft.linkedScript.CarriedSlots.Add(slot); //This is linked to the craft's contained slots

        foreach (Slot_Class sc in slot.containedSlots)
        {
            LoadChild(sc, voidcraft, value);
        }
    }

    public void UnloadSlot(Slot_Class slot, Advanced_System_Craft voidcraft, int value)
    {
        manager.sManager.MoveSlotLocation(slot, "Planet", voidcraft.linkedCraft.starID, voidcraft.linkedCraft.planetN);
        voidcraft.linkedCraft.uIDTransported.RemoveAt(value - 1);

        foreach (Slot_Class sc in slot.containedSlots)
        {
            UnloadChild(sc, voidcraft, value);
        }


        ChangeParent(slot);

        int carried = 0;

        for (int i = 0; i < voidcraft.linkedScript.CarriedSlots.Count; i++)
        {
            if (voidcraft.linkedScript.CarriedSlots[i].craftID == voidcraft.linkedCraft.ID)
            {
                if (voidcraft.linkedScript.CarriedSlots[i].squad)
                {
                    carried += voidcraft.linkedScript.CarriedSlots[i].numberOfTroopers;
                }
            }
            else
            {
                voidcraft.linkedScript.CarriedSlots.RemoveAt(i);
                i -= 1;
            }
        }

        voidcraft.linkedCraft.capacityF = carried;

        UpdateCraft();
    }

    public void UnloadChild(Slot_Class slot, Advanced_System_Craft craft, int value)
    {
        manager.sManager.MoveSlotLocation(slot, "Planet", craft.linkedCraft.starID, craft.linkedCraft.planetN);
        craft.linkedCraft.uIDTransported.RemoveAt(value - 1);


        foreach (Slot_Class sc in slot.containedSlots)
        {
            UnloadChild(sc, craft, value);
        }
    }

    public void LoadSlot(Dropdown dropdown)
    {
        Slot_Class slot = advancedCraft.availableSlots[dropdown.value - 1];
        manager.sManager.MoveSlotLocation(slot, "Craft", selectedCraft.craftClass.ID, 0);
        selectedCraft.craftClass.uIDTransported.Add(slot.uID);
        selectedCraft.CarriedSlots.Add(slot);


        foreach (Slot_Class sc in slot.containedSlots)
        {
            LoadChild(sc, dropdown.value);
        }

        int carried = 0;
        foreach (Slot_Class sc in selectedCraft.CarriedSlots)
        {
            if (sc.squad)
            {
                carried += sc.numberOfTroopers;
            }
        }
        selectedCraft.craftClass.capacityF = carried;

        ChangeParent(slot);

        advancedCraft.DoUpdateFake();
    }

    public void LoadChild(Slot_Class slot,  int value)
    {
        manager.sManager.MoveSlotLocation(slot, "Craft", selectedCraft.craftClass.ID, 0);
        selectedCraft.craftClass.uIDTransported.Add(slot.uID);
        selectedCraft.CarriedSlots.Add(slot);

        foreach (Slot_Class sc in slot.containedSlots)
        {
            LoadChild(sc, value);
        }
    }

    public void UnloadSlot(Dropdown dropdown)
    {
        Slot_Class slot = selectedCraft.CarriedSlots[dropdown.value - 1];
        manager.sManager.MoveSlotLocation(slot, "Planet", selectedCraft.craftClass.starID, selectedCraft.craftClass.planetN);
        selectedCraft.craftClass.uIDTransported.RemoveAt(dropdown.value - 1);

        foreach (Slot_Class sc in slot.containedSlots)
        {
            UnloadChild(sc, dropdown.value);
        }

        ChangeParent(slot);

        int carried = 0;

        for (int i = 0; i < selectedCraft.CarriedSlots.Count; i++)
        {
            if (selectedCraft.CarriedSlots[i].craftID == selectedCraft.craftClass.ID)
            {
                if (selectedCraft.CarriedSlots[i].squad)
                {
                    carried += selectedCraft.CarriedSlots[i].numberOfTroopers;
                }
            }
            else
            {
                selectedCraft.CarriedSlots.RemoveAt(i);
                i -= 1;
            }
        }

        selectedCraft.craftClass.capacityF = carried;

        advancedCraft.DoUpdateFake();
    }

    public void UnloadChild(Slot_Class slot, int value)
    {
        manager.sManager.MoveSlotLocation(slot, "Planet", selectedCraft.craftClass.starID, selectedCraft.craftClass.planetN);
        selectedCraft.craftClass.uIDTransported.RemoveAt(value - 1);

        foreach (Slot_Class sc in slot.containedSlots)
        {
            UnloadChild(sc, value);
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
