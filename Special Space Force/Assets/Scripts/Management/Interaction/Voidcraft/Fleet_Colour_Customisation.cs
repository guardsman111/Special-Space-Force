using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fleet_Colour_Customisation : MonoBehaviour
{
    public Voidcraft_Script craft;
    public Voidcraft_Script screenCraft;
    public Fleet_Script currentFleet;
    public Fleet_Manager manager;

    public Image[] images;

    public List<Color32> clipboardColors;

    public void SetupScreen()
    {
        currentFleet = manager.viewedFleet;
        if (manager.viewedFleet.containedCraft.Count > 0)
        {
            craft = manager.viewedFleet.containedCraft[0];

            //Remember that trim and equipment are the wrong way round in code compared to on screen
            images[0].color = craft.craftImages[1].color;
            images[1].color = craft.craftImages[2].color;
            images[2].color = craft.craftImages[3].color;
            images[3].color = craft.craftImages[4].color;
            images[4].color = craft.craftImages[5].color;

            screenCraft.MakeCraft(craft.craftClass, manager, craft.craftClass.positionID, manager.viewedFleet);


            screenCraft.craftImages[1].color = craft.craftImages[1].color;
            screenCraft.craftImages[2].color = craft.craftImages[2].color;
            screenCraft.craftImages[3].color = craft.craftImages[3].color;
            screenCraft.craftImages[4].color = craft.craftImages[4].color;
            screenCraft.craftImages[5].color = craft.craftImages[5].color;
        }
    }

    public void SaveToSlot()
    {
        currentFleet.fleetClass.fleetColours = new List<Color32>();
        foreach (Image i in images)
        {
            currentFleet.fleetClass.fleetColours.Add(i.color);
        }
    }

    public void CopyColours()
    {
        clipboardColors = new List<Color32>();
        foreach (Image i in images)
        {
            clipboardColors.Add(i.color);
        }
    }

    public void PasteColours()
    {
        for (int i = 0; i < clipboardColors.Count; i++)
        {
            images[i].color = clipboardColors[i];
        }


        screenCraft.craftImages[1].color = craft.craftImages[0].color;
        screenCraft.craftImages[2].color = craft.craftImages[1].color;
        screenCraft.craftImages[3].color = craft.craftImages[2].color;
        screenCraft.craftImages[4].color = craft.craftImages[3].color;
        screenCraft.craftImages[5].color = craft.craftImages[4].color;
    }
}
