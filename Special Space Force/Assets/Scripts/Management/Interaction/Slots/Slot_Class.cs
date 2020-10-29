using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot_Class
{
    /// <summary>
    /// Slot Class stores the information to be saved from the Slot Script they are attatched too
    /// </summary>
    public List<Slot_Class> containedSlots;
    public List<Trooper_Class> containedTroopers;
    public string slotName;
    public int positionID;
    public bool squad;
    public int squadRole;
    public int numberOfTroopers;

    public int slotHeight;

    public string TemplateImageLocation;

    public Slot_Class()
    {

    }
}
