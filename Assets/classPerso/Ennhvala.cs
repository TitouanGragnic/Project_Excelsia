using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class Ennhvala : Perso
    {
        public float maxHealth = 1150f;
        public float maxGuard = 230f;
        public float atk = 25f;
        public float armor = 0.1f;
        public float health;
        
        public float malus = 0f;
        public float bonus = 1f;

        public float vitesse;
        private void Update()
        {
            this.health -= malus;
            this.atk = atk * (maxHealth / health) * bonus;
        }
        private void Ulti()
        {
            malus = 0.01f;
            vitesse *= 1.25f;
            bonus *= 1.25f;
        }
    }
}

