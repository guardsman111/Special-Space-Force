using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet_Generator : MonoBehaviour
{
    public FileFinder finder;
    public Manager_Script modManager;
    public Fleet_Manager fManager;
    public List<Fleet_Class> fleets;
    public string fileLocation;
    public GameObject genericFleet;
    public GameObject genericShip;
    public GameObject content;

    public bool loading;


    public List<Fleet_Class> SetDefaultFleets()
    {
        fleets = new List<Fleet_Class>();

        Fleet_Class tempFc = new Fleet_Class();
        tempFc.containedCraft = new List<Voidcraft_Class>();
        tempFc.fleetName = "Testing Fleet";

        Voidcraft_Class tempVc = new Voidcraft_Class();
        tempVc.className = "Farsky Heavy Destroyer";
        tempVc.craftName = "Hell's Bells";
        tempVc.type = "Destroyer";

        tempFc.containedCraft.Add(tempVc);

        tempVc = new Voidcraft_Class();
        tempVc.className = "Hifrin Cruiser";
        tempVc.craftName = "Bad Motherfucker";
        tempVc.type = "Cruiser";

        tempFc.containedCraft.Add(tempVc);

        fleets.Add(tempFc);


        fManager.Fleets = fleets;
        fManager.generator = this;
        fManager.viewedFleet = null;

        foreach (Fleet_Class fc in fleets)
        {
            GameObject temp = Instantiate(genericFleet, content.transform);

            //if loading, load the slots intead
            if (loading)
            {
                //temp.GetComponent<Fleet_Script>().LoadSlot(fc, count, manager);
            }
            else
            {
                temp.GetComponent<Fleet_Script>().MakeFleet(fc, fManager);
            }

            temp.GetComponent<Fleet_Script>().SetPosition(fManager.viewedFleet);
            fManager.FleetsS.Add(temp.GetComponent<Fleet_Script>());

        }

        for (int i = 0; i < fleets.Count; i++)
        {
            int count = 0;
            foreach (Voidcraft_Class vc in fleets[i].containedCraft)
            {
                GameObject temp = fManager.FleetsS[i].gameObject;
                count += 1;
                GameObject tempV = Instantiate(genericShip, temp.transform);
                tempV.GetComponent<Voidcraft_Script>().MakeCraft(vc, modManager, fManager, count, temp.GetComponent<Fleet_Script>());
                tempV.GetComponent<Voidcraft_Script>().SetPosition(null);
                temp.GetComponent<Fleet_Script>().containedCraft.Add(tempV.GetComponent<Voidcraft_Script>());
                fManager.Craft.Add(tempV.GetComponent<Voidcraft_Script>().craftClass);
            }
        }

        fManager.gameObject.SetActive(false);

        return fleets;
    }

}
