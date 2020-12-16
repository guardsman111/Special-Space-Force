using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Force_Manager : MonoBehaviour
{
    /// <summary>
    /// This script handles all interactions between the player's force and the player faction, such as exchanging of money, research and information
    /// </summary>
    public float playerFinances;
    public Faction_Manager factionManager;
    private List<Stockpile_Class> stockpile;

    public Text visibleRequisition1;
    public Text visibleRequisition2;

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
        playerFinances += income;
        visibleRequisition1.text = playerFinances.ToString();
        visibleRequisition2.text = playerFinances.ToString();
    }


}
