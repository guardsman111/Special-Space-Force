using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Voidcraft_Manager : MonoBehaviour
{
    public FileFinder finder;
    public Core_Voidcraft coreVoidcraft;
    public List<Voidcraft_Pack> voidcraftPacks;

    public Dropdown CraftDropdownCustom;
    public Dropdown CraftDropdownManagement;

    public List<Color32> playerFleetColours;

    public void Begin()
    {
        voidcraftPacks = coreVoidcraft.Core();

        SetupDropdown(CraftDropdownCustom);

    }


    public void SetupDropdown(Dropdown dropdown)
    {
        if (dropdown.name == "Voidcraft Dropdown")
        {
            List<string> names = new List<string>();
            names.Add("None");
            foreach (Voidcraft_Pack p in voidcraftPacks)
            {
                if (!names.Contains(p.className))
                {
                    names.Add(p.className);
                }
            }
            dropdown.ClearOptions();
            dropdown.AddOptions(names);
            dropdown.value = 1;
        }
    }

    //This is used for the customisation screen chaning the craft on screen
    public void ChangeCraft(Dropdown dropdown, Voidcraft_Script craft)
    {
        if (dropdown.name == "Voidcraft Dropdown")
        {
            if (dropdown.options[dropdown.value].text != "None")
            {
                Voidcraft_Pack pack = new Voidcraft_Pack();
                craft.craftImages[0].gameObject.SetActive(true);
                craft.craftImages[1].gameObject.SetActive(true);
                craft.craftImages[2].gameObject.SetActive(true);
                craft.craftImages[3].gameObject.SetActive(true);
                craft.craftImages[4].gameObject.SetActive(true);
                craft.craftImages[5].gameObject.SetActive(true);

                foreach (Voidcraft_Pack p in voidcraftPacks)
                {
                    if (p.className == dropdown.options[dropdown.value].text)
                    {
                        pack = p;
                        break;
                    }
                }


                craft.craftImages[0].sprite = pack.containedSprites[0];
                craft.craftImages[1].sprite = pack.containedSprites[1];
                craft.craftImages[2].sprite = pack.containedSprites[2];
                craft.craftImages[3].sprite = pack.containedSprites[3];
                craft.craftImages[4].sprite = pack.containedSprites[4];
                craft.craftImages[5].sprite = pack.containedSprites[5];
            }
            else
            {
                craft.craftImages[0].gameObject.SetActive(false);
                craft.craftImages[1].gameObject.SetActive(false);
                craft.craftImages[2].gameObject.SetActive(false);
                craft.craftImages[3].gameObject.SetActive(false);
                craft.craftImages[4].gameObject.SetActive(false);
                craft.craftImages[5].gameObject.SetActive(false);
            }

        }

    }

    //Gets players default colours
    //Remember that trim and equipment are the wrong way round in code compared to on screen
    public List<Color32> GetColours(Image[] images)
    {
        List<Color32> colours = new List<Color32>();

        foreach (Image i in images)
        {
            colours.Add(i.color);
        }

        playerFleetColours = colours;
        return colours;
    }

    //Sets dropdowns on Craft selection - Waiting for UI to complete
    //public void SetDropdowns(Trooper_Script trooper)
    //{
    //    if (ArmourDropdownTrooper.options[ArmourDropdownTrooper.value].text != trooper.armour)
    //    {
    //        if (trooper.armour == "None")
    //        {
    //            ArmourDropdownTrooper.value = 0;
    //        }
    //        else
    //        {
    //            for (int i = 0; i < ArmourDropdownTrooper.options.Count; i++)
    //            {
    //                if (ArmourDropdownTrooper.options[i].text == trooper.armour)
    //                {
    //                    ArmourDropdownTrooper.value = i;
    //                }
    //            }
    //        }
    //    }

    //}

    //Loads the craft from a save file
    public void LoadCraft(Voidcraft_Script craft, string name)
    {
        if (name != "None")
        {
            Voidcraft_Pack pack = new Voidcraft_Pack();
            craft.craftImages[0].gameObject.SetActive(true);
            craft.craftImages[1].gameObject.SetActive(true);
            craft.craftImages[2].gameObject.SetActive(true);
            craft.craftImages[3].gameObject.SetActive(true);
            craft.craftImages[4].gameObject.SetActive(true);
            craft.craftImages[5].gameObject.SetActive(true);

            foreach (Voidcraft_Pack p in voidcraftPacks)
            {
                if (p.className == name)
                {
                    pack = p;
                    break;
                }
            }


            craft.craftImages[0].sprite = pack.containedSprites[0];
            craft.craftImages[1].sprite = pack.containedSprites[1];
            craft.craftImages[2].sprite = pack.containedSprites[2];
            craft.craftImages[3].sprite = pack.containedSprites[3];
            craft.craftImages[4].sprite = pack.containedSprites[4];
            craft.craftImages[5].sprite = pack.containedSprites[5];
        }
        else
        {
            craft.craftImages[0].gameObject.SetActive(false);
            craft.craftImages[1].gameObject.SetActive(false);
            craft.craftImages[2].gameObject.SetActive(false);
            craft.craftImages[3].gameObject.SetActive(false);
            craft.craftImages[4].gameObject.SetActive(false);
            craft.craftImages[5].gameObject.SetActive(false);
        }

    }
}
