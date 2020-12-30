using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Slot_Generator : MonoBehaviour
{
    /// <summary>
    /// Generates the slots for the force organisation UI
    /// </summary>
    /// 
    public FileFinder finder;
    public Slot_Manager manager;
    public List<Slot_Class> slots;
    public string fileLocation;
    public GameObject genericSlot;
    public GameObject genericTrooper;
    public GameObject slotN1;
    public List<Slot_Script> squads;
    public int troopersPerSquad;
    public bool createTroopersFromTemplate;
    private bool loading = false;

    public Text nTroopersText;
    public Dropdown templateDropdown;
    public Image templatePreview;

    //public void Start()
    //{
    //    SaveDefaults();
    //}

    //Sets the dropdown for Force organisation templates
    public void SetupTemplateDropdown()
    {
        templateDropdown.ClearOptions();

        List<string> fileLocations = finder.Retrieve("SlotLayout", ".meta");
        List<string> names = new List<string>();

        foreach (string s in fileLocations)
        {
            string name = Path.GetFileName(s);
            names.Add(name);
        }
        templateDropdown.AddOptions(names);
        ChangedTemplateDropdown();
    }

    //Loads new image for the new template when selected from the dropdown
    public void ChangedTemplateDropdown()
    {
        List<string> fileLocations = finder.Retrieve(templateDropdown.options[templateDropdown.value].text, ".meta");
        Slot_Class temp = Serializer.Deserialize<Slot_Class>(fileLocations[0]);
        if (temp.TemplateImageLocation != null)
        {
            Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
            byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp.TemplateImageLocation);
            newTex.LoadImage(bytes);
            newTex.filterMode = FilterMode.Point;
            Sprite newSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f));
            templatePreview.sprite = newSprite;
        } 
        else
        {
            templatePreview.sprite = null;
        }
    }

    //Creates new slots from template
    public List<Slot_Class> FindDefaultSlots()
    {
        slots = new List<Slot_Class>();
        List<string> fileLocations = finder.Retrieve(templateDropdown.options[templateDropdown.value].text, ".meta");
        fileLocation = fileLocations[0];

        if (fileLocation == null)
        {

        }

        try
        {
            Slot_Class temp = Serializer.Deserialize<Slot_Class>(fileLocation);
            //For each Slot_Class add it to the permanent slots List
            foreach (Slot_Class tempS in temp.containedSlots)
            {
                slots.Add(tempS);
            }
        }
        catch (UnauthorizedAccessException UAEx)
        {
            Console.WriteLine(UAEx.Message);
        }
        catch (PathTooLongException PathEx)
        {
            Console.WriteLine(PathEx.Message);
        }
        catch (FileNotFoundException FileNull)
        {
            Console.WriteLine(FileNull.Message);
        }

        CreateTopSlots();
        return slots;
    }

    //Loads the slots from a list
    public void LoadSlots(List<Slot_Class> slotClasses)
    {
        slots = slotClasses;
        loading = true;
        CreateTopSlots();
    }

    //Creates the slots of height 0, which in turn set their children
    public void CreateTopSlots()
    {
        manager.Slots = slots;
        manager.generator = this;
        manager.slotN1 = slotN1;
        manager.viewedSlot = slotN1.GetComponent<Slot_Script>();

        int count = 0;

        foreach (Slot_Class sc in slots)
        {
            if(sc.slotHeight == 0)
            {
                count += 1;
                GameObject temp = Instantiate(genericSlot, slotN1.transform);
                temp.GetComponent<Slot_Script>().squad = false;
                temp.GetComponent<Slot_Script>().ID = count;
                temp.GetComponent<Slot_Script>().slotParent = slotN1.GetComponent<Slot_Script>();

                //if loading, load the slots intead
                if (loading)
                {
                    temp.GetComponent<Slot_Script>().LoadSlot(sc, count, manager);
                }
                else
                {
                    temp.GetComponent<Slot_Script>().MakeSlot(sc, count, manager);
                }

                temp.GetComponent<Slot_Script>().containedSlots = FillSlots(sc, temp.GetComponent<Slot_Script>());
                temp.GetComponent<Slot_Script>().SetPosition(slotN1.GetComponent<Slot_Script>(), slotN1.GetComponent<Slot_Script>());
                slotN1.GetComponent<Slot_Script>().containedSlots.Add(temp.GetComponent<Slot_Script>());
            }
        }
        manager.gameObject.SetActive(false);
    }

    //Creates the slots for the children of other slots
    public List<Slot_Script> FillSlots(Slot_Class slot, Slot_Script slotScript)
    {
        List<Slot_Script> tempSlots = new List<Slot_Script>();

        int count = 0;

        //Create a Slot_Script for each slot_class the current slot_class contains
        foreach (Slot_Class sc in slot.containedSlots)
        {
            if (sc.slotHeight == slot.slotHeight + 1)
            {
                count += 1;
                GameObject temp = Instantiate(genericSlot, slotScript.transform);
                Slot_Script tempS = temp.GetComponent<Slot_Script>();
                if (loading)
                {
                    tempS.LoadSlot(sc, count, manager);
                } 
                else
                {
                    tempS.MakeSlot(sc, count, manager);
                }
                tempS.ID = count;
                tempS.slotParent = slotScript;

                //if the slot is a squad, create troopers for it

                if (tempS.squad)
                {
                    if (createTroopersFromTemplate && sc.containedTroopers.Count == 0)
                    {
                        if(sc.numberOfTroopers == 0)
                        {
                            sc.numberOfTroopers = troopersPerSquad;
                        }
                        sc.containedTroopers = new List<Trooper_Class>();
                        tempS.containedTroopers = new List<Trooper_Script>();
                        for (int i = 0; i < sc.numberOfTroopers; i++)
                        {
                            Trooper_Class tempTC = new Trooper_Class();
                            tempTC.trooperName = "Name";
                            tempTC.trooperRank = "Private";
                            tempTC.trooperPosition = i + 1;
                            sc.containedTroopers.Add(tempTC);
                        }
                        tempS.containedTroopers = FillSlots(sc, tempS, 0);
                        Debug.Log(sc.numberOfTroopers);
                    }
                    else if (createTroopersFromTemplate)
                    {
                        tempS.containedTroopers = new List<Trooper_Script>();
                        tempS.containedTroopers = FillSlots(sc, tempS, 0);
                        Debug.Log(sc.numberOfTroopers);
                    }
                    else
                    {
                        sc.containedTroopers = new List<Trooper_Class>();
                        tempS.containedTroopers = new List<Trooper_Script>();
                        for (int i = 0; i < troopersPerSquad; i++)
                        {
                            Trooper_Class tempTC = new Trooper_Class();
                            tempTC.trooperName = "Name";
                            tempTC.trooperRank = "Private";
                            tempTC.trooperPosition = i + 1;
                            sc.containedTroopers.Add(tempTC);
                        }
                        tempS.containedTroopers = FillSlots(sc, tempS, 0);
                        Debug.Log(troopersPerSquad);
                    }
                }
                else
                {
                    tempS.containedSlots = FillSlots(sc, tempS);
                }
                tempSlots.Add(tempS);
            }
            else if (sc.slotHeight > slot.slotHeight + 1)
            {

            }
            else
            {
                Debug.Log("Slot of same or lower height detected in wrong place " + sc.slotName + " Slot height " + sc.slotHeight + " with parent height " + slot.slotHeight);
            }
        }

        return tempSlots;
    }

    //Fills the trooper slots of squads
    public List<Trooper_Script> FillSlots(Slot_Class squad, Slot_Script slotParent, int squadV)
    {
        List<Trooper_Script> tempTroopers = new List<Trooper_Script>();

        int count = 0;
        foreach (Trooper_Class tc in squad.containedTroopers)
        {
            if (loading)
            {
                count += 1;

                GameObject temp = Instantiate(genericTrooper, slotParent.transform);
                Trooper_Script tempS = temp.GetComponent<Trooper_Script>();
                tempS.MakeTrooper(tc, count, manager);
                tempS.trooperSquad = slotParent;
                tempS.FindSlotIdentifier();
                tempTroopers.Add(tempS);
            } 
            else
            {
                count += 1;
                //// Troop Generation Script
                tc.gender = UnityEngine.Random.Range(0, 2);
                tc.trooperName = manager.manager.localisationManager.CreateTrooperName("TrooperNames", tc.gender);

                tc.trooperFace = UnityEngine.Random.Range(0, manager.trooperSkinPack.containedSprites.Count);

                if (tc.gender == 0)
                {
                    tc.trooperHair = UnityEngine.Random.Range(0, manager.femaleHairPack.containedSprites.Count);
                }
                if (tc.gender == 1)
                {
                    tc.trooperHair = UnityEngine.Random.Range(0, manager.maleHairPack.containedSprites.Count);
                }

                tc.hairColour = UnityEngine.Random.Range(0, manager.hairColours.Length);

                //Rank still needs to be done

                ////
                GameObject temp = Instantiate(genericTrooper, slotParent.transform);
                Trooper_Script tempS = temp.GetComponent<Trooper_Script>();
                tempS.MakeTrooper(tc, count, manager);
                tempS.trooperSquad = slotParent;
                tempS.FindSlotIdentifier();
                tempTroopers.Add(tempS);
            }
        }

        return tempTroopers;
    }

    //Creates the default slot template
    public Slot_Class GenerateDefaultLayout()
    {
        Slot_Class tempS = new Slot_Class();

        tempS.containedSlots = new List<Slot_Class>();
        tempS.squad = false;
        tempS.slotHeight = -1;

        Slot_Class tempS2 = new Slot_Class();
        tempS2.slotHeight = 0;
        tempS2.squad = false;
        tempS2.slotName = "1st Company";
        tempS2.containedSlots = new List<Slot_Class>();
        Slot_Class tempS22 = new Slot_Class();
        tempS22.slotHeight = 1;
        tempS22.squad = false;
        tempS22.slotName = "1st Platoon";
        tempS2.containedSlots.Add(tempS22);
        Slot_Class tempS23 = new Slot_Class();
        tempS23.slotHeight = 1;
        tempS23.squad = false;
        tempS23.slotName = "2nd Platoon";
        tempS2.containedSlots.Add(tempS23);
        Slot_Class tempS24 = new Slot_Class();
        tempS24.slotHeight = 1;
        tempS24.squad = false;
        tempS24.slotName = "3rd Platoon";
        tempS2.containedSlots.Add(tempS24);

        tempS.containedSlots.Add(tempS2);

        Slot_Class tempS3 = new Slot_Class();
        tempS3.slotHeight = 0;
        tempS3.squad = false;
        tempS3.slotName = "2nd Company";
        tempS3.containedSlots = new List<Slot_Class>();
        Slot_Class tempS32 = new Slot_Class();
        tempS32.slotHeight = 1;
        tempS32.squad = false;
        tempS32.slotName = "1st Platoon";
        tempS3.containedSlots.Add(tempS32);
        Slot_Class tempS33 = new Slot_Class();
        tempS33.slotHeight = 1;
        tempS33.squad = false;
        tempS33.slotName = "2nd Platoon";
        tempS3.containedSlots.Add(tempS33);
        Slot_Class tempS34 = new Slot_Class();
        tempS34.slotHeight = 1;
        tempS34.squad = false;
        tempS34.slotName = "3rd Platoon";
        tempS3.containedSlots.Add(tempS34);

        tempS.containedSlots.Add(tempS3);

        return tempS;
    }

    //Saves the default Slots
    public void SaveDefaults()
    {
        var file = File.Create(finder.defaultPath + "/Resources/Core/Organisation/DefaultSlotLayout.xml");
        file.Close();
        Slot_Class fileToSave = GenerateDefaultLayout();
        Serializer.Serialize(fileToSave, finder.defaultPath + "/Resources/Core/Organisation/DefaultSlotLayout.xml");
        //Debug.Log("File written");
    }

    //Sets whether to follow the templates number of troops or the sliders
    public void ToggleTroopersFromTemplate(Toggle toggle)
    {
        createTroopersFromTemplate = toggle.isOn;
    }

    //Sets the number of troops to be generated if not following the template numbers
    public void ChangeTroopersPerSquad(Slider slider)
    {
        troopersPerSquad = (int)slider.value;
        nTroopersText.text = troopersPerSquad.ToString();
    }
}
