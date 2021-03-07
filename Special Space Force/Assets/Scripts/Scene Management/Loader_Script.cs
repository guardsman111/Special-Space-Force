using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader_Script : MonoBehaviour
{
    public static string SaveName;

    public void SetLoad()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
