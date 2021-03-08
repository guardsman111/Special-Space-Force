using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_Manager : MonoBehaviour
{
    public List<GameObject> avoiders;
    public GraphicRaycaster raycaster;
    public Load_Manager lManager;

    public static string saveString;

    public Loader_Script loader;

    public void Exit()
    {
        Application.Quit();
    }

    public void GoToGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        lManager.OpenMenu();
    }
    public void LoadMenuClose()
    {
        lManager.CloseMenu();
    }

    public void LoadGame(Load_Class selected)
    {
        saveString = selected.savePath;
        SceneManager.LoadScene(1);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToURL(string url)
    {
        Application.OpenURL(url);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.LogError("Hallo, Debugger Here!");
        }
    }
}
