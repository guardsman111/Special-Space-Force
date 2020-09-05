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
        switch (trooperPosition)
        {

            case 1:
                rTransform.position = parent.squadPositions[0].transform.position;
                break;

            case 2:
                rTransform.position = parent.squadPositions[1].transform.position;
                break;

            case 3:
                rTransform.position = parent.squadPositions[2].transform.position;
                break;

            case 4:
                rTransform.position = parent.squadPositions[3].transform.position;
                break;

            case 5:
                rTransform.position = parent.squadPositions[4].transform.position;
                break;

            case 6:
                rTransform.position = parent.squadPositions[5].transform.position;
                break;

            case 7:
                rTransform.position = parent.squadPositions[6].transform.position;
                break;

            case 8:
                rTransform.position = parent.squadPositions[7].transform.position;
                break;

            case 9:
                rTransform.position = parent.squadPositions[8].transform.position;
                break;

        }
        gameObject.transform.localScale = new Vector3(1, 1);
        input.transform.localScale = new Vector3(1, 1);
        input.textComponent.transform.localScale = new Vector3(1, 1);
        input.textComponent.enableWordWrapping = true;

        gameObject.transform.localScale = new Vector3(1, 1);


        //If its parent is the viewed slot it is set visible here
        if (trooperSquad == viewedSlot)
        {
            input.gameObject.SetActive(true);
            image.SetActive(true);
        } 
        else if (viewedSlot.containedSlots.Contains(trooperSquad))
        {
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
