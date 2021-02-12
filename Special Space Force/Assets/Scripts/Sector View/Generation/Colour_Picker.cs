using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Colour_Picker : MonoBehaviour
{
    /// <summary>
    /// This script handles picking colours for the AI and will handle colour picking for player uniforms later
    /// </summary>
    public Generation_Settings_Director Director;

    public GameObject picker;
    public Image pickerImageColour;
    public Image pickerImageGrey;
    public RectTransform pickerRect;
    public Image boxBackup;
    public Image[] previewI;
    public Text[] previewT;

    private void Awake()
    {
        if (boxBackup != null)
        {
            if (previewI.Length > 0)
            {
                foreach (Image i in previewI)
                {
                    i.color = boxBackup.color;
                }
            }
            if (previewT.Length > 0)
            {
                foreach (Text t in previewT)
                {
                    t.color = boxBackup.color;
                }
            }
        }
    }

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
        if (previewI.Length > 0)
        {
            foreach (Image i in previewI)
            {
                i.color = pickerImageColour.sprite.texture.GetPixel((int)localPoint.x, (int)localPoint.y);
            }
        }
        if (previewT.Length > 0)
        {
            foreach (Text t in previewT)
            {
                t.color = pickerImageColour.sprite.texture.GetPixel((int)localPoint.x, (int)localPoint.y);
            }
        }
        boxBackup.color = pickerImageColour.sprite.texture.GetPixel((int)localPoint.x, (int)localPoint.y);
    }

    //Handles click and drag on the colour chart
    public void handleGreyChartDrag(BaseEventData eventData)
    {
        PointerEventData pEventData = eventData as PointerEventData;
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(pickerRect, pEventData.position, null, out localPoint);
        if (previewI.Length > 0)
        {
            foreach (Image i in previewI)
            {
                i.color = pickerImageGrey.sprite.texture.GetPixel((int)localPoint.x, (int)localPoint.y); ;
            }
        }
        if (previewT.Length > 0)
        {
            foreach (Text t in previewT)
            {
                t.color = pickerImageGrey.sprite.texture.GetPixel((int)localPoint.x, (int)localPoint.y); ;
            }
        }
        boxBackup.color = pickerImageGrey.sprite.texture.GetPixel((int)localPoint.x, (int)localPoint.y);
    }

    public void CopyColour()
    {
        Director.CopyColour(this);
    }

    public void PasteColour()
    {
        Director.PasteColour(this);
    }

    public void OnMouseUp()
    {
        if (picker.activeSelf)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(pickerRect, Input.mousePosition, null, out localPoint);
            if (!pickerRect.rect.Contains(localPoint))
            {
                TogglePicker();
            }
        }
    }

    public void SetColour(Color32 nColour)
    {
        if (previewI.Length > 0)
        {
            foreach (Image i in previewI)
            {
                i.color = nColour;
            }
        }
        if (previewT.Length > 0)
        {
            foreach (Text t in previewT)
            {
                t.color = nColour;
            }
        }
        boxBackup.color = nColour;
    }
}
