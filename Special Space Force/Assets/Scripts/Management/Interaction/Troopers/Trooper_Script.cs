using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trooper_Script : MonoBehaviour
{
    public string trooperName;
    public string trooperRank;
    public int trooperPosition;
    public int trooperFace;
    public int trooperHair;
    public int hairColour;
    public Trooper_Class trooperClass;
    public Slot_Script trooperSquad;
    public string armour;
    public string armourPattern;
    public string fatiguePattern;
    public string helmetPattern;
    public string EquipmentPattern;

    public Slot_Manager manager;
    public Equipment_Manager equipmentManager;
    public GameObject image;
    public TMP_InputField input;

    public Image[] trooperImages; //Trooper Outline, T Colour, Hair Colour, Fatigue Primary, F Secondary, F Tertiary, F Special, F Outline,
                                  //Armour Primary, A Secondary, A Tertiary, A Special, A Equipment, A Force Icon, A Outline, Helmet Primary,
                                  //H Secondary, H Tertiary, H Equipment, H Visor, H Outline, Weapon, W Colour

    public void MakeTrooper(Trooper_Class trooper, int positionID, Slot_Manager nManager)
    {
        manager = nManager;
        equipmentManager = GameObject.FindGameObjectWithTag("EquipmentManager").GetComponent<Equipment_Manager>();
        trooperClass = trooper;
        trooperName = trooper.trooperName;
        trooperRank = trooper.trooperRank;
        trooperPosition = positionID;
        trooperFace = trooper.trooperFace;
        trooperHair = trooper.trooperHair;
        hairColour = trooper.hairColour;

        trooperImages[1].sprite = manager.trooperSkinPack.containedSprites[trooperFace];
        if (trooper.gender == 0)
        {
            trooperImages[2].sprite = manager.femaleHairPack.containedSprites[trooperHair];
        }
        if (trooper.gender == 1)
        {
            trooperImages[2].sprite = manager.maleHairPack.containedSprites[trooperHair];
        }

        trooperImages[2].color = manager.hairColours[hairColour];

        input.text = trooperName;

        armour = "Mk1 Armour";
        armourPattern = "Primary1";
    }


    //Sets the position and scale of the slot according to its slot height relative to the currently viewed slot
    public void SetPosition(Slot_Script parent, Slot_Script viewedSlot)
    {
        RectTransform rTransform = GetComponent<RectTransform>();
        if (trooperSquad == viewedSlot) 
        {
            switch (trooperPosition)
            {

                case 1:
                    rTransform.position = parent.squadPositions[0].transform.position;
                    break;

                case 2:
                    rTransform.position = parent.squadPositions[1].transform.position;
                    break;

                case 3:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(250,0);
                    break;

                case 4:
                    rTransform.position = parent.squadPositions[1].transform.position + new Vector3(250, 0);
                    break;

                case 5:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(500, 0);
                    break;

                case 6:
                    rTransform.position = parent.squadPositions[1].transform.position + new Vector3(500, 0);
                    break;

                case 7:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(750, 0);
                    break;

                case 8:
                    rTransform.position = parent.squadPositions[1].transform.position + new Vector3(750, 0);
                    break;

                case 9:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(1000, 0);
                    break;

                case 10:
                    rTransform.position = parent.squadPositions[1].transform.position + new Vector3(1000, 0);
                    break;

                case 11:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(1250, 0);
                    break;

                case 12:
                    rTransform.position = parent.squadPositions[1].transform.position + new Vector3(1250, 0);
                    break;

                case 13:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(1500, 0);
                    break;

                case 14:
                    rTransform.position = parent.squadPositions[1].transform.position + new Vector3(1500, 0);
                    break;

                case 15:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(1750, 0);
                    break;

                case 16:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(1750, 0);
                    break;

                case 17:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(2000, 0);
                    break;

                case 18:
                    rTransform.position = parent.squadPositions[1].transform.position + new Vector3(2000, 0);
                    break;

                case 19:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(2250, 0);
                    break;

                case 20:
                    rTransform.position = parent.squadPositions[1].transform.position + new Vector3(2250, 0);
                    break;

            }
            gameObject.transform.localScale = new Vector3(1, 1);
            input.transform.localScale = new Vector3(1, 1);
            input.textComponent.transform.localScale = new Vector3(1, 1);
            input.textComponent.enableWordWrapping = true;

            gameObject.transform.localScale = new Vector3(1, 1);
            input.gameObject.SetActive(true);
            image.SetActive(true);
        }
        else if (viewedSlot.containedSlots.Contains(trooperSquad))
        {
            switch (trooperPosition)
            {

                case 1:
                    rTransform.position = parent.squadPositions[0].transform.position;
                    break;

                case 2:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175),0);
                    break;

                case 3:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 2), 0);
                    break;

                case 4:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 3), 0);
                    break;

                case 5:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 4), 0);
                    break;

                case 6:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(0, -200);
                    break;

                case 7:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175), -200);
                    break;

                case 8:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 2), -200);
                    break;

                case 9:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 3), -200);
                    break;

                case 10:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 4), -200);
                    break;

                case 11:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(0, -400);
                    break;

                case 12:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175), -400);
                    break;

                case 13:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 2), -400);
                    break;

                case 14:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 3), -400);
                    break;

                case 15:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 4), -400);
                    break;

                case 16:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3(0, -600);
                    break;

                case 17:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175), -600);
                    break;

                case 18:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 2), -600);
                    break;

                case 19:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 3), -600);
                    break;

                case 20:
                    rTransform.position = parent.squadPositions[0].transform.position + new Vector3((175 * 4), -600);
                    break;

            }

            input.gameObject.SetActive(true);
            image.SetActive(true);
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f);
            input.transform.localScale = new Vector3(2, 2);
        }
        else
        {
            input.gameObject.SetActive(false);
            image.SetActive(false);
        }
    }

    public void ChangeEquipment(Dropdown dropdown)
    {
        equipmentManager.ChangeEquipment(this, dropdown);
    }
    public void ChangePattern(Dropdown dropdown)
    {
        equipmentManager.ChangePattern(this, dropdown);
    }
}
