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
    public GameObject optionsMenu;

    public AudioSource musicSource;
    public List<AudioSource> soundSources;

    public Loader_Script loader;

    public static string saveString;

    private string preferencesPath;

    public static float musicVolume = 100;
    public static float soundVolume = 100;
    public static float cameraRot;
    public static float cameraMov;
    public static string uiScale;

    public Slider musicS;
    public Slider soundS;
    public Slider movS;
    public Slider rotS;
    public Dropdown qualityD;

    void Start()
    {
        preferencesPath = Application.dataPath + "/Resources/Preferences.xml";
        try
        {
            loader = Serializer.Deserialize<Loader_Script>(preferencesPath);
            musicVolume = loader.musicVolume;
            soundVolume = loader.soundVolume;
            cameraRot = loader.cameraRotSpeed;
            cameraMov = loader.cameraMovSpeed;
            if (musicS != null)
            {
                musicS.value = musicVolume;
                soundS.value = soundVolume;
                rotS.value = cameraRot;
                movS.value = cameraMov;
                qualityD.value = loader.quality;
            }
            QualitySettings.SetQualityLevel(loader.quality);

        }
        catch
        {
            Debug.Log("Options Load Error");
            musicVolume = 100f;
            soundVolume = 100f;
            cameraRot = 7f;
            cameraMov = 7f;
            QualitySettings.SetQualityLevel(5);
            loader = new Loader_Script();
            loader.musicVolume = musicVolume;
            loader.soundVolume = soundVolume;
            loader.cameraRotSpeed = cameraRot;
            loader.cameraMovSpeed = cameraMov;
            loader.quality = 5;
            Serializer.Serialize(loader, preferencesPath);
        }

        if(musicSource != null)
        {
            SetMusicVolume();
        }

        if(soundSources.Count > 0)
        {
            SetSoundVolume();
        }
    }

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
        GoToGame();
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
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Debug.LogError("Hallo, Debugger Here!");
        }
    }

    public void OptionsMenu()
    {
        optionsMenu.SetActive(true);
    }

    public void OptionsClose()
    {
        loader.musicVolume = musicVolume;
        loader.soundVolume = soundVolume;
        loader.cameraRotSpeed = cameraRot;
        loader.cameraMovSpeed = cameraMov;
        loader.quality = QualitySettings.GetQualityLevel();
        Serializer.Serialize(loader, preferencesPath);
        optionsMenu.SetActive(false);
    }

    public void ChangeMusicVol(Slider slider)
    {
        musicVolume = slider.value;
        musicSource.volume = musicVolume / 10;
    }

    public void ChangeSoundsVol(Slider slider)
    {
        soundVolume = slider.value;
        for (int i = 0; i < soundSources.Count; i++)
        {
            soundSources[i].volume = soundVolume/10;
        }
    }

    public void ChangeCameraRot(Slider slider)
    {
        cameraRot = slider.value;
    }

    public void ChangeCameraMove(Slider slider)
    {
        cameraMov = slider.value;
    }

    public void ChangeQuality(Dropdown dropdown)
    {
        QualitySettings.SetQualityLevel(dropdown.value);
    }

    public void SetMusicVolume()
    {
        musicSource.volume = musicVolume / 10;
    }

    public void SetSoundVolume()
    {
        for (int i = 0; i < soundSources.Count; i++)
        {
            soundSources[i].volume = soundVolume / 10;
        }
    }
}
