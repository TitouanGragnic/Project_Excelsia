using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginScripts : MonoBehaviour
{

    float time;
    public GameObject begin;
    public Transform start;
    public BeginDesactive desScript;

    // Start is called before the first frame update
    void Start()
    {       
        begin.SetActive(true);
        time = Time.time;
        begin.transform.position = new Vector3(32, 1389, -300);
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if (Time.time-time > 4)
        {
            if (desScript.enabled)
                desScript.Desactivate();
            begin.SetActive(false);
        }
    }
}
