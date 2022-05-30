using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;
using Mirror;

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
            atk = 4;


            //actif
            actifCooldown = actifCooldownMax;
            actifState = false;
            personnage = "Ennhvala";
        }

        private void Update()
        {
            CoolDown();
            //this.health -= malus;
            this.atk = GetATK(health) * bonus;
            if (ultiOn)
                SetVFXSmoke();
        }

        float GetATK(float life)
        {
            if (life < 100)
                return 50f;
            return 0.00001f * life * life - 0.05f * life + 49.14f +(float) Math.Sqrt(life * 0.39f);
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

        public bool ultiOn;
        public new void Ulti()
        {
            ultiOn = true;
            malus = 0.01f;
            vitesse *= 1.25f;
            bonus *= 1.5f;
        }
        [SerializeField]
        VisualEffect smokeVFX;
        [SerializeField] LayerMask mask;
        void SetVFXSmoke()
        {
            smokeVFX.SetVector3("position", new Vector3(transform.position.x, cam.transform.position.y - 1, transform.position.z));

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 50f, mask))
                smokeVFX.SetFloat("Ground", hit.point.y) ;
            else
                smokeVFX.SetFloat("Ground", 0);
        }
    }
}

