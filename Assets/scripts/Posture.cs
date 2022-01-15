using System;
using System.Collections;
using System.Collections.Generic;



namespace scripts
{

    public class Posture
    {
        private bool state;
        private int maxCooldown;
        public int cooldown;


        public bool State
        {
            get => state;
            set { state = value; }
        }



        public Posture(int maxCooldown)
        {
            this.maxCooldown = maxCooldown;
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
