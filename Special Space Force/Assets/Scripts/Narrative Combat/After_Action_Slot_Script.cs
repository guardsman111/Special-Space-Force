using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class After_Action_Slot_Script : MonoBehaviour
{
    public Slot_Class slotClass;
    public After_Action_Report_Manager manager;
    public Text nameText;
    public Text kia;
    public Text mia;
    public Text injured;
    public Text fled;

    public void SetupSlot(Combat_Slot_Script combatSlot, After_Action_Report_Manager newManager)
    {
        slotClass = combatSlot.SlotClass;
        manager = newManager;
        if (!combatSlot.SlotClass.squad)
        {
            int kiaI = 0;
            int miaI = 0;
            int injI = 0;
            int brokI = 0;
            foreach (Combat_Slot_Script css in manager.cSManager.selectedSlots)
            {
                if (combatSlot.SlotClass.containedSlots.Contains(css.SlotClass))
                {
                    kiaI += css.dead;
                    if (manager.result == "Defeat")
                    {
                        miaI += css.crit;
                    }
                    else
                    {
                        injI += css.crit;
                    }
                    injI += css.inj;
                    brokI += css.brok;
                }
            }
            kia.text = kiaI.ToString();
            mia.text = miaI.ToString();
            injured.text = injI.ToString();
            fled.text = brokI.ToString();
        }
        else
        {
            kia.text = combatSlot.dead.ToString();
            if (manager.result == "Defeat")
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
        nameText.text = combatSlot.SlotClass.slotName;
    }
}
