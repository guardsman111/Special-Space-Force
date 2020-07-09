using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Click : MonoBehaviour
{
    [SerializeField]
    private Camera systemCamera;
    [SerializeField]
    private Camera planetCamera;

    // Start is called before the first frame update
    void Start()
    {
        planetCamera = GameObject.Find("PlanetCamera").GetComponent<Camera>();
        systemCamera = GameObject.Find("SystemCamera").GetComponent<Camera>();
    }

    private void OnMouseDown()
    {
        planetCamera.GetComponent<Camera_Targeted>().target = transform;
        systemCamera.enabled = false;
        planetCamera.enabled = true;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            systemCamera.enabled = true;
            planetCamera.enabled = false;
        }
    }
}
