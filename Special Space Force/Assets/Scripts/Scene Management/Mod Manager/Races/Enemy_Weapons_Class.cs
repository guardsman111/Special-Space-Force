using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Enemy_Weapons_Class
{
    [XmlElement("Weapon_Name")]
    public string weaponName;
    public string nickname;

    public int strength;
    public int range;
    public string type;

    public List<Category_Class> categories;

    public Enemy_Weapons_Class()
    {

    }
}
