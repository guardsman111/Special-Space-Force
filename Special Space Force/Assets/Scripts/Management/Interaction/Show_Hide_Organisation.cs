using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Show_Hide_Organisation : MonoBehaviour
{
    /// <summary>
    /// Manages showing/hiding of UI elements between force organisation and sector views
    /// </summary>
    public GameObject userInterface;
    public GameObject forceOrganisation;
    public Text TrooperCounterText;
    public Slot_Manager manager;

    //SHows or hides depnding on which is currently active
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
