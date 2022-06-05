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
        desScript.place();
    }
    
    
    // Update is called once per frame
    void Update()
    {
        desScript.place1();
        /*if(start.position.z > 0)
            begin.transform.position = new Vector3(begin.transform.position.x, begin.transform.position.y, -300);
        else
           begin.transform.position = new Vector3(begin.transform.position.x, begin.transform.position.y, 300);*/
        if (Time.time-time > 4)
        {
            if (desScript.enabled)
                desScript.Desactivate();
            begin.SetActive(false);
        }
    }
}
