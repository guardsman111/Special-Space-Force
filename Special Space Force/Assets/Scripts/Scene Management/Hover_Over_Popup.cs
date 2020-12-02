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
    }

    public void Hide()
    {
        showing = false;
        hoverObject.SetActive(false);
    }

    void Update()
    {
        if (showing)
        {
            hoverObject.transform.position = Input.mousePosition - new Vector3(30, 50, -3);
        }
    }
}
