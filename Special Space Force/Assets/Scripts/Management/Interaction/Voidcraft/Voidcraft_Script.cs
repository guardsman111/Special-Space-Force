using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Voidcraft_Script : MonoBehaviour
{
    public Manager_Script modManager;

    public string craftName;
    public int id;
    public int starID;
    public Voidcraft_Class craftClass;
    public Fleet_Script craftFleet;
    public int craftPosition;

    //Base values
    [SerializeField]
    private int movementSpeed;
    [SerializeField]
    private int constructionPoints;
    [SerializeField]
    private int carryingCapacity;

    private List<Void_Weapon_Class> weapons;

    public Image[] craftImages; //0 Outline, 1 Primary, 2 Secondary, 3 Tertiary, 4 Trim, 5 Special

    public GameObject image;
    public TMP_InputField input;

    public void MakeCraft(Voidcraft_Class craft, Manager_Script m, int ID, Fleet_Script fleet)
    {
        modManager = m;
        id = ID;
        craftFleet = fleet;
    }

    //Sets the crafts colour scheme
    public void CraftColours()
    {
        craftImages[1].color = modManager.voidcraftManager.playerFleetColours[1];
        craftImages[2].color = modManager.voidcraftManager.playerFleetColours[2];
        craftImages[3].color = modManager.voidcraftManager.playerFleetColours[3];
        craftImages[4].color = modManager.voidcraftManager.playerFleetColours[4];
        craftImages[5].color = modManager.voidcraftManager.playerFleetColours[5];
    }

    public void ChangeCraft(Dropdown dropdown)
    {
        modManager.voidcraftManager.ChangeCraft(dropdown, this);
    }

    //Sets the position and scale of the slot according to its slot height relative to the currently viewed slot
    public void SetPosition(Fleet_Script viewedFleet)
    {
        RectTransform rTransform = GetComponent<RectTransform>();
        if (craftFleet == viewedFleet)
        {
            switch (craftPosition)
            {

                case 1:
                    rTransform.position = craftFleet.craftPositions[0].transform.position;
                    break;

                case 2:
                    rTransform.position = craftFleet.craftPositions[1].transform.position;
                    break;

                case 3:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(300, 0);
                    break;

                case 4:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(300, 0);
                    break;

                case 5:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(600, 0);
                    break;

                case 6:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(600, 0);
                    break;

                case 7:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(900, 0);
                    break;

                case 8:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(900, 0);
                    break;

                case 9:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(1200, 0);
                    break;

                case 10:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(1200, 0);
                    break;

                case 11:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(1500, 0);
                    break;

                case 12:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(1500, 0);
                    break;

                case 13:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(1800, 0);
                    break;

                case 14:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(1800, 0);
                    break;

                case 15:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(2100, 0);
                    break;

                case 16:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(2100, 0);
                    break;

                case 17:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(2400, 0);
                    break;

                case 18:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(2400, 0);
                    break;

                case 19:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(2700, 0);
                    break;

                case 20:
                    rTransform.position = craftFleet.craftPositions[1].transform.position + new Vector3(2700, 0);
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
        else if (viewedFleet == null)
        {
            switch (craftPosition)
            {

                case 1:
                    rTransform.position = craftFleet.craftPositions[0].transform.position;
                    break;

                case 2:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3((175), 0);
                    break;

                case 3:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3((175 * 2), 0);
                    break;

                case 4:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3((175 * 3), 0);
                    break;

                case 5:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3((175 * 4), 0);
                    break;

                case 6:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(0, -200);
                    break;

                case 7:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3((175), -200);
                    break;

                case 8:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3((175 * 2), -200);
                    break;

                case 9:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3((175 * 3), -200);
                    break;

                case 10:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3((175 * 4), -200);
                    break;

                case 11:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(0, -400);
                    break;

                case 12:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3((175), -400);
                    break;

                case 13:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3((175 * 2), -400);
                    break;

                case 14:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3((175 * 3), -400);
                    break;

                case 15:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3((175 * 4), -400);
                    break;

                case 16:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3(0, -600);
                    break;

                case 17:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3((175), -600);
                    break;

                case 18:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3((175 * 2), -600);
                    break;

                case 19:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3((175 * 3), -600);
                    break;

                case 20:
                    rTransform.position = craftFleet.craftPositions[0].transform.position + new Vector3((175 * 4), -600);
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

}
