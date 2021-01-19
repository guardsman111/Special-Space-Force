using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voidcraft_Manager : MonoBehaviour
{
    public FileFinder finder;
    public Core_Voidcraft coreVoidcraft;
    public List<Voidcraft_Pack> voidcraftClasses;

    public List<Color32> playerDefaultColours;

    public void Begin()
    {
        voidcraftClasses = coreVoidcraft.Core();
    }
}
