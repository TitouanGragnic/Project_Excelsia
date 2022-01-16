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
        public Posture pp = new Posture(500);
        [SerializeField]
        public HealthSystem healthSystem;



        void Update()
        {
            if (healthSystem != null)
            {
                if (Input.GetMouseButtonDown(1))
                    healthSystem.TakeDamage(50f, "normal", pp);

                healthSystem.UpdateLife();
            }
        }
        public void InitHs(GameObject target)
        {
            healthSystem.healthBar.target = target;
            healthSystem.nameId = perso_object.transform.name;

            Camera cam = perso_object.GetComponentInChildren<Camera>();
            cam.cullingMask = ~(1<<health_object.layer); 
        }


    }
}


        
