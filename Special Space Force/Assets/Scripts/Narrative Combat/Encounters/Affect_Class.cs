using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Affect_Class //This could also be known as the conditions
{
    public string effectName;
    public string effectType; //Injured, Healed, Unconcious, Dead, Broken, Rallied, Fatigued
    public Enemy_Class effectingEnemy;
    public Enemy_Weapons_Class effectingWeapon;
    public Trooper_Class effectingTrooper;

    public Affect_Class()
    {

    }
}
