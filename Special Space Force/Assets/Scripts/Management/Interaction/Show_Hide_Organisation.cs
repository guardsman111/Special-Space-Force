using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Show_Hide_Organisation : MonoBehaviour
{
    /// <summary>
    /// Manages showing/hiding of UI elements between force organisation and sector views
    /// </summary>
    public GameObject Object1;
    public GameObject Object2;
    public Text TrooperCounterText;
    public Slot_Manager manager;

    public GameObject secondaryActivation;

    //SHows or hides depnding on which is currently active
    public void ShowHide()
    {
        if (Object1.activeSelf)
        {
            Object2.SetActive(true);

            if (manager != null)
            {
                TrooperCounterText.text = manager.GetTroopers();
            }

            Object1.SetActive(false);
        }
        else
        {
            Object1.SetActive(true);

            Object2.SetActive(false);
        }
    }

    public void BasicShowHide()
    {
        if (Object1.activeSelf)
        {
            Object2.SetActive(true);

            Object1.SetActive(false);

            if (secondaryActivation != null)
            {
                secondaryActivation.SetActive(true);
            }
        }
        else
        {
            Object1.SetActive(true);

            Object2.SetActive(false);

            if (secondaryActivation != null)
            {
                secondaryActivation.SetActive(false);
            }
        }
    }
}
