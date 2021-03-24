using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_Slot_Script : MonoBehaviour
{
    public Text nameText;
    public Text locationText;
    public Text strengthText;
    public Toggle toggleSelected;

    private Slot_Class slotClass;

    public Slot_Class SlotClass
    {
        get { return slotClass; }

        set
        {
            if (value != slotClass)
            {
                slotClass = value;
            }
        }
    }

    public void SetupCombatSlot(Slot_Class combatScript)
    {
        slotClass = combatScript;

        nameText.text = slotClass.slotName;
        strengthText.text = slotClass.numberOfTroopers.ToString();
    }
}
