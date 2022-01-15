using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class HealthSystem
    {
        float health; // = 1000f;
        float maxHealth;

        float armor; // = 0.1f;

        float guard; //= 0f;
        float maxGuard; //= 200f; 

        public HealthBar healthBar;
        public Health_UI heath_UI;

        public HealthSystem(GameObject object_health,GameObject health_UI_object, float health, float armor, float maxGuard)
        {
            healthBar = object_health.GetComponent(typeof(HealthBar)) as HealthBar;

            this.health = health;
            this.maxHealth = health;

            this.maxGuard = maxGuard;
            this.guard = 0f;

            this.armor = armor;

           
        }

        private void UpdateLife()
        {
            //healthBar.health = this.health;
            heath_UI.health = this.health;
        }

        public void TakeDamage(float damage, string type, Posture posture)
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

            UpdateLife();

        }
    }
}