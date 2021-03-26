using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Threat_Class
{
    /// <summary>
    /// Contains information for threats - editable by user
    /// </summary>
    [XmlElement("Threat_Name")]
    public string threatName;
    [XmlElement("Threat_Faction_Name")]
    public string threatFaction;
    public int planetType;

    [XmlElement("Number_of_levels")]
    public int nLevels;
    [XmlElement("Level_Growth_Rate")]
    public int growth;

    public List<float> levelStrength;
    public List<string> levelDescriptions;
    public List<string> missionPaths;

    public int growthCounter;
    public int currentLevel;
    public int uID;

    public Threat_Class()
    {

    }
}
