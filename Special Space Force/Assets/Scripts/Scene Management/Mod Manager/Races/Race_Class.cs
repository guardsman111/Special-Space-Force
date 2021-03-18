using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Race_Class
{
    /// <summary>
    /// The Race Class is where Race information is stored
    /// </summary>
    [XmlElement("Species_Name")]
    public string raceName;
    [XmlElement("Empire_Name")]
    public string empireName;
    [XmlElement("Source")]
    public string source;

    [XmlElement("Xenophobia_Modifier")]
    public float xenoMod;
    [XmlElement("Militarism_Modifier")]
    public float miliMod;
    [XmlElement("Expansionism_Modifier")]
    public float expaMod;
    [XmlElement("Industrialism_Modifier")]
    public float induMod;

    [XmlElement("Path_to_Race_Units_Class")]
    public string raceUnitsPath;

    [XmlElement("List_of_Threat_Class_paths")]
    public List<string> threatPaths;

    public List<Category_Class> Categories;

    public Race_Class()
    {

    }
}
