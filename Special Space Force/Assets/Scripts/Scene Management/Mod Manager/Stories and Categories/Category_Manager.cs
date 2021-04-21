using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Category_Manager : MonoBehaviour
{
    public FileFinder finder;
    public List<string> collectionFiles;
    public List<Category_Collection> collections;

    public void Run()
    {
        collections = new List<Category_Collection>();

        collectionFiles = finder.Retrieve("Categories.xml", ".meta");
        collections = FindCollections();
    }

    public List<Category_Collection> FindCollections() //Deserializes all of the catergory collections
    {
        List<Category_Collection> returner = new List<Category_Collection>();

        foreach(string s in collectionFiles)
        {
            Category_Collection catCol = Serializer.Deserialize<Category_Collection>(s);
            returner.Add(catCol);
        }

        return returner;
    }
}
