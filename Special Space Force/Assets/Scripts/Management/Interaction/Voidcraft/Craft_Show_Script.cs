using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Craft_Show_Script : MonoBehaviour
{
    public Text craftClass;
    public Text speed;
    public Text armour;
    public Text location;

    public Contents_Viewer weaponBar;
    public Contents_Viewer squadBar;

    public Voidcraft_Indepth_Manager loader;


    //Updates the UI
    public void ChangeCraft(Voidcraft_Script craft)
    {
        craftClass.text = craft.GetStat("Class");
        speed.text = craft.GetStat("Speed");
        armour.text = craft.GetStat("Armour");
        location.text = craft.GetStat("Location");

        weaponBar.SetNewWeapons(craft);
        squadBar.SetNewSquads(craft);

        loader.MenuCraftDropdowns(craft);
    }
}
