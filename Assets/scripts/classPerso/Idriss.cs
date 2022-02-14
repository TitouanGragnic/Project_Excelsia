using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace scripts
{
    public class Idriss : Perso
    {
        [SerializeField]
        Grapple grapple;

        public int electricCooldown;
        public int electricCooldownMax = 2000;
        public bool electricState;
        public bool coolDownEState;

        // Start is called before the first frame update
        void Start()
        {
            //passif
            atk = 30;
            grapple.maxGrappleCooldown = 100;
            //actif
            electricCooldown = electricCooldownMax;
            electricState = false;
            coolDownEState = false;

        }

        [Command]
        void ChangeTypeATK(string newType)
        {
            typeAtk = newType;
        }

        // Update is called once per frame
        void Update()
        {
            CoolDown();

            if (electricCooldown == 0 && Input.GetKey(KeyCode.C))
                StartElectric();

        }

        private void CoolDown()
        {
            if (electricCooldown <= 1500 && electricState)
                EndElectric();
            if (electricCooldown <= 0 && coolDownEState)
                EndECoolDown();
            if (electricCooldown>0)
                electricCooldown -= 1;
        }
        private void EndECoolDown()
        {
            electricCooldown = 0;
            coolDownEState = false;
        }
        private void EndElectric()
        {
            electricState = false;
            ChangeTypeATK("normal");
        }
        private void StartElectric()
        {
            ChangeTypeATK("electric");
            electricCooldown = electricCooldownMax;
            electricState = true;
            coolDownEState = true;
        }
    }
}
