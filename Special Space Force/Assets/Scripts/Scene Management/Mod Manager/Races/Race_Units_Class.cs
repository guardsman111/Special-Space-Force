using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;

public class Race_Units_Class
{
    public string raceName;

    [XmlElement("Race_Weapons_Class_Path")]
    public string weaponsPath;
    public List<string> enemyPaths;

    public List<Unit_Class> units;

    public Race_Units_Class()
    {

    }
}
