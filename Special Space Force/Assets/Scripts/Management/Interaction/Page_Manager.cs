using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page_Manager : MonoBehaviour
{
    /// <summary>
    /// Manages visibility of pages on the customisation menu
    /// </summary>

    //Changes the page to newPage
    public void ChangePage(GameObject newPage)
    {
        newPage.SetActive(true);
        gameObject.SetActive(false);
    }
}
