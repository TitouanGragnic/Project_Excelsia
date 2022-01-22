using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


namespace scripts
{

    public class Posture : NetworkBehaviour
    {
        [SerializeField]
        Guard_UI guard_UI;

        [SerializeField]
        Perso perso;
        [SyncVar]
        public bool state;
        private int maxCooldown;
        public int cooldown;


        public bool State
        {
            get => state;
            set { state = value; }
        }


        public void Start()
        {
            this.maxCooldown = 5000;
            this.cooldown = 0;
            this.state = false;
        }
        void Update()
        {


            if (Input.GetKey(KeyCode.A) && cooldown<=0)
            {
                if (!state)
                    perso.NewGard(true);
                state = true;
                guard_UI.SetActive(true);
            }
            else
            {
                if (state)
                {
                    cooldown = maxCooldown / 2;
                }
                state = false;
                perso.NewGard(false);
                if (cooldown > 0)
                    cooldown -= 1;
                else
                    cooldown = 0;
            }


        }

        public void Break()
        {
            this.state = false;
            this.cooldown = this.maxCooldown;
            guard_UI.SetActive(false);
        }



    }
}
