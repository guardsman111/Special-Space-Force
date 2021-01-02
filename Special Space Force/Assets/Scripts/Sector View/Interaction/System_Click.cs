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

    public GameObject forceOrg;

    //Finds all the cameras
    private void Start()
    {
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        systemCamera = GameObject.Find("SystemCamera").GetComponent<Camera>();
        systemScreenCamera = GameObject.Find("System Screen Camera").GetComponent<Camera>();
        planetCamera = GameObject.Find("PlanetCamera").GetComponent<Camera>();
        forceOrg = GameObject.Find("User Interface").GetComponentInChildren<Button>().gameObject;
        starLight = GameObject.Find("Directional Light").GetComponent<Light>();
        systemScreen = systemCamera.GetComponentInChildren<System_Screen>();
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
                systemCamera.GetComponent<Camera_Container_Script>().systemHelper.HideHelper();
                forceOrg.SetActive(true);
                systemCamera.GetComponent<Camera_Container_Script>().sectorHelper.ShowHelper();
            }
            else
            {
                systemCamera.transform.position = cameraTransform.position;
                mainCamera.enabled = false;
                systemCamera.enabled = true;
                systemScreenCamera.enabled = true;
                System_Script system = GetComponent<System_Script>();

                systemScreen.name.text = "System: " + system.Star.systemName;
                systemScreen.allegiance.text = "Owner: " + system.allegiance;
                systemScreen.output.text = "Total Output: " + system.combinedOutput.ToString("00,0") + " Kilo-Tonnes";

                starLight.GetComponent<Light_Colour>().ChangeColour(gameObject.GetComponent<System_Script>().Star.colour);

                ToggleVisiblePlanets.TogglePlanetsOn(true);

                systemCamera.GetComponent<Camera_Container_Script>().systemHelper.ShowHelper();
                forceOrg.SetActive(false);
                systemCamera.GetComponent<Camera_Container_Script>().sectorHelper.HideHelper();
            }
        }
    }
}
