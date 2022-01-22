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
        [SyncVar]
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

            CmdPostureSystem(Input.GetKey(KeyCode.A));

            GraphicGuard(Input.GetKey(KeyCode.A));


        }
        [Command]
        public void CmdPostureSystem(bool press)
        {
            if (press && cooldown <= 0)
            {
                if (!state)
                    perso.NewGard();
                state = true;
            }
            else
            {
                if (state)
                {
                    cooldown = 200;
                }
                state = false;
                perso.NewGard();
                if (cooldown > 0)
                    cooldown -= 1;
                else
                    cooldown = 0;
            }
        }
        [Client]
        public void GraphicGuard(bool press)
        {
            guard_UI.SetActive(press && cooldown <= 0);
        }

        public void Break()
        {
            this.state = false;
            this.cooldown = this.maxCooldown;
            guard_UI.SetActive(false);
        }



    }
}
