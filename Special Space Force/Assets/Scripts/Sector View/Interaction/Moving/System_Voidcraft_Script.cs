using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class System_Voidcraft_Script : MonoBehaviour
{
    public Sector_Manager manager;
    public Manager_Script modManager;
    public Fleet_Manager fManager;
    public Camera sectorCamera;

    private System_Script currentSystem;
    public Canvas canvas;

    public RectTransform UI_Element;

    public GameObject content;
    public GameObject prefabCraft;
    public GameObject craftSpace1;
    public List<System_Craft> craft;

    public System_Script CurrentSystem
    {
        get { return currentSystem; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectSystem(System_Script system)
    {
        if(currentSystem != null)
        {
            DeselectSystem();
        }
        currentSystem = system;
        gameObject.transform.position = system.craftIcon.transform.position;
        
        canvas.enabled = true;

        foreach (Voidcraft_Class vc in fManager.Craft)
        {
            if (vc.starID == system.Star.uID)
            {
                GameObject temp = Instantiate(prefabCraft, content.transform);
                System_Craft tempc = temp.GetComponent<System_Craft>();
                tempc.Create(vc, this);
                temp.transform.position = craftSpace1.transform.position + new Vector3(0, -100 * craft.Count, 0);
                craft.Add(tempc);
            }
        }
    }

    public void DeselectSystem()
    {
        while (craft.Count > 0)
        {
            if(craft[0].linkedCraft.starID == currentSystem.Star.uID)
            {
                currentSystem.craftIcon.SetActive(true);
            }
            Destroy(craft[0].gameObject);
            craft.RemoveAt(0);
        }
        currentSystem = null;
        canvas.enabled = false;
    }

    public void MoveCraft(System_Script system, int nTurns)
    {
        foreach(System_Craft sc in craft)
        {
            if(sc.selected.isOn)
            {
                modManager.factionManager.MoveCraftToSystem(system, currentSystem, sc.linkedCraft, nTurns);
            }
        }
        DeselectSystem();
    }

    public void CheckClose()
    {
        canvas.enabled = false;
        currentSystem.craftIcon.SetActive(true);
    }
}
