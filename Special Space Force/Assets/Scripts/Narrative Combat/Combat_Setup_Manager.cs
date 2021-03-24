using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_Setup_Manager : MonoBehaviour
{
    public Manager_Script modManager;
    public GameObject content;
    public GameObject n1;
    public GameObject combatUnitPrefab;
    public List<Combat_Slot_Script> slots;
    public List<Voidcraft_Class> orbitVoidcraft;

    private Mission_Script missionSelected;
    private Planet_Script planetSelected;

    public void SetupMission(Mission_Script mission, Planet_Script planet)
    {
        missionSelected = mission;
        planetSelected = planet;
        orbitVoidcraft = new List<Voidcraft_Class>();

        foreach(Voidcraft_Class vc in modManager.fManager.Craft)
        {
            if(vc.starID == planet.parentSystem.Star.uID && vc.planetN == planet.parentSystem.SystemPlanets.IndexOf(planet) + 1)
            {
                orbitVoidcraft.Add(vc);
            }
        }

        foreach(Slot_Class slot in modManager.sManager.Slots)
        {
            if(slot.systemID == planet.parentSystem.Star.uID)
            {
                GameObject temp = Instantiate(combatUnitPrefab, content.transform);
                Combat_Slot_Script tempCS = temp.GetComponent<Combat_Slot_Script>();
                tempCS.SetupCombatSlot(slot);
                tempCS.locationText.text = planet.planetName;
                temp.transform.localPosition = n1.transform.localPosition;
                temp.transform.localPosition += new Vector3(0, -100 * slots.Count, 0);
                slots.Add(tempCS);
            }

            CheckChildSlotsPlanet(slot);
        }


        foreach (Voidcraft_Class vc in orbitVoidcraft)
        {
            foreach (Slot_Class slot in modManager.sManager.Slots)
            {
                if (vc.uIDTransported.Contains(slot.uID))
                {
                    GameObject temp = Instantiate(combatUnitPrefab, content.transform);
                    Combat_Slot_Script tempCS = temp.GetComponent<Combat_Slot_Script>();
                    tempCS.SetupCombatSlot(slot);
                    tempCS.locationText.text = vc.craftName;
                    temp.transform.localPosition = n1.transform.localPosition;
                    temp.transform.localPosition += new Vector3(0, -100 * slots.Count, 0);
                    slots.Add(tempCS);
                    CheckChildSlotsVoidcraft(slot, vc);
                }
                else if (slot.craftID == 0 || slot.craftID == vc.ID)
                {
                    CheckChildSlotsVoidcraft(slot, vc);
                }
            }
        }
    }

    public void CheckChildSlotsPlanet(Slot_Class slot)
    {
        foreach (Slot_Class sc in slot.containedSlots)
        {
            if (sc.systemID == planetSelected.parentSystem.Star.uID)
            {
                GameObject temp = Instantiate(combatUnitPrefab, content.transform);
                Combat_Slot_Script tempCS = temp.GetComponent<Combat_Slot_Script>();
                tempCS.SetupCombatSlot(sc);
                tempCS.locationText.text = planetSelected.planetName;
                temp.transform.localPosition = n1.transform.localPosition;
                temp.transform.localPosition += new Vector3(0, -100 * slots.Count, 0);
                slots.Add(tempCS);
            }
            
            CheckChildSlotsPlanet(sc);

        }
    }

    public void CheckChildSlotsVoidcraft(Slot_Class slot, Voidcraft_Class craft)
    {
        foreach (Slot_Class sc in slot.containedSlots)
        {
            if (sc.craftID == craft.ID)
            {
                GameObject temp = Instantiate(combatUnitPrefab, content.transform);
                Combat_Slot_Script tempCS = temp.GetComponent<Combat_Slot_Script>();
                tempCS.SetupCombatSlot(sc);
                tempCS.locationText.text = craft.craftName;
                temp.transform.localPosition = n1.transform.localPosition;
                temp.transform.localPosition += new Vector3(0, -100 * slots.Count, 0);
                slots.Add(tempCS);
            }
            CheckChildSlotsVoidcraft(sc, craft);
        }
    }


    public void CloseManager()
    {
        while(slots.Count > 0)
        {
            Destroy(slots[0].gameObject);
            slots.RemoveAt(0);
        }

        gameObject.SetActive(false);
    }
}
