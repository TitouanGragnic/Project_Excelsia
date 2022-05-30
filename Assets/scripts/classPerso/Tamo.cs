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



        void Start()
        {
            typeAtk = "poison";
            attackSystem.range = 5f;

            //actif
            startCooldownActif = GameManager.GetTime();
            startCooldownUlti = GameManager.GetTime();
            personnage = "Tamo";
        }
        private void Update()
        {
            CoolDown();
        }
        private void CoolDown()
        {
        }
        public new void Actif()
        {
            if (GameManager.GetTime() - startCooldownActif > this.maxCooldownActif || true)
            {
                SpawnMine();
                startCooldownActif = GameManager.GetTime();
            }

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
