using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Load_Class : MonoBehaviour
{
    public string savePath;
    public Load_Manager manager;
    public Text loadName;

    public void PressedSave()
    {
        manager.OpenSave(this);
    }

}
