using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ulti : MonoBehaviour
{
    public GameObject blade1;
    public GameObject blade2;

    public bool open = false;
    bool state_open = false;



    public void Open()
    {
        blade1.transform.Translate(Vector3.left * 0.005f );
        blade1.transform.Rotate(Vector3.forward * 5f);
        blade2.transform.Translate(Vector3.left* 0.005f );
        blade2.transform.Rotate(Vector3.forward * 5f);
        state_open = true;
    }

    public void Close()
    {
        blade1.transform.Translate(Vector3.left * -0.005f);
        blade1.transform.Rotate(Vector3.forward * -5f);
        blade2.transform.Translate(Vector3.left * -0.005f);
        blade2.transform.Rotate(Vector3.forward * -5f);
        state_open = false; 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        if (open && !state_open)
            Open();
        if (!open && state_open)
            Close();
    }
}
