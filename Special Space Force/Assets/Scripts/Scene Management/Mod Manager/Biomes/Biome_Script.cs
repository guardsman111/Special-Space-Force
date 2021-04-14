using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome_Script
{
    public string biomeName;

    public Biome_Class biomeC;
    public Material biomeMaterial;

    public Texture2D biomeImage;

    //Linked to the planets mesh renderer so we can change the material, allowing for players to create their own.
    public MeshRenderer planetSkin;

    public Biome_Script()
    {

    }
}
