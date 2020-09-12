using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Localisation_Manager : MonoBehaviour
{
    public FileFinder finder;

    public List<String_List_Class> localisationList;

    public void FindLocalisationFiles()
    {
        localisationList = new List<String_List_Class>();
        List<string> fileLocations = finder.Retrieve("Localisation", ".meta");

        foreach(string s in fileLocations)
        {
            String_List_Class temp = Serializer.Deserialize<String_List_Class>(s);
            localisationList.Add(temp);
        }
    }
}
