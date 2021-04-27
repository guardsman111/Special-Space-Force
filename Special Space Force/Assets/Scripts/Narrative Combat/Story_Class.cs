﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Story_Class
{
    public string storyName;
    public string storyType;
    public string storyEnvironment; //Urban, Rural
    public string storySize;
    [XmlElement("Number_Of_Trooper_Injuries")]
    public int nTInjuries;
    [XmlElement("Number_Of_Trooper_Incapacitated")]
    public int nTIncapacitated;
    [XmlElement("Number_Of_Trooper_Deaths")]
    public int nTDeath;
    [XmlElement("Number_Of_Enemy_Deaths")]
    public int nEDeath;
    public List<string> strings;

    public Story_Class()
    {

    }
}
