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

        [SerializeField] Animator arms;
        [SerializeField] Animator bodyAnim;
        public AudioSource lecteur;
        public AudioClip sound;

        public bool actifState;
        public bool ultiState;
        int maxCooldownUltiON = 10;

        public float malus = 0f;
        public float bonus = 1f;

        public float vitesse;
        private void Start()
        {

            startCooldownActif = GameManager.GetTime();
            startCooldownUlti = GameManager.GetTime();


            maxHealth = 1150f;
            maxGuard = 230f;
            health = maxHealth;
            
            armor = 0.1f;
            atk = 4;

            preActifState = false;

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

        [SerializeField] Transform spawnK;
        private void CoolDown()
        {
            //ulti 
            if (ultiOn && GameManager.GetTime() - startCooldownUlti > maxCooldownUltiON)
                EndUlti(); 
            if (preActifState && GameManager.GetTimeMili() - preActif > cooldownPreActif)
            {
                Cmd_SpawnK(spawnK.position, cam.transform.forward);
                preActifState = false;
            }
        }
        int preActif;
        bool preActifState;
        public int cooldownPreActif = 900;
        public new void Actif()
        {
            if (GameManager.GetTime() - startCooldownActif > this.maxCooldownActif  )
            {
                preActif = GameManager.GetTimeMili();
                preActifState = true;
                startCooldownActif = GameManager.GetTime();
                arms.Play("actif");
                bodyAnim.Play("actif");
            }
            
        }

        [Command]
        private void Cmd_SpawnK(Vector3 pos, Vector3 forward)
        {
            GameObject kn = Instantiate(knife, pos + forward.normalized * 2, camHolder.transform.rotation);
            Rigidbody rb = kn.GetComponent<Rigidbody>();
            rb.AddForce(forward.normalized * 5, ForceMode.Impulse);
            NetworkServer.Spawn(kn);

        }

        public bool ultiOn;
        public new void Ulti()
        {
            if (GameManager.GetTime() - startCooldownUlti > this.maxCooldownUlti)
            {
                lecteur.clip = sound;
                lecteur.Play();
                startCooldownUlti = GameManager.GetTime();
                ultiOn = true;
                TakeDamage(125f, "normal");
                vitesse *= 1.25f;
                bonus = 2f;

                smokeVFX.SetBool("Loop", true);
                CmdSetVFX(true);
                arms.Play("ulti");
            }
        }

        [Command] void CmdSetVFX(bool state) { SetVFX(state); }

        [ClientRpc] void SetVFX(bool state)
        {
            smokeVFX.SetBool("Loop", state);
        }

        void EndUlti()
        {
            ultiOn = false;
            vitesse *= (4f / 5f);
            bonus = 1;
            smokeVFX.SetBool("Loop", false);
            CmdSetVFX(false);
        }
        [SerializeField]
        VisualEffect smokeVFX;
        [SerializeField] LayerMask mask;
        [Command]void SetVFXSmoke()
        {

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 50f, mask))
                ClientSetSmokeVFX( hit.point.y, new Vector3(transform.position.x, cam.transform.position.y - 1, transform.position.z));
            else
                ClientSetSmokeVFX(0, new Vector3(transform.position.x, cam.transform.position.y - 1, transform.position.z));
        }

        [ClientRpc] void ClientSetSmokeVFX(float y,Vector3 pos)
        {
                smokeVFX.SetFloat("Ground", y);
            smokeVFX.SetVector3("position",pos);

        }
    }
}

