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

    // Changes colour of light to enact star colour
    public void ChangeColour(string newColour)
    {
        if (newColour == "Red")
        {
            Light.color = Red;
        }
        if (newColour == "Orange")
        {
            Light.color = Orange;
        }
        if (newColour == "Yellow")
        {
            Light.color = Yellow;
        }
        if (newColour == "White")
        {
            Light.color = White;
        }
        if (newColour == "Purple")
        {
            Light.color = Purple;
        }
        if (newColour == "Pink")
        {
            Light.color = Pink;
        }
        if (newColour == "LBlue")
        {
            Light.color = LBlue;
        }
        if (newColour == "Blue")
        {
            Light.color = Blue;
        }
        if (newColour == "LGreen")
        {
            Light.color = LGreen;
        }
        if (newColour == "Green")
        {
            Light.color = Green;
        }
    }


    //Colours
    public Color32 Red = new Color32();
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
