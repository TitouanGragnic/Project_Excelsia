using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class Tamo : Perso
    {
        [SerializeField]
        GameObject mine;
        [SerializeField]
        AttackSystem attackSystem;





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
                SpawnMine();
                actifState = true;
                actifCooldown = actifCooldownMax;
            }

        }
        private void SpawnMine()
        {
            GameObject mi = Instantiate(mine);
            mi.transform.position = arm.transform.position + cam.transform.forward.normalized * 2;
            mi.transform.forward = cam.transform.forward;
            mi.GetComponent<Mine>().rotate = cam.transform.forward;
            Rigidbody rb = mi.GetComponent<Rigidbody>();
            rb.AddForce(cam.transform.forward.normalized * 5, ForceMode.Impulse);
        }
    }
}
