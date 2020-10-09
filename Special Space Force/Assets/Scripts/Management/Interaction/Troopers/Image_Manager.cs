using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Image_Manager : MonoBehaviour
{
    public Image selected;

    public void TurnOn(string toTurnOn)
    {
        if(toTurnOn == "selected")
        {
            selected.gameObject.SetActive(true);
        }
    }


    public void TurnOff(string toTurnOff)
    {
        if (toTurnOff == "selected")
        {
            selected.gameObject.SetActive(false);
        }
    }
}
