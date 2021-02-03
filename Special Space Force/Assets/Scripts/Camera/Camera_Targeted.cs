using UnityEngine;
using System.Collections;

public class Camera_Targeted : MonoBehaviour
{
    /// <summary>
    /// This script moves the camera around the planet in a cool way. Taken from online
    /// </summary>
    // This Script was taken from an example here - https://answers.unity.com/questions/1257281/how-to-rotate-camera-orbit-around-a-game-object-on.html - and then modified

    public Transform target;
    public float distance = 70.0f;
    public float xSpeed = 0.15f;
    public float ySpeed = 5f;
    public float yMinLimit = -90f;
    public float yMaxLimit = 90f;
    public float distanceMin = 40f;
    public float distanceMax = 100f;
    public float smoothTime = 15f;
    float rotationYAxis = 0.0f;
    float rotationXAxis = 0.0f;
    float velocityX = 0.0f;
    float velocityY = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationYAxis = angles.y;
        rotationXAxis = angles.x;
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    void LateUpdate()
    {
        if (target)
        {
            if (Input.GetMouseButton(0))
            {
                velocityX += xSpeed * Input.GetAxis("Mouse X") * distance * 0.01f;
                velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.01f;
            }
            rotationYAxis += velocityX;
            rotationXAxis -= velocityY;
            rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);
            Quaternion fromRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
            Quaternion rotation = toRotation;

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 20, distanceMin, distanceMax);
            RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position, out hit, ~9))
            {
                distance -= hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
            velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
            velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
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

    public void SetShipTarget(Transform ship)
    {
        target = ship;
        distance = 5.0f;
        xSpeed = 3f;
        ySpeed = 5f;
        yMinLimit = -90f;
        yMaxLimit = 90f;
        distanceMin = 1f;
        distanceMax = 7f;
        smoothTime = 15f;
    }

    public void SetDefaults(GameObject  planet)
    {
        target = planet.transform;
        distance = 70.0f;
        xSpeed = 0.15f;
        ySpeed = 5f;
        yMinLimit = -90f;
        yMaxLimit = 90f;
        distanceMin = 40f;
        distanceMax = 100f;
        smoothTime = 15f;
    }
}
