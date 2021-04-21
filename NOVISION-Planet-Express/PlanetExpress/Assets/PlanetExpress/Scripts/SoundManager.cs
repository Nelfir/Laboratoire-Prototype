using System.Collections;
using System.Collections.Generic;
using PlanetExpress.Scripts.Utils.Scripts.Utils.Objects;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{ 
    public AudioSource audioSource;
public void Start ()
{
    audioSource = this.gameObject.AddComponent<AudioSource>();
}

public void PlaySound(string son)
{
    AudioClip audioClip = Resources.Load<AudioClip>(son);
    audioSource.PlayOneShot(audioClip);
}
}