using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Class
{
    /// <summary>
    /// A Class that holds save information to be easily serialized
    /// </summary>
    public string saveName;
    public int height;
    public int width;

    public List<System_Class> systems;

    public Save_Class()
    {

    }
}
