using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    
    public class Ennhvala : Perso
    {
        [SerializeField]
        GameObject knife;

        public int actifCooldown;
        private int actifCooldownMax = 7000;
        public bool actifState;



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


            //actif
            actifCooldown = actifCooldownMax;
            actifState = false;
        }

        private void Update()
        {
            CoolDown();
            //this.health -= malus;
            //this.atk = atk * (maxHealth / health) * bonus;
        }
        private void CoolDown()
        {
            if (actifCooldown <= 0 && actifState)
                EndActifCoolDown();
            if (actifCooldown > 0)
                actifCooldown -= 1;
        }
        void EndActifCoolDown()
        {
            actifState = false;
            actifCooldown = 0;
        }
        public new void Actif()
        {
            if (actifCooldown == 0) 
            {
                SpawnKnife();
                actifState = true;
                actifCooldown = actifCooldownMax;
            }
            
        }

        private void SpawnKnife()
        {
            GameObject kn = Instantiate(knife);
            kn.transform.position = arm.transform.position + cam.transform.forward.normalized*2;
            kn.transform.forward = cam.transform.forward;
            kn.GetComponent<Knife>().rotate = cam.transform.forward;
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

