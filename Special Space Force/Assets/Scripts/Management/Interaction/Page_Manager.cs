using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page_Manager : MonoBehaviour
{

    public void ChangePage(GameObject newPage)
    {
        newPage.SetActive(true);
        gameObject.SetActive(false);
    }
}
