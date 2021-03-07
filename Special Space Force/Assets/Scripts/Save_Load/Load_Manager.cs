using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_Manager : MonoBehaviour
{
    public FileFinder finder;
    public List<GameObject> loads;
    public GameObject position1;
    public GameObject prefabLoad;
    public GameObject mainMenu;
    public Transform content;
    public Scene_Manager manager;

    public List<string> savePaths;

    public void OpenMenu()
    {
        mainMenu.SetActive(false);
        gameObject.SetActive(true);
        savePaths = finder.Retrieve(".Save.", ".meta");

        foreach(string s in savePaths)
        {
            GameObject newLoad = Instantiate(prefabLoad, content);
            newLoad.transform.position = position1.transform.position + new Vector3(0, -100 * savePaths.IndexOf(s), 0);
            loads.Add(newLoad);
            newLoad.GetComponent<Load_Class>().savePath = s;
            newLoad.GetComponent<Load_Class>().manager = this;
            Save_Class saveTemp = Serializer.Deserialize<Save_Class>(s);
            newLoad.GetComponent<Load_Class>().loadName.text = saveTemp.saveName;
        }
    }

    public void OpenSave(Load_Class selected)
    {
        manager.LoadGame(selected);
    }

    public void CloseMenu()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
