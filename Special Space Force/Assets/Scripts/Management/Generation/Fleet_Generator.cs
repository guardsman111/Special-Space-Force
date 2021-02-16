using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Fleet_Generator : MonoBehaviour
{
    public FileFinder finder;
    public Manager_Script modManager;
    public Fleet_Manager fManager;
    public List<Fleet_Class> fleets;
    public string fileLocation;
    public GameObject genericFleet;
    public GameObject genericShip;
    public GameObject content;

    public Dropdown templateDropdown;
    public Image templatePreview;

    public bool loading;


    //public void Start()
    //{
    //    SaveDefaults();
    //}

    //Sets the dropdown for Fleet templates
    public void SetupTemplateDropdown()
    {
        templateDropdown.ClearOptions();

        List<string> fileLocations = finder.Retrieve("FleetLayout", ".meta");
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
        List<Fleet_Class> temp = Serializer.Deserialize<List<Fleet_Class>>(fileLocations[0]);
        if (temp[0].TemplateImageLocation != null)
        {
            Texture2D newTex = new Texture2D(4, 4, TextureFormat.RGBA32, false);
            byte[] bytes = File.ReadAllBytes(UnityEngine.Application.dataPath + "/Resources/" + temp[0].TemplateImageLocation);
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

    //Loads the slots from a list
    public void LoadFleets(List<Fleet_Class> fleetClasses)
    {
        fleets = fleetClasses;
        loading = true;
        CreateTopFleets();
    }

    //Creates new slots from template
    public List<Fleet_Class> FindDefaultFleets()
    {
        fleets = new List<Fleet_Class>();
        List<string> fileLocations = finder.Retrieve(templateDropdown.options[templateDropdown.value].text, ".meta");
        fileLocation = fileLocations[0];

        if (fileLocation == null)
        {

        }

        try
        {
            List<string> names = new List<string>();
            foreach (Voidcraft_Build_Class vc in modManager.voidcraftManager.voidcraftClasses)
            {
                names.Add(vc.className);
            }
            List<Fleet_Class> fleetsTemp = Serializer.Deserialize<List<Fleet_Class>>(fileLocation);
            //For each Fleet_Class add it to the permanent fleets List
            foreach (Fleet_Class fc in fleetsTemp)
            {
                Fleet_Class tempC = new Fleet_Class();
                tempC.containedCraft = new List<Voidcraft_Class>();
                tempC.fleetName = fc.fleetName;
                for (int i = 0; i < fc.containedCraft.Count; i++)
                {
                    if (names.Contains(fc.containedCraft[i].className)) 
                    {
                        Voidcraft_Build_Class vp = modManager.voidcraftManager.voidcraftClasses[names.IndexOf(fc.containedCraft[i].className)];
                        fc.containedCraft[i].className = vp.className;
                        fc.containedCraft[i].weapons = vp.weapons;
                        fc.containedCraft[i].speed = vp.speed;
                        fc.containedCraft[i].costPerCraft = vp.costPerCraft;
                        fc.containedCraft[i].armour = vp.armour;
                        fc.containedCraft[i].capacity = vp.capacity;
                    }
                    tempC.containedCraft.Add(fc.containedCraft[i]);

                }
                fleets.Add(tempC);
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

        CreateTopFleets();
        return fleets;
    }

    public List<Fleet_Class> CreateTopFleets()
    {
        fManager.Fleets = fleets;
        fManager.generator = this;
        fManager.viewedFleet = null;

        foreach (Fleet_Class fc in fleets)
        {
            GameObject temp = Instantiate(genericFleet, content.transform);

            //if loading, load the slots intead
            if (loading)
            {
                temp.GetComponent<Fleet_Script>().LoadFleet(fc, fManager);
            }
            else
            {
                temp.GetComponent<Fleet_Script>().MakeFleet(fc, fManager);
            }

            temp.GetComponent<Fleet_Script>().SetPosition(fManager.viewedFleet);
            fManager.FleetsS.Add(temp.GetComponent<Fleet_Script>());

        }

        if (loading)
        {
            for (int i = 0; i < fleets.Count; i++)
            {
                int count = 0;
                foreach (Voidcraft_Class vc in fleets[i].containedCraft)
                {
                    GameObject temp = fManager.FleetsS[i].gameObject;
                    count += 1;
                    GameObject tempV = Instantiate(genericShip, temp.transform);
                    tempV.GetComponent<Voidcraft_Script>().LoadCraft(vc, modManager, fManager);
                    tempV.GetComponent<Voidcraft_Script>().SetPosition(null);
                    temp.GetComponent<Fleet_Script>().containedCraft.Add(tempV.GetComponent<Voidcraft_Script>());
                    fManager.Craft.Add(tempV.GetComponent<Voidcraft_Script>().craftClass);
                }
            }
        }
        else
        {
            for (int i = 0; i < fleets.Count; i++)
            {
                int count = 0;
                foreach (Voidcraft_Class vc in fleets[i].containedCraft)
                {
                    GameObject temp = fManager.FleetsS[i].gameObject;
                    count += 1;
                    GameObject tempV = Instantiate(genericShip, temp.transform);
                    tempV.GetComponent<Voidcraft_Script>().MakeCraft(vc, modManager, fManager, count, temp.GetComponent<Fleet_Script>());
                    tempV.GetComponent<Voidcraft_Script>().SetPosition(null);
                    temp.GetComponent<Fleet_Script>().containedCraft.Add(tempV.GetComponent<Voidcraft_Script>());
                    fManager.Craft.Add(tempV.GetComponent<Voidcraft_Script>().craftClass);
                }
            }
        }

        fManager.gameObject.SetActive(false);

        return fleets;
    }

    public void SaveDefaults()
    {
        var file = File.Create(finder.defaultPath + "/Organisation/DefaultFleetLayout.xml");
        file.Close();
        List<Fleet_Class> fileToSave = GenerateDefaultLayout();
        Serializer.Serialize(fileToSave, finder.defaultPath + "/Organisation/DefaultFleetLayout.xml");
    }

    //Creates the default fleet template
    public List<Fleet_Class> GenerateDefaultLayout()
    {
        List<Fleet_Class> returner = new List<Fleet_Class>();



        Fleet_Class tempF = new Fleet_Class();
        tempF.fleetName = "1st Fleet";
        tempF.containedCraft = new List<Voidcraft_Class>();
        Voidcraft_Class tempF1 = new Voidcraft_Class();
        tempF1.className = "Farsky Heavy Destroyer";
        tempF.containedCraft.Add(tempF1);
        Voidcraft_Class tempF2 = new Voidcraft_Class();
        tempF2.className = "Farsky Heavy Destroyer";
        tempF.containedCraft.Add(tempF2);
        Voidcraft_Class tempF3 = new Voidcraft_Class();
        tempF3.className = "Farsky Heavy Destroyer";
        tempF.containedCraft.Add(tempF3);
        Voidcraft_Class tempF4 = new Voidcraft_Class();
        tempF4.className = "Hifrin Cruiser";
        tempF.containedCraft.Add(tempF4);
        Voidcraft_Class tempF5 = new Voidcraft_Class();
        tempF5.className = "Hifrin Cruiser";
        tempF.containedCraft.Add(tempF5);
        Voidcraft_Class tempF6 = new Voidcraft_Class();
        tempF6.className = "Hifrin Cruiser";
        tempF.containedCraft.Add(tempF6);

        returner.Add(tempF);

        return returner;
    }
}
