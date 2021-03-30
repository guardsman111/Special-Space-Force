using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contents_Viewer : MonoBehaviour
{
    public Manager_Script manager;
    public Show_Hide_Organisation showHide;

    public GameObject Prefab;

    public List<GameObject> objects;
    public GameObject content;

    void Start()
    {
        objects = new List<GameObject>();
    }

    public void SetNewWeapons(Voidcraft_Script craft)
    {
        while(objects.Count > 0)
        {
            Destroy(objects[0]);
            objects.RemoveAt(0);
        }
        Voidcraft_Class tempC = craft.craftClass;
        foreach(Void_Weapon_Class wc in tempC.weapons)
        {
            GameObject tempW = Instantiate(Prefab, content.transform);
            Weapon_Script weaponS = tempW.GetComponent<Weapon_Script>();
            weaponS.iName.text = wc.name;
            weaponS.iNumber.text = wc.number.ToString();
            weaponS.iRange.text = (wc.range.ToString() + "km");
            weaponS.iDamage.text = wc.damage.ToString();
            objects.Add(tempW);
        }
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].transform.position = objects[i].transform.position + new Vector3(0, (-40 * i));
        }
    }

    public void SetNewSquads(Voidcraft_Script craft)
    {
        while (objects.Count > 0)
        {
            Destroy(objects[0]);
            objects.RemoveAt(0);
        }
        foreach (Slot_Class sc in craft.CarriedSlots)
        {
            if (sc.squad)
            {
                GameObject tempW = Instantiate(Prefab, content.transform);
                Squad_Script sScript = tempW.GetComponent<Squad_Script>();
                sScript.sName.text = sc.slotName;
                sScript.sSize.text = sc.numberOfTroopers.ToString();
                sScript.sType.text = sc.squadRole;
                sScript.slotClass = sc;
                sScript.parentScript = this;
                objects.Add(tempW);
            }
        }
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].transform.position = objects[i].transform.position + new Vector3(0, (-80 * i));
        }
    }

    public void OpenSquad(Slot_Class slotClass)
    {
        GameObject tempS = Instantiate(manager.sManager.prefabSlot);
        tempS.GetComponent<Slot_Script>().LoadSlotSimple(slotClass, 0, manager.sManager);
        showHide.BasicShowHide();
        manager.sManager.OpenSlot(tempS.GetComponent<Slot_Script>());
        Destroy(tempS);
    }
}
