using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Quickview_Voidcraft_Manager : MonoBehaviour
{
    public List<GameObject> craft;
    public Manager_Script manager;
    public Fleet_Manager fManager;
    public GraphicRaycaster raycaster;
    public Camera systemCamera;

    public List<Voidcraft_Script> selectedCraft;

    public GameObject content;
    public GameObject prefabCraft;

    public GameObject craftSpace1;

    private void Start()
    {
        selectedCraft = new List<Voidcraft_Script>();
    }

    private void Update()
    {
        if (systemCamera.enabled)
        {
            if (Input.GetMouseButtonUp(0))
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                List<RaycastResult> results = new List<RaycastResult>();

                pointerData.position = Input.mousePosition;
                raycaster.Raycast(pointerData, results);

                if (results.Count > 0)
                {
                    if (results[0].gameObject.GetComponent<Voidcraft_Script>() != null)
                    {
                        Voidcraft_Script pressed = results[0].gameObject.GetComponent<Voidcraft_Script>();
                        pressed.gameObject.GetComponent<Craft_Click>().ClickCraft();
                    }
                }
            }
        }
    }

    public void OpenSystem(System_Class system)
    {
        foreach(Voidcraft_Class vc in fManager.Craft)
        {
            if(vc.starID == system.uID)
            {
                GameObject temp = Instantiate(prefabCraft, content.transform);
                Voidcraft_Script tempS = temp.GetComponent<Voidcraft_Script>();
                tempS.LoadQuickView(vc, manager, fManager);
                temp.transform.position = craftSpace1.transform.position + new Vector3(450 * craft.Count, 0,0);
                craft.Add(temp);
            }
        }
    }

    public void CloseSystem()
    {
        if (craft.Count > 0)
        {
            foreach (Voidcraft_Script vs in selectedCraft)
            {
                vs.imageManager.TurnOff("selected");
            }
            selectedCraft.Clear();
            selectedCraft.Add(GetComponent<Voidcraft_Script>());
            while (craft.Count > 0)
            {
                Destroy(craft[0]);
                craft.RemoveAt(0);
            }
        }
    }

    public void MoveCraft(Planet_Script nPlanet)
    {
        foreach(Voidcraft_Script vs in selectedCraft)
        {
            vs.MoveShip(nPlanet);
        }
    }
}
