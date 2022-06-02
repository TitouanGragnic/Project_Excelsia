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

        void Start()
        {
            typeAtk = "poison";
            attackSystem.range = 5f;

            //actif
            startCooldownActif = GameManager.GetTime();
            startCooldownUlti = GameManager.GetTime();
            personnage = "Tamo";

            ultiOn = false;
        }
        private void Update()
        {
            CoolDown();
        }
        private void CoolDown()
        {
            if (ultiOn && GameManager.GetTimeMili() - startCooldownUltiOn > endCooldownUlti)
                EndUlti(); 
            if (ultiOn && !attack && GameManager.GetTimeMili() - startCooldownUltiOn > endCooldownUlti/2)
                AttackUlti();
        }
        public new void Actif()
        {
            if (GameManager.GetTime() - startCooldownActif > this.maxCooldownActif || true)
            {
                SpawnMine();
                startCooldownActif = GameManager.GetTime();
                arms.Play("actif");
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
            if (GameManager.GetTime() - startCooldownUlti > this.maxCooldownUlti|| true)
            {
                ChangeTypeATK("ultiTamo");
                attack = false;
                startCooldownUltiOn = GameManager.GetTimeMili();
                ultiOn = true;
                armInator.Play("ulti");
                foreach(var e in ultiEffect)
                    e.Active(true);
            }
        }

        [SerializeField] LayerMask layerMask;

        void AttackUlti()
        {

            bool touche = false;
            float k = 1;

            while (!touche && k >= 0)
            {
                RaycastHit hit;

                for (int i = -1; i < 2; i += 2)
                {
                    Vector3 dir = new Vector3(i *k, 0, 0) + camHolder.transform.forward;
                    if (!touche && Physics.Raycast(cam.transform.position, dir, out hit, attackSystem.range + 5, layerMask))
                    { 
                        Debug.Log("touche");
                        touche = true;
                        CmdPlayerAttack(hit.collider.name, hit.point, 90);
                    }
                }
                k-=0.1f;

            }



            
        }

        void EndUlti()
        {
            foreach(var e in ultiEffect)
                e.Active(false);
            ultiOn = false;
            ChangeTypeATK("normal");
        }
        private void SpawnMine()
        {
            Cmd_SpawnM(arm.transform.position, cam.transform.forward);
        }

        [Command]
        private void Cmd_SpawnM(Vector3 pos, Vector3 forward)
        {
            GameObject mi = Instantiate(mine, pos + forward.normalized * 2, new Quaternion(0,0,0, 0));
            mi.GetComponent<Mine>().rotate = forward;
            Rigidbody rb = mi.GetComponent<Rigidbody>();
            rb.AddForce(forward.normalized * 5, ForceMode.Impulse);
            NetworkServer.Spawn(mi);

        }
    }
}
