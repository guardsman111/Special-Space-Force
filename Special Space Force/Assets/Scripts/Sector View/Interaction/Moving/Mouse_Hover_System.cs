using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mouse_Hover_System : MonoBehaviour
{
    public Text numberOfTurns;

    public bool active = false;

    public void Display()
    {
        this.gameObject.SetActive(true);
        active = true;
    }

    public void DisplayOff()
    {
        Invoke("TurnOff", 0.2f);
    }

    public void TurnOff()
    {
        this.gameObject.SetActive(false);
        active = false;
    }
    public void ChangeNTurns(int nTurns)
    {
        numberOfTurns.text = nTurns.ToString();
    }
}
