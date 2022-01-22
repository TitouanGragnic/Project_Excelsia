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
        GameObject guard;
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
            this.maxCooldown = 100;
            this.cooldown = 0;
            this.state = false;
        }
        void Update()
        {
            this.state = guard.activeSelf;
        }

        public void Break()
        {
            this.state = false;
            this.cooldown = this.maxCooldown;
            guard.SetActive(false);
        }



    }
}
