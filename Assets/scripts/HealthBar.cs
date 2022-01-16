using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace scripts
{
    public class HealthBar : MonoBehaviour
    {
        public float health;
        public float maxHealth;

        
        public GameObject object_health;

        public GameObject[] HP;

        public GameObject target;




        void UptdateLife()
        {
            int nb = (int)((health / maxHealth)*10);
            int i = 0;
            while (i<10 && i <= nb) 
            {
                HP[i].SetActive(true);
                i += 1;
            }
            while(i<10)
            {
                HP[i].SetActive(false);
                i += 1;
            }
        }

        void Rotation()
        {
            if (target != null)
            {
                object_health.transform.LookAt(target.transform);
                object_health.transform.Rotate(90, 0, 0);

            }
        }



        // Start is called before the first frame update
        void Start()
        {
            this.maxHealth = this.health+1;
        }

        // Update is called once per frame
        void Update()
        {
            
            
            UptdateLife();
            Rotation();
        }
    }
}
