using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Colour_Picker : MonoBehaviour
{
    public GameObject picker;
    public Image pickerImage;
    public RectTransform pickerRect;
    public Image preview;

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

    public void handleColorChartDrag(BaseEventData eventData)
    {
        PointerEventData pEventData = eventData as PointerEventData;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(pickerRect, pEventData.position, null, out localPoint);
        preview.color = pickerImage.sprite.texture.GetPixel((int)localPoint.x, (int)localPoint.y);
    }
}
