using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter_Class 
{
    public List<Slot_Class> slots;
    public List<Enemy_Unit_Instance> enemyUnits;

    public float playerStrength;
    public List<Affected_Trooper_Class> affectedTroopers; //troopers that were affected this round - this is reset each step
    public List<Affected_Trooper_Class> capableTroopers;
    public List<Affected_Trooper_Class> incapacitatedTroopers;
    public List<Affected_Trooper_Class> brokenTroopers;
    public List<Affected_Trooper_Class> deadTroopers;

    public string stepType;
    public float distance;
    public bool complete;

    public float enemyStrength;
    public float nEnemies;
    public float nInjuredEnemies; //Injured enemies this step
    public float nDeadEnemies; //Dead enemies this step
    public float nDeadEnemiesTotal; //Total dead enemies

    public Encounter_Class()
    {

    }
}
