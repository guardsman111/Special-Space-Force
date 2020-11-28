using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hover_Over_Popup : MonoBehaviour
{

    public void HoverOver()
    {
        Debug.Log("Mouse Entered");
    }

    public void HoverLeave()
    {
        Debug.Log("Mouse Exit");
    }
}
