using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginDesactive : MonoBehaviour
{
    public Behaviour[] beginSetup;
    public GameObject cam;
    public GameObject canvas;
    public Animator beginning;

    private void Start()
    { 
        cam.SetActive(false);
        canvas.SetActive(false);
        beginning.Play("move");
        for(int i = 0; i<beginSetup.Length; i++)
        {
            beginSetup[i].enabled = false;
        }
        
    }
    public void Desactivate()
    {
        cam.SetActive(true);
        canvas.SetActive(true);
        for (int i = 0; i < beginSetup.Length; i++)
        {
            beginSetup[i].enabled = true;
        }
    }
}
