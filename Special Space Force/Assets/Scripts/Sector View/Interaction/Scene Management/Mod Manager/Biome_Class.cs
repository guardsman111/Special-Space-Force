using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Biome_Class
{
    //Biome Class is where biome information is stored.
    [XmlElement("Name")]
    public string biomeName;
    [XmlElement("Happiness_Modifier")]
    public float happinessModifier;

    [XmlElement("Can_Have_Surface_Pop")]
    public bool SurfacePop = true;
    [XmlElement("Breathable_Atmosphere")]
    public bool Atmo = true;

    [XmlElement("Base_Biome")]
    public string baseBiome;
    [XmlElement("Material_Texture_Path")]
    public string materialTexture;
    [XmlElement("Material_Normal_Path")]
    public string materialNormal;
    [XmlElement("Material_Mask_Path")]
    public string materialMask;

    public Biome_Class()
    {
    }
}
