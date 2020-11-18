using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trooper_Show_Script : MonoBehaviour
{
    public Text Speed;
    public Text Agility;
    public Text Strength;
    public Text Size;
    public Text Morale;
    public Text BreakValue;
    public Text Melee;
    public Text Ranged;
    public Text Stealth;
    public Text Stamina;
    public Text Trait1;
    public Text Trait2;


    public void ChangeTrooper(Trooper_Script trooper)
    {
        Speed.text = trooper.GetStat("Speed").ToString();
        Agility.text = trooper.GetStat("Agility").ToString();
        Strength.text = trooper.GetStat("Strength").ToString();
        Size.text = trooper.GetStat("Size").ToString();
        Morale.text = trooper.GetStat("Morale").ToString();
        BreakValue.text = trooper.GetStat("Break Value").ToString();
        Melee.text = trooper.GetStat("Melee").ToString();
        Ranged.text = trooper.GetStat("Ranged").ToString();
        Stealth.text = trooper.GetStat("Stealth").ToString();
        Stamina.text = trooper.GetStat("Stamina").ToString();
        Trait1.text = trooper.GetTrait("Trait1");
        Trait2.text = trooper.GetTrait("Trait2");
    }
}
