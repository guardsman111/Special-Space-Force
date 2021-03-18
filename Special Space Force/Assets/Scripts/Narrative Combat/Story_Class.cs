using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Story_Class
{
    public string storyName;

    public string storyType;

    [XmlElement("Story_Strings")]
    public List<string> story;

    public Story_Class()
    {

    }
}
