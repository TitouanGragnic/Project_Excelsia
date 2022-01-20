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

        [SerializeField]
        public float guard; //= 0f;
        float maxGuard; //= 200f; 

        public HealthBar healthBar;
        public Health_UI heath_UI;
        public Guard_UI guard_UI;
        public string nameId;

        void start()
        {
            maxHealth = health;

            armor = 0.1f;

            maxGuard = guard;
        }


        public void UpdateLife(float new_health, float new_guard)
        {
            this.health = new_health;
            this.guard = new_guard;

            if (heath_UI != null)
                heath_UI.health = this.health;

            if (guard_UI != null)
                guard_UI.guard_value = this.guard;

            healthBar.health = health;
            

        }

        public (float,float) TakeDamage(float damage, string type, Posture posture)
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

            return (health, guard);
        }
    }
}