using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;

public class Race_Units_Class : MonoBehaviour
{
    public string raceName;

    [XmlElement("Race_Weapons_Class_Path")]
    public string weaponsPath;
    [XmlElement("Enemy_Class_Paths")]
    public List<string> enemyPaths;

    [XmlElement("Units")]
    public List<Unit_Class> units;

    public Race_Units_Class()
    {

    }
}
