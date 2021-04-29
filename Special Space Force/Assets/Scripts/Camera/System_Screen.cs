using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class System_Screen : MonoBehaviour
{

    public Text sname;
    public Text allegiance;
    public Text output;
    public System_Click currentSystemClicker;
    public Quickview_Voidcraft_Manager QVManager;
    public Voidcraft_Indepth_Manager aManager;
    public Mouse_Hover_System mouseHoverer;

    public void CloseSystem()
    {
        currentSystemClicker.CloseSystem();
    }
}
