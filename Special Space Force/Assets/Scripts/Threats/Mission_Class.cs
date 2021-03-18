using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Mission_Class
{
    [XmlElement("Mission_Name")]
    public string missionName;
    [XmlElement("Minimum_available_Level")]
    public int minLevel;
    [XmlElement("Maximum_available_Level")]
    public int maxLevel;
    [XmlElement("Level_Reduction")]
    public int reduction;

    [XmlElement("Unit_Descriptions")]
    public List<Unit_Description> units;

    [XmlElement("Mission_based_Story_Class_List")]
    public List<Story_Class> stories;

    public Mission_Class()
    {

    }
}
