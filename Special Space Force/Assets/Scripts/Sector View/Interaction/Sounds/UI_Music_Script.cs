using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Music_Script : MonoBehaviour
{
    public Music_Sound_Script manager;
    public Toggle toggle;
    public Image image;
    public Text songName;
    public AudioClip song;

    public void ToggleSong()
    {
        manager.ToggleSong(this);
    }
}
