﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Slot_Generator : MonoBehaviour
{
    public FileFinder finder;
    public Slot_Manager manager;
    public List<Slot_Class> slots;
    public string fileLocation;
    public GameObject genericSlot;
    public GameObject genericTrooper;
    public GameObject slotN1;
    public List<Slot_Script> squads;

    //public void Start()
    //{
    //    SaveDefaults();
    //}

    //Creates new slots from template
    public List<Slot_Class> FindDefaultSlots()
    {
        slots = new List<Slot_Class>();
        List<string> fileLocations = finder.Retrieve("DefaultSlotLayout.xml", ".meta");
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
        CreateTopSlots();
    }

    //Creates the slots of height 0, which in turn set their children
    public void CreateTopSlots()
    {
        manager.slots = slots;
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
                temp.GetComponent<Slot_Script>().MakeSlot(sc, count, manager);
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
        foreach (Slot_Class sc in slot.containedSlots)
        {
            if (sc.slotHeight == slot.slotHeight + 1)
            {
                count += 1;
                GameObject temp = Instantiate(genericSlot, slotScript.transform);
                Slot_Script tempS = temp.GetComponent<Slot_Script>();
                tempS.MakeSlot(sc, count, manager);
                tempS.ID = count;
                tempS.slotParent = slotScript;
                if (tempS.squad)
                {
                    sc.containedTroopers = new List<Trooper_Class>();
                    for(int i = 0; i < 10; i++)
                    {
                        Trooper_Class tempTC = new Trooper_Class();
                        tempTC.trooperName = "Name";
                        tempTC.trooperRank = "Private";
                        tempTC.trooperPosition = i + 1;
                        sc.containedTroopers.Add(tempTC);
                    }
                    tempS.containedTroopers = FillSlots(sc, tempS, 0);
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

    public List<Trooper_Script> FillSlots(Slot_Class squad, Slot_Script slotParent, int squadV)
    {
        List<Trooper_Script> tempTroopers = new List<Trooper_Script>();

        int count = 0;
        foreach (Trooper_Class tc in squad.containedTroopers)
        {
            count += 1;
            ////

            //Insert generation for troopers here?

            ////
            GameObject temp = Instantiate(genericTrooper, slotParent.transform);
            Trooper_Script tempS = temp.GetComponent<Trooper_Script>();
            tempS.MakeTrooper(tc, count, manager);
            tempS.trooperSquad = slotParent;
            tempTroopers.Add(tempS);
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
}
