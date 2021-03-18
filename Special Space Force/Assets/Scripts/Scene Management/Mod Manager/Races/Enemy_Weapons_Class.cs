using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Enemy_Weapons_Class : MonoBehaviour
{
    [XmlElement("Weapon_Name")]
    public string weaponName;
    public string nickname;

    public int strength;
    public string type;

    [XmlElement("List_of_Categories")]
    public List<Category_Class> categories;

    public Enemy_Weapons_Class()
    {

    }
}
