using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    //Toggles the Cameras depending on the current enabled camera
    private void OnMouseDown()
    {
        if (!planetCamera.enabled)
        {
            if (systemCamera.transform.position == cameraTransform.position)
            {
                systemCamera.transform.position = mainCamera.transform.position;
                mainCamera.enabled = true;
                systemCamera.enabled = false;
                systemScreenCamera.enabled = false;
                ToggleVisiblePlanets.TogglePlanetsOn(false);
                systemScreen.QVManager.CloseManager();
                systemCamera.GetComponent<Camera_Container_Script>().systemHelper.HideHelper();
                foreach(Button b in UIs)
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

                systemScreen.sname.text = "System: " + system.Star.systemName;
                systemScreen.allegiance.text = "Owner: " + system.allegiance;
                systemScreen.output.text = "Total Output: " + system.combinedOutput.ToString("00,0") + " Kilo-Tonnes";
                systemScreen.QVManager.OpenSystem(system.Star);

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

    private void Update()
    {
        if (systemCamera.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                systemCamera.transform.position = mainCamera.transform.position;
                mainCamera.enabled = true;
                systemCamera.enabled = false;
                systemScreenCamera.enabled = false;
                systemScreen.QVManager.CloseManager();
                ToggleVisiblePlanets.TogglePlanetsOn(false);
                systemCamera.GetComponent<Camera_Container_Script>().systemHelper.HideHelper();
                foreach (Button b in UIs)
                {
                    b.gameObject.SetActive(true);
                }
                systemCamera.GetComponent<Camera_Container_Script>().sectorHelper.ShowHelper();
            }
        }
    }
}
