using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Squad_Colour_Customisation : MonoBehaviour
{
    public Trooper_Script trooper;
    public Trooper_Script screenTrooper;
    public Slot_Script currentSlot;
    public Slot_Manager manager;

    private int identifierLoc = 0;
    public int nSlotLocations;

    public Image[] images;

    public List<Color32> clipboardColors;

    public void SetupScreen()
    {
        currentSlot = manager.viewedSlot;
        if (manager.viewedSlot.containedTroopers.Count > 0)
        {
            trooper = manager.viewedSlot.containedTroopers[0];

            //Remember that trim and equipment are the wrong way round in code compared to on screen
            images[0].color = trooper.trooperImages[17].color;
            images[1].color = trooper.trooperImages[18].color;
            images[2].color = trooper.trooperImages[19].color;
            images[3].color = trooper.trooperImages[20].color;
            images[4].color = trooper.trooperImages[10].color;
            images[5].color = trooper.trooperImages[11].color;
            images[6].color = trooper.trooperImages[12].color;
            images[7].color = trooper.trooperImages[13].color;
            images[8].color = trooper.trooperImages[14].color;
            images[9].color = trooper.trooperImages[4].color;
            images[10].color = trooper.trooperImages[5].color;
            images[11].color = trooper.trooperImages[6].color;
            images[12].color = trooper.trooperImages[7].color;
            images[13].color = trooper.trooperImages[8].color;
            images[14].color = trooper.trooperImages[27].color;
            images[15].color = trooper.trooperImages[28].color;
            images[16].color = trooper.trooperImages[24].color;
            images[17].color = trooper.trooperImages[25].color;
            images[18].color = trooper.slotLocations[0].color;

            screenTrooper.LoadTrooper(trooper.trooperClass,trooper.manager, trooper.trooperSquad);
            screenTrooper.FindSlotIdentifier();


            screenTrooper.trooperImages[17].color = trooper.trooperImages[17].color;
            screenTrooper.trooperImages[18].color = trooper.trooperImages[18].color;
            screenTrooper.trooperImages[19].color = trooper.trooperImages[19].color;
            screenTrooper.trooperImages[20].color = trooper.trooperImages[20].color;
            screenTrooper.trooperImages[10].color = trooper.trooperImages[10].color;
            screenTrooper.trooperImages[11].color = trooper.trooperImages[11].color;
            screenTrooper.trooperImages[12].color = trooper.trooperImages[12].color;
            screenTrooper.trooperImages[13].color = trooper.trooperImages[13].color;
            screenTrooper.trooperImages[14].color = trooper.trooperImages[14].color;
            screenTrooper.trooperImages[4].color = trooper.trooperImages[4].color;
            screenTrooper.trooperImages[5].color = trooper.trooperImages[5].color;
            screenTrooper.trooperImages[6].color = trooper.trooperImages[6].color;
            screenTrooper.trooperImages[7].color = trooper.trooperImages[7].color;
            screenTrooper.trooperImages[8].color = trooper.trooperImages[8].color;
            screenTrooper.trooperImages[27].color = trooper.trooperImages[27].color;
            screenTrooper.trooperImages[28].color = trooper.trooperImages[28].color;
            screenTrooper.trooperImages[24].color = trooper.trooperImages[24].color;
            screenTrooper.trooperImages[25].color = trooper.trooperImages[25].color;
            screenTrooper.trooperImages[30].color = trooper.trooperImages[27].color;
            screenTrooper.trooperImages[31].color = trooper.trooperImages[28].color;
            for (int i = 0; i < screenTrooper.slotLocations.Length; i++)
            {
                screenTrooper.slotLocations[i].color = trooper.slotLocations[i].color;
            }
        }
    }
    
    public void SaveToSlot()
    {
        currentSlot.slotClass.squadColours = new List<Color32>();
        foreach (Image i in images)
        {
            currentSlot.slotClass.squadColours.Add(i.color);
        }

        foreach(Trooper_Script ts in currentSlot.containedTroopers)
        {
            ts.TrooperColours();
        }
    }

    public void SetIdentifierLocation(int modifier)
    {

        if (identifierLoc < nSlotLocations)
        {
            if (modifier == +1 && identifierLoc == nSlotLocations - 1)
            {
                identifierLoc = 0;
            }
            else if (modifier == -1 && identifierLoc == 0)
            {
                identifierLoc = nSlotLocations - 1;
            }
            else
            {
                identifierLoc += modifier;
            }
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
        for(int i = 0; i < clipboardColors.Count; i++ )
        {
            images[i].color = clipboardColors[i];
        }

        screenTrooper.trooperImages[17].color = clipboardColors[0];
        screenTrooper.trooperImages[18].color = clipboardColors[1];
        screenTrooper.trooperImages[19].color = clipboardColors[2];
        screenTrooper.trooperImages[20].color = clipboardColors[3];
        screenTrooper.trooperImages[10].color = clipboardColors[4];
        screenTrooper.trooperImages[11].color = clipboardColors[5];
        screenTrooper.trooperImages[12].color = clipboardColors[6];
        screenTrooper.trooperImages[13].color = clipboardColors[7];
        screenTrooper.trooperImages[14].color = clipboardColors[8];
        screenTrooper.trooperImages[4].color = clipboardColors[9];
        screenTrooper.trooperImages[5].color = clipboardColors[10];
        screenTrooper.trooperImages[6].color = clipboardColors[11];
        screenTrooper.trooperImages[7].color = clipboardColors[12];
        screenTrooper.trooperImages[8].color = clipboardColors[13];
        screenTrooper.trooperImages[27].color = clipboardColors[14];
        screenTrooper.trooperImages[28].color = clipboardColors[15];
        screenTrooper.trooperImages[24].color = clipboardColors[16];
        screenTrooper.trooperImages[25].color = clipboardColors[17];
        screenTrooper.trooperImages[30].color = clipboardColors[14];
        screenTrooper.trooperImages[31].color = clipboardColors[15];
        for (int i = 0; i < screenTrooper.slotLocations.Length; i++)
        {
            screenTrooper.slotLocations[i].color = clipboardColors[18]; ;
        }
    }
}
