using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Advanced_System_Craft : System_Craft
{
    public Text fleet;
    public Text capacity;
    public Text capacityF;
    public Text location;
    public Voidcraft_Script linkedScript;

    public Voidcraft_Indepth_Manager advManager;
    public Dropdown load;
    public List<Slot_Class> availableSlots;
    public Dropdown unload;
    public List<Slot_Class> containedSlots;

    public void Create(Voidcraft_Class craft, System_Voidcraft_Script managerS, Voidcraft_Indepth_Manager managerA)
    {
        base.Create(craft, managerS);
        advManager = managerA;
        availableSlots = new List<Slot_Class>();
        containedSlots = new List<Slot_Class>();

        foreach(Fleet_Script fs in manager.fManager.FleetsS)
        {
            foreach(Voidcraft_Script vs in fs.containedCraft)
            {
                if(vs.craftClass.ID == craft.ID)
                {
                    fleet.text = fs.fleetName;
                    linkedScript = vs;
                    break;
                }
            }
        }

        containedSlots = linkedScript.CarriedSlots;

        capacity.text = craft.capacity.ToString();
        capacityF.text = craft.capacityF.ToString();
        location.text = linkedScript.GetStat("Location");

        advManager.SetupLoaderDropdowns(load, unload, this);
    }

    public void DoUpdate()
    {
        capacity.text = linkedCraft.capacity.ToString();
        capacityF.text = linkedCraft.capacityF.ToString();
        location.text = linkedScript.GetStat("Location");
        advManager.ReloadDropdowns(load, unload, this);
    }

    //Does an update for the Fleet manager menu where we don't need to see a visual change to the advanced craft
    public void DoUpdateFake()
    {
        advManager.MenuCraftDropdowns(linkedScript);
    }

    public void LoadSlots(Dropdown dropdown)
    {
        advManager.speakerManager.PlaySound();
        if (dropdown.value != 0)
        {
            advManager.LoadSlot(availableSlots[dropdown.value - 1], this, dropdown.value);
            dropdown.value = 0;
        }

    }

    public void UnloadSlots(Dropdown dropdown)
    {
        advManager.speakerManager.PlaySound();
        if (dropdown.value != 0)
        {
            advManager.UnloadSlot(containedSlots[dropdown.value - 1], this, dropdown.value);
            dropdown.value = 0;
        }

    }
}
