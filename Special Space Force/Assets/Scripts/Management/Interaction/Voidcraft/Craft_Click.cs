using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft_Click : MonoBehaviour
{
    public Voidcraft_Script attatched;
    [SerializeField]
    private Quickview_Voidcraft_Manager manager;

    private void Start()
    {
        manager = GameObject.Find("CraftWindow").GetComponent<Quickview_Voidcraft_Manager>();
        attatched = GetComponent<Voidcraft_Script>();
    }


    public void ClickCraft()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            attatched.imageManager.TurnOn("selected");
            manager.selectedCraft.Add(attatched);
        }
        else 
        {
            foreach(Voidcraft_Script vs in manager.selectedCraft)
            {
                vs.imageManager.TurnOff("selected");
            }
            manager.selectedCraft.Clear();
            attatched.imageManager.TurnOn("selected");
            manager.selectedCraft.Add(GetComponent<Voidcraft_Script>());
        }
    }
}
