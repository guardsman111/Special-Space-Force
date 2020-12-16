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

    [XmlElement("Xenophobia Modifier")]
    public float xenoMod;
    [XmlElement("Militarism Modifier")]
    public float miliMod;
    [XmlElement("Expansionism Modifier")]
    public float expaMod;
    [XmlElement("Industrialism Modifier")]
    public float induMod;


    public Race_Class()
    {

    }
}
