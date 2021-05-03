using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_Slot_Script : MonoBehaviour
{
    /// <summary>
    /// This helps to display slots on the combat screen, slot_class editing here will change the saved class - this may be an unwanted effect, but I am hoping it 
    /// will make the player able to change gear just before combat in the future
    /// </summary>
    public Text nameText;
    public Text locationText;
    public Text strengthText;
    public Toggle toggleSelected;
    public int dead;
    public int crit;
    public int inj;
    public int brok;

    private Combat_Setup_Manager manager;

    private Slot_Class slotClass;
    private bool over = false;

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

    public void SetupCombatSlot(Slot_Class combatScript, Combat_Setup_Manager cm)
    {
        slotClass = combatScript;
        manager = cm;
        dead = 0;
        crit = 0;
        inj = 0;
        brok = 0;

        nameText.text = slotClass.slotName;
        strengthText.text = slotClass.numberOfTroopers.ToString();
    }

    public void ChangeSelected()
    {
        if (manager.changing == false)
        {
            manager.changing = true;
            if (toggleSelected.isOn)
            {
                manager.AddSelected(this);
            }
            else
            {
                manager.RemoveSelected(this);
            }
            Invoke("ChangingBool", 0.15f);
        }
    }

    public void ChangingBool()
    {
        manager.changing = false;
    }
}
