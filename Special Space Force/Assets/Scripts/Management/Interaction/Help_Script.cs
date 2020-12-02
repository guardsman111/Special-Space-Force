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
            helperImage.gameObject.SetActive(true);
            showing = true;
        }
    }

    public void MouseUp()
    {
        if (showing)
        {
            helperImage.gameObject.SetActive(false);
            showing = false;
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
