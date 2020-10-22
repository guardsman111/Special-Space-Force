using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Show_Hide_Organisation : MonoBehaviour
{
    public GameObject userInterface;
    public GameObject forceOrganisation;
    public Text TrooperCounterText;
    public Slot_Manager manager;

    public void ShowHide()
    {
        if (userInterface.activeSelf)
        {
            forceOrganisation.SetActive(true);

            TrooperCounterText.text = manager.GetTroopers();

            userInterface.SetActive(false);
        }
        else
        {
            userInterface.SetActive(true);

            forceOrganisation.SetActive(false);
        }
    }

}
