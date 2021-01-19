using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Voidcraft_Script : MonoBehaviour
{
    public Manager_Script modManager;

    public string craftName;
    public int id;
    public int starID;
    public Voidcraft_Class craftClass;

    //Base values
    [SerializeField]
    private int movementSpeed;
    [SerializeField]
    private int constructionPoints;
    [SerializeField]
    private int carryingCapacity;

    private List<Void_Weapon_Class> weapons;

    public Image[] craftImages; //0 Outline, 1 Primary, 2 Secondary, 3 Tertiary, 4 Trim, 5 Special

    public void MakeCraft(Voidcraft_Class craft, Manager_Script m, int ID, int systemID)
    {
        modManager = m;
        id = ID;
        starID = systemID;
    }

    //Sets the crafts colour scheme
    public void TrooperColours()
    {
        craftImages[1].color = modManager.voidcraftManager.playerDefaultColours[1];
        craftImages[2].color = modManager.voidcraftManager.playerDefaultColours[2];
        craftImages[3].color = modManager.voidcraftManager.playerDefaultColours[3];
        craftImages[4].color = modManager.voidcraftManager.playerDefaultColours[4];
        craftImages[5].color = modManager.voidcraftManager.playerDefaultColours[5];
    }
}
