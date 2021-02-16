using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Script : MonoBehaviour
{
    [SerializeField]
    protected List<AudioClip> sounds;

    public AudioSource speaker;


    public void PlaySound()
    {
        int random = Random.Range(0, sounds.Count);
        speaker.clip = sounds[random];
        speaker.Play();
    }
}
