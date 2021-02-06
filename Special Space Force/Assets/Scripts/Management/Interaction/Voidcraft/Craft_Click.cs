using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft_Click : MonoBehaviour
{
    public Voidcraft_Script attatched;
    [SerializeField]
    private Quickview_Voidcraft_Manager manager;
    [SerializeField]
    private Quickview_Voidcraft_Manager managerP;

    private void Start()
    {
        attatched = GetComponent<Voidcraft_Script>();
        manager = attatched.modManager.craftSystem;
        managerP = attatched.modManager.craftPlanet;
    }


    public void ClickCraft()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            attatched.imageManager.TurnOn("selected");
            if (!manager.selectedCraft.Contains(attatched))
            {
                manager.selectedCraft.Add(attatched);
            }
        }
        else 
        {
            if (manager.selectedCraft.Count > 0)
            {
                foreach (Voidcraft_Script vs in manager.selectedCraft)
                {
                    vs.imageManager.TurnOff("selected");
                }
            }
            manager.selectedCraft.Clear();
            attatched.imageManager.TurnOn("selected");
            manager.selectedCraft.Add(GetComponent<Voidcraft_Script>());
        }
    }

    public void ClickCraftPlanet()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            attatched.imageManager.TurnOn("selected");
            if (!managerP.selectedCraft.Contains(attatched))
            {
                managerP.selectedCraft.Add(attatched);
            }
        }
        else
        {
            if (managerP.selectedCraft.Count > 0)
            {
                foreach (Voidcraft_Script vs in managerP.selectedCraft)
                {
                    vs.imageManager.TurnOff("selected");
                }
            }
            managerP.selectedCraft.Clear();
            attatched.imageManager.TurnOn("selected");
            managerP.selectedCraft.Add(GetComponent<Voidcraft_Script>());
        }
    }
}
