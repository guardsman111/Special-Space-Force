using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Viewer : MonoBehaviour
{
    public GameObject weaponPrefab;

    public List<GameObject> weapons;

    public void SetNewWeapons(Voidcraft_Script craft)
    {
        while(weapons.Count > 0)
        {
            Destroy(weapons[0]);
        }
    }
}
