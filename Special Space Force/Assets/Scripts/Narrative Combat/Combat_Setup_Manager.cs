using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_Setup_Manager : MonoBehaviour
{
    /// <summary>
    /// This manages the combat setup screen, just before entering the narrative combat system. It allows the player to choose their squads or slots to
    /// take into combat
    /// </summary>
    public Manager_Script modManager;
    public GameObject content;
    public GameObject n1;
    public GameObject combatUnitPrefab;
    public List<Combat_Slot_Script> slots;
    public List<Voidcraft_Class> orbitVoidcraft;
    public List<Combat_Slot_Script> selectedSlots;
    public Display_Roles_Manager roleDisplay;
    public Text tStrength;
    public Text tTroopers;
    public bool changing = false;

    private int totalStrength;
    private int totalTroopers;

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
                if (slot.planetN - 1 == planet.parentSystem.SystemPlanets.IndexOf(planet))
                {
                    GameObject temp = Instantiate(combatUnitPrefab, content.transform);
                    Combat_Slot_Script tempCS = temp.GetComponent<Combat_Slot_Script>();
                    tempCS.SetupCombatSlot(slot, this);
                    tempCS.locationText.text = planet.planetName;
                    temp.transform.localPosition = n1.transform.localPosition;
                    temp.transform.localPosition += new Vector3(0, -125 * slots.Count, 0);
                    slots.Add(tempCS);
                }
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
                    tempCS.SetupCombatSlot(slot, this);
                    tempCS.locationText.text = vc.craftName;
                    temp.transform.localPosition = n1.transform.localPosition;
                    temp.transform.localPosition += new Vector3(0, -125 * slots.Count, 0);
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
                if (slot.planetN == planetSelected.parentSystem.SystemPlanets.IndexOf(planetSelected))
                {
                    if (sc.containedSlots.Count == 0)
                    {
                        GameObject temp = Instantiate(combatUnitPrefab, content.transform);
                        Combat_Slot_Script tempCS = temp.GetComponent<Combat_Slot_Script>();
                        tempCS.SetupCombatSlot(sc, this);
                        tempCS.locationText.text = planetSelected.planetName;
                        temp.transform.localPosition = n1.transform.localPosition;
                        temp.transform.localPosition += new Vector3(0, -125 * slots.Count, 0);
                        slots.Add(tempCS);
                    }
                }
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
                tempCS.SetupCombatSlot(sc, this);
                tempCS.locationText.text = craft.craftName;
                temp.transform.localPosition = n1.transform.localPosition;
                temp.transform.localPosition += new Vector3(0, -125 * slots.Count, 0);
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

    public void AddSelected(Combat_Slot_Script cSlot)
    {
        selectedSlots.Add(cSlot);
        if (cSlot.SlotClass.containedSlots.Count == 0) // if doesn't have any child slots add it's strength
        {
            totalStrength += cSlot.SlotClass.numberOfTroopers * 2; // Insert strength calculation later
            totalTroopers += cSlot.SlotClass.numberOfTroopers;

            tStrength.text = totalStrength.ToString();
            tTroopers.text = totalTroopers.ToString();

            if (cSlot.SlotClass.squadRole == "Infantry - Line")
            {
                roleDisplay.nLine += 1;
                roleDisplay.LineT.text = roleDisplay.nLine.ToString();
            }
        }
        else
        {
            foreach(Slot_Class sc in cSlot.SlotClass.containedSlots)
            {
                foreach(Combat_Slot_Script css in slots) 
                {
                    if (css.SlotClass == sc)
                    {
                        selectedSlots.Add(css);
                        css.toggleSelected.isOn = true;
                        if (sc.containedSlots.Count == 0) // if doesn't have any child slots add it's strength
                        {
                            totalStrength += sc.numberOfTroopers * 2; // Insert strength calculation later
                            totalTroopers += sc.numberOfTroopers;

                            tStrength.text = totalStrength.ToString();
                            tTroopers.text = totalTroopers.ToString();

                            if (sc.squadRole == "Infantry - Line")
                            {
                                roleDisplay.nLine += 1;
                                roleDisplay.LineT.text = roleDisplay.nLine.ToString();
                            }
                        }
                        else
                        {
                            AddChildrenSelected(sc);
                        }
                        break;
                    }
                    else
                    {
                    }
                }
            }
        }
    }

    public void AddChildrenSelected(Slot_Class slot)
    {
        foreach (Slot_Class sc in slot.containedSlots)
        {
            foreach (Combat_Slot_Script css in slots)
            {
                if (css.SlotClass == sc)
                {
                    selectedSlots.Add(css);
                    css.toggleSelected.isOn = true;
                    if (sc.containedSlots.Count == 0) // if doesn't have any child slots add it's strength
                    {
                        totalStrength += sc.numberOfTroopers * 2; // Insert strength calculation later
                        totalTroopers += sc.numberOfTroopers;

                        tStrength.text = totalStrength.ToString();
                        tTroopers.text = totalTroopers.ToString();

                        if (sc.squadRole == "Infantry - Line")
                        {
                            roleDisplay.nLine += 1;
                            roleDisplay.LineT.text = roleDisplay.nLine.ToString();
                        }
                    }
                    else
                    {
                        AddChildrenSelected(sc);
                    }
                }
                else
                {
                }
            }
        }
    }

    public void RemoveSelected(Combat_Slot_Script cSlot)
    {

        selectedSlots.Remove(cSlot);
        if (cSlot.SlotClass.containedSlots.Count == 0) // if doesn't have any child slots add it's strength
        {
            totalStrength -= cSlot.SlotClass.numberOfTroopers * 2; // Insert strength calculation later
            totalTroopers -= cSlot.SlotClass.numberOfTroopers;

            tStrength.text = totalStrength.ToString();
            tTroopers.text = totalTroopers.ToString();

            if (cSlot.SlotClass.squadRole == "Infantry - Line")
            {
                roleDisplay.nLine -= 1;
                roleDisplay.LineT.text = roleDisplay.nLine.ToString();
            }
        }
        else
        {
            foreach (Slot_Class sc in cSlot.SlotClass.containedSlots)
            {
                foreach (Combat_Slot_Script css in slots)
                {
                    if (css.SlotClass == sc)
                    {
                        selectedSlots.Remove(css);
                        css.toggleSelected.isOn = false;
                        if (sc.containedSlots.Count == 0) // if doesn't have any child slots add it's strength
                        {
                            totalStrength -= sc.numberOfTroopers * 2; // Insert strength calculation later
                            totalTroopers -= sc.numberOfTroopers;

                            tStrength.text = totalStrength.ToString();
                            tTroopers.text = totalTroopers.ToString();

                            if (sc.squadRole == "Infantry - Line")
                            {
                                roleDisplay.nLine -= 1;
                                roleDisplay.LineT.text = roleDisplay.nLine.ToString();
                            }
                        }
                        else
                        {
                            RemoveChildrenSelected(sc);
                        }
                        break;
                    }
                    else
                    {
                    }
                }
            }
        }
    }


    public void RemoveChildrenSelected(Slot_Class slot)
    {
        foreach (Slot_Class sc in slot.containedSlots)
        {
            foreach (Combat_Slot_Script css in slots)
            {
                if (css.SlotClass == sc)
                {
                    selectedSlots.Remove(css);
                    css.toggleSelected.isOn = false;
                    if (sc.containedSlots.Count == 0) // if doesn't have any child slots add it's strength
                    {
                        totalStrength -= sc.numberOfTroopers * 2; // Insert strength calculation later
                        totalTroopers -= sc.numberOfTroopers;

                        tStrength.text = totalStrength.ToString();
                        tTroopers.text = totalTroopers.ToString();

                        if (sc.squadRole == "Infantry - Line")
                        {
                            roleDisplay.nLine -= 1;
                            roleDisplay.LineT.text = roleDisplay.nLine.ToString();
                        }
                    }
                    else
                    {
                        RemoveChildrenSelected(sc);
                    }
                }
                else
                {
                }
            }
        }
    }
}
