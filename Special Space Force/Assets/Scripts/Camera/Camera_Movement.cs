﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    /// <summary>
    /// This script hold basic camera functions like moving and scrolling
    /// </summary>

    public float cameraSpeed;
    public float cameraRot;
    public float cameraMinHeight;
    public float cameraMaxHeight;
    public float cameraMinX;
    public float cameraMaxX;
    public float cameraMinZ;
    public float cameraMaxZ;
    public float yMinLimit = -90f;
    public float yMaxLimit = 90f;
    float rotationYAxis = 0.0f;
    float rotationXAxis = 0.0f;
    float velocityX = 0.0f;
    float velocityY = 0.0f;
    public float smoothTime = 5f;
    private float cameraSpeedDefault;
    public Camera thisCamera;
    public Transform rotationKeeper;

    public bool pause = false;

    private bool isSpeeding;
    private float count;

    // Panning Variables
    public float panSpeed = 4.0f;

    private Vector3 mouseOrigin;
    private bool isPanning;

    // Start is called before the first frame update
    void Start()
    {
        cameraSpeedDefault = Scene_Manager.cameraMov;
        cameraRot = Scene_Manager.cameraRot;
        rotationXAxis = thisCamera.transform.rotation.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (thisCamera.enabled)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                if (transform.position.z < cameraMaxZ)
                {
                    if (transform.position.y > cameraMaxHeight - cameraMinHeight) transform.position += rotationKeeper.forward * cameraSpeed * 2;
                    else transform.position += rotationKeeper.forward * cameraSpeed;
                }
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                if (transform.position.z > cameraMinZ)
                {
                    if (transform.position.y > cameraMaxHeight - cameraMinHeight) transform.position += -rotationKeeper.forward * cameraSpeed * 2;
                    else transform.position += -rotationKeeper.forward * cameraSpeed;
                }
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (transform.position.x > cameraMinX)
                {
                    if (transform.position.y > cameraMaxHeight - cameraMinHeight) transform.position += -thisCamera.transform.right * cameraSpeed * 2;
                    else transform.position += -thisCamera.transform.right * cameraSpeed;
                }
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (transform.position.x < cameraMaxX)
                {
                    if (transform.position.y > cameraMaxHeight - cameraMinHeight) transform.position += thisCamera.transform.right * cameraSpeed * 2;
                    else transform.position += thisCamera.transform.right * cameraSpeed;
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0.0f && !pause)
            {
                if (transform.position.y < cameraMaxHeight)
                {
                    transform.position += new Vector3(0, cameraSpeed * 50, 0);
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0.0f && !pause)
            {
                if (transform.position.y > cameraMinHeight)
                {
                    transform.position += new Vector3(0, -cameraSpeed * 50, 0);
                }
            }

            if (Input.GetMouseButtonDown(2) && Input.GetKey(KeyCode.LeftShift))
            {
                mouseOrigin = Input.mousePosition;

                isPanning = true;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else if (Input.GetMouseButton(2))
            {
                velocityX += cameraRot * Input.GetAxis("Mouse X") * 0.01f;
                velocityY += cameraRot * Input.GetAxis("Mouse Y") * 0.01f;
                rotationYAxis += velocityX;
                rotationXAxis -= velocityY;
                rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);
                Quaternion fromRotation = Quaternion.Euler(thisCamera.transform.rotation.eulerAngles.x, thisCamera.transform.rotation.eulerAngles.y, 0);
                Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
                Quaternion rotation = toRotation;

                float distance = Mathf.Clamp(transform.position.y - Input.GetAxis("Mouse ScrollWheel") * 20, cameraMinHeight, cameraMaxHeight);
                Vector3 position = rotation * transform.position;

                thisCamera.transform.rotation = rotation;
                velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
                velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
                if(new Vector3(thisCamera.transform.forward.x, transform.forward.y, thisCamera.transform.forward.z) != Vector3.zero)
                {
                    rotationKeeper.transform.forward = new Vector3(thisCamera.transform.forward.x, transform.forward.y, thisCamera.transform.forward.z);
                }
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            
            if (!Input.GetMouseButton(2))
            {

                isPanning = false;
            }

            if (!Input.GetMouseButton(1))
            {

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (isPanning)
            {
                Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
                Vector3 move = new Vector3(-pos.x * panSpeed, 0, -pos.y * panSpeed);

                transform.Translate(move, Space.World);
            }
        }
    }
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
