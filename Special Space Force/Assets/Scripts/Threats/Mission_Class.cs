using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Mission_Class
{
    /// <summary>
    /// Holds all information for missions - editable by the player
    /// </summary>
    [XmlElement("Mission_Name")]
    public string missionName;
    [XmlElement("Mission_Description")]
    public string missionDescription;
    [XmlElement("Minimum_available_Level")]
    public int minLevel;
    [XmlElement("Maximum_available_Level")]
    public int maxLevel;
    [XmlElement("Level_Reduction")]
    public int reduction;

    public List<Unit_Description> enemyForce;
    public List<Story_Class> introStories;
    public List<Story_Class> objectiveStories;
    public List<Story_Class> victoryStories;
    public List<Story_Class> defeatStories;

    public Mission_Class()
    {

    }
}
