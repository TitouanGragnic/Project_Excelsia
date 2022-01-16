using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace scripts
{

    public class Posture : MonoBehaviour
    {
        private bool state;
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

        public void Break()
        {
            this.state = false;
            this.cooldown = this.maxCooldown;
        }



    }
}
