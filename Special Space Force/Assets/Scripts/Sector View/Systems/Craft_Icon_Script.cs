using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft_Icon_Script : MonoBehaviour
{
    public System_Script system;
    public System_Voidcraft_Script manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("System Craft Viewer").GetComponent<System_Voidcraft_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Sets up the system craft menu
    private void OnMouseDown()
    {
        manager.SelectSystem(system);
        gameObject.SetActive(false);
    }
}
