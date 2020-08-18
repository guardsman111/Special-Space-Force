﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Colour_Picker : MonoBehaviour
{
    /// <summary>
    /// This script handles picking colours for the AI and will handle colour picking for player uniforms later
    /// </summary>
    
    public GameObject picker;
    public Image pickerImage;
    public RectTransform pickerRect;
    public Image preview;

    //Makes the colour picker visible or invisible depending
    public void TogglePicker()
    {
        if(picker.activeSelf)
        {
            picker.SetActive(false);
        }
        else
        {
            picker.SetActive(true);
            Colour_Picker[] Pickers = FindObjectsOfType<Colour_Picker>();
            foreach(Colour_Picker cp in Pickers)
            {
                if(cp == this)
                {

                } else
                {
                    cp.picker.gameObject.SetActive(false);
                }
            }
        }
    }

    //Handles click and drag on the colour chart
    public void handleColorChartDrag(BaseEventData eventData)
    {
        PointerEventData pEventData = eventData as PointerEventData;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(pickerRect, pEventData.position, null, out localPoint);
        preview.color = pickerImage.sprite.texture.GetPixel((int)localPoint.x, (int)localPoint.y);
    }
}
