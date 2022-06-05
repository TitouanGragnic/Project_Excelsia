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
<<<<<<< HEAD

    public Transform surBegin;

    public void place()
    {
        if (start.position.z < 0)
            surBegin.position += new Vector3(0, 0, 600);
    }
    public void place1()
    {
        if (start.position.z > 0)
            begin.transform.position = new Vector3(begin.transform.position.x, begin.transform.position.y, -300);
        else
            begin.transform.position = new Vector3(begin.transform.position.x, begin.transform.position.y, 300);
    }
=======
>>>>>>> 1127f9eb0f0336ba7847793eff04f5d5524681ad
}
