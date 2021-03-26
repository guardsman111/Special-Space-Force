using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Force_Manager : MonoBehaviour
{
    /// <summary>
    /// This script handles all interactions between the player's force and the player faction, such as exchanging of money, research and information
    /// </summary>
    /// 
    public Manager_Script modManager;
    public int playerFinances;
    public Faction_Manager factionManager;
    public Force_Class forceClass;
    private List<Stockpile_Class> stockpile;

    public Text visibleRequisition1;
    public Text visibleRequisition2;

    private int numberOfTroopers;
    public Text nTroopers;



    public List<Stockpile_Class> Stockpile
    {
        get { return stockpile; }

        set
        {
            if(value != stockpile)
            {
                stockpile = value;
            }
        }
    }

    void Start()
    {
        stockpile = new List<Stockpile_Class>();
    }

    // Turn end handles the transition between one turn and the next, for income right now
    public void TurnEnd(float income)
    {
        if (nTroopers.text != "# of Troops")
        {
            numberOfTroopers = int.Parse(nTroopers.text);
        }
        playerFinances += (int)income;
        visibleRequisition1.text = playerFinances.ToString();
        visibleRequisition2.text = playerFinances.ToString();
        if(forceClass == null)
        {
            forceClass = new Force_Class();
            forceClass.funds = playerFinances;
            forceClass.nTroopers = numberOfTroopers;
        }
        else
        {
            forceClass.funds = playerFinances;
            forceClass.nTroopers = numberOfTroopers;
        }
    }


    // Turn end handles the transition between one turn and the next, for income right now
    public void Load(Generation_Class product)
    {
        forceClass = product.force;
        numberOfTroopers = forceClass.nTroopers;
        nTroopers.text = forceClass.nTroopers.ToString();
        playerFinances = forceClass.funds;
        visibleRequisition1.text = playerFinances.ToString();
        visibleRequisition2.text = playerFinances.ToString();
    }

    //Calculates the total force strength of the player - currently 2 points per trooper
    public float GetForceStrength()
    {
        float strengthR = 0;
        foreach(Slot_Class sc in modManager.sManager.Slots[0].containedSlots)
        {
            if(sc.squad == true)
            {
                strengthR += sc.numberOfTroopers * 3;
            }
            else
            {
                strengthR += GetStrengthChildren(sc);
            }
        }

        return strengthR;
    }

    //Continuation of above
    public float GetStrengthChildren(Slot_Class slot)
    {
        float strengthR = 0;


        foreach (Slot_Class sc in slot.containedSlots)
        {
            if (sc.squad == true)
            {
                strengthR += sc.numberOfTroopers * 3;
            }
            else
            {
                strengthR += GetStrengthChildren(sc);
            }
        }

        return strengthR;
    }
}
