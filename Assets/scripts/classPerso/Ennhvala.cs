using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class Ennhvala : Perso
    {
        public float malus = 0f;
        public float bonus = 1f;

        public float vitesse;
        private void Start()
        {
            maxHealth = 1150f;
            maxGuard = 230f;
            health = maxHealth;
            
            armor = 0.1f;
            atk = 25f;
        }

        private void Update()
        {

            //this.health -= malus;
            //this.atk = atk * (maxHealth / health) * bonus;
        }
        private void Ulti()
        {
            malus = 0.01f;
            vitesse *= 1.25f;
            bonus *= 1.25f;
        }
    }
}

