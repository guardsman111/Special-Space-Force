﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class After_Action_Slot_Script : MonoBehaviour
{
    public Slot_Class slotClass;
    public After_Action_Report_Manager manager;
    public Text kia;
    public Text mia;
    public Text injured;
    public Text fled;

    public void SetupSlot(Combat_Slot_Script combatSlot, After_Action_Report_Manager newManager)
    {
        slotClass = combatSlot.SlotClass;
        manager = newManager;
        kia.text = combatSlot.dead.ToString();
        if(manager.result == "Defeat")
        {
            mia.text = combatSlot.crit.ToString();
        }
        else
        {
            combatSlot.inj += combatSlot.crit;
        }
        injured.text = combatSlot.inj.ToString();
        fled.text = combatSlot.brok.ToString();
    }
}
