using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class selectMusic : MonoBehaviour
{
    [SerializeField]
    AudioSource jukebox;
    [SerializeField]
    AudioClip[] selection;

    [SerializeField]
    Text texte;

    [SerializeField]
    Slider time;

    public int song = 0;
    private void Start()
    {
        song = 0;
        jukebox.clip = selection[song];
        jukebox.Play();
    }
    void Update()
    {
        time.maxValue = selection[song].length;
        texte.text = ((int)jukebox.time / 60).ToString() +" : "+ ((int)jukebox.time%60).ToString();
        time.value = jukebox.time;
        if (!jukebox.isPlaying)
        {
            song = (song + 1) % selection.Length;
            jukebox.clip = selection[song];
            jukebox.Play();
        }
    }
    public void right()
    {
        song = (song + 1) % selection.Length;
        jukebox.clip = selection[song];
        jukebox.Play();
    }
    public void left()
    {
        song = (song + selection.Length-1)%selection.Length;
        jukebox.clip = selection[song];
        jukebox.Play();
    }

}
