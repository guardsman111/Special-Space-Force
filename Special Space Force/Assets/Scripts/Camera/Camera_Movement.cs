using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    /// <summary>
    /// This script hold basic camera functions like moving and scrolling
    /// </summary>

    public float cameraSpeed;
    public float cameraMinHeight;
    public float cameraMaxHeight;
    public float cameraMinX;
    public float cameraMaxX;
    public float cameraMinZ;
    public float cameraMaxZ;
    private float cameraSpeedDefault;

    private bool isSpeeding;
    private float count;

    // Start is called before the first frame update
    void Start()
    {
        cameraSpeedDefault = cameraSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (transform.position.z < cameraMaxZ)
            {
                if (transform.position.y > cameraMaxHeight - cameraMinHeight) transform.position += new Vector3(0, 0, cameraSpeed * 2);
                else transform.position += new Vector3(0, 0, cameraSpeed);
            }
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (transform.position.z > cameraMinZ)
            {
                if (transform.position.y > cameraMaxHeight - cameraMinHeight) transform.position += new Vector3(0, 0, -cameraSpeed * 2);
                else transform.position += new Vector3(0, 0, -cameraSpeed);
            }
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > cameraMinX)
            {
                if (transform.position.y > cameraMaxHeight - cameraMinHeight) transform.position += new Vector3(-cameraSpeed * 2, 0, 0);
                else transform.position += new Vector3(-cameraSpeed, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < cameraMaxX)
            {
                if (transform.position.y > cameraMaxHeight - cameraMinHeight) transform.position += new Vector3(cameraSpeed * 2, 0, 0);
                else transform.position += new Vector3(cameraSpeed, 0, 0);
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
        {
            if (transform.position.y < cameraMaxHeight)
            {
                transform.position += new Vector3(0, cameraSpeed * 50, 0);
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
        {
            if (transform.position.y > cameraMinHeight)
            {
                transform.position += new Vector3(0, -cameraSpeed * 50, 0);
            }
        }
    }
}
