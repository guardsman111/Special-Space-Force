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

    //Retrieves (and saves the core) Race Files. Ignores .meta files
    public bool Run()
    {
        bool done = false;
        CoreRaces();
        SaveDefaults();
        raceFiles = finder.Retrieve("Races.xml", ".meta");
        races = FindBiomeFiles();
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

    //Send the Files over to the serializer to be converted into Race_Classes, for use in game
    private List<Race_Class> FindBiomeFiles()
    {
        List<Race_Class> raceList = new List<Race_Class>();

        try
        {
            //For each race, check for duplicates and if there are duplicates, replace any core ones and remove non-core ones
            foreach (string s in raceFiles)
            {
                List<Race_Class> temp = Serializer.Deserialize<List<Race_Class>>(s);
                foreach (Race_Class tempB in temp)
                {
                    int counter = 0;
                    bool duplicate = false;
                    foreach (Race_Class checkR in raceList)
                    {
                        if (tempB.raceName == checkR.raceName)
                        {
                            duplicate = true;
                            if (checkR.source == "Core")
                            {
                                //Debug.Log(counter);
                                raceList[counter] = tempB;
                                Debug.Log(tempB.raceName + " Replaced Vanilla");
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
                        raceList.Add(tempB);
                        //Debug.Log(tempB.biomeName);
                    }
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
}
