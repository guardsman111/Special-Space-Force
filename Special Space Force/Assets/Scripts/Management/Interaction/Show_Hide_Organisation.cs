using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_Hide_Organisation : MonoBehaviour
{
    public GameObject userInterface;
    public GameObject forceOrganisation;

    public void ShowHide()
    {
        if (userInterface.activeSelf)
        {
            userInterface.SetActive(false);
            forceOrganisation.SetActive(true);
        }
        else
        {
            userInterface.SetActive(true);
            forceOrganisation.SetActive(false);
        }
    }

}
