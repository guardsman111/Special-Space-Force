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

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && Input.GetKeyUp(KeyCode.LeftShift))
        {
            Application.Quit();
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
