using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Mirror;

namespace scripts
{
    public class Tamo : Perso
    {
        [SerializeField]
        GameObject mine;
        [SerializeField]
        AttackSystem attackSystem;

        [SerializeField] Animator arms;
        [SerializeField] Animator body;

        void Start()
        {
            typeAtk = "poison";
            attackSystem.range = 5f;

            //actif
            startCooldownActif = GameManager.GetTime();
            startCooldownUlti = GameManager.GetTime();
            personnage = "Tamo";

            ultiOn = false;
            preActifState = false;
        }
        private void Update()
        {
            CoolDown();
        }
        private void CoolDown()
        {
            if (ultiOn && GameManager.GetTimeMili() - startCooldownUltiOn > endCooldownUlti)
                EndUlti(); 
            if (ultiOn && !attack  && !attack && GameManager.GetTimeMili() - startCooldownUltiOn > endCooldownUlti/2)
                AttackUlti();
            if (preActifState && GameManager.GetTimeMili() - preActif > cooldownPreActif)
            {
                SpawnMine();
                preActifState = false;
            }
        }
        int preActif;
        bool preActifState;
        public int cooldownPreActif = 900;
        public new void Actif()
        {
            if (GameManager.GetTime() - startCooldownActif > this.maxCooldownActif )
            {
                preActifState = true;
                preActif = GameManager.GetTimeMili();
                startCooldownActif = GameManager.GetTime();
                arms.Play("actif");
                body.Play("actif");
            }

        }
        [SerializeField] Animator armInator;
        bool ultiOn;
        bool attack;
        int startCooldownUltiOn;
        int endCooldownUlti = 2000;

        [SerializeField] UltiTamo[] ultiEffect;
        public new void Ulti()
        {
            if (GameManager.GetTime() - startCooldownUlti > this.maxCooldownUlti)
            {
                ChangeTypeATK("ultiTamo");
                attack = false;
                startCooldownUltiOn = GameManager.GetTimeMili();
                ultiOn = true;
                CmdSetULti(true);
            }
        }
        [Command] void CmdSetULti(bool state) { RpcSetUlti(state);}
        [ClientRpc]
        void RpcSetUlti(bool state)
        {
            if (state)
            {
                armInator.Play("ulti");
                body.Play("ulti");
            }    
            foreach (var e in ultiEffect)
                e.Active(state);
        }

        [SerializeField] LayerMask layerMask;

        void AttackUlti()
        {
            attack = true;
            bool touche = false;
            float k = 1;
            startCooldownUlti = GameManager.GetTime();

            while (!touche && k >= 0)
            {
                RaycastHit hit;

                for (int i = -1; i < 2; i += 2)
                {
                    Vector3 dir = new Vector3(i *k, 0, 0) + camHolder.transform.forward;
                    if (!touche && Physics.Raycast(cam.transform.position, dir, out hit, attackSystem.range + 5, layerMask))
                    { 
                        touche = true;
                        CmdPlayerAttack(hit.collider.name, hit.point, 200);
                    }
                }
                k-=0.1f;

            }



            
        }

        void EndUlti()
        {
            CmdSetULti(false);
            ultiOn = false;
            ChangeTypeATK("normal");
        }

        [SerializeField] Transform spawnMine;
        private void SpawnMine()
        {
            Cmd_SpawnM(spawnMine.position, cam.transform.forward);
        }

        [Command]
        private void Cmd_SpawnM(Vector3 pos, Vector3 forward)
        {
            GameObject mi = Instantiate(mine, pos + forward.normalized * 2, new Quaternion(0,0,0, 0));
            mi.GetComponent<Mine>().rotate = forward;
            Rigidbody rb = mi.GetComponent<Rigidbody>();
            rb.AddForce(forward.normalized * 10 + GetComponent<Rigidbody>().velocity, ForceMode.Impulse);
            NetworkServer.Spawn(mi);

        }
    }
}
