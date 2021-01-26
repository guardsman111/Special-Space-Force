using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Viewer : MonoBehaviour
{
    public GameObject weaponPrefab;

    public List<GameObject> weaponObjects;
    public GameObject content;

    void Start()
    {
        weaponObjects = new List<GameObject>();
    }

    public void SetNewWeapons(Voidcraft_Script craft)
    {
        while(weaponObjects.Count > 0)
        {
            Destroy(weaponObjects[0]);
            weaponObjects.RemoveAt(0);
        }
        Voidcraft_Class tempC = craft.craftClass;
        foreach(Void_Weapon_Class wc in tempC.weapons)
        {
            GameObject tempW = Instantiate(weaponPrefab, content.transform);
            Weapon_Script weaponS = tempW.GetComponent<Weapon_Script>();
            weaponS.iName.text = wc.name;
            weaponS.iNumber.text = wc.number.ToString();
            weaponS.iRange.text = (wc.range.ToString() + "km");
            weaponS.iDamage.text = wc.damage.ToString();
            weaponObjects.Add(tempW);
        }
        for (int i = 0; i < weaponObjects.Count; i++)
        {
            weaponObjects[i].transform.position = weaponObjects[i].transform.position + new Vector3(0, (-40 * i));
        }
    }
}
