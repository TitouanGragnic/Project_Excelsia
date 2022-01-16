using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scripts
{
    public class HealthSystem : MonoBehaviour
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


        public void UpdateLife()
        {
            if (heath_UI != null)
                heath_UI.health = this.health;

            foreach (KeyValuePair<string, Perso> player_ex in GameManager.Players)
            {
                if (player_ex.Value.transform.name != nameId)
                {
                    healthBar.health = player_ex.Value.healthSystem.health;
                }
            }

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

        }
    }
}