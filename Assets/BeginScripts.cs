using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginScripts : MonoBehaviour
{

    public Behaviour[] beginSetup;
    public GameObject cam;
    public GameObject begin;
    public Animator beginning;
    float time = Time.time;
    // Start is called before the first frame update
    void Start()
    {
        cam.SetActive(false);
        begin.SetActive(true);
        beginning.Play("move");
        begin.transform.position = new Vector3(32, 1389, -300);
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time-time > 4)
        {
            cam.SetActive(true);
            begin.SetActive(false);
        }
    }
}
