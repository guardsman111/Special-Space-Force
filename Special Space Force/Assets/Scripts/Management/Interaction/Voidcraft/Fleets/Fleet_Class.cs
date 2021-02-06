using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet_Class
{
    public List<Voidcraft_Class> containedCraft;
    public string fleetName;
    public int uID; // Unique ID given on creation
    public bool useFleetColours;

    public string TemplateImageLocation;

    public List<Color32> fleetColours;

    public Fleet_Class()
    {

    }
}
