using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginScripts : MonoBehaviour
{

    float time;
    public GameObject begin;
    public Transform start;

    // Start is called before the first frame update
    void Start()
    {       
        begin.SetActive(true);
        time = Time.time;
        begin.transform.position = new Vector3(32, 1389, -300);
    }
    
    public BeginDesactive desScript;
    // Update is called once per frame
    void Update()
    {
        if(start.position.z > 0)
            begin.transform.position = new Vector3(begin.transform.position.x, begin.transform.position.y, -300);
        else
            begin.transform.position = new Vector3(begin.transform.position.x, begin.transform.position.y, 300);
        if (Time.time-time > 4)
        {
            if (desScript.enabled)
                desScript.Desactivate();
            begin.SetActive(false);
        }
    }
}
