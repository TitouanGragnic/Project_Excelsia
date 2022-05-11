using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace scripts
{
    
    public class Ennhvala : Perso
    {
        [SerializeField]
        GameObject knife;
        [SerializeField] Slider sliderAc;

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
            personnage = "Ennhvala";
        }

        private void Update()
        {
            CoolDown();
            //this.health -= malus;
            sliderAc.value = actifCooldownMax - actifCooldown;
            this.atk = atk * (maxHealth / health) * bonus;
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
            if (actifCooldown == 0|| true ) 
            {
                SpawnKnife();
                actifState = true;
                actifCooldown = actifCooldownMax;
            }
            
        }

        private void SpawnKnife()
        {
            Cmd_SpawnK(arm.transform.position, cam.transform.forward);
        }
        [Command]
        private void Cmd_SpawnK(Vector3 pos, Vector3 forward)
        {
            GameObject kn = Instantiate(knife, pos + forward.normalized * 2, new Quaternion(forward.x,forward.y,forward.z,0));
            kn.GetComponent<Knife>().rotate = forward;
            Rigidbody rb = kn.GetComponent<Rigidbody>();
            rb.AddForce(forward.normalized * 5, ForceMode.Impulse);
            NetworkServer.Spawn(kn);

        }

        private void Ulti()
        {
            malus = 0.01f;
            vitesse *= 1.25f;
            bonus *= 1.25f;
        }
    }
}

