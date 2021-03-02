using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Help_Script : MonoBehaviour
{
    public Image helperImage;
    public Image helperIcon;
    private bool showing = false;

    
    public void MouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            if (showing)
            {
                helperImage.gameObject.SetActive(false);
                showing = false;
            }
            else
            {
                helperImage.gameObject.SetActive(true);
                showing = true;
            }
        }
    }


    public void ShowHelper()
    {
        helperIcon.gameObject.SetActive(true);
    }

    public void HideHelper()
    {
        helperIcon.gameObject.SetActive(false);
    }
}
