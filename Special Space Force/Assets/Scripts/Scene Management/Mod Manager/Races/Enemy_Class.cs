using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Enemy_Class
{
    public string enemyName;

    public List<string> nicknames;

    public int health;
    public int size;
    public int speed;
    public int strength;

    [XmlElement("Primary_Weapon")]
    public string weapon1;
    [XmlElement("Secondary_Weapon")]
    public string weapon2;

    [XmlElement("List_of_Categories")]
    public List<Category_Class> Categories;

    public Enemy_Class()
    {

    }
}
