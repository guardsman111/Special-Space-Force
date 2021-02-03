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
    public Camera screenCamera;
    public Quickview_Voidcraft_Manager systemQVManger;

    public List<Voidcraft_Script> selectedCraft;

    public GameObject content;
    public GameObject prefabCraft;

    public GameObject craftSpace1;

    public List<GameObject> Orbiters;

    public float lastClick;
    public bool DoubleClick = false;

    private void Start()
    {
        selectedCraft = new List<Voidcraft_Script>();
    }

    private void Update()
    {
        if (screenCamera.enabled)
        {
            if (Input.GetMouseButtonUp(0))
            {
                float nowTime = Time.time;

                //Captures double click event
                if(lastClick != 0)
                {
                    if (nowTime - lastClick <= 0.5f)
                    {
                        DoubleClick = true;
                    }
                }

                lastClick = nowTime;

                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                List<RaycastResult> results = new List<RaycastResult>();

                pointerData.position = Input.mousePosition;
                raycaster.Raycast(pointerData, results);

                if (results.Count > 0)
                {
                    if (results[0].gameObject.GetComponent<Voidcraft_Script>() != null)
                    {
                        Voidcraft_Script pressed = results[0].gameObject.GetComponent<Voidcraft_Script>();
                        pressed.gameObject.GetComponent<Craft_Click>().ClickCraftPlanet();
                        if (DoubleClick)
                        {
                            if (Orbiters != null)
                            {
                                foreach (GameObject os in Orbiters)
                                {
                                    if (pressed.craftClass == os.GetComponent<Orbiter_Script>().linkedCraft)
                                    {
                                        screenCamera.GetComponent<Camera_Targeted>().SetShipTarget(os.GetComponent<Orbiter_Script>().location);
                                    }
                                }
                            }
                        }
                    }
                }
                DoubleClick = false;
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

    public void OpenPlanet(Planet_Script planet, System_Script system)
    {
        foreach (GameObject go in systemQVManger.craft)
        {
            if (system.SystemPlanets.IndexOf(planet) + 1 == go.GetComponent<Voidcraft_Script>().craftClass.planetN)
            {
                GameObject temp = Instantiate(prefabCraft, content.transform);
                Voidcraft_Script tempS = temp.GetComponent<Voidcraft_Script>();
                tempS.LoadQuickView(go.GetComponent<Voidcraft_Script>().craftClass, manager, fManager);
                temp.transform.position = craftSpace1.transform.position;
                temp.transform.position += new Vector3(50 * craft.Count, 0, 0);
                craft.Add(temp);
            }
        }
        if(craft.Count == 0)
        {
            GetComponent<Slider_Script>().headerImage.enabled = false;
        }
        else
        {
            GetComponent<Slider_Script>().headerImage.enabled = true;
        }
    }

    public void CloseManager()
    {
        if (craft.Count > 0)
        {
            if (selectedCraft.Count > 0)
            {
                foreach (Voidcraft_Script vs in selectedCraft)
                {
                    vs.imageManager.TurnOff("selected");
                }
            }
            selectedCraft.Clear();
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
            vs.imageManager.TurnOff("selected");
        }
        selectedCraft.Clear();
    }
}
