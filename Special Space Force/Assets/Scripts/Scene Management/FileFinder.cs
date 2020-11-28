using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class FileFinder : MonoBehaviour
{
    /// <summary>
    /// This script gets all files under given paths and sorts them according to paramters
    /// </summary>
    /// 
    public string savePath;
    public string defaultPath;
    public string modsPath;
    FileInfo[] fileArrayDef;
    FileInfo[] fileArrayMod;
    FileInfo[] fileArraySav;
    public Biome_Manager biomeManager;
    public Galaxy_Generation_Manager GenerationManager;
    public bool foundSave;

    //Finds all files under the two paths, Resources and Mods, and places them in arrays.
    public bool Run()
    {
        bool done = false;

        if (savePath == "" || savePath == null)
        {
            foundSave = false;
        } 
        else
        {
            foundSave = true;
            savePath = Application.dataPath + "/Resources";
        }
        if (defaultPath == "" || defaultPath == null)
        {
            defaultPath = Application.dataPath + "/Resources/Core";
        }
        if (modsPath == "" || modsPath == null)
        {
            modsPath = Application.dataPath + "/Resources/Mods";
        }

        List<string> fileList = new List<string>();
        DirectoryInfo info = new DirectoryInfo(defaultPath);
        DirectoryInfo modInfo = new DirectoryInfo(modsPath);
        DirectoryInfo saveInfo = new DirectoryInfo(savePath);

        try
        {
            string path = Application.dataPath;

            fileArrayDef = info.GetFiles("*.*", SearchOption.AllDirectories);
            fileArrayMod = modInfo.GetFiles("*.*", SearchOption.AllDirectories);
            fileArraySav = saveInfo.GetFiles("*.*", SearchOption.AllDirectories);

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

        return done;
    }

    //Sorts through the file arrays to get the dedicated files the script has called for
    //by default avoids .meta files but this can be changed if other issues occur
    public List<string> Retrieve(string have, string avoid)
    {
        List<string> fileList = new List<string>();
        foreach (FileInfo s in fileArrayDef)
        {
            if (s.Name.Contains(have) && !s.Name.Contains(avoid))
            {
                Debug.Log(s.FullName + " Added");
                fileList.Add(s.FullName);
            }
        }
        foreach (FileInfo s in fileArrayMod)
        {
            if (s.Name.Contains(have) && !s.Name.Contains(avoid))
            {
                Debug.Log(s.FullName + " Added");
                fileList.Add(s.FullName);
            }
        }
        if (have.Contains(".Save."))
        {
            foreach (FileInfo s in fileArraySav)
            {
                if (s.Name.Contains(have) && !s.Name.Contains(avoid))
                {
                    Debug.Log(s.FullName + " Added");
                    fileList.Add(s.FullName);
                }
            }
        }
        return fileList;
    }

    //Sorts through the file arrays to get the dedicated files the script has called for
    //by default avoids .meta files but this can be changed if other issues occur
    public List<string> Retrieve(string have, string avoid, string avoid2, string avoid3)
    {
        List<string> fileList = new List<string>();
        foreach (FileInfo s in fileArrayDef)
        {
            if (s.Name.Contains(have) && !s.Name.Contains(avoid) && !s.Name.Contains(avoid2) && !s.Name.Contains(avoid3))
            {
                Debug.Log(s.FullName + " Added");
                fileList.Add(s.FullName);
            }
        }
        foreach (FileInfo s in fileArrayMod)
        {
            if (s.Name.Contains(have) && !s.Name.Contains(avoid) && !s.Name.Contains(avoid2) && !s.Name.Contains(avoid3))
            {
                Debug.Log(s.FullName + " Added");
                fileList.Add(s.FullName);
            }
        }
        return fileList;
    }
}
