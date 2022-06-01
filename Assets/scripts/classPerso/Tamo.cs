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
        int startCooldownUltiOn;
        int endCooldownUlti = 2000;
        [SerializeField] UltiTamo[] ultiEffect;
        public new void Ulti()
        {
            if (GameManager.GetTime() - startCooldownUlti > this.maxCooldownUlti|| true)
            {
                startCooldownUltiOn = GameManager.GetTimeMili();
                ultiOn = true;
                armInator.Play("ulti");
                foreach(var e in ultiEffect)
                    e.Active(true);
            }
        }

        void EndUlti()
        {
            foreach(var e in ultiEffect)
                e.Active(false);
            ultiOn = false;
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
