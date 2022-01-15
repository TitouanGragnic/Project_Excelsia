using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;





namespace scripts
{
    public class Perso : MonoBehaviour
    {
        public GameObject perso_object;
        public GameObject health_object;
        public GameObject health_UI_object;

        public Posture pp = new Posture(500);
        public HealthSystem hs;


        void Start()
        {
            
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(1))
                hs.TakeDamage(50f, "normal", pp);
        }
        public void InitHs(GameObject target)
        {
            hs = new HealthSystem(health_object, health_UI_object, 1000f, 0.1f, 200f);
            hs.healthBar.target = target;

            Camera cam = perso_object.GetComponentInChildren<Camera>();
            cam.cullingMask = ~(1<<health_object.layer); 
        }


    }
}


        
