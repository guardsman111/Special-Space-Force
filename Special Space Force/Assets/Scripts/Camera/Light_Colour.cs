using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Colour : MonoBehaviour
{
    /// <summary>
    /// A currently unused script to set the light effect on planets based on the colour of the sun
    /// </summary>
    private Light Light;

    // Start is called before the first frame update
    void Start()
    {
        Light = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOrange)
        {
            Light.color = Orange;
        }
        if (isYellow)
        {
            Light.color = Yellow;
        }
        if (isWhite)
        {
            Light.color = White;
        }
        if (isPurple)
        {
            Light.color = Purple;
        }
        if (isPink)
        {
            Light.color = Pink;
        }
        if (isLBlue)
        {
            Light.color = LBlue;
        }
        if (isBlue)
        {
            Light.color = Blue;
        }
        if (isLGreen)
        {
            Light.color = LGreen;
        }
        if (isGreen)
        {
            Light.color = Green;
        }
    }

    public bool isOrange = false;
    public bool isYellow = false;
    public bool isWhite = false;
    public bool isPurple = false;
    public bool isPink = false;
    public bool isLBlue = false;
    public bool isBlue = false;
    public bool isLGreen = false;
    public bool isGreen = false;

    //Colours
    public Color32 Orange = new Color32();
    public Color32 Yellow = new Color32();
    public Color32 White = new Color32();
    public Color32 Purple = new Color32();
    public Color32 Pink = new Color32();
    public Color32 LBlue = new Color32();
    public Color32 Blue = new Color32();
    public Color32 LGreen = new Color32();
    public Color32 Green = new Color32();

}
