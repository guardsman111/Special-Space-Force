using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hover_Over_Popup : MonoBehaviour
{
    private bool showing;
    public GameObject hoverObject;


    public void HoverOver()
    {
        if (!Input.GetMouseButton(0))
        {
            showing = true;
            Invoke("Show", 0.5f);
            //Debug.Log("Mouse Entered");
        }
    }

    public void HoverLeave()
    {
        if (!Input.GetMouseButton(0))
        {
            showing = false;
            CancelInvoke("Show");
            Hide();
            //Debug.Log("Mouse Exit");
        }
    }

    public void Show()
    {
        showing = true;
        hoverObject.SetActive(true);
        if(Input.mousePosition.x > Screen.width / 2)
        {
            hoverObject.transform.position = new Vector3(Input.mousePosition.x - 300, 0, 0);
        } 
        else
        {
            hoverObject.transform.position = new Vector3(Input.mousePosition.x + 30, 0, 0);
        }
        if (Input.mousePosition.y > Screen.height / 2)
        {
            hoverObject.transform.position += new Vector3(0, Input.mousePosition.y - 50, 0);
        }
        else
        {
            hoverObject.transform.position += new Vector3(0, Input.mousePosition.y + 150, 0);
        }
    }

    public void Hide()
    {
        showing = false;
        hoverObject.SetActive(false);
    }

}
