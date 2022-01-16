using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace scripts
{
    public class HealthSystem : NetworkBehaviour
    {
        [SerializeField]
        public float health; // = 1000f;
        float maxHealth;

        float armor; // = 0.1f;

        float guard; //= 0f;
        [SerializeField]
        float maxGuard; //= 200f; 

        public HealthBar healthBar;
        public Health_UI heath_UI;
        public string nameId;

        void start()
        {
            maxHealth = health;

            armor = 0.1f;

            guard = 0f;
        }


        public void UpdateLife(float new_health)
        {
            this.health = new_health;
            if (heath_UI != null)
                heath_UI.health = this.health;

            healthBar.health = health;
            

        }

        public float TakeDamage(float damage, string type, Posture posture)
        {
            switch (type)
            {
                case "normal":
                    if (posture.State)
                    {
                        guard -= damage;
                        if (guard <= 0)
                        {
                            posture.Break();
                        }
                    }
                    else
                    {
                        health -= damage * (1f - armor);
                    }
                    break;
                
                default:
                    break;
            }

            return health;
        }
    }
}