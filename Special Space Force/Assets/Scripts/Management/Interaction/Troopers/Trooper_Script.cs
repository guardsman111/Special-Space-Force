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
    public Trooper_Class trooperClass;
    public Slot_Script trooperSquad;

    public Slot_Manager manager;
    public GameObject image;
    public TMP_InputField input;

    public void MakeTrooper(Trooper_Class trooper, int positionID, Slot_Manager nManager)
    {
        trooperClass = trooper;
        trooperName = trooper.trooperName;
        trooperRank = trooper.trooperRank;
        trooperPosition = positionID;

        input.text = trooperName;
        manager = nManager;
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

}
