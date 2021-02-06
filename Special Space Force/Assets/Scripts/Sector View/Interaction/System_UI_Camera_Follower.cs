using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_UI_Camera_Follower : MonoBehaviour
{
    public Camera SectorCamera;

    private void Start()
    {
        SectorCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.LookAt(SectorCamera.transform);
    }
}
