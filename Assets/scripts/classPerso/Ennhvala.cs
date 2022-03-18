using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    
    public class Ennhvala : Perso
    {
        [SerializeField]
        GameObject knife;
        [SerializeField]
        Camera cam;

        public float malus = 0f;
        public float bonus = 1f;

        public float vitesse;
        private void Start()
        {
            maxHealth = 1150f;
            maxGuard = 230f;
            health = maxHealth;
            
            armor = 0.1f;
            atk = 25f;
        }

        private void Update()
        {

            //this.health -= malus;
            //this.atk = atk * (maxHealth / health) * bonus;
        }

        public new void Actif()
        {
            SpawnKnife();
        }

        private void SpawnKnife()
        {
            GameObject kn = Instantiate(knife);
            kn.transform.position = arm.transform.position + cam.transform.forward.normalized*2;
            Rigidbody rb = kn.GetComponent<Rigidbody>();
            rb.AddForce(cam.transform.forward.normalized * 5, ForceMode.Impulse);
        }

        private void Ulti()
        {
            malus = 0.01f;
            vitesse *= 1.25f;
            bonus *= 1.25f;
        }
    }
}

