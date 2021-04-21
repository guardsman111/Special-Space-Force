using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter_Class 
{
    public List<Slot_Class> slots;
    public List<Enemy_Unit_Instance> enemyUnits;

    public float playerStrength;
    public List<Affected_Trooper_Class> affectedTroopers;
    public List<Trooper_Class> deadTroopers;

    public float enemyStrength;
    public float nEnemies;
    public float nDeadEnemiesTotal; //Total dead enemies
    public float nDeadEnemies; //Dead enemies this step

    public Encounter_Class()
    {

    }
}
