﻿using System.Collections;
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

    public Race_Class()
    {

    }
}
