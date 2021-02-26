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
    public List<UI_Music_Script> disabledSongs;

    public UI_Music_Script currentSong;

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
            if (currentSong == null)
            {
                int random1 = Random.Range(0, songs.Count);
                currentSong = songs[random1];
            }
            currentSong.image.color = defaultC;
            int random = Random.Range(0, songs.Count);
            if (!songs[random].toggle.isOn)
            {
                InvokeRepeating("PlayMusic", 0.1f,0.1f);
            }
            else
            {
                if(currentSong != songs[random])
                {
                    currentSong = songs[random];
                    speaker.clip = songs[random].song;
                    songs[random].image.color = orangeC;
                    speaker.Play();
                    CancelInvoke("PlayMusic");
                }
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
            if (speaker.clip == uiM.song)
            {
                uiM.image.color = defaultC;

                PlayMusic();
            }
            disabledSongs.Add(songs[songs.IndexOf(uiM)]);
        }
    }

    public void ToggleGenre()
    {
        ResetSongs();
    }

    public void ResetSongs()
    {
        if(songs == null)
        {
            songs = new List<UI_Music_Script>();
        }

        int extraValues = 0;

        int count;
        if (ambientTog.isOn)
        {
            count = 0;
            foreach(AudioClip ac in ambientSounds)
            {
                if (songs.Count > ambientSounds.IndexOf(ac))
                {
                    if (songs[ambientSounds.IndexOf(ac)].song == ac)
                    {

                    }
                    else
                    {
                        Destroy(songs[ambientSounds.IndexOf(ac)].gameObject);
                        songs.RemoveAt(ambientSounds.IndexOf(ac));
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
                else
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
            extraValues += ambientSounds.Count;
        }

        if (electronicTog.isOn)
        {
            count = 0;
            foreach (AudioClip ac in electronicSounds)
            {
                if (songs.Count > electronicSounds.IndexOf(ac) + extraValues)
                {
                    if (songs[electronicSounds.IndexOf(ac) + extraValues].song == ac)
                    {

                    }
                    else
                    {
                        Destroy(songs[electronicSounds.IndexOf(ac) + extraValues].gameObject);
                        songs.RemoveAt(electronicSounds.IndexOf(ac) + extraValues);
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
                else
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
            extraValues += electronicSounds.Count;
        }

        if (epicTog.isOn)
        {
            count = 0;
            foreach (AudioClip ac in epicSounds)
            {
                if (songs.Count > epicSounds.IndexOf(ac) + extraValues)
                {
                    if (songs[epicSounds.IndexOf(ac) + extraValues].song == ac)
                    {

                    }
                    else
                    {
                        Destroy(songs[epicSounds.IndexOf(ac) + extraValues].gameObject);
                        songs.RemoveAt(epicSounds.IndexOf(ac) + extraValues);
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
                else
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
            extraValues += epicSounds.Count;
        }

        if (rockTog.isOn)
        {
            count = 0;
            foreach (AudioClip ac in rockSounds)
            {
                if (songs.Count > rockSounds.IndexOf(ac) + extraValues)
                {
                    if (songs[rockSounds.IndexOf(ac) + extraValues].song == ac)
                    {

                    }
                    else
                    {
                        Destroy(songs[rockSounds.IndexOf(ac) + extraValues].gameObject);
                        songs.RemoveAt(rockSounds.IndexOf(ac) + extraValues);
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
                else
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
            extraValues += rockSounds.Count;
        }

        foreach(UI_Music_Script uiM in songs)
        {
            if (disabledSongs.Contains(uiM))
            {
                uiM.toggle.isOn = false;
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
