using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Click : MonoBehaviour
{
    public Transform cameraTransform;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Camera systemCamera;

    private void Start()
    {
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        systemCamera = GameObject.Find("SystemCamera").GetComponent<Camera>();
    }

    private void OnMouseDown()
    {
        if (systemCamera.transform.position == cameraTransform.position)
        {
            systemCamera.transform.position = mainCamera.transform.position;
            mainCamera.enabled = true;
            systemCamera.enabled = false;
            ToggleVisiblePlanets.TogglePlanetsOn(false);
        }
        else
        {
            systemCamera.transform.position = cameraTransform.position;
            mainCamera.enabled = false;
            systemCamera.enabled = true;
            ToggleVisiblePlanets.TogglePlanetsOn(true);
        }
    }
}
