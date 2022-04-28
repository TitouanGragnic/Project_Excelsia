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

        [SerializeField] Slider sliderAc;



        public int actifCooldown;
        private int actifCooldownMax = 7000;
        public bool actifState;



        void Start()
        {
            typeAtk = "poison";
            attackSystem.range = 5f;

            //actif
            actifCooldown = actifCooldownMax;
            actifState = false;
        }
        private void Update()
        {
            CoolDown();
            sliderAc.value = actifCooldownMax - actifCooldown;
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
            if (actifCooldown == 0|| true)
            {
                SpawnMine();
                actifState = true;
                actifCooldown = actifCooldownMax;
            }

        }
        private void SpawnMine()
        {
            Cmd_SpawnM(arm.transform.position, cam.transform.forward);
        }

        [Command]
        private void Cmd_SpawnM(Vector3 pos, Vector3 forward)
        {
            GameObject mi = Instantiate(mine, pos + forward.normalized * 2, new Quaternion(forward.x, forward.y, forward.z, 0));
            mi.GetComponent<Mine>().rotate = forward;
            Rigidbody rb = mi.GetComponent<Rigidbody>();
            rb.AddForce(forward.normalized * 5, ForceMode.Impulse);
            NetworkServer.Spawn(mi);

        }
    }
}
