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
        private int electricCooldownMax = 7000;
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


        }

        public new void Actif()
        {
            if (electricCooldown == 0)
                StartElectric();
        }

        private void CoolDown()
        {
            if (electricCooldown <= 4* electricCooldownMax/5 && electricState)
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
