using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Menu_Script : MonoBehaviour
{
    public GameObject nonMenu;
    public GameObject menu;
    public Scene_Manager manager;
    public Turn_Manager tManager;

    public void ShowHide()
    {
        if(menu.activeSelf == true)
        {
            menu.SetActive(false);
            nonMenu.SetActive(true);
        }
        else 
        {
            menu.SetActive(true);
            nonMenu.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowHide();
        }
    }

    public void Save()
    {
        tManager.AutoSave();
    }

    public void ToMenu()
    {
        manager.GoToMenu();
    }

    public void Exit()
    {
        manager.Exit();
    }
}
