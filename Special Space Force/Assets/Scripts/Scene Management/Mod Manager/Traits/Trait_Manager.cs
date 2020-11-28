using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Trait_Manager : MonoBehaviour
{
    /// <summary>
    /// This script grabs all trait files to be used in the game
    /// </summary>
    public FileFinder finder;

    private List<string> traitFiles;
    [SerializeField]
    private List<Trait_Class> traits;


    //Retrieves (and saves the core) Trait Files. Ignores .meta files
    public bool Run()
    {
        bool done = false;
        CoreTraits();
        SaveDefaults();
        traitFiles = finder.Retrieve("Traits.xml", ".meta");
        traits = FindTraitFiles();
        return done;
    }


    //Send the Files over to the serializer to be converted into Trait_Classes, for use in game
    private List<Trait_Class> FindTraitFiles()
    {
        List<Trait_Class> traitList = new List<Trait_Class>();

        try
        {
            //For each trait, check for duplicates and if there are duplicates, replace any core ones and remove non-core ones
            foreach (string s in traitFiles)
            {
                List<Trait_Class> temp = Serializer.Deserialize<List<Trait_Class>>(s);
                foreach (Trait_Class tempT in temp)
                {
                    int counter = 0;
                    bool duplicate = false;
                    foreach (Trait_Class checkR in traitList)
                    {
                        if (tempT.traitName == checkR.traitName)
                        {
                            duplicate = true;
                            if (checkR.source == "Core")
                            {
                                //Debug.Log(counter);
                                traitList[counter] = tempT;
                                Debug.Log(tempT.traitName + " Replaced Vanilla");
                            }
                            else
                            {
                                traitList.Remove(checkR);
                                //Debug.Log("Duplicates Removed");
                            }
                            break;
                        }
                        counter += 1;
                    }
                    if (!duplicate)
                    {
                        traitList.Add(tempT);
                        //Debug.Log(tempB.biomeName);
                    }
                }
            }

            return traitList;
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

    //Creates the core traits
    public void CoreTraits()
    {
        traits = new List<Trait_Class>();

        CreateTrait("Hero", "The hero type", "", "", "+20", "", "+30", "/5", "", "", "", "+40", "Core");
        CreateTrait("Strong", "Unusually strong", "", "", "+20", "", "", "", "", "", "", "+10", "Core");
        CreateTrait("Jogger", "Passionate jogger", "+10", "", "", "", "", "", "", "", "", "+20", "Core");
        CreateTrait("Sharpshooter", "Eagle eyed", "", "", "", "", "", "", "", "+25", "", "", "Core");
        CreateTrait("Duellist", "Skilled swordsman", "", "", "", "", "", "", "+25", "", "", "", "Core");
        CreateTrait("Nimble", "Doesn't hang around", "+10", "+20", "", "", "", "", "", "", "", "", "Core");
        CreateTrait("Quiet", "Strangely silent", "", "", "", "", "", "", "", "", "-20", "", "Core");
    }

    //Creates a trait from code above
    public void CreateTrait(string name, string desc, string speed, string agility, string strength, string size, string morale, string breakValue, string melee, string ranged, string stealth, string stamina, string source)
    {
        Trait_Class newClass = new Trait_Class();

        newClass.traitName = name;
        newClass.traitDesc = desc;
        newClass.source = source;
        newClass.speed = speed;
        newClass.agility = agility;
        newClass.strength = strength;
        newClass.size = size;
        newClass.morale = morale;
        newClass.breakValue = breakValue;
        newClass.melee = melee;
        newClass.ranged = ranged;
        newClass.stealth = stealth;
        newClass.stamina = stamina;

        traits.Add(newClass);
    }

    //Saves the core traits
    public void SaveDefaults()
    {
        var file = File.Create(finder.defaultPath + "/Traits/CoreTraits.xml");
        file.Close();
        Serializer.Serialize(traits, finder.defaultPath + "/Traits/CoreTraits.xml");
        //Debug.Log("File written");
    }

    //Returns the list of traits
    public List<Trait_Class> GetTraits()
    {
        return traits;
    }
}
