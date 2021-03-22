using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class Race_Manager : MonoBehaviour
{
    /// <summary>
    /// The Race Manager retrieves all of the Race_Classes and holds them to be easily read
    /// </summary>
    /// 
    public FileFinder finder;
    private List<string> raceFiles;
    private List<Race_Class> races;
    private List<Race_Units_Class> raceUnits;
    private List<Race_Weapons_Class> raceWeapons;
    public Manager_Script modManager;

    //Retrieves (and saves the core) Race Files. Ignores .meta files
    public bool Run()
    {
        bool done = false;
        //CoreRaces();
        //SaveDefaults();
        raceFiles = finder.Retrieve("Race.xml", ".meta");
        races = FindRaceFiles();
        foreach(Race_Class rc in races)
        {
            modManager.threatManager.AddThreats(FindThreats(rc));
        }
        return done;
    }

    public List<Race_Class> Races
    {
        get { return races; }

        set
        {
            if
              (value != races)
            {
                races = value;
            }
        }
    }
    public List<Race_Units_Class> RaceUnits
    {
        get { return raceUnits; }

        set
        {
            if
              (value != raceUnits)
            {
                raceUnits = value;
            }
        }
    }
    public List<Race_Weapons_Class> RaceWeapons
    {
        get { return raceWeapons; }

        set
        {
            if
              (value != raceWeapons)
            {
                raceWeapons = value;
            }
        }
    }

    //Send the Files over to the serializer to be converted into Race_Classes, for use in game
    private List<Race_Class> FindRaceFiles()
    {
        List<Race_Class> raceList = new List<Race_Class>();

        raceUnits = new List<Race_Units_Class>();
        raceWeapons = new List<Race_Weapons_Class>();

        try
        {
            //For each race, check for duplicates and if there are duplicates, replace any core ones and remove non-core ones
            foreach (string s in raceFiles)
            {
                Race_Class temp = Serializer.Deserialize<Race_Class>(s);
                int counter = 0;
                bool duplicate = false;
                try
                {
                    Race_Units_Class tempUC = Serializer.Deserialize<Race_Units_Class>(Application.dataPath + "/Resources/Core" + temp.raceUnitsPath);
                    raceUnits.Add(tempUC);
                    try
                    {
                        raceWeapons.Add(Serializer.Deserialize<Race_Weapons_Class>(Application.dataPath + "/Resources/Core" + tempUC.weaponsPath));
                    }
                    catch
                    {
                        Debug.Log("Error loading Weapons List - " + tempUC.weaponsPath);
                    }
                }
                catch
                {
                    Debug.Log("Checking if is mod");
                    try
                    {
                        Race_Units_Class tempUC = Serializer.Deserialize<Race_Units_Class>(Application.dataPath + "/Resources/Mods" + temp.raceUnitsPath);
                        raceUnits.Add(tempUC);
                        try
                        {
                            raceWeapons.Add(Serializer.Deserialize<Race_Weapons_Class>(Application.dataPath + "/Resources/Mods" + tempUC.weaponsPath));
                        }
                        catch
                        {
                            Debug.Log("Error loading Weapons List - " + tempUC.weaponsPath);
                        }
                    }
                    catch
                    {
                        Debug.Log("Couldn't find in mods either - " + temp.raceUnitsPath);
                    }
                }
                foreach (Race_Class checkR in raceList)
                {
                    if (temp.raceName == checkR.raceName)
                    {
                        duplicate = true;
                        if (checkR.source == "Core")
                        {
                            //Debug.Log(counter);
                            raceList[counter] = temp;
                            Debug.Log(temp.raceName + " Replaced Vanilla");
                        }
                        else
                        {
                            raceList.Remove(checkR);
                            //Debug.Log("Duplicates Removed");
                        }
                        break;
                    }
                    counter += 1;
                }
                if (!duplicate)
                {
                    raceList.Add(temp);
                    //Debug.Log(tempB.biomeName);
                }
            }

            return raceList;
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
        return null;
    }

    //Saves the default Biomes
    public void SaveDefaults()
    {
        var file = File.Create(finder.defaultPath + "/Races/CoreRaces.xml");
        file.Close();
        Serializer.Serialize(races, finder.defaultPath + "/Races/CoreRaces.xml");
        //Debug.Log("File written");
    }

    //Creates the default core Races
    public void CoreRaces()
    {
        races = new List<Race_Class>();

        CreateRace("Orx", "Orx Territory", "Core");
        CreateRace("Ivorian", "The Ivorian Empire", "Core");
        CreateRace("Enslavers", "The Enslavers", "Core");
    }

    //Creates a Race_Class in code (only used for core Race)
    public void CreateRace(string name, string empire, string sourceFile)
    {
        var tempRace = new Race_Class();
        tempRace.raceName = name;
        tempRace.empireName = empire;
        tempRace.source = sourceFile;
        races.Add(tempRace);
    }

    public List<Threat_Class> FindThreats(Race_Class race)
    {
        List<Threat_Class> threats = new List<Threat_Class>();

        foreach(string s in race.threatPaths)
        {
            try
            {
                Threat_Class temp = Serializer.Deserialize<Threat_Class>(Application.dataPath + "/Resources/Core" + s);
                threats.Add(temp);
            }
            catch 
            {

            }
            try
            {
                Threat_Class temp = Serializer.Deserialize<Threat_Class>(Application.dataPath + "/Resources/Mods" + s);
                threats.Add(temp);
            }
            catch
            {

            }

        }

        return threats;
    }
}
