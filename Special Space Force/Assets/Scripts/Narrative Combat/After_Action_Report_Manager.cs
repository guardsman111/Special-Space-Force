using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class After_Action_Report_Manager : MonoBehaviour
{
    public GameObject prefabAARSlot;
    public GameObject prefabAARSquad;
    public GameObject content;
    public List<After_Action_Slot_Script> slots;
    public GameObject n1;
    public string result;

    //Visual Elements
    public TextMeshProUGUI resultText;
    public Text startStrength;
    public Text remainStrength;
    public Text deadText;
    public Text missingText;
    public Text criticalText;
    public Text injuredText;
    public Text brokenText;

    public void OpenAfterActionreport(List<Combat_Slot_Script> combatSlots, string newResult)
    {
        slots = new List<After_Action_Slot_Script>();
        result = newResult;
        resultText.text = result;
        int sStrength = 0;
        int rStrength = 0;
        int dStrength = 0;
        int mStrength = 0;
        int cStrength = 0;
        int iStrength = 0;
        int bStrength = 0;

        foreach (Combat_Slot_Script css in combatSlots)
        {
            if (css.SlotClass.squad)
            {
                GameObject temp = Instantiate(prefabAARSquad, content.transform);
                After_Action_Slot_Script tempAAS = temp.GetComponent<After_Action_Slot_Script>();
                tempAAS.SetupSlot(css, this);
                temp.transform.localPosition = n1.transform.localPosition;
                temp.transform.localPosition += new Vector3(0, -205 * slots.Count, 0);
                sStrength += css.SlotClass.containedTroopers.Count;
                if (result == "Victory" || result == "Draw") 
                {
                    rStrength += (css.SlotClass.containedTroopers.Count - css.dead);
                    cStrength += css.crit;
                }
                else
                {
                    rStrength += (css.SlotClass.containedTroopers.Count - (css.dead + css.crit));
                    mStrength += css.crit;
                }
                dStrength += css.dead;
                iStrength += css.inj;
                bStrength += css.brok;
                slots.Add(tempAAS);
            }
            else
            {
                GameObject temp = Instantiate(prefabAARSlot, content.transform);
                After_Action_Slot_Script tempAAS = temp.GetComponent<After_Action_Slot_Script>();
                tempAAS.SetupSlot(css, this);
                temp.transform.localPosition = n1.transform.localPosition;
                temp.transform.localPosition += new Vector3(0, -205 * slots.Count, 0);
                slots.Add(tempAAS);
            }
        }

        startStrength.text = sStrength.ToString();
        remainStrength.text = rStrength.ToString();
        deadText.text = dStrength.ToString();
        criticalText.text = cStrength.ToString();
        missingText.text = mStrength.ToString();
        injuredText.text = iStrength.ToString();
        brokenText.text = bStrength.ToString();
    }
}
