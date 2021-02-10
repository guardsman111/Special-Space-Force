using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Squad_Script : MonoBehaviour
{
    public Text sName;
    public Text sSize;
    public Text sType;

    public Slot_Class slotClass;

    public Contents_Viewer parentScript;

    public void ViewSquad()
    {
        parentScript.OpenSquad(slotClass);
    }
}
