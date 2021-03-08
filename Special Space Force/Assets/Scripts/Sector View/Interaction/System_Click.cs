using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class System_Click : MonoBehaviour
{
    /// <summary>
    /// This script manages the cameras on clicking a system
    /// </summary>

    public Transform cameraTransform;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Camera systemCamera;
    [SerializeField]
    private Camera systemScreenCamera;
    [SerializeField]
    private Camera planetCamera;

    private Light starLight;

    public System_Screen systemScreen;
    public GameObject canvas;
    public System_Voidcraft_Script mover;
    public List<GameObject> avoiders;
    public Manager_Script manager;

    public Button[] UIs;

    //Finds all the cameras
    private void Start()
    {
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        systemCamera = GameObject.Find("SystemCamera").GetComponent<Camera>();
        systemScreenCamera = GameObject.Find("System Screen Camera").GetComponent<Camera>();
        planetCamera = GameObject.Find("PlanetCamera").GetComponent<Camera>();
        UIs = GameObject.Find("User Interface").GetComponentsInChildren<Button>();
        starLight = GameObject.Find("Directional Light").GetComponent<Light>();
        systemScreen = systemCamera.GetComponentInChildren<System_Screen>();
        canvas = systemCamera.GetComponentInChildren<System_Screen>().gameObject;
        mover = GameObject.Find("System Craft Viewer").GetComponent<System_Voidcraft_Script>();
        avoiders = GameObject.Find("Scene Manager").GetComponent<Scene_Manager>().avoiders;
        manager = GameObject.Find("Mod Manager").GetComponent<Manager_Script>();
    }

    //Toggles the Cameras depending on the current enabled camera
    private void OnMouseDown()
    {
        if (!planetCamera.enabled && !manager.sManager.gameObject.activeSelf && !manager.fManager.gameObject.activeSelf)
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            List<RaycastResult> hits = new List<RaycastResult>();
            bool stop = false;

            pointerData.position = Input.mousePosition;
            manager.craftMover.raycaster.Raycast(pointerData, hits);

            foreach (RaycastResult hit in hits)
            {
                if (avoiders.Contains(hit.gameObject))
                {
                    Debug.Log(hit.gameObject.name + " avoider has been clicked, stopping you from opening " + GetComponent<System_Script>().Star.systemName);
                    stop = true;
                }
                if (hit.gameObject.tag == "UIForce")
                {
                    Debug.Log(hit.gameObject.name + " has been clicked, stopping you from opening " + GetComponent<System_Script>().Star.systemName);
                    stop = true;
                }
            }

            if (!stop)
            {
                if (systemCamera.transform.position == cameraTransform.position)
                {
                    systemCamera.transform.position = mainCamera.transform.position;
                    mainCamera.enabled = true;
                    systemCamera.enabled = false;
                    systemScreenCamera.enabled = false;
                    ToggleVisiblePlanets.TogglePlanetsOn(false);
                    System_Script system = GetComponent<System_Script>();
                    system.SName.enabled = true;
                    system.Faction.enabled = true;
                    if (systemScreen.QVManager.craft.Count > 0)
                    {
                        system.craftIcon.gameObject.SetActive(true);
                    }
                    if (system.Colonising)
                    {
                        system.coloIcon.gameObject.SetActive(true);
                    }
                    systemScreen.QVManager.CloseManager();
                    systemScreen.aManager.CloseManager();
                    systemCamera.GetComponent<Camera_Container_Script>().systemHelper.HideHelper();
                    foreach (Button b in UIs)
                    {
                        b.gameObject.SetActive(true);
                    }
                    systemCamera.GetComponent<Camera_Container_Script>().sectorHelper.ShowHelper();
                    canvas.SetActive(false);
                }
                else
                {
                    systemCamera.transform.position = cameraTransform.position;
                    mainCamera.enabled = false;
                    canvas.SetActive(true);
                    systemCamera.enabled = true;
                    systemScreenCamera.enabled = true;
                    System_Script system = GetComponent<System_Script>();
                    manager.craftMover.DeselectSystem();

                    systemScreen.sname.text = "System: " + system.Star.systemName;
                    systemScreen.allegiance.text = "Owner: " + system.allegiance;
                    systemScreen.output.text = "Total Output: " + system.combinedOutput.ToString("00,0") + " Kilo-Tonnes";
                    system.SName.enabled = false;
                    system.Faction.enabled = false;
                    system.craftIcon.gameObject.SetActive(false);
                    system.coloIcon.gameObject.SetActive(false);
                    systemScreen.QVManager.OpenSystem(system.Star);
                    systemScreen.aManager.OpenSystem(system.Star);

                    starLight.GetComponent<Light_Colour>().ChangeColour(gameObject.GetComponent<System_Script>().Star.colour);

                    ToggleVisiblePlanets.TogglePlanetsOn(true);

                    systemCamera.GetComponent<Camera_Container_Script>().systemHelper.ShowHelper();
                    foreach (Button b in UIs)
                    {
                        b.gameObject.SetActive(false);
                    }
                    systemCamera.GetComponent<Camera_Container_Script>().sectorHelper.HideHelper();
                }
            }
        }
    }

    private void OnMouseOver()
    {
        if (mover.View.activeSelf)
        {
            if (systemScreen.mouseHoverer.active == false)
            {
                System_Script system = GetComponent<System_Script>();
                float distance = Vector3.Distance(mover.CurrentSystem.transform.position, system.transform.position);
                int turns = (int)distance / 1000;
                systemScreen.mouseHoverer.Display();

                systemScreen.mouseHoverer.ChangeNTurns(turns);

                systemScreen.mouseHoverer.transform.localPosition = new Vector3(Input.mousePosition.x - (Screen.width/2), Input.mousePosition.y - (Screen.height/2));
            }

            if (Input.GetMouseButtonUp(1))
            {
                System_Script system = GetComponent<System_Script>();
                float distance = Vector3.Distance(mover.CurrentSystem.transform.position, system.transform.position);
                int turns = (int)distance / 1000;
                mover.MoveCraft(system, turns);
            }
        }
    }

    private void OnMouseExit()
    {
        if (systemScreen.mouseHoverer.active == true)
        {
            systemScreen.mouseHoverer.DisplayOff();
        }
    }

    private void Update()
    {
        if (systemCamera.enabled)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (systemCamera.transform.position == cameraTransform.position)
                {
                    systemCamera.transform.position = mainCamera.transform.position;
                    mainCamera.enabled = true;
                    systemCamera.enabled = false;
                    systemScreenCamera.enabled = false;
                    ToggleVisiblePlanets.TogglePlanetsOn(false);
                    System_Script system = GetComponent<System_Script>();
                    system.SName.enabled = true;
                    system.Faction.enabled = true;
                    if (systemScreen.QVManager.craft.Count > 0)
                    {
                        system.craftIcon.gameObject.SetActive(true);
                    }
                    if (system.Colonising)
                    {
                        system.coloIcon.gameObject.SetActive(true);
                    }
                    systemScreen.QVManager.CloseManager();
                    systemScreen.aManager.CloseManager();
                    systemCamera.GetComponent<Camera_Container_Script>().systemHelper.HideHelper();
                    foreach (Button b in UIs)
                    {
                        b.gameObject.SetActive(true);
                    }
                    systemCamera.GetComponent<Camera_Container_Script>().sectorHelper.ShowHelper();
                    canvas.SetActive(false);
                }
            }
        }
    }
}
