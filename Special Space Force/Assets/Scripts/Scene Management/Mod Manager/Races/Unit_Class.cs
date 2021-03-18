using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Class : MonoBehaviour
{
    [XmlElement("Unit_Name")]
    public string unitName;
    [XmlElement("Estimated_unit_strength")]
    public float strength;

    [XmlElement("Contained_Enemies")]
    public List<Enemy_Container> containedEnemies;

    public Unit_Class()
    {

    }
}
