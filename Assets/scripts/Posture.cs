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
        [SerializeField] Animator arm;
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
            bool input = Input.GetKey(KeyCode.A);
           
            CmdPostureSystem(input);

            GraphicGuard(input);

            if (state)
                arm.SetBool("block", true);
            else
                arm.SetBool("block", false);

        }
        [Command(requiresAuthority = false)]
        public void CmdPostureSystem(bool press)
        {
            if (press && cooldown <= 0)
            {
                    
                state = true;
            }
            else
            {
                if (state)
                {
                    cooldown = 200;
                }
                state = false;
                if (cooldown > 0)
                    cooldown -= 1;
                else
                    cooldown = 0;
            }
            perso.NewGard();
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
