using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Image_Manager : MonoBehaviour
{
    /// <summary>
    /// Manages additional images on the trooper game object
    /// </summary>
    public Image selected;
    public Image travelling;

    //Turns on selected image
    public void TurnOn(string toTurnOn)
    {
        if(toTurnOn == "selected")
        {
            selected.gameObject.SetActive(true);
        }
        if (toTurnOn == "travelling")
        {
            travelling.gameObject.SetActive(true);
        }
    }

    //Turns off selected image
    public void TurnOff(string toTurnOff)
    {
        if (toTurnOff == "selected")
        {
            selected.gameObject.SetActive(false);
        }
        if (toTurnOff == "travelling")
        {
            travelling.gameObject.SetActive(false);
        }
    }
}
