using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiter_Script : MonoBehaviour
{
    public MeshRenderer primaryHull;
    public MeshRenderer secondaryHull;
    public MeshRenderer tertiaryHull;
    public MeshRenderer trimHull;
    public MeshRenderer specialHull;

    public Transform location;

    public MeshRenderer[] turrets;

    public Voidcraft_Class linkedCraft;

    public void CreateOrbiter(Fleet_Manager manager, Voidcraft_Class vClass)
    {
        float rx = Random.Range(0, 360);
        float ry = Random.Range(0, 360);
        float rz = Random.Range(0, 360);

        linkedCraft = vClass;

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(rx, ry, rz));

        Voidcraft_Script craft = new Voidcraft_Script();
        foreach(Fleet_Script fs in manager.FleetsS)
        {
            foreach(Voidcraft_Script vs in fs.containedCraft)
            {
                if(vs.craftClass.ID == vClass.ID)
                {
                    craft = vs;
                    break;
                }
            }
        }

        if (craft != null)
        {
            if (craft.craftFleet.fColours == true)
            {
                ChangeMaterialColours(craft.craftFleet.fleetClass.fleetColours);
            }
            else
            {
                ChangeMaterialColours(manager.modManager.voidcraftManager.playerFleetColours);
            }
        }
    }

    public void ChangeMaterialColours(List<Color32> colours)
    {

        primaryHull.material.color = colours[0];
        //secondaryHull.material.color = colours[1];
        tertiaryHull.material.color = colours[2];
        //trimHull.material.color = colours[3];
        specialHull.material.color = colours[4];

        foreach(MeshRenderer m in turrets)
        {
            m.materials[0].color = colours[0];
            m.materials[1].color = colours[2];
        }
    }
}
