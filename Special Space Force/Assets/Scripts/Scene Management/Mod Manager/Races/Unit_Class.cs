using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Class
{
    [XmlElement("Unit_Name")]
    public string unitName;
    [XmlElement("Estimated_unit_strength")]
    public float strength;
    [XmlElement("Unit_Movement_Speed")]
    public float speed;

    public List<Enemy_Container> containedEnemies;

    public Unit_Class()
    {

    }
}
