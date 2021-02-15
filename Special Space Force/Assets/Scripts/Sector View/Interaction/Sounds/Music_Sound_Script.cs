using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music_Sound_Script : Sound_Script
{
    public List<AudioClip> ambientSounds;
    public List<string> ambientNames;
    public List<AudioClip> electronicSounds;
    public List<string> electronicNames;
    public List<AudioClip> epicSounds;
    public List<string> epicNames;
    public List<AudioClip> rockSounds;
    public List<string> rockNames;

    public List<UI_Music_Script> songs;

    public Toggle ambientTog;
    public Toggle electronicTog;
    public Toggle epicTog;
    public Toggle rockTog;

    public Color defaultC;
    public Color orangeC;

    public Toggle musicTog;

    public Slider volume;

    public GameObject prefabUIMusic;
    public GameObject content;

    public GameObject menuParent;

    public bool hide = true;

    public void StartPlaying()
    {
        ResetSongs();
        InvokeRepeating("CheckPlaying", 1.0f, 1.0f);
        GetVolume();
    }

    public void HideMenu()
    {
        if (hide)
        {
            hide = false;
            menuParent.SetActive(true);
        }
        else
        {
            hide = true;
            menuParent.SetActive(false);
        }
    }

    private void CheckPlaying()
    {
        if (musicTog.isOn == true)
        {
            if (!speaker.isPlaying)
            {
                ResetSongs();
                PlayMusic();
            }
        }
    }

    public void SetVolume(Slider slider)
    {
        speaker.volume = slider.value;
    }

    public void GetVolume()
    {
        volume.value = speaker.volume;
    }
    
    public void PlayMusic()
    {
        if (musicTog.isOn == true)
        {
            int random = Random.Range(0, songs.Count);
            if (songs[random].toggle.isOn == true)
            {
                speaker.clip = songs[random].song;
                songs[random].image.color = orangeC;
                speaker.Play();
            }
        }
    }

    public void StopMusic(Toggle toggle)
    {
        if (toggle.isOn)
        {

        }
        else
        {
            speaker.Stop();
        }
    }

    public void ToggleSong(UI_Music_Script uiM)
    {
        if (!uiM.toggle.isOn)
        {
            if (speaker.clip = uiM.song)
            {
                uiM.image.color = defaultC;
                PlayMusic();
            }
        }
    }

    public void ToggleGenre()
    {
        ResetSongs();
    }

    public void ResetSongs()
    {
        while(songs.Count > 0)
        {
            Destroy(songs[0].gameObject);
            songs.RemoveAt(0);
        }


        int count;
        if (ambientTog.isOn)
        {
            count = 0;
            foreach(AudioClip ac in ambientSounds)
            {
                GameObject tempGO = Instantiate(prefabUIMusic, content.transform);
                UI_Music_Script tempMUI = tempGO.GetComponent<UI_Music_Script>();
                tempMUI.manager = this;
                tempMUI.songName.text = ambientNames[count];
                tempMUI.song = ac;
                tempGO.transform.position += new Vector3(0, -22 * songs.Count);
                songs.Add(tempMUI);
                count += 1;
            }
        }

        if (electronicTog.isOn)
        {
            count = 0;
            foreach (AudioClip ac in electronicSounds)
            {
                GameObject tempGO = Instantiate(prefabUIMusic, content.transform);
                UI_Music_Script tempMUI = tempGO.GetComponent<UI_Music_Script>();
                tempMUI.manager = this;
                tempMUI.songName.text = electronicNames[count];
                tempMUI.song = ac;
                tempGO.transform.position += new Vector3(0, -22 * songs.Count);
                songs.Add(tempMUI);
                count += 1;
            }
        }

        if (epicTog.isOn)
        {
            count = 0;
            foreach (AudioClip ac in epicSounds)
            {
                GameObject tempGO = Instantiate(prefabUIMusic, content.transform);
                UI_Music_Script tempMUI = tempGO.GetComponent<UI_Music_Script>();
                tempMUI.manager = this;
                tempMUI.songName.text = epicNames[count];
                tempMUI.song = ac;
                tempGO.transform.position += new Vector3(0, -22 * songs.Count);
                songs.Add(tempMUI);
                count += 1;
            }
        }

        if (rockTog.isOn)
        {
            count = 0;
            foreach (AudioClip ac in rockSounds)
            {
                GameObject tempGO = Instantiate(prefabUIMusic, content.transform);
                UI_Music_Script tempMUI = tempGO.GetComponent<UI_Music_Script>();
                tempMUI.manager = this;
                tempMUI.songName.text = rockNames[count];
                tempMUI.song = ac;
                tempGO.transform.position += new Vector3(0, -22 * songs.Count);
                songs.Add(tempMUI);
                count += 1;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Period))
        {
            ResetSongs();
            PlayMusic();
        }
    }

}
