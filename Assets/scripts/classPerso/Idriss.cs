using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace scripts
{
    public class Idriss : Perso
    {
        [SerializeField]
        Grapple grapple;
        [SerializeField] Slider sliderAc;

        public int electricCooldown;
        private int electricCooldownMax = 7000;
        public bool electricState;
        public bool coolDownEState;

        // Start is called before the first frame update
        void Start()
        {
            //passif
            atk = 7;
            grapple.maxGrappleCooldown = 100;
            //actif
            electricCooldown = electricCooldownMax;
            electricState = false;
            coolDownEState = false;
            personnage = "Idriss";
        }

        
        // Update is called once per frame
        void Update()
        {
            CoolDown();
            sliderAc.value = electricCooldownMax - electricCooldown;

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
